using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;
    public Animator animator;

    private float characterSpeed = 1.0f;

    private float minY, maxY;

    private bool hitEdgeOfScreen;

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
        // if (transform.position.x < minX || transform.position.x > maxX ||
        //     transform.position.y < minY || transform.position.y > maxY)
        // {
        //     Debug.Log("Edge of Screen");
        // }
        if (rb.position.y <= minY || rb.position.y >= (maxY- boxCollider.bounds.size.y))
        {
            Debug.Log("Edge of Screen");
            hitEdgeOfScreen = true;
        }
        else
        {
            hitEdgeOfScreen = false;
        }
    }

    public void MoveCharacter(bool isMoving, float direction, float magnitude)
    {
        animator.SetBool("1_Move", isMoving);
        if (isMoving && !hitEdgeOfScreen)
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
