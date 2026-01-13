using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using ETouch = UnityEngine.InputSystem.EnhancedTouch;

public class ControlButton : MonoBehaviour
{
    [SerializeField] private RectTransform rectTransform;

    private Finger MovementFinger;
    private Vector2 MovementAmount;

    private WeaponController weaponController;

    private bool isFingerDown = false;

    private void OnEnable()
    {
        EnhancedTouchSupport.Enable();
        ETouch.Touch.onFingerDown += OnTouchFingerDown;
        ETouch.Touch.onFingerUp += OnTouchFingerUp;
        ETouch.Touch.onFingerMove += OnTouchFingerMove;
    }

    void Start()
    {
        weaponController = GameManager.Instance.weaponController;
    }

    void Update()
    {
        if (Time.time >= weaponController.nextFireTime && isFingerDown)
        {
            weaponController.FireWeapon();
        }
    }

    private void OnTouchFingerDown(Finger TouchedFinger)
    {
        Debug.Log("Touch Finger Down");
        isFingerDown = true;

        MovementFinger = TouchedFinger;
        MovementAmount = Vector2.zero;
    }

    private void OnTouchFingerUp(Finger obj)
    {
        isFingerDown = false;
    }

    private void OnTouchFingerMove(Finger obj)
    {
        
    }

    private void OnDisable()
    {
        ETouch.Touch.onFingerDown -= OnTouchFingerDown;
        ETouch.Touch.onFingerUp -= OnTouchFingerUp;
        ETouch.Touch.onFingerMove -= OnTouchFingerMove;
        EnhancedTouchSupport.Disable();
    }
}
