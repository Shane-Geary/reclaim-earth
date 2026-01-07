using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public InputAction MoveAction;
    Rigidbody2D rb;
    EnemyController enemyController;

    Animator animator;

    public float characterSpeed = 3.0f;
    public float characterHealth;

    private bool isInvisible = false;
    private float invisibleDuration = 2.0f; // Duration of invisibility after being hit

    Vector2 move;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MoveAction.Enable();
        rb = GetComponent<Rigidbody2D>();
        enemyController = FindFirstObjectByType<EnemyController>();

        animator = GetComponentInChildren<Animator>();
        
        characterHealth = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        move = MoveAction.ReadValue<Vector2>(); // Value of the move action

        // Animation updates
        animator.SetBool("1_Move", move.y != 0);

        if (isInvisible)
        {
            invisibleDuration -= Time.deltaTime;
            if (invisibleDuration <= 0)
            {
                isInvisible = false;
                invisibleDuration = 2.0f; // Reset the duration for next hit
            }
        }
    }

    void FixedUpdate()
    {
        Vector2 position = (Vector2)rb.position + characterSpeed * Time.deltaTime * move;
        rb.MovePosition(position);
    }
}
