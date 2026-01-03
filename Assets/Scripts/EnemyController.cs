using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
	Rigidbody2D rigidbody2d;
	EnemySpawner enemySpawner;
	Animator animator;
	PlayerController playerController;
	public Barricade currentBarricadeSection;

	private SpriteRenderer[] spriteRenderers;
	// private Color originalColor;
	private Dictionary<SpriteRenderer, Color> originalColors;

	public float enemySpeed;
	public float enemyHealth;
	public float enemyDamage;
	float attackTimer;
	readonly float attackCooldown = 1.0f;

	float flashTimer;
    readonly float flashDuration = 0.3f;

	// public float destroyDelay = 0.5f; // Delay to allow particle effect to finish

	//private SpriteRenderer spriteRenderer;
	//private float hitFlashTimer;
	//private Color defaultColor;
	private Transform playerPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
	{
		rigidbody2d = GetComponent<Rigidbody2D>();
		//spriteRenderer = GetComponent<SpriteRenderer>();
		animator = GetComponentInChildren<Animator>();
		playerController = FindFirstObjectByType<PlayerController>();

		enemySpawner = FindFirstObjectByType<EnemySpawner>();
		playerPosition = GameObject.FindGameObjectWithTag("Player").transform;

		InitEnemy();
	}
	void Awake()
	{
		spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
		originalColors = new Dictionary<SpriteRenderer, Color>();
		foreach (SpriteRenderer sr in spriteRenderers)
		{
			originalColors[sr] = sr.color;
		}
	}
    void InitEnemy()
    {
        //hitFlashTimer = 0;
        //spriteRenderer.color = defaultColor;
        if (gameObject.CompareTag("EnemyGrunt"))
        {
            {
                enemySpeed = 0.5f;
                enemyHealth = 1.0f;
                enemyDamage = 0.05f;
            }
        }
    }

    // Update is called once per frame
    void Update()
	{
		if (attackTimer > 0)
		{
			attackTimer -= Time.deltaTime;
			if (attackTimer <= 0)
			{
				OnAttackBarricade();
			}
		}

		if (flashTimer > 0)
        {
            flashTimer -= Time.deltaTime;
            if (flashTimer <= 0)
            {
                foreach (SpriteRenderer sr in spriteRenderers)
				{
					sr.color = originalColors[sr];
				}
            }
        }
	}

	void FixedUpdate()
	{
		if (playerPosition)
		{
			Vector2 direction = (playerPosition.position - transform.position).normalized;
			Vector2 newPosition = rigidbody2d.position + enemySpeed * Time.fixedDeltaTime * direction;
			if (!currentBarricadeSection)
			{
				rigidbody2d.MovePosition(newPosition);
				animator.SetBool("1_Move", true);
			}
			else
			{
				// Stop movement when barricade is present
				rigidbody2d.linearVelocity = Vector2.zero;
				animator.SetBool("1_Move", false);
            }
		}
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		string objectTagName = other.gameObject.tag;
        if (objectTagName == "Projectile")
		{
			//hitFlashTimer = 0.5f;

			// Projectile projectile = other.gameObject.GetComponent<Projectile>();
			//PlayHitEffect(projectile.projectileHitPosition);
			//spriteRenderer.color = Color.red;
			// enemyHealth -= projectile.projectileDamage;
			// if (enemyHealth <= 0)
			// {
			// 	rigidbody2d.linearVelocity = Vector2.zero;
			// 	enemySpawner.ReturnToPool(gameObject);
			// 	InitEnemy();
			// }
		}
		else if (objectTagName == "Barricade")
		{
            currentBarricadeSection = other.GetComponent<Barricade>();
			OnAttackBarricade();
        }
    }

	private void OnTriggerExit2D(Collider2D other)
	{
        if (other.gameObject.CompareTag("Barricade"))
		{
			currentBarricadeSection = null;
        }
    }

	public void OnAttackBarricade()
	{
			animator.SetTrigger("2_Attack");
			attackTimer = attackCooldown;
	}

	public void TakeDamageFromProjectile(float damage)
	{
		foreach (SpriteRenderer sr in spriteRenderers)
		{
			sr.color = Color.red;
		}
        flashTimer = flashDuration;
		enemyHealth -= damage;
		if (enemyHealth <= 0)
		{
			rigidbody2d.linearVelocity = Vector2.zero;
			enemySpawner.ReturnToPool(gameObject);
			InitEnemy();
		}
	}

 //   void PlayHitEffect(Vector2 hitPosition)
	//{
	//	//Debug.Log("hitPosition: " + hitPosition);
	//	if (impactEffect != null)
	//	{
	//		ParticleSystem bloodEffect = Instantiate(impactEffect, hitPosition, Quaternion.identity);
	//		bloodEffect.Play();
	//		Destroy(bloodEffect.gameObject, destroyDelay);
	//	}
	//}
}
