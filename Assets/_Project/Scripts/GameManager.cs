using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject selectionCursorPrefab;

    private ObjectPoolManager objectPoolManager;

    [Header("Events")]
    [SerializeField] private UnityEvent<List<Action>> OnActionListSelected;
    [SerializeField] private UnityEvent OnActionListCleared;

    private SelectionStateMachine selectionStateMachine;

    private UnitSelectionState unitSelectionState;
    private BuildingSelectionState buildingSelectionState;

    [Inject]
    public void Construct(ObjectPoolManager objectPoolManager)
    {
        this.objectPoolManager = objectPoolManager;
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

    private class SelectionStateMachine
    {
        private SelectionState currentState;

        public SelectionState CurrentState
        {
            get { return currentState; }
            set
            {
                if (currentState == value) return;
                currentState?.OnExit();
                currentState = value;
                currentState?.OnEnter();
            }
        }

        public void OnCursorUp(Vector2 cursorPosition) => currentState?.OnCursorUp(cursorPosition);
        public void OnCursorDown(Vector2 cursorPosition) => currentState?.OnCursorDown(cursorPosition);

        public void Update() => currentState?.Update();
    }

    private abstract class SelectionState
    {
        protected GameManager gameManager;

        public SelectionState(GameManager gameManager)
        {
            this.gameManager = gameManager;
        }

        public static float SelectionRadius = .01f;

        public abstract void OnCursorUp(Vector2 cursorPosition);
        public abstract void OnCursorDown(Vector2 cursorPosition);
        public abstract void Update();

        public abstract void OnEnter();
        public abstract void OnExit();

        public static Vector2 WorldPos(Vector2 cursorPos) => Camera.main.ScreenToWorldPoint(cursorPos);

        public static Collider2D[] GetHits(Vector2 worldPos) => Physics2D.OverlapCircleAll(worldPos, SelectionRadius);

        public static bool ContainsComponentOfType<T>(Collider2D[] hits, out T target) where T : Component
        {
            foreach (var hit in hits)
            {
                if (hit.TryGetComponent(out T t))
                {
                    target = t;
                    return true;
                }
            }
            target = null;
            return false;
        }
    }

    private class UnitSelectionState : SelectionState
    {
        private Unit selectedUnit;

        private Unit SelectedUnit { 
            get { return selectedUnit; }
            set
            {
                if (selectedUnit != null) OnUnitDeselected(selectedUnit);
                selectedUnit = value;
                if (selectedUnit != null) OnUnitSelected(selectedUnit);
            }
        }

        public UnitSelectionState(GameManager gameManager) : base(gameManager) { }

        public override void OnCursorDown(Vector2 cursorPosition) { }

        public override void OnCursorUp(Vector2 cursorPosition)
        {
            Vector2 worldPosition = WorldPos(cursorPosition);
            var hits = GetHits(worldPosition);

            if (ContainsComponentOfType<Unit>(hits, out var unit))
            {
                if (selectedUnit == unit)
                {
                    SelectedUnit = null;
                }
                else
                {
                    SelectedUnit = unit;
                }
                return;
            }

            if (ContainsComponentOfType<Ground>(hits, out var _))
            {
                OnGroundSelected(worldPosition);
                return;
            }
        }

        public override void OnEnter() { }

        public override void OnExit() { }

        public override void Update() { }


        private void OnGroundSelected(Vector2 position)
        {
            if (selectedUnit is HumanoidUnit humanoidUnit)
            {
                humanoidUnit.MoveTo(position);
                gameManager.objectPoolManager.SpawnObject<SelectionCursor>(gameManager.selectionCursorPrefab, position);
            }
        }

        private void OnUnitSelected(Unit unit)
        {
            unit.Selected = true;
            gameManager.OnActionListSelected?.Invoke(unit.Actions);
        }

        private void OnUnitDeselected(Unit unit)
        {
            unit.Selected = false;
            gameManager.OnActionListCleared?.Invoke();
        }
    }

    private class BuildingSelectionState : SelectionState
    {
        public BuildingSelectionState(GameManager gameManager) : base(gameManager) { }

        public override void OnCursorDown(Vector2 cursorPosition)
        {

        }

        public override void OnCursorUp(Vector2 cursorPosition)
        {

        }

        public override void OnEnter()
        {

        }

        public override void OnExit()
        {

        }

        public override void Update()
        {

        }
    }
}
