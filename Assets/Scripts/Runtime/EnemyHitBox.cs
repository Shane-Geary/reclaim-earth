using UnityEngine;

public class EnemyHitBox : MonoBehaviour
{
    EnemyController parentController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        parentController = GetComponentInParent<EnemyController>();

    }

    public void EnemyAttackEvent()
    {
            parentController.currentBarricadeSection?.TakeDamage(parentController.enemyDamage);
    }

    public void TakeDamageFromProjectile(float damage)
	{
		parentController.TakeDamage(damage);
    }
}
