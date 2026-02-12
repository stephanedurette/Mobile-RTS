using UnityEngine;

public class HumanoidUnit : Unit
{
    public bool IsMoving => velocity.sqrMagnitude > 0;

    protected Vector3 velocity;
    protected Vector3? positionLastFrame;

    protected Animator animator;
    protected AIPawn aiPawn;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        aiPawn = GetComponent<AIPawn>();
    }

    public void MoveTo(Vector2 position)
    {
        aiPawn.Destination = position;
        unitSprite.flipX = position.x < transform.position.x;
    }

    private void Update()
    {
        UpdateVelocity();
        UpdateAnimations();
    }

    private void UpdateAnimations()
    {
        animator.Play(IsMoving ? "Run" : "Idle");
    }

    private void UpdateVelocity()
    {
        velocity = (transform.position - (positionLastFrame ?? transform.position)) / Time.deltaTime;
        positionLastFrame = transform.position;
    }
}
