using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Chase_strategi : MonoBehaviour, IMove_Strategi
{
    private Vector3 _newVelocity;
    private float _newAngle;
    public Vector3 NewVelocity => _newVelocity;
    public float NewAngle => _newAngle;
    

    /*  public Chase_strategi()
{ }*/
    public void MoveStrategi(Transform target,Transform _myTransform,Rigidbody2D _Rb, float _speed,float rotationSpeed,ObstacleAvoidance avoidance)
    {
        Vector2 direction = (Vector2)target.position - _Rb.position;
        Vector3 rotateAmound = Vector3.Cross(direction.normalized, avoidance.GetDir().normalized);
        _newAngle = -rotateAmound.z * rotationSpeed * _speed/3;
        _newVelocity = _myTransform.up * _speed ;
    }

}
