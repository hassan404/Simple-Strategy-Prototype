using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A very simple implementation of movement that paths to destination without considering terrain and obstacles.
/// </summary>

public class SimpleMoveable : Moveable
{

    private Vector3 target;

    public override void Move(Vector3 target)
    {
        Stop();
        this.target = target;
        StartCoroutine(nameof(MoveTowardsTarget));
    }

    public override void Stop()
    {
        StopCoroutine(nameof(MoveTowardsTarget));
    }

    public override void Teleport(Vector3 target)
    {
        transform.position = target;
    }

    private IEnumerator MoveTowardsTarget()
    {
        while (true)
        {
            //Get the difference.
            var offset = target - transform.position;
            //If we're further away than .1 unit, move towards the target.
            //The minimum allowable tolerance varies with the speed of the object and the framerate. 
            // 2 * tolerance must be >= moveSpeed / framerate or the object will jump right over the stop.
            if (offset.magnitude > movementParameters.stopAtDist)
            {
                //normalize it and account for movement speed.
                Vector3 movementDirection = offset.normalized;
                offset = movementDirection * movementParameters.moveSpeed;

                //actually move the character.
                transform.position += (offset * Time.deltaTime);

                if (movementDirection != Vector3.zero)
                {
                    Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
                    toRotation = Quaternion.Euler(0f, toRotation.eulerAngles.y, 0f);

                    transform.rotation = Quaternion.RotateTowards
                        (transform.rotation, toRotation, movementParameters.rotationSpeed * Time.deltaTime);
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
