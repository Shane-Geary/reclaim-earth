using System.Collections;
using UnityEngine;

public class Barricade : MonoBehaviour
{
    public Canvas healthBarCanvas;
    public float maxHealth = 1.0f;
    public float currentHealth;

    float flashTimer;
    readonly float flashDuration = 0.3f;

    SpriteRenderer spriteRenderer;
    Color originalColor;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        healthBarCanvas = GetComponent<Canvas>();
        originalColor = spriteRenderer.color;
        currentHealth = maxHealth; // Initialize current health to max health
    }

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

        healthBarCanvas.transform.position = new Vector3(gameObject.transform.position.x - 5, gameObject.transform.position.y);

        // isHit = true; // Set the hit flag to true
        //if (health <= 0)
        //{
        //    Destroy(gameObject);
        //}
    }
}
