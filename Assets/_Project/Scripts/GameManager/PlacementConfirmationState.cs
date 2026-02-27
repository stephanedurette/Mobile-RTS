using UnityEngine;

public partial class GameManager
{
    private class PlacementConfirmationState : GameState
    {
        private ConfirmationWindow confirmationWindow;
        private PlacementCursor placementCursor;

        public BuildAction SelectedBuildAction;

        public PlacementConfirmationState(GameManager gameManager) : base(gameManager)
        {
        }

        public override void OnCursorDown(Vector2 cursorPosition)
        {
            //var hits = GetHits(WorldCursorPosition);
        }

        public override void OnCursorUp(Vector2 cursorPosition)
        {
            
        }

        public override void OnEnter()
        {
            placementCursor = gameManager.effectFactory.CreatePlacementCursor(WorldCursorPosition, SelectedBuildAction);

            confirmationWindow = gameManager.effectFactory.CreateConfirmationWindow(WorldCursorPosition, OnConfirmButtonClicked, OnCancelButtonClicked);
        }

        public override void OnExit()
        {
            confirmationWindow.gameObject.SetActive(false);
            placementCursor.gameObject.SetActive(false);
        }

        public override void Update()
        {
            
        }

        private void OnConfirmButtonClicked()
        {
            Debug.Log("placement confirmed");
            //remove currency
            confirmationWindow.gameObject.SetActive(false);
        }

        private void OnCancelButtonClicked()
        {
            gameManager.gameStateMachine.CurrentState = gameManager.unitSelectionState;
        }
    }
}
