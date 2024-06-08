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
    public void MoveStrategi
        (Transform target,Transform _myTransform,Rigidbody2D _Rb, float _speed,float rotationSpeed,ObstacleAvoidance avoidance)
    {
        //get the direction to player
        Vector2 TargetDirection = (Vector2)target.position - _Rb.position;
        
        //get the direction to the wall i shall avoid
        Vector3 ObstacleDirection = Vector3.Cross(_myTransform.up, avoidance.GetTileDir().normalized);
        
        //get the direction to chase the player
        //Vector3 ChaseDirection = Vector3.Cross(TargetDirection.normalized, _myTransform.up);

        //get the rotation to avoid the walls and chase the player
        //float rotateAmound = ObstacleDirection.z + ChaseDirection.z;

            //Vector3.Cross(ChaseDirection,ObstacleDirection).z;

        //float rotateAmound = ChaseDirection.z;
        float rotateAmound = ObstacleDirection.z;
        _newAngle = rotateAmound * rotationSpeed * _speed;
        _newVelocity = _myTransform.up * _speed ;
    }

}
