using UnityEngine;
using UnityEngine.UIElements;

public partial class GameManager
{
    private class BuildingSelectionState : SelectionState
    {
        public BuildAction SelectedBuildAction;

        private PlacementCursor placementCursor;

        private Vector2 worldCursorPosition => WorldPos(gameManager.inputManager.GetCursorPosition().Value);

        private Vector2 worldPositionSnappedToGrid => gameManager.gridManager.WorldPositionSnappedToGrid(worldCursorPosition);

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
            placementCursor = gameManager.effectFactory.CreatePlacementCursor(worldCursorPosition);
            placementCursor.Sprite = SelectedBuildAction.PlacementSprite;
        }

        public override void OnExit()
        {

        }

        public override void Update()
        {
            if (worldPositionSnappedToGrid != worldCursorPosition) { 
                placementCursor.transform.position = worldPositionSnappedToGrid;
            }
        }
    }
}

