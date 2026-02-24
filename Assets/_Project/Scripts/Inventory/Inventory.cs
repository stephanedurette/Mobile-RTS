using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private ItemValueList startingItemValues;

    private HashSet<InventoryItem> items;

    public HashSet<InventoryItem> Items;

    private void Start()
    {
        SetValues(startingItemValues);
    }

    public void SetValues(ItemValueList itemValueList)
    {
        foreach (var item in itemValueList.Items)
        {
            InventoryItem newItem = new InventoryItem(item.ItemModel, item.StartingAmount);
            items.Add(newItem);
        }
    }

    [Serializable]
    public class ItemValueList
    {
        [SerializeField] private List<ItemValue> items;

        public List<ItemValue> Items;
    }

    [Serializable]
    public class ItemValue
    {
        public InventoryItemModel ItemModel;
        public int StartingAmount;
    }
}
