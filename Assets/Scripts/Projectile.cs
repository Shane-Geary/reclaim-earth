using UnityEngine;
using UnityEngine.InputSystem;

public class Projectile : MonoBehaviour
{
    Rigidbody2D rb;
    ProjectilePooler projectilePooller;

    public float projectileDamage;
    public Vector2 projectileHitPosition;

    private string targetName;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        targetName = gameObject.name;

        if (targetName == "ProjectileLaserGun(Clone)")
        {
            projectileDamage = 0.1f;
        }
    }

    void FixedUpdate()
    {
        Camera cam = Camera.main;
        float camHeight = 2f * cam.orthographicSize;
        float camWidth = camHeight * cam.aspect;

        float minX = cam.transform.position.x - camWidth / 2;
        float maxX = cam.transform.position.x + camWidth / 2;
        float minY = cam.transform.position.y - camHeight / 2;
        float maxY = cam.transform.position.y + camHeight / 2;

        if (transform.position.x < minX || transform.position.x > maxX ||
            transform.position.y < minY || transform.position.y > maxY)
        {
            ResetProjectile();
            projectilePooller.ReturnToPool(gameObject);
        }
    }

    public void Launch(Vector2 direction, float force, ProjectilePooler controller)
    {
        rb.angularVelocity = 0f;
        rb.angularVelocity = 0f;
        projectileHitPosition = Vector2.zero; // Reset hit position


        rb.AddForce(direction * force);
        projectilePooller = controller;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        EnemyController enemy = other.GetComponent<EnemyController>();
        if (enemy != null)
        {
            projectileHitPosition = enemy.transform.position;
            Debug.Log("Projectile hit position: " + projectileHitPosition);
        }
        ResetProjectile();
        if (projectilePooller != null)
        {
            projectilePooller.ReturnToPool(gameObject);
        }
    }

    private void ResetProjectile()
    {
        rb.linearVelocity = Vector2.zero; // Stop movement
        rb.angularVelocity = 0f;
        projectileHitPosition = Vector2.zero; // Clear hit position
    }
}
