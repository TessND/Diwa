using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<ItemsHolder> Items = new();

    public void AddItem(ItemsHolder newItem)
    {
        Items.Add(newItem);
        newItem.GameObject().SetActive(true);
    }
    
    public void RemoveItem(ItemsHolder newItem)
    {
        Items.Remove(newItem);
        newItem.GameObject().SetActive(false);
    }
}
