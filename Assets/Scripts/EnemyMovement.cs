using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Transform[] patrolPoints;
    [SerializeField] private float threshold = 0.5f;
    
    private int nextPoint = 0;
    private NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        MoveToNextPoint();
    }

    private void MoveToNextPoint()
    {
        if (patrolPoints.Length < 1) return;

        agent.destination = patrolPoints[nextPoint].position;

        nextPoint = (nextPoint + 1) % patrolPoints.Length;
    }

    private void Update()
    {
        if (agent.remainingDistance < threshold)
            MoveToNextPoint();
    }
}
