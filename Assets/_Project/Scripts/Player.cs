using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private ResourceAmount[] startingResources;
    [SerializeField] private UnityEvent<ResourceData, int> OnResourceAmountChanged;

    private HashSet<Resource> resources;

    private void Awake()
    {
        resources = new();
    }

    private void Start()
    {
        foreach (var resource in startingResources) {
            var r = new Resource(resource.Cost, resource.Resource);
            resources.Add(r);
            r.OnAmountChanged += (d, r) => OnResourceAmountChanged?.Invoke(d, r);
            OnResourceAmountChanged?.Invoke(resource.Resource, resource.Cost);
        }
    }

    public void SetResourceCount(ResourceData data, int amount)
    {
        GetOrCreateResource(data).Amount = amount;
    }

    public void AddResource(ResourceData data, int amount)
    {
        SetResourceCount(data, GetResourceCount(data) + amount);
    }

    public int GetResourceCount(ResourceData resourceData)
    {
        return GetOrCreateResource(resourceData).Amount;
    }

    private Resource GetOrCreateResource(ResourceData resourceData)
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
