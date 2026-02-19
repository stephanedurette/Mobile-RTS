using UnityEngine;
using Zenject;

public partial class GameManager
{
    private class UnitSelectionState : SelectionState
    {

        private Unit selectedUnit;

        private Unit SelectedUnit
        {
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
                gameManager.effectFactory.CreateSelectionCursor(position);
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
}

