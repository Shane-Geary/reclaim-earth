using UnityEngine;
using UnityEngine.UIElements;

public class BarricadeHealthUI : MonoBehaviour
{
    private UnityEngine.UIElements.UIDocument uiDocument;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnEnable()
    {
        uiDocument = GetComponent<UIDocument>();
        // healthBar = uiDocument.rootVisualElement.Q<ProgressBar>("HealthBar");

    }
}
