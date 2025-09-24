using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [Header("UI 할당할 것들")]
    public TMP_InputField inputSearchName;    
    public Transform contentParent;         
    public GameObject TxT;     

    private List<Item> items = new List<Item>();
    private List<GameObject> itemList = new List<GameObject>();

    void Start()
    {
        items.Clear();
        for (int i = 0; i < 100; i++)
        {
            items.Add(new Item($"Item_{i:D2}", 1));
        }
        items.Sort((a, b) => a.itemName.CompareTo(b.itemName));
        DisplayItems(items);
    }

    private void ClearContent()
    {
        foreach (var go in itemList)
        {
            Destroy(go);
        }
        itemList.Clear();
    }

    private void DisplayItems(List<Item> list)
    {
        ClearContent();

        foreach (var item in list)
        {
            GameObject go = Instantiate(TxT, contentParent);
            TMP_Text txt = go.GetComponent<TMP_Text>();
            txt.text = item.itemName;
            itemList.Add(go);
        }
    }
    public void LinearSearchBtn()
    {
        string target = inputSearchName.text.Trim();
        if (string.IsNullOrEmpty(target)) return;

        Item found = FindItemLinear(target);
        if (found != null)
            DisplayItems(new List<Item> { found });
        else
            ClearContent();
    }
    public void BinarySearchBtn()
    {
        string target = inputSearchName.text.Trim();
        if (string.IsNullOrEmpty(target)) return;

        Item found = FindItemBinary(target);
        if (found != null)
            DisplayItems(new List<Item> { found });
        else
            ClearContent();
    }
    public Item FindItemLinear(string _itemName)
    {
        foreach (var item in items)
        {
            if (item.itemName == _itemName) return item;
        }
        return null; //못 찾으면 null
    }
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