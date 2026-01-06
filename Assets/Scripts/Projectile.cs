using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Projectile : MonoBehaviour
{
    private Rigidbody2D rb;
    private ProjectilePooler projectilePooler;

    public EnemyController enemyController;

    public float projectileDamage;
    public int projectileSpeed;

    private string targetName;

    [SerializeField] private float defaultSpeed = 2f;

    private float minX, maxX, minY, maxY;

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
        EnemyController enemy = collision.gameObject.GetComponent<EnemyController>();

        // Debug.Log("Enemy hit: " + enemy);
        enemy?.TakeDamageFromProjectile(projectileDamage);
        ResetProjectile();
    }

    private void ResetProjectile()
    {
        rb.linearVelocity = Vector2.zero; // Stop movement
        rb.angularVelocity = 0f;
        projectilePooler.GetComponent<ProjectilePooler>().ReturnToPool(gameObject);
    }
}
