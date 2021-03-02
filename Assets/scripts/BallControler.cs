using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControler : MonoBehaviour
{
    public float speed;
    public GameObject Target;
    MovementControler MovementControler;

    public void Awake()
    {
        MovementControler = GetComponent<MovementControler>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MovementControler.MoveToTarget(speed, Target.transform, GetComponent<Rigidbody2D>());
    }

    public void SetTarget()
    { 
        
    }
}
