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

    private ObjectPoolManager objectPoolManager;
    private GameObject moveEffectPrefab;
    private GameObject placementEffectPrefab;

    [Inject]
    public void Construct(ObjectPoolManager objectPoolManager, [Inject(Id = "MoveCursor")] GameObject moveEffectPrefab, [Inject(Id = "PlacementCursor")] GameObject placementEffectPrefab)
    {
        this.objectPoolManager = objectPoolManager;
        this.moveEffectPrefab = moveEffectPrefab;
        this.placementEffectPrefab = placementEffectPrefab;
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
