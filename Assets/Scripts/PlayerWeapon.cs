using System.Collections;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] private float fireRate = 1f;
    [SerializeField] private Transform spawnPoint;
    
    private KeyBindings keyBindings;
    private Transform playerTransform;
    private IBulletProvider bulletProvider;

    private float coolDownTime;
    private bool isCoolingDown;
    
    void Start()
    {
        playerTransform = transform;
        keyBindings = GameManager.instance.KeyBindings;
        bulletProvider = GameManager.instance.BulletProvider;
        coolDownTime = 1 / fireRate;
    }

    void Update()
    {
        if (Input.GetKeyDown(keyBindings.Fire))
        {
            Attack();
        }   
    }

    private void Attack()
    {
        if (!isCoolingDown)
        {
            StartCoroutine(StartCoolDown(coolDownTime));
            
            GameObject bullet = bulletProvider.GetBullet();
            bullet.transform.position = spawnPoint.position;
            bullet.transform.rotation = Quaternion.Euler(spawnPoint.eulerAngles);

            bullet.SetActive(true);
        }
    }

    private IEnumerator StartCoolDown(float time)
    {
        isCoolingDown = true;
        yield return new WaitForSeconds(time);
        isCoolingDown = false;
    }
}
