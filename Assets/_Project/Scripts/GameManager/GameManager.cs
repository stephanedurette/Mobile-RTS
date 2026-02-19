using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public partial class GameManager : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] private UnityEvent<List<Action>> OnActionListSelected;
    [SerializeField] private UnityEvent OnActionListCleared;

    private SelectionStateMachine selectionStateMachine;

    private UnitSelectionState unitSelectionState;
    private BuildingSelectionState buildingSelectionState;

    private EffectFactory effectFactory;
    private InputManager inputManager;

    [Inject]
    public void Construct(EffectFactory effectFactory, InputManager inputManager)
    {
        this.effectFactory = effectFactory;
        this.inputManager = inputManager;
    }

    private void Awake()
    {
        selectionStateMachine = new();

        unitSelectionState = new(this);
        buildingSelectionState = new(this);

        selectionStateMachine.CurrentState = unitSelectionState;
    }

    private void Update()
    {
        selectionStateMachine.Update();
    }

    public void OnCursorDown(Vector2 cursorPos) => selectionStateMachine.OnCursorDown(cursorPos);

    public void OnCursorUp(Vector2 cursorPos) => selectionStateMachine.OnCursorUp(cursorPos);

    public void OnActionButtonClicked(ActionButton actionButton)
    {
        actionButton.Action.Execute(this);
    }

    public void OnBuildActionExecute(BuildAction buildAction) { 
        buildingSelectionState.SelectedBuildAction = buildAction;
        selectionStateMachine.CurrentState = buildingSelectionState;
    }
}
