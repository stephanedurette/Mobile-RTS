using UnityEngine;

public partial class GameManager
{
    private abstract class SelectionState
    {
        protected GameManager gameManager;

        public SelectionState(GameManager gameManager)
        {
            this.gameManager = gameManager;
        }

        public static float SelectionRadius = .5f;

        public abstract void OnCursorUp(Vector2 cursorPosition);
        public abstract void OnCursorDown(Vector2 cursorPosition);
        public abstract void Update();

        public abstract void OnEnter();
        public abstract void OnExit();

        public static Vector2 WorldPos(Vector2 cursorPos) => Camera.main.ScreenToWorldPoint(cursorPos);

        public static Collider2D[] GetHits(Vector2 worldPos) => Physics2D.OverlapCircleAll(worldPos, SelectionRadius);

        public static bool ContainsComponentOfType<T>(Collider2D[] hits, out T target) where T : Component
        {
            foreach (var hit in hits)
            {
                if (hit.TryGetComponent(out T t))
                {
                    target = t;
                    return true;
                }
            }
            target = null;
            return false;
        }
    }
}

