using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class SelectionManager : MonoBehaviour
{
    [SerializeField] private float selectionRadius = 5;

    [SerializeField] private UnityEvent<Unit> OnUnitSelected;
    [SerializeField] private UnityEvent<Unit> OnUnitDeselected;

    [SerializeField] private UnityEvent<Vector2> OnGroundSelected;

    private Unit selectedUnit;

    private bool isCursorOverUI;

    public Unit SelectedUnit
    {
        get { return selectedUnit; }
        private set {
            if (value == selectedUnit) return;
            if (selectedUnit != null) OnUnitDeselected?.Invoke(selectedUnit);
            selectedUnit = value; 
            if (selectedUnit != null) OnUnitSelected?.Invoke(selectedUnit);
        }
    }

    public void OnCursorReleased(Vector2 cursorPos)
    {
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(cursorPos);

        var hits = Physics2D.OverlapCircleAll(worldPos, selectionRadius);

        if (isCursorOverUI) return;

        if (ContainsComponentOfType<Unit>(hits, out var unit))
        {
            if (SelectedUnit == unit)
            {
                SelectedUnit = null;
            } else
            {
                SelectedUnit = unit;
            }
            return;
        }

        if (ContainsComponentOfType<Ground>(hits, out var _))
        {
            OnGroundSelected?.Invoke(worldPos);
            return;
        }
    }

    private bool ContainsComponentOfType<T>(Collider2D[] hits, out T target) where T : Component
    {
        foreach (var hit in hits) { 
            if (hit.TryGetComponent(out T t))
            {
                target = t;
                return true;
            }
        }
        target = null;
        return false;

    }

    private void Update()
    {
        isCursorOverUI = EventSystem.current.IsPointerOverGameObject();
    }
}
