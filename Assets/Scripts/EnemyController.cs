using UnityEngine;

/** TODO - The majority of this is a placeholder/blueprint for the enemy controller.
will need to make adjustments with the final sprite assets and animations **/

public class EnemyController : MonoBehaviour
{
	Rigidbody2D rigidbody2d;
	//Projectile projectile;
	EnemySpawner enemySpawner;

	public float enemySpeed;
	public float enemyHealth;
	public float enemyDamage;

	public ParticleSystem impactEffect;
	public float destroyDelay = 0.5f; // Delay to allow particle effect to finish

	//private SpriteRenderer spriteRenderer;
	//private float hitFlashTimer;
	//private Color defaultColor;
	private Transform player;
	private Vector2 movement;
	private bool playerContact = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
	{
		rigidbody2d = GetComponent<Rigidbody2D>();
		//spriteRenderer = GetComponent<SpriteRenderer>();

		enemySpawner = FindFirstObjectByType<EnemySpawner>();
		player = GameObject.FindGameObjectWithTag("Player").transform;

		//defaultColor = spriteRenderer.color;

		InitEnemy();
	}
    void InitEnemy()
    {
        //hitFlashTimer = 0;
        //spriteRenderer.color = defaultColor;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        if (gameObject.CompareTag("EnemyGrunt"))
        {
            {
                enemySpeed = 0.5f;
                enemyHealth = 1.0f;
                enemyDamage = 0.15f;
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
        if (player != null)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            Vector2 newPosition = rigidbody2d.position + enemySpeed * Time.fixedDeltaTime * direction;
            if (!playerContact)
			{
                rigidbody2d.MovePosition(newPosition);
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
			//Debug.Log("Enemy health: " + enemyHealth);
			if (enemyHealth <= 0)
			{
				rigidbody2d.linearVelocity = Vector2.zero;
				enemySpawner.ReturnToPool(gameObject);
				InitEnemy();
			}
		}
		else if (objectTagName == "Player")
		{
			playerContact = true;
        }
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			playerContact = false;
		}
    }

    void PlayHitEffect(Vector2 hitPosition)
	{
		//Debug.Log("hitPosition: " + hitPosition);
		if (impactEffect != null)
		{
			ParticleSystem bloodEffect = Instantiate(impactEffect, hitPosition, Quaternion.identity);
			bloodEffect.Play();
			Destroy(bloodEffect.gameObject, destroyDelay);
		}
	}
}
