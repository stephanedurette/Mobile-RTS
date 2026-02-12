using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private SelectionManager selectionManager;

    public void OnGroundSelected(Vector2 position)
    {
        if (selectionManager.SelectedUnit is HumanoidUnit humanoidUnit)
        {
            humanoidUnit.MoveTo(position);
        }
    }
}
