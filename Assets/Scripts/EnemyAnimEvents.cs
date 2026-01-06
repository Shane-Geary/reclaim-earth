using Unity.VisualScripting;
using UnityEngine;

public class EnemyAnimEvents : MonoBehaviour
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

    public void EnemyDamagedEvent(float damage)
    {
        parentController.TakeDamageFromProjectile(damage);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("EnemyAnimEvents detected collision with " + other.gameObject.name);
        if (other.gameObject.CompareTag("Projectile")) {
            parentController.TakeDamageFromProjectile(other.GetComponent<Projectile>().projectileDamage);
        }
    }
}
