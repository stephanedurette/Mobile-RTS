using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private Inventory.ItemValueList startingInventory;
    [SerializeField] private UnityEvent<Inventory> OnInventoryInitialized;

    private Inventory inventory;

    private void Start()
    {
        inventory = new(startingInventory);
        OnInventoryInitialized?.Invoke(inventory);
    }
}
