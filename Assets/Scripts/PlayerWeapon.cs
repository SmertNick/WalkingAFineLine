using System.Collections;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] private float fireRate = 1f;
    [SerializeField] private Transform spawnPoint;
    
    private Camera cam;
    private Vector3 aimPoint;
    private const float maxDistance = 100f;
    private GameObject previouslyTargeted;

    private KeyBindings keyBindings;
    private IBulletProvider bulletProvider;

    private float coolDownTime;
    private bool isCoolingDown;
    
    void Start()
    {
        keyBindings = GameManager.instance.KeyBindings;
        bulletProvider = GameManager.instance.BulletProvider;
        coolDownTime = 1 / fireRate;

        cam = GetComponentInChildren<Camera>();
        aimPoint = new Vector3(0.5f * Screen.width, 0.5f * Screen.height, 0f);
    }

    void Update()
    {
        aimPoint = Aim();
        
        if (Input.GetKey(keyBindings.Fire))
        {
            Attack(aimPoint);
        }
    }

    private void Attack(Vector3 targetPoint)
    {
        if (!isCoolingDown)
        {
            StartCoroutine(StartCoolDown(coolDownTime));
            
            GameObject bullet = bulletProvider.GetBullet();
            Vector3 startPosition = spawnPoint.position;
            bullet.transform.position = startPosition;
            bullet.transform.rotation = Quaternion.LookRotation(targetPoint - startPosition);

            bullet.SetActive(true);
        }
    }

    private Vector3 Aim()
    {
        // In case user rescales window or changes resolution at runtime
        aimPoint.x = 0.5f * Screen.width;
        aimPoint.y = 0.5f * Screen.height;

        Ray aimRay = cam.ScreenPointToRay(aimPoint);
        
        if (Physics.Raycast(aimRay, out RaycastHit hit, maxDistance))
        {
            if (previouslyTargeted != hit.transform.gameObject)
            {
                DeTarget(previouslyTargeted);
            }

            previouslyTargeted = hit.transform.gameObject;
            
            ITargetable[] targetables = hit.transform.GetComponents<ITargetable>();
            if (targetables != null)
            {
                foreach (ITargetable effect in targetables)
                {
                    effect.OnTarget();
                }
            }

            return hit.point;
        }
        
        DeTarget(previouslyTargeted);

        // Aim at max distance point
        return aimRay.GetPoint(maxDistance);
    }

    private void DeTarget(GameObject previousTarget)
    {
        if (previousTarget == null) return;
        
        ITargetable[] targetables = previousTarget.GetComponents<ITargetable>();
        if (targetables == null) return;

        foreach (ITargetable effect in targetables)
        {
            effect.OnDeTarget();
        }
    }

    private IEnumerator StartCoolDown(float time)
    {
        isCoolingDown = true;
        yield return new WaitForSeconds(time);
        isCoolingDown = false;
    }
}
