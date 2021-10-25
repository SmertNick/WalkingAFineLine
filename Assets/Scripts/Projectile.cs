using System;
using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour, IProjectile
{
    [SerializeField] private float speed = 15f;
    [SerializeField] private float damage = 1f;
    [SerializeField] private float lifeTime = 5f;
    [SerializeField] private ParticleSystem particles;
    
    private Transform projectileTransform;

    private void Start()
    {
        projectileTransform = transform;
    }

    private void OnEnable()
    {
        StartCoroutine(Despawn());
    }

    private void Update()
    {
        projectileTransform.position += projectileTransform.forward * speed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision other)
    {
        IDamageable[] effects = other.gameObject.GetComponents<IDamageable>();
        if (effects != null)
        {
            foreach (IDamageable effect in effects)
            {
                effect.OnDamage(projectileTransform.forward, damage);
            }
        }
        
        Xplode();
        gameObject.SetActive(false);
    }

    private void Xplode()
    {
        //particles.Play();
    }

    private IEnumerator Despawn()
    {
        yield return new WaitForSeconds(lifeTime);
        gameObject.SetActive(false);
    }

    public void Fire(Vector3 spawnPosition, Vector3 direction)
    {
        projectileTransform.position = spawnPosition;
        projectileTransform.rotation = Quaternion.Euler(direction);
        gameObject.SetActive(true);
    }
    
}
