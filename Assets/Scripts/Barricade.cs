using System.Collections;
using UnityEngine;

public class Barricade : MonoBehaviour
{

    public float maxHealth = 1.0f;
    public float currentHealth;
    // public bool isHit = false; // Flag to check if the barricade is hit
    float flashTimer;
    readonly float flashDuration = 0.3f;

    SpriteRenderer spriteRenderer;
    Color originalColor;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        currentHealth = maxHealth; // Initialize current health to max health
    }

    // Update is called once per frame
    void Update()
    {
        if (flashTimer > 0)
        {
            flashTimer -= Time.deltaTime;
            if (flashTimer <= 0)
            {
                spriteRenderer.color = originalColor; // Reset color after flash duration
            }
        }
    }

    public void TakeDamage(float enemyDamage)
    {
        spriteRenderer.color = Color.red;
        flashTimer = flashDuration;

        maxHealth -= enemyDamage; // Reduce health by the damage amount
        // isHit = true; // Set the hit flag to true
        //if (health <= 0)
        //{
        //    Destroy(gameObject);
        //}
    }
}
