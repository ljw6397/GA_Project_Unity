using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;
using System;

public class Turnfantasy : MonoBehaviour
{
    class Character
    {
        public string name;
        public float speed;
        public Character(string name, float speed)
        {
            this.name = name;
            this.speed = speed;
        }
    }

    public class SimplePriorityQueue<T>
    {
        private List<(T item, float priority)> heap = new List<(T, float)>();

        public int Count => heap.Count;

        public void Enqueue(T item, float priority)
        {
            heap.Add((item, priority));
            HeapifyUp(heap.Count - 1);
        }

        public T Dequeue()
        {
            if (heap.Count == 0)
                throw new InvalidOperationException("Queue is empty");

            T rootItem = heap[0].item;
            heap[0] = heap[^1];
            heap.RemoveAt(heap.Count - 1);
            HeapifyDown(0);
            return rootItem;
        }

        public float PeekPriority()
        {
            if (heap.Count == 0) throw new InvalidOperationException("Queue is empty");
            return heap[0].priority;
        }

        private void HeapifyUp(int i)
        {
            while (i > 0)
            {
                int parent = (i - 1) / 2;
                if (heap[i].priority >= heap[parent].priority)
                    break;
                (heap[i], heap[parent]) = (heap[parent], heap[i]);
                i = parent;
            }
        }

        private void HeapifyDown(int i)
        {
            int last = heap.Count - 1;
            while (true)
            {
                int left = 2 * i + 1;
                int right = 2 * i + 2;
                int smallest = i;

                if (left <= last && heap[left].priority < heap[smallest].priority)
                    smallest = left;
                if (right <= last && heap[right].priority < heap[smallest].priority)
                    smallest = right;

                if (smallest == i) break;
                (heap[i], heap[smallest]) = (heap[smallest], heap[i]);
                i = smallest;
            }
        }
    }

    void TurnGame()
    {
        var queue = new SimplePriorityQueue<Character>();
        List<Character> characters = new List<Character>()
        {
            new Character("전사", 5f),
            new Character("마법사", 7f),
            new Character("궁수", 10f),
            new Character("도적", 12f)
        };

        // 초기 턴 타이머(모두 0으로 시작)
        foreach (var c in characters)
            queue.Enqueue(c, 0f);

        float currentTime = 0f;

        for (int turn = 1; turn <= 20; turn++)
        {
            // 다음 턴 주인 꺼내기
            var current = queue.Dequeue();
            currentTime = queue.Count > 0 ? queue.PeekPriority() : currentTime;

            Debug.Log($"[{turn}턴] {current.name}의 턴입니다! (speed={current.speed})");

            // 다음 행동 시간 계산: 빠를수록 더 빨리 다시 턴이 옴
            float nextActTime = currentTime + (100f / current.speed);
            queue.Enqueue(current, nextActTime);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TurnGame();
        }
    }
}
