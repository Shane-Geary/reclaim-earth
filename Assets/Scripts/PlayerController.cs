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
                Debug.Log("Hero is now visible again.");
            }
        }
    }

    void FixedUpdate()
    {
        Vector2 position = (Vector2)rb.position + characterSpeed * Time.deltaTime * move;
        rb.MovePosition(position);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collision with: " + other.gameObject.name);
        if (other.gameObject.name.StartsWith("Enemy"))
        {
            enemyController = other.GetComponent<EnemyController>();

            if (!isInvisible)
            {
                OnDamageFromEnemy();
            }
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.name.StartsWith("Enemy"))
        {
            if (!isInvisible)
            {
                OnDamageFromEnemy();
            }
        }
    }

    void OnDamageFromEnemy()
    {
        isInvisible = true;
        characterHealth -= enemyController.enemyDamage;
        Debug.Log("Hero Hit! Health: " + characterHealth);

        if (characterHealth > 0)
        {
            animator.SetTrigger("3_Damaged");
        }
        else
        {
            Debug.Log("Hero is Dead!");
            //Destroy(gameObject);
        }
    }
}
