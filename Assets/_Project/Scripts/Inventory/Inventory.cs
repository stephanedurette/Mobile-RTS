using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory
{
    public Action<Inventory> OnUpdated = delegate { };

    private HashSet<InventoryItem> items;

    public HashSet<InventoryItem> Items => items;

    public Inventory()
    {

    }

    public Inventory(ItemValueList initialValues)
    {
        SetValues(initialValues);
    }

    public void SetValues(ItemValueList itemValueList)
    {
        items = new();
        foreach (var item in itemValueList.Items)
        {
            InventoryItem newItem = new InventoryItem(item.ItemModel, item.Amount);
            newItem.OnCountChanged += (model, amount) => OnUpdated?.Invoke(this);
            items.Add(newItem);
        }
        OnUpdated?.Invoke(this);
    }

    public bool ContainsItem(InventoryItemModel model, int amount)
    {
        InventoryItem foundItem = Items.First(x => x.Model == model);

        if (foundItem == null) return false;
        if (foundItem.Value < amount) return false;

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
        foreach (InventoryItem item in Other.Items)
        {
            if (!ContainsItem(item.Model, item.Value)) return false;
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
