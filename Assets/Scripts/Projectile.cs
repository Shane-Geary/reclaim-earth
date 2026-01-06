using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Projectile : MonoBehaviour
{
    private Rigidbody2D rb;
    public GameObject projectilePooler;

    public EnemyController enemyController;

    public float projectileDamage;
    public int projectileSpeed;

    private string targetName;

    [SerializeField] private float defaultSpeed = 15f;

    private readonly Dictionary<string, float> cameraBounds = new();
    private float camHeight;
    private float camWidth;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        projectilePooler = GameObject.Find("InfiniteAmmoClip");

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

    private void OnEnable() 
    {
        LaunchProjectile();
    }

    void FixedUpdate()
    {
        if (transform.position.x < cameraBounds["minX"] || transform.position.x > cameraBounds["maxX"] ||
            transform.position.y < cameraBounds["minY"] || transform.position.y > cameraBounds["maxY"])
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
