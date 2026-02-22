using UnityEngine;

public partial class GameManager
{
    private class BuildingSelectionState : SelectionState
    {
        public BuildAction SelectedBuildAction;

        private PlacementCursor placementCursor;

        private Vector2 lastGridSnappedPosition;

        public BuildingSelectionState(GameManager gameManager) : base(gameManager) { }

        public override void OnCursorDown(Vector2 cursorPosition)
        {

        }

        public override void OnCursorUp(Vector2 cursorPosition)
        {
            placementCursor.gameObject.SetActive(false);
            gameManager.selectionStateMachine.CurrentState = gameManager.unitSelectionState;
        }

        public override void OnEnter()
        {
            placementCursor = gameManager.effectFactory.CreatePlacementCursor(WorldPos(gameManager.inputManager.GetCursorPosition().Value));
            placementCursor.Sprite = SelectedBuildAction.PlacementSprite;
        }

        public override void OnExit()
        {

        }

        public override void Update()
        {
            Vector2 worldCursorPosition = WorldPos(gameManager.inputManager.GetCursorPosition().Value);
            Vector2 worldPositionSnappedToGrid = gameManager.gridManager.WorldPositionSnappedToGrid(worldCursorPosition);

            if (worldPositionSnappedToGrid == lastGridSnappedPosition) return;

            lastGridSnappedPosition = worldPositionSnappedToGrid;
            placementCursor.transform.position = worldPositionSnappedToGrid;

            gameManager.gridManager.ClearHighlights();
            if (gameManager.gridManager.IsComponentOnGrid(worldPositionSnappedToGrid, SelectedBuildAction.GridSize, out Unit unit))
            {
                gameManager.gridManager.HighlightSquares(worldPositionSnappedToGrid, SelectedBuildAction.GridSize, Color.red);
            } else
            {
                gameManager.gridManager.HighlightSquares(worldPositionSnappedToGrid, SelectedBuildAction.GridSize, Color.green);
            }

        }
    }
}

