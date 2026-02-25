using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
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
            InventoryItem newItem = new InventoryItem(item.ItemModel, item.Amount);
            newItem.OnCountChanged += (model, amount) => OnInventoryUpdated?.Invoke(this);
            items.Add(newItem);
        }
        OnInventoryInitialized?.Invoke(this);
    }

    public bool ContainsItem(InventoryItemModel model, int amount)
    {
        InventoryItem foundItem = Items.First(x => x.Model == model);

        if (foundItem == null) return false;
        if (foundItem.Value < amount) return false;
        
        return true;

    }

    public bool ContainsItems(Inventory Other)
    {
        foreach(InventoryItem item in Other.Items)
        {
            if(!ContainsItem(item.Model, item.Value)) return false;
        }
        return true;
    }

    public bool ContainsItems(ItemValueList itemValues)
    {
        foreach (ItemValue item in itemValues.Items)
        {
            if (!ContainsItem(item.ItemModel, item.Amount)) return false;
        }
        return true;
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
        public int Amount;
    }
}
