using System.Collections.Generic;
using UnityEngine;

public class ProjectilePooler : MonoBehaviour
{
    public GameObject projectilePrefab;
    private Queue<GameObject> projectilePoolQueue = new();

    private readonly int initialprojectilePoolSize = 20;
    private readonly int maxPoolSize = 40;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Create a pool of projectiles
        for (int i = 0; i < initialprojectilePoolSize; i++)
        {
            GameObject projectileObj = Instantiate(projectilePrefab);
            projectileObj.SetActive(false);
            projectilePoolQueue.Enqueue(projectileObj);
        }
    }

    // Get a projectile from the pool
    public GameObject GetFromPool(Vector3 position)
    {
        if (projectilePoolQueue.Count > 0)
        {
            GameObject obj = projectilePoolQueue.Dequeue();
            obj.SetActive(true);
            obj.transform.position = position;
            Debug.Log("Retreive from pool ");
            return obj;
        }
        else
        {
            // If the pool is empty, instantiate a new projectile
            if (projectilePoolQueue.Count < maxPoolSize)
            {
                GameObject newObj = Instantiate(projectilePrefab, position, Quaternion.identity);
                Debug.Log("Instantiate new projectile ");
                return newObj;
            }
            else
            {
                Debug.LogWarning("Projectile pool is full. No more projectiles available.");
                return null;
            }
        }
    }

    public void ReturnToPool(GameObject obj)
    {
        Debug.Log(obj);
        obj.SetActive(false);
        projectilePoolQueue.Enqueue(obj);
    }
}
