using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private Inventory.ItemValueList startingItems;
    [SerializeField] private UnityEvent<Inventory> OnInventoryInitialized;

    private Inventory inventory;

    public Inventory Inventory => inventory;

    private void Start()
    {
        inventory = new(startingItems);
        OnInventoryInitialized?.Invoke(inventory);
    }
}
