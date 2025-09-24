using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using static UnityEditor.Progress;

public class InventoryBigTest : MonoBehaviour
{
    List<Item> items = new List<Item>();

    private System.Random rand = new System.Random();

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 100000; i++)
        {
            string name = $"Item_{i:D5}";
            int qty = rand.Next(1, 100);
            items.Add(new Item(name, qty));
        }

        //선형 탐색 테스트
        string target = "Item_45672";
        Stopwatch sw = Stopwatch.StartNew();
        Item foundLinear = FindItemLinear(target);
        sw.Stop();
        UnityEngine.Debug.Log($"[선형 탐색] {target} 개수 : {foundLinear?.quantity}, 시간 : {sw.ElapsedMilliseconds}ms");


        //이진 탐색 테스트
        sw.Restart();
        Item foundBinary = FindItemBinary(target);
        sw.Stop();
        UnityEngine.Debug.Log($"[이진 탐색] {target} 개수 : {foundBinary?.quantity}, 시간 : {sw.ElapsedMilliseconds}ms");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //선형 탐색
    public Item FindItemLinear(string _itemName)
    {
        foreach (var item in items)
        {
            if (item.itemName == _itemName) return item;
        }
        return null; //못 찾으면 null
    }

    //이진 탐색
    public Item FindItemBinary(string targetName)
    {
        int left = 0;
        int right = items.Count - 1;

        while (left <= right)
        {
            int mid = (left + right) / 2;
            int compare = items[mid].itemName.CompareTo(targetName);

            if (compare == 0)
            {
                return items[mid]; //찾음
            }
            else if (compare < 0)
            {
                left = mid + 1;
            }
            else
            {
                right = mid - 1; //왼쪽 탐색;
            }
        }
        return null; // 못 찾음
    }
}
