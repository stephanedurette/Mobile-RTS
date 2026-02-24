using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryView : MonoBehaviour
{
    [SerializeField] private List<InventoryItemView> inventoryItemViews;

    private Inventory inventory;

    private void Awake()
    {
        DisableAllViews();
    }

    public void Bind(Inventory inventory)
    {
        DisableAllViews();
        Unbind();
        this.inventory = inventory;
        foreach(var inventoryItem in inventory.Items)
        {
            GetInactiveView().Bind(inventoryItem);
        }
    }

    public void Unbind()
    {
        if (inventory != null) {
            foreach (var view in inventoryItemViews)
            {
                view.Unbind();
            }
        }
        inventory = null;
    }

    private void DisableAllViews()
    {
        foreach (var itemView in inventoryItemViews) 
            itemView.gameObject.SetActive(false);
    }

    private InventoryItemView GetInactiveView()
    {
        return inventoryItemViews.FirstOrDefault((v) => !v.gameObject.activeSelf);
    }
}
