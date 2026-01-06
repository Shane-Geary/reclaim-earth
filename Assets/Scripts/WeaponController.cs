using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class WeaponController : MonoBehaviour
{

    Animator animator;
    Rigidbody2D rb;

    public ProjectilePooler projectilePooler;

    [SerializeField] private Transform projectileSpawnPoint;

    private readonly float fireRate = 0.5f; // Time between shots in seconds
    private float nextFireTime = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextFireTime)
        {
            FireWeapon();
        }
    }

    public void FireWeapon()
    {
        if (Keyboard.current.spaceKey.IsPressed())
        {
            nextFireTime = Time.time + fireRate;
            animator.Play("Fire", 0, 0f);
            projectilePooler.GetFromPool(projectileSpawnPoint.position);
        }
        if (!Keyboard.current.spaceKey.IsPressed())
        {
            animator.StopPlayback();
        }
    }
}
