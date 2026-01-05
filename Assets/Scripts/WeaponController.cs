using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class WeaponController : MonoBehaviour
{

    Animator animator;
    Rigidbody2D rb;

    private Projectile projectile;
    public ProjectilePooler projectilePooler;

    private float fireRate;
    private readonly float fireCooldown = 0.2f; // seconds between shots
    public int projectileSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (fireRate > 0)
        {
            fireRate -= Time.deltaTime;
            Debug.Log("Fire rate cooldown: " + fireRate);
        }
    }

    public void OnFireReleased()
    {
        fireRate = 0f;
    }

    public void FireWeapon()
    {
        if (fireRate <= 0)
        {
            Debug.Log("Firing weapon");
            animator.SetTrigger("Fire");
            fireRate = fireCooldown;
        }
    }

    public void SpawnProjectile()
    {
        Vector3 spawnPosition = rb.position + Vector2.right * 0.1f;
        GameObject projectileObj = projectilePooler.GetFromPool(spawnPosition);
        Debug.Log(projectileObj);

        projectile = projectileObj.GetComponent<Projectile>();
        if (projectile != null)
        {
            projectile.Launch(Vector2.right, projectileSpeed, projectilePooler);
        }
        else
        {
            Debug.Log("Projectile component is null");
        }
    }
}
