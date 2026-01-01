using System;
using System.Collections.Generic;
using UnityEngine;

/** TODO - The majority of this is a placeholder/blueprint for the enemy controller.
will need to make adjustments with the final sprite assets and animations **/

public class EnemyController : MonoBehaviour
{
	Rigidbody2D rigidbody2d;
	EnemySpawner enemySpawner;
	Animator animator;
	PlayerController playerController;
	public Barricade currentBarricadeSection;

	public float enemySpeed;
	public float enemyHealth;
	public float enemyDamage;

	public float destroyDelay = 0.5f; // Delay to allow particle effect to finish

	//private SpriteRenderer spriteRenderer;
	//private float hitFlashTimer;
	//private Color defaultColor;
	private Transform playerPosition;
	private Vector2 movement;

	private List<Barricade> overlappingBarricades = new();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
	{
		rigidbody2d = GetComponent<Rigidbody2D>();
		//spriteRenderer = GetComponent<SpriteRenderer>();
		animator = GetComponentInChildren<Animator>();
		playerController = FindFirstObjectByType<PlayerController>();

		enemySpawner = FindFirstObjectByType<EnemySpawner>();
		playerPosition = GameObject.FindGameObjectWithTag("Player").transform;

		//defaultColor = spriteRenderer.color;

		InitEnemy();
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
		//if (hitFlashTimer > 0)
		//{
		//	hitFlashTimer -= Time.deltaTime;
		//}
		//else if (hitFlashTimer <= 0)
		//{
		//	spriteRenderer.color = defaultColor;
		//}
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
                Debug.Log("Enemy Attack");
                animator.SetTrigger("2_Attack");
            }
		}
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		string objectTagName = other.gameObject.tag;
        if (objectTagName == "Projectile")
		{
			//hitFlashTimer = 0.5f;

			Projectile projectile = other.gameObject.GetComponent<Projectile>();
			//PlayHitEffect(projectile.projectileHitPosition);
			//spriteRenderer.color = Color.red;
			enemyHealth -= projectile.projectileDamage;
			if (enemyHealth <= 0)
			{
				rigidbody2d.linearVelocity = Vector2.zero;
				enemySpawner.ReturnToPool(gameObject);
				InitEnemy();
			}
		}
		else if (objectTagName == "Barricade")
		{
            currentBarricadeSection = other.GetComponent<Barricade>();
        }

    }

	private void OnTriggerExit2D(Collider2D other)
	{
		Debug.Log("OnTriggerExit2D: " + other.gameObject.name);
        if (other.gameObject.CompareTag("Barricade"))
		{
			currentBarricadeSection = null;
        }
    }

	//public void OnAttackPlayer()
	//{
	//	playerController.OnDamageFromEnemy();
	//}

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
