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

    // Update is called once per frame
    // void Update()
    // {
    //     if (Time.time >= nextFireTime)
    //     {
    //         FireWeapon(true);
    //     }
    // }

    public void FireWeapon(bool isControlButtonPressed)
    {
        Debug.Log("FireWeapon called. isControlButtonPressed: " + isControlButtonPressed);
        if (isControlButtonPressed)
        {
            nextFireTime = Time.time + fireRate;
            animator.Play("Fire", 0, 0f);
            projectilePooler.GetFromPool(projectileSpawnPoint.position);
        }
        // if (!isControlButtonPressed)
        // {
        //     animator.Play("Idle", 0, 0f);
        // }
    }
}
