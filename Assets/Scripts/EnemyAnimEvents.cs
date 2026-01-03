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

    // public void OnTriggerEnter2D(Collider2D other)
    // {
    //     Debug.Log("Trigger detected with " + other.gameObject.name);
    // }

    public void EnemyAttackEvent()
    {
            parentController.currentBarricadeSection?.TakeDamage(parentController.enemyDamage);
    }

    // public void EnemyDamagedEvent(float damage)
    // {
    //     parentController.TakeDamageFromProjectile(damage);
    // }
    public void OnTriggerEnter2D(Collider2D other)
    {
        parentController.TakeDamageFromProjectile(other.GetComponent<Projectile>().projectileDamage);
    }
}
