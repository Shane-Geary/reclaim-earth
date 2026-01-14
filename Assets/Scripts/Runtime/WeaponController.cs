using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class WeaponController : MonoBehaviour
{
    Animator animator;

    private ProjectilePooler projectilePooler;

    [SerializeField] private Transform projectileSpawnPoint;

    private readonly float fireRate = 1.0f; // Time between shots in seconds
    public float nextFireTime = 0f;

    void Start()
    {
        animator = GetComponent<Animator>();
        projectilePooler = GameManager.Instance.projectilePooler;
    }

    public void FireWeapon()
    {
        nextFireTime = Time.time + fireRate;
        animator.SetTrigger("Fire");
        projectilePooler.GetFromPool(projectileSpawnPoint.position);
    }
}
