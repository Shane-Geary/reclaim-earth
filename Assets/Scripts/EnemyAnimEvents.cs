using Unity.VisualScripting;
using UnityEngine;

public class EnemyAnimEvents : MonoBehaviour
{

    //Barricade barricade;
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
            parentController.currentBarricadeSection?.TakeDamage(parentController.enemyDamage);
    }
}
