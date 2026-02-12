using UnityEngine;

public class HumanoidUnit : Unit
{
    public bool IsMoving => velocity.sqrMagnitude > 0;

    protected Vector3 velocity;
    protected Vector3? positionLastFrame;

    private void Update()
    {
        UpdateVelocity();
    }

    private void UpdateVelocity()
    {
        velocity = (transform.position - (positionLastFrame ?? transform.position)) / Time.deltaTime;
        positionLastFrame = transform.position;
    }
}
