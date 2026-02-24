using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Inventory : MonoBehaviour
{
    [SerializeField] private ItemValueList startingItemValues;

    [SerializeField] private UnityEvent<Inventory> OnInventoryInitialized;
    [SerializeField] private UnityEvent<Inventory> OnInventoryUpdated;

    private HashSet<InventoryItem> items;

    public HashSet<InventoryItem> Items => items;

    private void Start()
    {
        SetValues(startingItemValues);
    }

    public void SetValues(ItemValueList itemValueList)
    {
        items = new();
        foreach (var item in itemValueList.Items)
        {
            InventoryItem newItem = new InventoryItem(item.ItemModel, item.StartingAmount);
            newItem.OnCountChanged += (model, amount) => OnInventoryUpdated?.Invoke(this);
            items.Add(newItem);
        }
        OnInventoryInitialized?.Invoke(this);
    }

    [Serializable]
    public class ItemValueList
    {
        [SerializeField] private List<ItemValue> items;
        public List<ItemValue> Items => items;
    }

    [Serializable]
    public class ItemValue
    {
        public InventoryItemModel ItemModel;
        public int StartingAmount;
    }
}
