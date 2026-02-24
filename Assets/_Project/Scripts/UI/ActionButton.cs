using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ActionButton : MonoBehaviour
{
    [SerializeField] private Image actionImage;
    [SerializeField] private InventoryView requiredResources;

    private Action action;

    [HideInInspector] public UnityEvent<ActionButton> OnActionButtonClicked;

    public Action Action
    {
        get { return action; }
        set => SetAction(value);
    }

    private void SetAction(Action action)
    {
        this.action = action;
        actionImage.sprite = action.ActionIcon;
        if (Action is BuildAction buildAction)
        {
            foreach (var resource in buildAction.ResourceCosts) {
                requiredResources.UpdateResourceAmount(resource.Resource, resource.Amount);
            }
            requiredResources.gameObject.SetActive(true);
        } else
        {
            requiredResources.gameObject.SetActive(false);
        }
    }

    public void OnClick()
    {
        OnActionButtonClicked?.Invoke(this);
    }
}
