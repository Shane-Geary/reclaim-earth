using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;


public class GameplayUI : MonoBehaviour
{
    private UIDocument uiDocument;
    private VisualElement playerControlButton;

    private WeaponController weaponController;

    public bool isControlButtonPressed = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        uiDocument = GetComponent<UIDocument>();
        weaponController = GameManager.Instance.weaponController;

        playerControlButton = uiDocument.rootVisualElement.Q<VisualElement>("PlayerControlButton");

        PointerDownEvent pointerDownEvent = new();
        PointerUpEvent pointerUpEvent = new();
        playerControlButton.RegisterCallback<PointerDownEvent>(ev => isControlButtonPressed = true);
        playerControlButton.RegisterCallback<PointerUpEvent>(ev => isControlButtonPressed = false);
    }

    void Update()
    {
        if (Time.time >= weaponController.nextFireTime && isControlButtonPressed)
        {
            weaponController.FireWeapon();
        }
    }

    void OnDestroy()
    {
        playerControlButton.UnregisterCallback<PointerDownEvent>(ev => isControlButtonPressed = true);
        playerControlButton.UnregisterCallback<PointerUpEvent>(ev => isControlButtonPressed = false);
    }
}
