using UnityEngine;

public class HeroAnimation : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of movement

    void Start()
    {
        // Initialization code
    }

    void Update()
    {
        // Get input from arrow keys or WASD
        float horizontal = Input.GetAxis("Horizontal"); // Left/Right or A/D
        float vertical = Input.GetAxis("Vertical");     // Up/Down or W/S

        // Create a movement vector
        Vector3 movement = new Vector3(horizontal, vertical, 0);

        // Move the player sprite
        transform.position += movement * moveSpeed * Time.deltaTime;
    }
}