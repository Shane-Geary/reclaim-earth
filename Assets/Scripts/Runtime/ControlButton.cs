using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using ETouch = UnityEngine.InputSystem.EnhancedTouch;

public class ControlButton : MonoBehaviour
{
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private RectTransform containerRectTransform;

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
        if (MovementFinger != null) return;

        MovementFinger = TouchedFinger;
        MovementAmount = Vector2.zero;

        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, MovementFinger.screenPosition, null, out localPoint);

        float radius = rectTransform.rect.width * 0.5f;
        bool isTouchInsideThis = localPoint.magnitude <= radius;
        
        if (isTouchInsideThis)
        {
            isFingerDown = true;
        }
    }

    private void OnTouchFingerMove(Finger MovedFinger)
    {
        if (MovedFinger == MovementFinger)
        {
            // Vector2 knobPosition;
            Debug.Log("MovedFinger: " + MovedFinger.currentTouch);
        }
    }

    private void OnTouchFingerUp(Finger obj)
    {
        MovementFinger = null;
        isFingerDown = false;
    }

    private void OnDisable()
    {
        ETouch.Touch.onFingerDown -= OnTouchFingerDown;
        ETouch.Touch.onFingerUp -= OnTouchFingerUp;
        ETouch.Touch.onFingerMove -= OnTouchFingerMove;
        EnhancedTouchSupport.Disable();
    }
}
