using UnityEngine;
using Zenject;

public partial class GameManager
{
    private class UnitSelectionState : GameState
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
            if (gameManager.inputManager.PointerOverUI()) return;

            var hits = GetHits(WorldCursorPosition);

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

            if (ContainsComponentOfType<Walkable>(hits, out var _))
            {
                OnWalkableSelected(WorldCursorPosition);
                return;
            }
        }

        public override void OnEnter() { }

        public override void OnExit() { }

        public override void Update() { }


        private void OnWalkableSelected(Vector2 position)
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
            if (unit.Owner == gameManager.HumanPlayer)
            {
                gameManager.OnPlayerUnitSelected?.Invoke(unit);
            }
        }

        private void OnUnitDeselected(Unit unit)
        {
            unit.Selected = false;
            if (unit.Owner == gameManager.HumanPlayer)
            {
                gameManager.OnPlayerUnitDeselected?.Invoke(unit);
            }
        }
    }
}

