using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class WeaponController : MonoBehaviour
{

    Animator animator;
    Rigidbody2D rb;

    public ProjectilePooler projectilePooler;

    [SerializeField] private Transform projectileSpawnPoint;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        FireWeapon();
    }

    public void FireWeapon()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            Debug.Log("Fire Weapon");
            animator.Play("Fire", 0, 0f);
            projectilePooler.GetFromPool(projectileSpawnPoint.position);
        }
    }

    // public void SpawnProjectile()
    // {
    //     Vector3 spawnPosition = rb.position + Vector2.right * 0.1f;
    //     GameObject projectileObj = projectilePooler.GetFromPool(spawnPosition);

        // projectile = projectileObj.GetComponent<Projectile>();
    //     if (projectile != null)
    //     {
    //         projectile.Launch(Vector2.right, projectileSpeed, projectilePooler);
    //     }
    //     else
    //     {
    //         Debug.Log("Projectile component is null");
    //     }
    // }
}
