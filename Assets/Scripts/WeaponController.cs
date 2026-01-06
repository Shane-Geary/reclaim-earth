using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class WeaponController : MonoBehaviour
{

    Animator animator;
    Rigidbody2D rb;

    public GameObject projectilePooler;

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
            projectilePooler = GameObject.Find("InfiniteAmmoClip");
            Debug.Log("Pooler: " + projectilePooler);
            animator.Play("Fire", 0, 0f);
            projectilePooler.GetComponent<ProjectilePooler>().GetFromPool(projectileSpawnPoint.position);
        }
    }
}
