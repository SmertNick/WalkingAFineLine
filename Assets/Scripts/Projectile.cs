using System;
using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour, IProjectile
{
    [SerializeField] private float speed = 15f;
    [SerializeField] private float damage = 1f;
    [SerializeField] private float lifeTime = 5f;
    
    private ParticleSystem particles;
    private IParticlesProvider particlesProvider;
    private Transform projectileTransform;
    private Rigidbody body;

    private void Awake()
    {
        projectileTransform = transform;
        body = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        particlesProvider = GameManager.instance.ParticlesProvider;
    }

    private void OnEnable()
    {
        StartCoroutine(Despawn());
        body.velocity = Vector3.zero;
        GetComponent<Rigidbody>().AddForce(projectileTransform.forward * speed, ForceMode.Impulse);
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
        particles = particlesProvider.GetParticles().GetComponent<ParticleSystem>();
        if (particles == null) return;
        particles.transform.position = projectileTransform.position;
        particles.gameObject.SetActive(true);
        particles.Play();
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
