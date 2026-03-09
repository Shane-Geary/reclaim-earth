using System.Collections;
using UnityEngine;

public class Barricade : MonoBehaviour
{
    public GameObject healthBarPrefab;
    private GameObject healthBar;
    public float maxHealth = 1.0f;
    public float currentHealth;

    float flashTimer;
    readonly float flashDuration = 0.3f;

    SpriteRenderer spriteRenderer;
    Color originalColor;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        currentHealth = maxHealth; // Initialize current health to max health

        healthBar = Instantiate(healthBarPrefab);
        healthBar.SetActive(false);
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

        healthBar.SetActive(true);
        healthBar.transform.position = new Vector3(gameObject.transform.position.x - (float)0.2, gameObject.transform.position.y);
        // isHit = true; // Set the hit flag to true
        //if (health <= 0)
        //{
        //    Destroy(gameObject);
        //}
    }
}
