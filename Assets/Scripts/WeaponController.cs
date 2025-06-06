using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class WeaponController : MonoBehaviour
{

    Animator animator;
    Rigidbody2D rb;

    public InputAction FireAction;
    public ProjectilePooler projectilePooler;

    //private float timer;
    private float fireRateCooldown;
    public int projectileSpeed = 250;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        FireAction.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        if (FireAction.IsPressed())
        {
            fireRateCooldown -= Time.deltaTime;
            if (fireRateCooldown <= 0.0f)
            {
                animator.SetTrigger("Fire");
                fireRateCooldown = 0.5f; // Reset cooldown
            }
        }
    }

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
