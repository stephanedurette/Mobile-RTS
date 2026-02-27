using UnityEngine;

public partial class GameManager
{
    private class GameStateMachine
    {
        private GameState currentState;

        public GameState CurrentState
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
}

