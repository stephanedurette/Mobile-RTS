using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject selectionCursorPrefab;

    private SelectionManager selectionManager;
    private ObjectPoolManager objectPoolManager;

    [Header("Events")]
    [SerializeField] private UnityEvent<List<Action>> OnActionListSelected;
    [SerializeField] private UnityEvent OnActionListCleared;

    [Inject]
    public void Construct(SelectionManager selectionManager, ObjectPoolManager objectPoolManager)
    {
        this.selectionManager = selectionManager;
        this.objectPoolManager = objectPoolManager;
    }

    public void OnGroundSelected(Vector2 position)
    {
        if (selectionManager.SelectedUnit is HumanoidUnit humanoidUnit)
        {
            humanoidUnit.MoveTo(position);
            objectPoolManager.SpawnObject<SelectionCursor>(selectionCursorPrefab, position);
        }
    }

    public void OnUnitSelected(Unit unit)
    {
        unit.Selected = true;
        OnActionListSelected?.Invoke(unit.Actions);
    }

    public void OnUnitDeselected(Unit unit)
    {
        unit.Selected = false;
        OnActionListCleared?.Invoke();
    }
}
