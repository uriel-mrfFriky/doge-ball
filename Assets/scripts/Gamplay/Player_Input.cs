using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Input : MonoBehaviour
{
    public float xMovement()
    {
        float xMovement = Input.GetAxis("Horizontal");
        return xMovement;
    }
    public float yMovement()
    {
        float yMovement = Input.GetAxis("Vertical");
        return yMovement;
    }
    public bool Action1()
    {
        if (Input.GetMouseButtonDown(0))
        {
            return true;
        }
        return false;
    }
}
