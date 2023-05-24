using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMove_Strategi 
{
    void MoveStrategi(Transform target,Transform _myTransform,Rigidbody2D _Rb, float _speed, float _rotationspeed);
}
