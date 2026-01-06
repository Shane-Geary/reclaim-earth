using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Projectile : MonoBehaviour
{
    private Rigidbody2D rb;
    public ProjectilePooler projectilePooler;

    public EnemyController enemyController;

    public float projectileDamage;
    public int projectileSpeed;

    private string targetName;

    [SerializeField] private float defaultSpeed = 15f;

    private Dictionary<string, float> cameraBounds = new();
    private float camHeight;
    private float camWidth;

    void Awake()
    {
        targetName = gameObject.name;

        if (targetName == "ProjectileLaserGun(Clone)")
        {
            projectileDamage = 0.1f;
        }

        Camera cam = Camera.main;
        camHeight = 2f * cam.orthographicSize;
        camWidth = camHeight * cam.aspect;
        cameraBounds["minX"] = cam.transform.position.x - camWidth / 2;
        cameraBounds["maxX"] = cam.transform.position.x + camWidth / 2;
        cameraBounds["minY"] = cam.transform.position.y - camHeight / 2;
        cameraBounds["maxY"] = cam.transform.position.y + camHeight / 2;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        projectilePooler = FindFirstObjectByType<ProjectilePooler>();
        LaunchProjectile();
    }

    void FixedUpdate()
    {
        // TODO: Why did I think recalculating camera bounds every frame was a good idea?
        if (transform.position.x < cameraBounds["minX"] || transform.position.x > cameraBounds["maxX"] ||
            transform.position.y < cameraBounds["minY"] || transform.position.y > cameraBounds["maxY"])
        {
            Debug.Log("Projectile out of bounds");
            ResetProjectile();
        }

        // Camera cam = Camera.main;
        // float camHeight = 2f * cam.orthographicSize;
        // float camWidth = camHeight * cam.aspect;

        // float minX = cam.transform.position.x - camWidth / 2;
        // float maxX = cam.transform.position.x + camWidth / 2;
        // float minY = cam.transform.position.y - camHeight / 2;
        // float maxY = cam.transform.position.y + camHeight / 2;

        // if (transform.position.x < minX || transform.position.x > maxX ||
        //     transform.position.y < minY || transform.position.y > maxY)
        // {
        //     ResetProjectile();
        //     projectilePooler.ReturnToPool(gameObject);
        // }
    }

    public void LaunchProjectile()
    {
        rb.linearVelocity = transform.right * defaultSpeed;
    }

    // public void Launch(Vector2 direction, float force, ProjectilePooler controller)
    // {
    //     rb.angularVelocity = 0f;
    //     rb.angularVelocity = 0f;

    //     rb.linearVelocity = direction * force;
    //     projectilePooler = controller;
    // }

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
        projectilePooler.ReturnToPool(gameObject);
    }
}
