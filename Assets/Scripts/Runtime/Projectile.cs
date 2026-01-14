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
        hasHit = false;
        GetComponent<Collider2D>().enabled = true; // Re-enable collider
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

    void OnTriggerEnter2D(Collider2D other)
    {
        // Debug.Log("Projectile detected collision with " + other.gameObject.tag);
        if (hasHit) return; // Prevent multiple hits

        hasHit = true;
        GetComponent<Collider2D>().enabled = false; // Disable collider to prevent further hits
        if (other.gameObject.TryGetComponent(out EnemyHitBox enemy))
        {
            enemy.TakeDamageFromProjectile(projectileDamage);
        }
        
        ResetProjectile();
    }

    private void ResetProjectile()
    {
        rb.linearVelocity = Vector2.zero; // Stop movement
        rb.angularVelocity = 0f;
        projectilePooler.GetComponent<ProjectilePooler>().ReturnToPool(gameObject);
    }
}
