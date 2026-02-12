using UnityEngine;

public class AIPawn : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float minMoveDistance;

    private Vector3? destination;

    public Vector3? Destination
    {
        get { return destination; }
        set { destination = value; }
    }

    private void Start()
    {
        Destination = Vector3.right * 4.5f;
    }

    private void Update()
    {
        MoveToDestination();
    }

    private void MoveToDestination()
    {
        if (!destination.HasValue) return;

        Vector3 distance = destination.Value - transform.position;

        transform.position += moveSpeed * Time.deltaTime * distance.normalized;

        if (distance.sqrMagnitude < minMoveDistance * minMoveDistance)
            destination = null;

    }
}
