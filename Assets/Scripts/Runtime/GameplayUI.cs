using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;


public class GameplayUI : MonoBehaviour
{
    private UIDocument uiDocument;
    private Button playerControlButton;

    [SerializeField] private WeaponController weaponController;

    public bool isControlButtonPressed = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        uiDocument = GetComponent<UIDocument>();
        playerControlButton = uiDocument.rootVisualElement.Q<Button>("PlayerControlButton");
        
        playerControlButton.RegisterCallback<PointerDownEvent>(ev => isControlButtonPressed = true);
        playerControlButton.RegisterCallback<PointerUpEvent>(ev => isControlButtonPressed = false);
    }

    void Update()
    {
        if (Time.time >= weaponController.nextFireTime)
        {
            weaponController.FireWeapon(isControlButtonPressed);
            if (Keyboard.current.spaceKey.isPressed) {
                weaponController.FireWeapon(true);
            }
        }
    }
}
