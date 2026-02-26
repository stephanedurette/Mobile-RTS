using UnityEngine;
using UnityEngine.UIElements;

public partial class GameManager
{
    private class BuildingSelectionState : SelectionState
    {
        public BuildAction SelectedBuildAction;

        private PlacementCursor placementCursor;

        private Vector2 lastGridSnappedPosition;

        private bool isOnValidBuildPosition;

        public BuildingSelectionState(GameManager gameManager) : base(gameManager) { }

        public override void OnCursorDown(Vector2 cursorPosition)
        {

        }

        public override void OnCursorUp(Vector2 cursorPosition)
        {
            if (isOnValidBuildPosition)
            {
                
            } else
            {
                placementCursor.gameObject.SetActive(false);
                gameManager.gridManager.ClearHighlights();
                gameManager.selectionStateMachine.CurrentState = gameManager.unitSelectionState;
            }
        }

        public override void OnEnter()
        {
            placementCursor = gameManager.effectFactory.CreatePlacementCursor(WorldPos(gameManager.inputManager.GetCursorPosition().Value));
            placementCursor.Sprite = (SelectedBuildAction.ActionModel as BuildActionModel).PlacementSprite;
        }

        public override void OnExit()
        {

        }

        public override void Update()
        {
            Vector2 worldCursorPosition = WorldPos(gameManager.inputManager.GetCursorPosition().Value);
            Vector2 worldPositionSnappedToGrid = gameManager.gridManager.WorldPositionSnappedToGrid(worldCursorPosition);

            if (worldPositionSnappedToGrid != lastGridSnappedPosition){
                placementCursor.transform.position = worldPositionSnappedToGrid;

                isOnValidBuildPosition = CanBuild(worldPositionSnappedToGrid, (SelectedBuildAction.ActionModel as BuildActionModel).GridSize);

                UpdateGridHighlights(worldPositionSnappedToGrid, isOnValidBuildPosition);

                lastGridSnappedPosition = worldPositionSnappedToGrid;
            }
        }

        private void UpdateGridHighlights(Vector2 position, bool validBuildPosition)
        {
            gameManager.gridManager.ClearHighlights();
            gameManager.gridManager.HighlightSquares(position, (SelectedBuildAction.ActionModel as BuildActionModel).GridSize, validBuildPosition ? Color.green : Color.red);
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

