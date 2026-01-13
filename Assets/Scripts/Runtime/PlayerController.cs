using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;

    public float characterSpeed = 3.0f;
    public float characterHealth;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        animator = GetComponentInChildren<Animator>();
        
        characterHealth = 1.0f;
    }

    // void Update()
    // {
        // move = MoveAction.ReadValue<Vector2>(); // Value of the move action

        // Animation updates
        // animator.SetBool("1_Move", move.y != 0);

        // if (isInvisible)
        // {
        //     invisibleDuration -= Time.deltaTime;
        //     if (invisibleDuration <= 0)
        //     {
        //         isInvisible = false;
        //         invisibleDuration = 2.0f; // Reset the duration for next hit
        //     }
        // }
    // }

    public void MoveCharacter(Vector2 direction)
    {
        rb.linearVelocity = direction * characterSpeed;
    }
}
