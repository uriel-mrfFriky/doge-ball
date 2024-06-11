using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Character_Player_Input))]
public class Character_PLayer_Model : MonoBehaviour
{
    private Rigidbody2D _rb;

    [SerializeField]
    private float _speed;
    public void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public void Movement(Vector2 Dir)
    {
        Dir *= _speed;
        _rb.velocity = Dir;
    }
}
