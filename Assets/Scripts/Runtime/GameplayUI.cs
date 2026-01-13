using System;
using UnityEngine;
using UnityEngine.UIElements;


public class GameplayUI : MonoBehaviour
{
    private UIDocument uiDocument;
    private VisualElement playerControlButton;

    private WeaponController weaponController;
    private PlayerController playerController;

    private bool isControlButtonPressed = false;
    // private bool isControlButtonMoved = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        uiDocument = GetComponent<UIDocument>();
        weaponController = GameManager.Instance.weaponController;
        playerController = GameManager.Instance.playerController;

        playerControlButton = uiDocument.rootVisualElement.Q<VisualElement>("PlayerControlButton");

        PointerDownEvent pointerDownEvent = new();
        PointerUpEvent pointerUpEvent = new();
        playerControlButton.RegisterCallback<PointerDownEvent>(ev => isControlButtonPressed = true);
        playerControlButton.RegisterCallback<PointerUpEvent>(ev =>
        {
            isControlButtonPressed = false;
            // if (isControlButtonMoved)
            // {
            //     isControlButtonMoved = false;
            //     // playerController.MoveCharacter(Vector2.zero);
            // }
        });

        // playerControlButton.RegisterCallback<PointerMoveEvent>(ev => 
        // {
        //     isControlButtonMoved = true;
        //     MoveControlButton(ev.position);
        // });
    }

    void Update()
    {
        // Weapon firing logic
        if (Time.time >= weaponController.nextFireTime && isControlButtonPressed)
        {
            weaponController.FireWeapon();
        }
    }

    private void MoveControlButton(Vector3 position)
    {

    }

    void OnDestroy()
    {
        playerControlButton.UnregisterCallback<PointerDownEvent>(ev => isControlButtonPressed = true);
        playerControlButton.UnregisterCallback<PointerUpEvent>(ev => isControlButtonPressed = false);
    }
}
