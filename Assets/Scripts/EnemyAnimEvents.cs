using Unity.VisualScripting;
using UnityEngine;

public class EnemyAnimEvents : MonoBehaviour
{

    Barricade barricade;
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
        if (barricade != null)
        {
            barricade.health -= parentController.enemyDamage;
            barricade.isHit = true;
            //Debug.Log("Barricade health: " + barricade.health);
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Barricade"))
        {
            barricade = collision.gameObject.GetComponent<Barricade>();

        }
        else
        {
            Debug.Log("Collision with: " + collision.gameObject.name);
        }
    }
}
