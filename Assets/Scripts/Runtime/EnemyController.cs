using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
	[SerializeField] private Rigidbody2D rigidbody2d;
	[SerializeField] private Collider2D collider2D;
	EnemySpawner enemySpawner;
	Animator animator;
	public Barricade currentBarricadeSection;
	[SerializeField] private LayerMask barricadeLayerMask;
	private float enemyRadius;

	private SpriteRenderer[] spriteRenderers;
	private Dictionary<SpriteRenderer, Color> originalColors;

	public float enemySpeed;
	public float enemyHealth;
	public float enemyDamage;

	float attackTimer;
	private readonly float attackCooldown = 1.0f;

	float flashTimer;
    private readonly float flashDuration = 0.3f;

	[SerializeField] private Transform playerPosition;

    void Start()
	{
		animator = GetComponentInChildren<Animator>();

		enemySpawner = FindFirstObjectByType<EnemySpawner>();
		playerPosition = GameObject.FindGameObjectWithTag("Player").transform;

		InitEnemy();
	}
	void Awake()
	{
		enemyRadius = collider2D.bounds.extents.x;
		spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
		originalColors = new Dictionary<SpriteRenderer, Color>();
		foreach (SpriteRenderer sr in spriteRenderers)
		{
			if (sr.gameObject.name != "Shadow")
			{
				originalColors[sr] = sr.color;
			}
		}
	}
    void InitEnemy()
    {
		flashTimer = 0f;
		attackTimer = 0f;
		foreach (SpriteRenderer sr in spriteRenderers)
		{
			if (sr.gameObject.name != "Shadow")
			{
				sr.color = originalColors[sr];
			}
		}
        if (gameObject.CompareTag("EnemyGrunt"))
        {
            {
                enemySpeed = 0.5f;
                enemyHealth = 1.0f;
                enemyDamage = 0.05f;
            }
        }
    }

    void Update()
	{
		if (attackTimer > 0)
		{
			attackTimer -= Time.deltaTime;
			if (attackTimer <= 0)
			{
				OnAttackBarricade();
				attackTimer = attackCooldown;
			}
		}
		if (flashTimer > 0)
        {
            flashTimer -= Time.deltaTime;
            if (flashTimer <= 0)
            {
                foreach (SpriteRenderer sr in spriteRenderers)
				{
					if (sr.gameObject.name != "Shadow")
					{
						sr.color = originalColors[sr];
					}
				}
            }
        }
	}

	void FixedUpdate()
	{
		if (!playerPosition) return;

		Vector2 currentPosition = transform.position;
		Vector2 castOrigin = collider2D.bounds.center;
		Vector2 playerPosVector2 = new(playerPosition.position.x, playerPosition.position.y);
		Vector2 direction = (playerPosVector2 - currentPosition).normalized;

		float moveDistance = enemySpeed * Time.fixedDeltaTime;
		float castDistance = enemyRadius / 5;

		// Debug.Log("RayCircle: " + rayDistance);
		RaycastHit2D barricadeCollision = Physics2D.CircleCast(castOrigin, enemyRadius, direction, castDistance, barricadeLayerMask);

		Debug.DrawRay(castOrigin, direction * (castDistance + enemyRadius), Color.red);
		
		if (barricadeCollision)
		{
			if (!currentBarricadeSection)
			{
				attackTimer = attackCooldown;
			}
			Debug.Log("Raycast2D: " + barricadeCollision.rigidbody.gameObject);
			animator.SetBool("1_Move", false);
			currentBarricadeSection = barricadeCollision.collider.GetComponent<Barricade>();

			// if (attackTimer > 0)
			// {
			// 	attackTimer -= Time.deltaTime;
			// 	if (attackTimer <= 0)
			// 	{
			// 		OnAttackBarricade();
			// 	}
			// }

			return;
			}

		Vector2 nextPosition = currentPosition + direction * moveDistance;
		rigidbody2d.MovePosition(nextPosition);
		animator.SetBool("1_Move", true);

		// if (currentBarricadeSection)
		// {
		// 	rigidbody2d.linearVelocity = Vector2.zero;
		// 	animator.SetBool("1_Move", false);
		// 	return;
		// }
		// Vector2 direction = (playerPosition.position - transform.position).normalized;
		// rigidbody2d.linearVelocity = direction * enemySpeed;
		// animator.SetBool("1_Move", true);
	}

    // private void OnCollisionEnter2D(Collision2D collision)
    // {
    //     // Debug.Log("EnemyController detected collision with " + collision.gameObject.name);
	// 	if (collision.gameObject.CompareTag("Barricade"))
	// 	{
	// 		rigidbody2d.linearVelocity = Vector2.zero;
	// 		animator.SetBool("1_Move", false);
	// 		currentBarricadeSection = collision.gameObject.GetComponent<Barricade>();
	// 		OnAttackBarricade();
	// 	}
    // }

	// private void OnCollisionExit2D(Collision2D collision)
	// {
	// 	if (collision.gameObject.CompareTag("Barricade"))
	// 	{
	// 		currentBarricadeSection = null;
	// 	}
	// }

	public void OnAttackBarricade()
	{
		animator.SetTrigger("2_Attack");
	}

	public void TakeDamage(float damage)
	{
		foreach (SpriteRenderer sr in spriteRenderers)
		{
			if (sr.gameObject.name != "Shadow")
			{
				sr.color = Color.red;
			}
		}
        flashTimer = flashDuration;
		enemyHealth -= damage;
		if (enemyHealth <= 0)
		{
			rigidbody2d.linearVelocity = Vector2.zero;
			animator.ResetTrigger("2_Attack");
			enemySpawner.ReturnToPool(gameObject);
			InitEnemy();
		}
	}
}
