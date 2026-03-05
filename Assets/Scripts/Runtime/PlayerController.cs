using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    public Animator animator;

    private float characterSpeed = 1.0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();        
    }

    public void MoveCharacter(bool isMoving, float direction, float magnitude)
    {
        animator.SetBool("1_Move", isMoving);
        if (isMoving)
        {
            rb.linearVelocity = new Vector2(0f, -Mathf.Sign(direction)) * magnitude * characterSpeed;
            animator.speed = magnitude / 2;
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }
    }
}
