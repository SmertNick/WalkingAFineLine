using System;
using System.Collections;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private float fireRate;
    [SerializeField] private Transform bulletSpawnPoint;

    private Transform enemyTransform;
    private Transform player;
    private float attackDelay;
    private IBulletProvider bulletProvider;

    private void Awake()
    {
        enemyTransform = transform;
    }

    private void Start()
    {
        attackDelay = 1 / fireRate;
        player = GameManager.instance.Player;
        bulletProvider = GameManager.instance.BulletProvider; 

        StartCoroutine(AttackPlayer(attackDelay));
    }

    private void Update()
    {
        enemyTransform.rotation = Quaternion.LookRotation(player.position - enemyTransform.position);
    }

    private IEnumerator AttackPlayer(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            Attack(player.position);
        }
    }

    private void Attack(Vector3 targetPoint)
    {
        GameObject bullet = bulletProvider.GetBullet();
        Vector3 startPosition = bulletSpawnPoint.position;
        bullet.transform.position = startPosition;
        bullet.transform.rotation = Quaternion.LookRotation(targetPoint - startPosition);

        bullet.SetActive(true);
    }
}
