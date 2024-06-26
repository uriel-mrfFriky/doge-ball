using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementControler : MonoBehaviour
{
    public void MoveToTarget(float speed ,Transform targetTransform,Rigidbody2D RB)
    {
        var direction =  targetTransform.position - transform.position;
        transform.position += direction.normalized * speed * Time.deltaTime;
    }

}
