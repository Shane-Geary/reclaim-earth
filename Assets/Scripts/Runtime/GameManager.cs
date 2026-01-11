using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public ProjectilePooler projectilePooler;

    public WeaponController weaponController;

    public Camera mainCamera;

    public float minX, maxX, minY, maxY;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        
        Instance = this;

        InitCameraBounds();
    }

    void InitCameraBounds()
    {
        float camHeight = 2f * mainCamera.orthographicSize;
        float camWidth = camHeight * mainCamera.aspect;

        minX = mainCamera.transform.position.x - camWidth / 2;
        maxX = mainCamera.transform.position.x + camWidth / 2;
        minY = mainCamera.transform.position.y - camHeight / 2;
        maxY = mainCamera.transform.position.y + camHeight / 2;
    }
}
