using System;
using UnityEngine;

public class KnockBack : MonoBehaviour, IDamageable
{
    [SerializeField] private float knockBackForce = 7f;
    [SerializeField] private float knockUpForce = 2f;

    private Rigidbody body;
    private Transform bodyTransform;
    private bool hasBody;

    private void Start()
    {
        body = GetComponent<Rigidbody>();
        if (body != null)
        {
            bodyTransform = body.transform;
            hasBody = true;
        }
    }

    public void OnDamage(Vector3 direction, float amount)
    {
        if (!hasBody) return;
        Vector3 totalDirection = knockBackForce * amount * direction + knockUpForce * bodyTransform.up; 
        body.AddForce(totalDirection, ForceMode.Impulse);
    }
}
