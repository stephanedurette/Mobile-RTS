using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public partial class GameManager : MonoBehaviour
{
    private SelectionStateMachine selectionStateMachine;

    private UnitSelectionState unitSelectionState;
    private BuildingSelectionState buildingSelectionState;

    private EffectFactory effectFactory;
    private InputManager inputManager;
    private GridManager gridManager;

    [Inject]
    public void Construct(EffectFactory effectFactory, InputManager inputManager, GridManager gridManager)
    {
        this.effectFactory = effectFactory;
        this.inputManager = inputManager;
        this.gridManager = gridManager;
    }

    private void Awake()
    {
        selectionStateMachine = new();

        unitSelectionState = new(this);
        buildingSelectionState = new(this);

        selectionStateMachine.CurrentState = unitSelectionState;
    }

    private void Start()
    {
    }

    private void Update()
    {
        selectionStateMachine.Update();
    }

    public void OnCursorDown(Vector2 cursorPos) => selectionStateMachine.OnCursorDown(cursorPos);

    public void OnCursorUp(Vector2 cursorPos) => selectionStateMachine.OnCursorUp(cursorPos);

    public void OnActionButtonClicked(ActionButton actionButton)
    {
        
    }

    public void OnBuildActionExecute(BuildActionModel buildAction) { 
        buildingSelectionState.SelectedBuildAction = buildAction;
        selectionStateMachine.CurrentState = buildingSelectionState;
    }
}
