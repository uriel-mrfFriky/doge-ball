using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Player_Input : MonoBehaviour
{
    public float xMovement()
    {
        float xMovement = Input.GetAxisRaw("Horizontal");
        return xMovement;
    }
    public float yMovement()
    {
        float yMovement = Input.GetAxisRaw("Vertical");
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
