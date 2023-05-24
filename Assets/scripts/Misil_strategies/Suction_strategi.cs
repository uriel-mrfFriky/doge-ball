using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Suction_strategi : MonoBehaviour,ISuction_Strategi
{
    private float _range;
    public float Range  =>_range;

    public bool InRange(float distance, float range)
    {
        if (distance > range) return false;
        else
            return true;
    }

    public void MoveStrategi(Transform target, Transform _myTransform, Rigidbody2D _targetRb, float _speed, float rotationSpeed)
    {
        Vector2 direction = (Vector2)_myTransform.position - _targetRb.position ;
        if (InRange(direction.magnitude,_range))
        {
            //target.position += new Vector3(direction.normalized.x * _speed * Time.deltaTime, direction.normalized.y * _speed * Time.deltaTime);
            _targetRb.velocity += direction.normalized * _speed * Time.deltaTime;
        }
    }

    public void steRange(float newRange)
    {
        _range = newRange;
    }

}
