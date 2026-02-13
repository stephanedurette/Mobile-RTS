using UnityEngine;

public class SelectionCursor : MonoBehaviour
{
    [SerializeField] private float activeDuration;

    private void OnEnable()
    {
        Destroy(gameObject, activeDuration);
    }
}
