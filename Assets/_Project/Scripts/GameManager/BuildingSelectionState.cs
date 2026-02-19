using UnityEngine;
using UnityEngine.UIElements;

public partial class GameManager
{
    private class BuildingSelectionState : SelectionState
    {
        public BuildAction SelectedBuildAction;

        private PlacementCursor placementCursor;

        public BuildingSelectionState(GameManager gameManager) : base(gameManager) { }

        public override void OnCursorDown(Vector2 cursorPosition)
        {
            
        }

        public override void OnCursorUp(Vector2 cursorPosition)
        {
            Debug.Log("on cursor up");
        }

        public override void OnEnter()
        {
            placementCursor = gameManager.effectFactory.CreatePlacementCursor(Vector2.zero);
            placementCursor.Sprite = SelectedBuildAction.PlacementSprite;
        }

        public override void OnExit()
        {

        }

        public override void Update()
        {
            //placementCursor.transform.position = WorldPos
        }
    }
}

