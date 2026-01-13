using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;

    public float characterSpeed = 0.5f;
    public float characterHealth;

    private ControlButton controlButton;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        controlButton = GameManager.Instance.controlButton;
        
        characterHealth = 1.0f;
    }

    public void MoveCharacter(bool isMoving, float direction, float magnitude)
    {
        Debug.Log(Mathf.Sign(direction));
        if (isMoving)
        {
            rb.linearVelocity = new Vector2(0f, Mathf.Sign(direction)) * magnitude * characterSpeed;
        }
        else
        {
            Debug.Log("Stop Movement");
            rb.linearVelocity = Vector2.zero;
        }
    }
}
