using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ActionButton : MonoBehaviour
{
    [SerializeField] private Image actionImage;
    [SerializeField] private InventoryView requiredResources;

    private ActionModel action;

    [HideInInspector] public UnityEvent<ActionButton> OnActionButtonClicked;

    public ActionModel Action
    {
        get { return action; }
        set => SetAction(value);
    }

    private void SetAction(ActionModel action)
    {
        this.action = action;
        actionImage.sprite = action.Icon;
    }

    public void OnClick()
    {
        OnActionButtonClicked?.Invoke(this);
    }
}
