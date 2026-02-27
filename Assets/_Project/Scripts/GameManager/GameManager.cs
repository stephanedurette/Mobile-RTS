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

    private GameStateMachine gameStateMachine;

    private UnitSelectionState unitSelectionState;
    private BuildingPlacementState buildingPlacementState;
    private PlacementConfirmationState placementConfirmationState;

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
        gameStateMachine = new();

        unitSelectionState = new(this);
        buildingPlacementState = new(this);
        placementConfirmationState = new(this);

        gameStateMachine.CurrentState = unitSelectionState;
    }

    private void Start()
    {
    }

    private void Update()
    {
        gameStateMachine.Update();
    }

    public void OnCursorDown(Vector2 cursorPos) => gameStateMachine.OnCursorDown(cursorPos);

    public void OnCursorUp(Vector2 cursorPos) => gameStateMachine.OnCursorUp(cursorPos);

    public void OnActionButtonClicked(ActionButton actionButton)
    {
        actionButton.Action.Execute(this);
    }

    public void BeginBuildPlacement(BuildAction buildAction) { 
        buildingPlacementState.SelectedBuildAction = buildAction;
        gameStateMachine.CurrentState = buildingPlacementState;
    }
}
