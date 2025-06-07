using UnityEngine;
using UnityEngine.Rendering;

public class YAxisSort : MonoBehaviour
{
    private SortingGroup sortingGroup;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sortingGroup = GetComponent<SortingGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        sortingGroup.sortingOrder = Mathf.RoundToInt(transform.position.y * -100f);
    }
}
