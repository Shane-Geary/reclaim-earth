using UnityEngine;

public class BarricadeHealthUI : MonoBehaviour
{

    public GameObject barricade;

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
        healthBar = uiDocument.rootVisualElement.Q<ProgressBar>("HealthBar");


    }
}
