using UnityEngine;
using UnityEngine.UI;

public class ActionView : MonoBehaviour
{
    [SerializeField] private Image icon;

    private InventoryView inventoryView;

    public Action BoundAction { get; private set; }

    private void Awake()
    {
        inventoryView = GetComponentInChildren<InventoryView>();
    }

    public void Bind(Action action)
    {
        BoundAction = action;
        icon.sprite = action.ActionModel.Icon;

        if (BoundAction is BuildAction buildAction) 
        {
            inventoryView.Bind(buildAction.Inventory);
        } 
        else
        {
            inventoryView.DisableAllViews();
        }
    }

    public void Unbind(Action action) 
    {
        if (BoundAction is BuildAction)
        {
            inventoryView.Unbind();
        }

        if (BoundAction != null) {
            BoundAction = null;
        }
    }
}
