using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NavMeshMoveable : MonoBehaviour, IMoveable
{
    [SerializeField]
    private float moveSpeed = 10f;
    [SerializeField]
    private float acceleration = 10f;
    [SerializeField]
    private float rotationSpeed = 100f;
    [SerializeField]
    private float stopAtDist = 0.5f;

    private NavMeshAgent myAgent;

    private void Awake()
    {
        myAgent = GetComponent<NavMeshAgent>();
        myAgent.speed = moveSpeed;
        myAgent.angularSpeed = rotationSpeed;
        myAgent.stoppingDistance = stopAtDist;
        myAgent.acceleration = acceleration;
    }

    public void Move(Vector3 target)
    {
        myAgent.SetDestination(target);
        myAgent.isStopped = false;
    }

    public void Stop()
    {
        myAgent.isStopped = true;
    }

    public void Teleport(Vector3 target)
    {
        myAgent.Warp(target);
    }
}
