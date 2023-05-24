using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISuction_Strategi : IMove_Strategi
{
    float Range { get; }
    bool InRange(float distance,float range);
    void steRange(float newRange);
}
