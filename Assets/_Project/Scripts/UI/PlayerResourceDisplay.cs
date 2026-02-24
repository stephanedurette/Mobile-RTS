using UnityEngine;

public class PlayerResourceDisplay : MonoBehaviour
{
    [SerializeField] private InventoryView resourceList;

    public void OnResourceAmountChanged(InventoryItemModel resourceData, int amount)
    {
        resourceList.UpdateResourceAmount(resourceData, amount);
    }
}
