using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class WeaponController : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rb;

    private ProjectilePooler projectilePooler;

    [SerializeField] private Transform projectileSpawnPoint;

    private readonly float fireRate = 1.0f; // Time between shots in seconds
    public float nextFireTime = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        projectilePooler = GameManager.Instance.projectilePooler;
    }

    public void FireWeapon()
    {
        Debug.Log("FireWeapon called.");
        nextFireTime = Time.time + fireRate;
        animator.Play("Fire", 0, 0f);
        projectilePooler.GetFromPool(projectileSpawnPoint.position);
    }
}
