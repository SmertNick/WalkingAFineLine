using UnityEngine;

public class EnemyDeath : MonoBehaviour, IDamageable
{
    public void OnDamage(Vector3 direction, float amount)
    {
        Destroy(this.gameObject); // enough for this case. If there were more enemies - make a pool for them as well
    }
}
