using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class WeaponController : MonoBehaviour
{

    Animator animator;
    Rigidbody2D rb;

    private float fireRate = 0f;
    private readonly float fireCooldown = 1f; // seconds between shots

    Projectile projectilePrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        projectilePrefab = GetComponent<Projectile>();
    }

    // Update is called once per frame
    void Update()
    {
        if (fireRate > 0)
        {
            fireRate -= Time.deltaTime;
            // Debug.Log("Fire rate cooldown: " + fireRate);
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

    public void ProjectileLaunchEvent()
    {
        projectilePrefab.SpawnProjectile();
    }
}
