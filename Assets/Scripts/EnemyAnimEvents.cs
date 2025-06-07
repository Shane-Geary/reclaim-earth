using UnityEngine;

public class EnemyAnimEvents : MonoBehaviour
{
    EnemyController parentController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        parentController = GetComponentInParent<EnemyController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void EnemyAttackEvent()
    {
        parentController.OnAttackPlayer();
    }
}
