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

            bool canBuild = CanBuild(worldPositionSnappedToGrid, SelectedBuildAction.GridSize);
            Color highlightColor = canBuild ? Color.green : Color.red;

            gameManager.gridManager.ClearHighlights();
            gameManager.gridManager.HighlightSquares(worldPositionSnappedToGrid, SelectedBuildAction.GridSize, highlightColor);
        }

        private bool CanBuild(Vector2 pos, Vector2Int buildingSize)
        {
            if (gameManager.gridManager.IsComponentOnGrid(pos, buildingSize, out Unit unit))
            {
                return false;
            }

            if (!gameManager.gridManager.IsComponentOnEveryGridPosition<Ground>(pos, buildingSize))
            {
                return false;
            }

            return true;
        }
    }
}

