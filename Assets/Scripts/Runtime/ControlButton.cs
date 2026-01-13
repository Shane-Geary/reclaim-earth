using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using ETouch = UnityEngine.InputSystem.EnhancedTouch;

public class ControlButton : MonoBehaviour
{
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private RectTransform containerRectTransform;

    private Finger MovementFinger;
    private Vector2 MovementAmount;
    private Vector2 startLocalPoint;

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

        RectTransformUtility.ScreenPointToLocalPointInRectangle(containerRectTransform, MovementFinger.screenPosition, null, out startLocalPoint);

        float radius = rectTransform.rect.width * 0.5f;
        bool isTouchInsideThis = startLocalPoint.magnitude <= radius;
        
        if (isTouchInsideThis)
        {
            isFingerDown = true;
        }
    }

    private void OnTouchFingerMove(Finger MovedFinger)
    {
        if (!isFingerDown || MovedFinger != MovementFinger) return;

        Vector2 currentLocalPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(containerRectTransform, MovedFinger.screenPosition, null, out currentLocalPoint);

        Vector2 delta = currentLocalPoint - startLocalPoint;

        // Clamp
        float radius = containerRectTransform.rect.width * 0.5f;
        float clampedX = Mathf.Clamp(delta.x, -radius, radius);
        // Debug.Log("ClampedDelta: " + clampedDelta);

        float inputX = clampedX / radius;
        Debug.Log("Input: " + inputX);
        rectTransform.anchoredPosition = new Vector2(clampedX, 0f);
    }

    private void OnTouchFingerUp(Finger LostFinger)
    {
        if (LostFinger != MovementFinger) return;

        isFingerDown = false;
        MovementFinger = null;
        rectTransform.anchoredPosition = Vector2.zero;
    }

    private void OnDisable()
    {
        ETouch.Touch.onFingerDown -= OnTouchFingerDown;
        ETouch.Touch.onFingerUp -= OnTouchFingerUp;
        ETouch.Touch.onFingerMove -= OnTouchFingerMove;
        EnhancedTouchSupport.Disable();
    }
}
