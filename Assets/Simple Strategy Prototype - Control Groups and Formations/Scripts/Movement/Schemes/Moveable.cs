using System.Collections;
using UnityEngine;

public abstract class Moveable : MonoBehaviour, IMoveable
{
    [System.Serializable]
    protected class MoveData
    {
        public float moveSpeed = 10f;
        public float acceleration = 10f;
        public float rotationSpeed = 100f;
        public float stopAtDist = 0.5f;
        public float collisionRadius = 0.5f;
    }

    [SerializeField]
    protected MoveData movementParameters;

    public virtual void Move(Vector3 target)
    {
    }

    public virtual void Teleport(Vector3 target)
    {
    }

    public virtual void Stop()
    {
    }
}