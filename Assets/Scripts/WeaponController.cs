using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class WeaponController : MonoBehaviour
{

    Animator animator;
    Rigidbody2D rb;

    // public InputAction FireAction;
    private bool fireHeld;
    private float nextFireTime;
    public ProjectilePooler projectilePooler;

    private float fireRate = 0.2f; // seconds between shots
    //private float timer;
    // private float fireRateCooldown;
    public int projectileSpeed = 250;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Current time: " + Time.time);
        Debug.Log("Next fire time: " + nextFireTime);
        if (fireHeld && Time.time >= nextFireTime)
        {
            TryFireInstant();
        }
    }

    public void OnFirePressed()
    {
        fireHeld = true;
        
        if (Time.time >= nextFireTime)
        {
            TryFireInstant();
        }
    }

    public void OnFireReleased()
    {
        fireHeld = false;
    }

    private void TryFireInstant()
    {
        animator.SetTrigger("Fire");
        // Set next allowed fire time
        nextFireTime = Time.time + fireRate;
    }

    // private void HandleFiring()
    // {
    //     if (fireHeld && Time.time >= nextFireTime)
    //     {
    //         animator.SetTrigger("Fire");
    //         nextFireTime = Time.time + fireRate;
    //     }
    // }

    public void SpawnProjectile()
    {

        Vector3 spawnPosition = rb.position + Vector2.right * 0.1f;
        GameObject projectileObj = projectilePooler.GetFromPool(spawnPosition);

        Projectile projectile = projectileObj.GetComponent<Projectile>();
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
