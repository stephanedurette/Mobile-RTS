using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ActionButton : MonoBehaviour
{
    private UnitActionView actionView;
    private Button button;
    private EventTrigger eventTrigger;

    [HideInInspector] public UnityEvent<ActionButton> OnActionButtonClicked;

    private UnitAction action;
    private Player player;

    public UnitAction Action => action;
    public Player Player => player;

    private void Awake()
    {
        actionView = GetComponentInChildren<UnitActionView>();
        button = GetComponentInChildren<Button>();
        eventTrigger = GetComponentInChildren<EventTrigger>();
    }

    public void Bind(UnitAction action, Player player)
    {
        this.action = action;
        this.player = player;

        actionView.Bind(action);

        SetButtonAvailability();
        player.Inventory.OnUpdated += OnPlayerInventoryUpdated;
        //set resource text red if not enough and listen for this
    }

    private void SetButtonAvailability()
    {
        if (action is BuildAction buildAction)
        {
            bool canExecute = buildAction.CanExecute(player);
            button.interactable = canExecute;
            eventTrigger.enabled = canExecute;
        } else
        {
            button.interactable = true;
            eventTrigger.enabled = true;
        }
    }

    private void OnPlayerInventoryUpdated(Inventory inventory)
    {
        SetButtonAvailability();
    }

    public void Unbind()
    {
        if (player != null) {
            player.Inventory.OnUpdated -= OnPlayerInventoryUpdated;
        }

        action = null;
        player = null;

        actionView.Unbind(action);
    }

    public void OnClick()
    {
        OnActionButtonClicked?.Invoke(this);
    }
}
