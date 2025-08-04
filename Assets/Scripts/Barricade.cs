using System.Collections;
using UnityEngine;

public class Barricade : MonoBehaviour
{

    public float health; // Health of the barricade
    public bool isHit = false; // Flag to check if the barricade is hit

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (isHit)
        {
            StartCoroutine(HitFlash());
            isHit = false; // Reset the hit flag
        }
    }

    IEnumerator HitFlash()     
    {
        // Flash the barricade when hit
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        Color originalColor = spriteRenderer.color;
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.3f);
        spriteRenderer.color = originalColor;
    }
}
