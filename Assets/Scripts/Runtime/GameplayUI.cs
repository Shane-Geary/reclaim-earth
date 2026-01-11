using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;


public class GameplayUI : MonoBehaviour
{
    private UIDocument uiDocument;
    private VisualElement playerControlButton;

    [SerializeField] private WeaponController weaponController;

    public bool isControlButtonPressed = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        uiDocument = GetComponent<UIDocument>();
        playerControlButton = uiDocument.rootVisualElement.Q<VisualElement>("PlayerControlButton");

        MouseDownEvent mouseDownEvent = new()
        {
            target = playerControlButton
        };
        playerControlButton.RegisterCallback<MouseDownEvent>(ev => isControlButtonPressed = true);

        MouseUpEvent mouseUpEvent = new()
        {
            target = playerControlButton
        };
        playerControlButton.RegisterCallback<MouseUpEvent>(ev => isControlButtonPressed = false);
        
        // playerControlButton.RegisterCallback<PointerDownEvent>(ev => isControlButtonPressed = true);
        // playerControlButton.RegisterCallback<PointerUpEvent>(ev => isControlButtonPressed = false);
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
