using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour, IBulletProvider, IParticlesProvider
{
    [Header("Bullets")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private int bulletPoolSize = 10;
    
    [Header("Bullet particles")]
    [SerializeField] private GameObject bulletCollisionParticlesPrefab;
    [SerializeField] private int particlesPoolSize = 10;

    private List<GameObject> bullets;
    private List<GameObject> particles;
    
    private void Start()
    {
        bullets = GenerateObjects(bulletPrefab, bulletPoolSize);
        particles = GenerateObjects(bulletCollisionParticlesPrefab, particlesPoolSize);
    }

    private List<GameObject> GenerateObjects(GameObject prefab, int amount)
    {
        List<GameObject> objects = new List<GameObject>();
        for (int i = 0; i < amount; i++)
        {
            GameObject obj = Instantiate(prefab);
            objects.Add(obj);
            obj.SetActive(false);
        }
        return objects;
    }

    public GameObject GetBullet()
    {
        // Search pool for first inactive object
        foreach (GameObject obj in bullets)
        {
            if (!obj.activeInHierarchy)
            {
                return obj;
            }
        }

        // If all are active - add new one to the pool
        GameObject extraBullet = Instantiate(bulletPrefab);
        bullets.Add(extraBullet);
        
        return extraBullet;
    }

    public GameObject GetParticles()
    {
        // Search pool for first inactive object
        foreach (GameObject obj in bullets)
        {
            if (!obj.activeInHierarchy)
            {
                return obj;
            }
        }

        // If all are active - add new one to the pool
        GameObject extraParticles = Instantiate(bulletPrefab);
        bullets.Add(extraParticles);
        
        return extraParticles;
    }
}
