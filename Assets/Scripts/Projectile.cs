using UnityEngine;
using UnityEngine.InputSystem;

public class Projectile : MonoBehaviour
{
    Rigidbody2D rb;
    ProjectilePooler projectilePooller;

    public EnemyController enemyController;

    public float projectileDamage;

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

        rb.linearVelocity = direction * force;
        projectilePooller = controller;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        EnemyController enemy = collision.gameObject.GetComponent<EnemyController>();

        // Debug.Log("Enemy hit: " + enemy);
        enemy?.TakeDamageFromProjectile(projectileDamage);
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
    }
}
