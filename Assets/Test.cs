using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    public Text resultText;

    private int[] GenerateRandomArray(int size)
    {
        int[] arr = new int[size];
        System.Random rand = new System.Random();
        for (int i = 0; i < size; i++)
        {
            arr[i] = rand.Next(0, 10000);
        }
        return arr;
    }

    public void SelectionSortButton()
    {
        int[] data = GenerateRandomArray(10000);
        Stopwatch sw = new Stopwatch();
        sw.Reset();
        sw.Start();
        Test.StartSelectionSort(data);
        sw.Stop();
        resultText.text = $"선택정렬: {sw.ElapsedMilliseconds} ms";
    }

    public void BubbleSortButton()
    {
        int[] data = GenerateRandomArray(10000);
        Stopwatch sw = new Stopwatch();
        sw.Reset();
        sw.Start();
        Test.StartBubbleSort(data);
        sw.Stop();
        resultText.text = $"버블정렬: {sw.ElapsedMilliseconds} ms";
    }

    public void QuickSortButton()
    {
        int[] data = GenerateRandomArray(10000);
        Stopwatch sw = new Stopwatch();
        sw.Reset();
        sw.Start();
        Test.StartQuickSort(data, 0, data.Length - 1);
        sw.Stop();
        resultText.text = $"퀵정렬: {sw.ElapsedMilliseconds} ms";
    }

    public static void StartQuickSort(int[] arr, int low, int high)
    {
        if (low < high)
        {
            int pivotIndex = Partition(arr, low, high);

            StartQuickSort(arr, low, pivotIndex - 1);
            StartQuickSort(arr, pivotIndex + 1, high);
        }
    }

    private static int Partition(int[] arr, int low, int high)
    {
        int pivot = arr[high];
        int i = (low - 1);

        for (int j = low; j < high; j++)
        {
            if (arr[j] < pivot)
            {
                i++;
                //swap
                int temp = arr[i];
                arr[i] = arr[j];
                arr[j] = temp;
            }
        }
        //pivot 자리 교환
        int temp2 = arr[i + 1];
        arr[i + 1] = arr[high];
        arr[high] = temp2;

        return i + 1;
    }

    public static void StartBubbleSort(int[] arr)
    {
        int n = arr.Length;
        for (int i = 0; i < n - 1; i++)
        {
            bool swapped = false;
            for (int j = 0; j < n - i - 1; j++)
            {
                if (arr[j] > arr[j + 1])
                {
                    int temp = arr[j];
                    arr[j] = arr[j + 1];
                    arr[j + 1] = temp;
                    swapped = true;
                }
            }
            if (!swapped) break;
        }
    }

    public static void StartSelectionSort(int[] arr)
    {
        int n = arr.Length;
        for (int i = 0; i < n - 1; i++)
        {
            int minIndex = i;
            for (int j = i + 1; j < n; j++)
            {
                if (arr[j] < arr[minIndex])
                {
                    minIndex = j;
                }
            }
            //swap
            int temp = arr[minIndex];
            arr[minIndex] = arr[i];
            arr[i] = temp;
        }
    }
}
