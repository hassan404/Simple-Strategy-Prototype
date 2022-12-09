using System.Collections;
using UnityEngine;

public interface IMoveable 
{
    public void Move(Vector3 target);
    public void Teleport(Vector3 target);
    public void Stop();
}
