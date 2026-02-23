using UnityEngine;

public class PlayerResourceDisplay : MonoBehaviour
{
    [SerializeField] private ResourceList resourceList;

    public void OnResourceAmountChanged(ResourceData resourceData, int amount)
    {
        resourceList.UpdateResourceAmount(resourceData, amount);
    }
}
