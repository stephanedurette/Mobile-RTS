using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory
{
    public Action<Inventory> OnUpdated = delegate { };

    private Dictionary<InventoryItemModel, InventoryItem> items;

    public Dictionary<InventoryItemModel, InventoryItem> Items => items;

    public Inventory()
    {

    }

    public Inventory(ItemValueList initialValues)
    {
        SetValues(initialValues);
    }

    public void AddItems(InventoryItemModel itemModel, int amount)
    {
        if (!items.ContainsKey(itemModel)) {
            InventoryItem newItem = new InventoryItem(itemModel, 0);
            newItem.OnCountChanged += (model, amount) => OnUpdated?.Invoke(this);
            items.Add(itemModel, newItem);
        }

        items[itemModel].Value += amount;
    }

    public void AddInventory(Inventory other)
    {
        foreach (var kp in other.Items) {
            AddItems(kp.Key, kp.Value.Value);
        }
    }

    public void RemoveItems(InventoryItemModel itemModel, int amount)
    {
        if (!items.ContainsKey(itemModel)) return;

        items[itemModel].Value -= amount;
    }

    public void RemoveInventory(Inventory other)
    {
        foreach (var kp in other.Items)
        {
            RemoveItems(kp.Key, kp.Value.Value);
        }
    }

    public void SetValues(ItemValueList itemValueList)
    {
        items = new();
        foreach (var item in itemValueList.Items)
        {
            InventoryItem newItem = new InventoryItem(item.ItemModel, item.Amount);
            newItem.OnCountChanged += (model, amount) => OnUpdated?.Invoke(this);
            items.Add(item.ItemModel, newItem);
        }
        OnUpdated?.Invoke(this);
    }

    public bool ContainsItem(InventoryItemModel model, int amount)
    {
        if (!items.ContainsKey(model)) return false;

        if (items[model].Value < amount) return false;

        return true;

    }

    public bool ContainsItems(ItemValueList itemValueList)
    {
        foreach (ItemValue itemValue in itemValueList.Items)
        {
            if (!ContainsItem(itemValue.ItemModel, itemValue.Amount)) return false;
        }
        return true;
    }

    public bool ContainsItems(Inventory Other)
    {
        foreach (var i in Other.Items)
        {
            if (!ContainsItem(i.Key, i.Value.Value)) return false;
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
