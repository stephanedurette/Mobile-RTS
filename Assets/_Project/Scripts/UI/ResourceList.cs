using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ResourceList : MonoBehaviour
{
    [SerializeField] private List<ResourceDisplay> resourceDisplays;

    private Dictionary<ResourceData, ResourceDisplay> resourceDisplaysDict;

    private void Awake()
    {
        resourceDisplaysDict = new();
    }

    public void UpdateResourceAmount(ResourceData resourceData, int amount)
    {
        GetOrCreateDisplay(resourceData).SetAmount(amount);
    }

    public void UpdateResourceDisplayColor(ResourceData resourceData, Color c)
    {
        GetOrCreateDisplay(resourceData).SetTextColor(c);
    }

    private ResourceDisplay GetOrCreateDisplay(ResourceData resourceData)
    {
        if (!resourceDisplaysDict.ContainsKey(resourceData)) { 
            ResourceDisplay firstInactiveDisplay = resourceDisplays.FirstOrDefault((r) => !r.gameObject.activeSelf);
            firstInactiveDisplay.SetResource(resourceData);
            resourceDisplaysDict.Add(resourceData, firstInactiveDisplay);
            firstInactiveDisplay.gameObject.SetActive(true);
        }

        return resourceDisplaysDict[resourceData];
    }
}
