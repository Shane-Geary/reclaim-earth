using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using ETouch = UnityEngine.InputSystem.EnhancedTouch;

public class ControlButton : MonoBehaviour
{
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private RectTransform containerRectTransform;

    private PlayerController playerController;

    private Finger MovementFinger;
    private Vector2 startLocalPoint;

    private WeaponController weaponController;

    private bool isFingerDown = false;
    private bool isFingerMoving = false;
    private float direction;
    private float magnitude;

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
        playerController = GameManager.Instance.playerController;
    }

    void Update()
    {
        if (Time.time >= weaponController.nextFireTime && isFingerDown)
        {
            weaponController.FireWeapon();
        }
    }

    void FixedUpdate()
    {
        if (isFingerMoving)
        {
            playerController.MoveCharacter(isFingerMoving, direction, magnitude);
        }
    }

    private void OnTouchFingerDown(Finger TouchedFinger)
    {
        if (MovementFinger != null) return;

        MovementFinger = TouchedFinger;

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
        float deadZone = 0.225f;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(containerRectTransform, MovedFinger.screenPosition, null, out currentLocalPoint);

        Vector2 delta = currentLocalPoint - startLocalPoint;

        // Clamp
        float radius = containerRectTransform.rect.width * 0.5f;
        float clampedX = Mathf.Clamp(delta.x, -radius / 1.5f, radius / 1.5f);

        // Move Control Button along x-axis, synced to touch movement
        float inputX = clampedX / radius;
        // rectTransform.anchoredPosition = new Vector2(clampedX, 0f);

        // Centered deadzone to stop movement. MoveCharacter is invoked when movement stops to reset linearVelocity
        if (Mathf.Abs(inputX) < deadZone)
        {
            direction = 0f;
            isFingerMoving = false;
            playerController.MoveCharacter(isFingerMoving, direction, magnitude);
        }
        else
        {
            direction = Mathf.Sign(inputX);
            magnitude = Mathf.Abs(inputX * 2);
            isFingerMoving = true;
        }
    }

    private void OnTouchFingerUp(Finger LostFinger)
    {
        if (LostFinger != MovementFinger) return;

        isFingerDown = false;
        isFingerMoving = false;
        direction = 0f;
        MovementFinger = null;
        // rectTransform.anchoredPosition = Vector2.zero;
        playerController.MoveCharacter(isFingerMoving, direction, magnitude);
    }

    private void OnDisable()
    {
        ETouch.Touch.onFingerDown -= OnTouchFingerDown;
        ETouch.Touch.onFingerUp -= OnTouchFingerUp;
        ETouch.Touch.onFingerMove -= OnTouchFingerMove;
        EnhancedTouchSupport.Disable();
    }
}
