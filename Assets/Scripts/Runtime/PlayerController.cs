using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;
    public Animator animator;

    private float characterSpeed = 1.0f;

    private float minY, maxY;

    private bool hitTopCameraBound = false;
    private bool hitBottomCameraBound = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponentInChildren<Animator>();

        minY = GameManager.Instance.minY;
        maxY = GameManager.Instance.maxY;    
    }

// Revisit this... can likely be optimized
    void FixedUpdate()
    {
        if (rb.position.y >= (maxY - boxCollider.bounds.size.y))
        {
            Debug.Log("Top?");
            hitTopCameraBound = true;
            hitBottomCameraBound = false;
        }
        else
        {
            hitTopCameraBound = false;
        }
        if (rb.position.y <= minY)
        {
            hitBottomCameraBound = true;
            hitTopCameraBound = false;
        }
        else
        {
            hitBottomCameraBound = false;
        }
    }

    public void MoveCharacter(bool isMoving, float direction, float magnitude)
    {
        animator.SetBool("1_Move", isMoving);
        if (isMoving && !hitBottomCameraBound && !hitTopCameraBound)
        {
            rb.linearVelocity = new Vector2(0f, -Mathf.Sign(direction)) * magnitude * characterSpeed;
            animator.speed = magnitude / 2;
        }
        else
        {
            if ((hitTopCameraBound && direction > 0) || (hitBottomCameraBound && direction < 0))
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
}
