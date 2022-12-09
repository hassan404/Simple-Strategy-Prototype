using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMoveable : MonoBehaviour, IMoveable
{
    private Vector3 target;
    [SerializeField]
    private float moveSpeed = 5f;
    [SerializeField]
    private float rotationSpeed = 100f;
    [SerializeField]
    private float stopAtDist = 0.5f;

    public void Move(Vector3 target)
    {
        Stop();
        this.target = target;
        StartCoroutine(nameof(MoveTowardsTarget));
    }

    public void Stop()
    {
        StopCoroutine(nameof(MoveTowardsTarget));
    }

    public void Teleport(Vector3 target)
    {
        transform.position = target;
    }

    public IEnumerator MoveTowardsTarget()
    {
        while (true)
        {
            //Get the difference.
            var offset = target - transform.position;
            //If we're further away than .1 unit, move towards the target.
            //The minimum allowable tolerance varies with the speed of the object and the framerate. 
            // 2 * tolerance must be >= moveSpeed / framerate or the object will jump right over the stop.
            if (offset.magnitude > stopAtDist)
            {
                //normalize it and account for movement speed.
                Vector3 movementDirection = offset.normalized;
                offset = movementDirection * moveSpeed;

                //actually move the character.
                transform.position += (offset * Time.deltaTime);

                if (movementDirection != Vector3.zero)
                {
                    Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
                    toRotation = Quaternion.Euler(0f, toRotation.eulerAngles.y, 0f);

                    transform.rotation = Quaternion.RotateTowards
                        (transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
                }
                yield return new WaitForEndOfFrame() ;
            }
            else
            {
                break;
            }

        }
    }
}
