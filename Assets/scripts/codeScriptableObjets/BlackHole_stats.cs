using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Misil/BlackHole_stats")]
public class BlackHole_stats : ScriptableObject
{
    public float succionSpeed;
    public float range;
    public Suction_strategi suction_Strategi;
}
