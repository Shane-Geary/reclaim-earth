using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Projectile : MonoBehaviour
{
    private Rigidbody2D rb;
    private ProjectilePooler projectilePooler;

    // public EnemyController enemyController;

    public float projectileDamage;

    private string targetName;

    private readonly float defaultSpeed = 4.0f;

    private float minX, maxX, minY, maxY;

    private List<EnemyController> enemiesHit = new();

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
    void Update()
    {
        if (enemiesHit.Count > 0)
        {
            Debug.Log("Added sorting group:" + enemiesHit.Count);

        }
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
        EnemyController enemy = collision.gameObject.GetComponent<EnemyController>();

        SortingGroup yAxisSort = collision.gameObject.GetComponentInChildren<SortingGroup>();
        Debug.Log("Collided with: " + yAxisSort.sortingOrder);
        if (enemy != null && !enemiesHit.Contains(enemy))
        {
            // enemy.TakeDamageFromProjectile(projectileDamage);
            enemiesHit.Add(enemy);
        }

        // enemy?.TakeDamageFromProjectile(projectileDamage);
        ResetProjectile();
    }

    private void ResetProjectile()
    {
        rb.linearVelocity = Vector2.zero; // Stop movement
        rb.angularVelocity = 0f;
        projectilePooler.GetComponent<ProjectilePooler>().ReturnToPool(gameObject);
    }
}
