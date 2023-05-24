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
    public void MoveStrategi(Transform target,Transform _myTransform,Rigidbody2D _Rb, float _speed,float rotationSpeed)
    {
        Vector2 direction = (Vector2)target.position - _Rb.position;
        float rotateAmound = Vector3.Cross(direction.normalized, _myTransform.up).z;
        _newAngle = -rotateAmound * rotationSpeed * _speed/3;
        _newVelocity = _myTransform.up * _speed ;
    }

}
