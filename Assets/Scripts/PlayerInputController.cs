using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    public WeaponController weaponController;

    public InputAction FireAction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        FireAction.Enable();    
    }

    // Update is called once per frame
    void Update()
    {
        if (FireAction.IsPressed())
        {
            // Debug.Log("Fire pressed");
            weaponController.FireWeapon();
        }
        if (FireAction.WasReleasedThisFrame())
        {
            // Debug.Log("Fire released");
            weaponController.OnFireReleased();
        }
    }
}
