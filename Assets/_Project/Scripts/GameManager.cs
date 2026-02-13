using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private SelectionManager selectionManager;
    [SerializeField] private GameObject selectionCursorPrefab;

    public void OnGroundSelected(Vector2 position)
    {
        if (selectionManager.SelectedUnit is HumanoidUnit humanoidUnit)
        {
            humanoidUnit.MoveTo(position);
            Instantiate(selectionCursorPrefab, position, Quaternion.identity);
        }
    }

    public void OnUnitSelected(Unit unit)
    {
        unit.Selected = true;
    }

    public void OnUnitDeselected(Unit unit)
    {
        unit.Selected = false;
    }
}
