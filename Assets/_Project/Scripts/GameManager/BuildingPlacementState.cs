using UnityEngine;
using UnityEngine.UIElements;

public partial class GameManager
{
    private class BuildingPlacementState : GameState
    {
        public BuildAction SelectedBuildAction;

        private PlacementCursor placementCursor;

        private Vector2 lastGridSnappedPosition;

        private bool isOnValidBuildPosition;

        public BuildingPlacementState(GameManager gameManager) : base(gameManager) { }

        public override void OnCursorDown(Vector2 cursorPosition)
        {

        }

        public override void OnCursorUp(Vector2 cursorPosition)
        {
            if (isOnValidBuildPosition)
            {
                gameManager.placementConfirmationState.SelectedBuildAction = SelectedBuildAction;
                gameManager.gameStateMachine.CurrentState = gameManager.placementConfirmationState;
            } else
            {
                gameManager.gameStateMachine.CurrentState = gameManager.unitSelectionState;
            }
        }

        public override void OnEnter()
        {
            placementCursor = gameManager.effectFactory.CreatePlacementCursor(WorldCursorPosition, SelectedBuildAction);
        }

        public override void OnExit()
        {
            placementCursor.gameObject.SetActive(false);
            gameManager.gridManager.ClearHighlights();
        }

        public override void Update()
        {
            Vector2 worldCursorPosition = WorldCursorPosition;
            Vector2 worldPositionSnappedToGrid = gameManager.gridManager.WorldPositionSnappedToGrid(worldCursorPosition);

            if (worldPositionSnappedToGrid != lastGridSnappedPosition){
                placementCursor.transform.position = worldPositionSnappedToGrid;

                isOnValidBuildPosition = CanBuild(worldPositionSnappedToGrid, SelectedBuildAction.BuildActionModel.GridSize);

                UpdateGridHighlights(worldPositionSnappedToGrid, isOnValidBuildPosition);

                lastGridSnappedPosition = worldPositionSnappedToGrid;
            }
        }

        private void UpdateGridHighlights(Vector2 position, bool validBuildPosition)
        {
            gameManager.gridManager.ClearHighlights();
            gameManager.gridManager.HighlightSquares(position, SelectedBuildAction.BuildActionModel.GridSize, validBuildPosition ? Color.green : Color.red);
        }

        private bool CanBuild(Vector2 pos, Vector2Int buildingSize)
        {
            if (gameManager.gridManager.IsComponentOnGrid(pos, buildingSize, out Unit unit))
            {
                return false;
            }

            if (!gameManager.gridManager.IsComponentOnEveryGridPosition<Buildable>(pos, buildingSize))
            {
                return false;
            }

            return true;
        }
    }
}

