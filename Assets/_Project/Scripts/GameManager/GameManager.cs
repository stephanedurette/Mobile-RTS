using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public partial class GameManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Player HumanPlayer;

    [Header("Events")]
    [SerializeField] private UnityEvent<Unit> OnPlayerUnitSelected;
    [SerializeField] private UnityEvent<Unit> OnPlayerUnitDeselected;

    private SelectionStateMachine selectionStateMachine;

    private UnitSelectionState unitSelectionState;
    private BuildingSelectionState buildingSelectionState;

    private SpawnFactory effectFactory;
    private InputManager inputManager;
    private GridManager gridManager;

    [Inject]
    public void Construct(SpawnFactory effectFactory, InputManager inputManager, GridManager gridManager)
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
        actionButton.Action.Execute(this);
    }

    public void BeginBuildPlacement(BuildAction buildAction) { 
        buildingSelectionState.SelectedBuildAction = buildAction;
        selectionStateMachine.CurrentState = buildingSelectionState;
    }
}
