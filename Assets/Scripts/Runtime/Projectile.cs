using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Projectile : MonoBehaviour
{
    private Rigidbody2D rb;
    private ProjectilePooler projectilePooler;

    public float projectileDamage;

    private string targetName;

    private readonly float defaultSpeed = 4.0f;

    private float minX, maxX, minY, maxY;

    private bool hasHit = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        projectilePooler = GameManager.Instance.projectilePooler;
    }

    void Start()
    {
        targetName = gameObject.name;
        if (targetName == "ProjectileLaserGun(Clone)")
        {
            projectileDamage = 0.1f;
        }

        minX = GameManager.Instance.minX;
        maxX = GameManager.Instance.maxX;
        minY = GameManager.Instance.minY;
        maxY = GameManager.Instance.maxY;
    }

    private void OnEnable() 
    {
        LaunchProjectile();
    }

    void FixedUpdate()
    {
        if (transform.position.x < minX || transform.position.x > maxX ||
            transform.position.y < minY || transform.position.y > maxY)
        {
            ResetProjectile();
        }
    }

    public void LaunchProjectile()
    {
        rb.linearVelocity = Vector2.zero; // Reset any existing velocity
        rb.angularVelocity = 0f;

        rb.linearVelocity = transform.right * defaultSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (hasHit) return; // Prevent multiple hits
        EnemyController enemy = collision.gameObject.GetComponent<EnemyController>();
        if (enemy == null) return;

        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, 0.2f);
        EnemyController targetEnemy = enemy;
        float minY = enemy.transform.position.y;
        foreach (Collider2D colliderHit in hitColliders)
        {
            EnemyController hitEnemy = colliderHit.GetComponent<EnemyController>();
            if (hitEnemy != null)
            {
                float y = hitEnemy.transform.position.y;
                if (y < minY)
                {
                    minY = y;
                    targetEnemy = hitEnemy;
                }
            }
        }
        // Damage the enemy with the lowest Y position (visually aligned hit on y-axis)
        targetEnemy?.TakeDamageFromProjectile(projectileDamage);
        hasHit = true;
        ResetProjectile();
    }

    private void ResetProjectile()
    {
        rb.linearVelocity = Vector2.zero; // Stop movement
        rb.angularVelocity = 0f;
        hasHit = false;
        projectilePooler.GetComponent<ProjectilePooler>().ReturnToPool(gameObject);
    }
}
