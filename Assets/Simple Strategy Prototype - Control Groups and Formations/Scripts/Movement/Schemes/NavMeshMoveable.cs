using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NavMeshMoveable : Moveable
{
    private NavMeshAgent myAgent;

    private void Awake()
    {
        myAgent = GetComponent<NavMeshAgent>();

        myAgent.speed = movementParameters.moveSpeed;
        myAgent.angularSpeed = movementParameters.rotationSpeed;
        myAgent.stoppingDistance = movementParameters.stopAtDist;
        myAgent.acceleration = movementParameters.acceleration;
        myAgent.radius = movementParameters.collisionRadius;
    }

    public override void Move(Vector3 target)
    {
        myAgent.SetDestination(target);
        myAgent.isStopped = false;
    }

    public override void Stop()
    {
        myAgent.isStopped = true;
    }

    public override void Teleport(Vector3 target)
    {
        myAgent.Warp(target);
    }
}
