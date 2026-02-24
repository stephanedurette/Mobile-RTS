using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] private UnityEvent<InventoryItemModel, int> OnResourceAmountChanged;

    private HashSet<Resource> resources;

    private void Awake()
    {
        resources = new();
    }

    public void SetResourceCount(InventoryItemModel data, int amount)
    {
        GetOrCreateResource(data).Amount = amount;
    }

    public void AddResource(InventoryItemModel data, int amount)
    {
        SetResourceCount(data, GetResourceCount(data) + amount);
    }

    public int GetResourceCount(InventoryItemModel resourceData)
    {
        return GetOrCreateResource(resourceData).Amount;
    }

    private Resource GetOrCreateResource(InventoryItemModel resourceData)
    {
        Resource r = resources.FirstOrDefault((r) => r.Data == resourceData);
        if (r == null)
        {
            r = new Resource(0, resourceData);
            resources.Add(r);
            r.OnAmountChanged += (d, r) => OnResourceAmountChanged?.Invoke(d, r);
        }
        return r;
    }
}
