using UnityEngine;
using UnityEngine.UIElements;


public class GameplayUI : MonoBehaviour
{
    private UIDocument uiDocument;
    private Button playerControlButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        uiDocument = GetComponent<UIDocument>();
        playerControlButton = uiDocument.rootVisualElement.Q<Button>("PlayerControlButton");
        playerControlButton.clicked += OnPlayerControlButtonClicked;
    }

    private void OnPlayerControlButtonClicked()
    {
        Debug.Log("Player Control Button Clicked!");
    }

    void OnDestroy()
    {
        playerControlButton.clicked -= OnPlayerControlButtonClicked;
    }
}
