using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Misil/Misil_stats")]
public class Misil_Stats : ScriptableObject
{
    public GameObject NextMisil;
    public Chase_strategi chase_Strategi;
    public float speed;
    public float rotationSpeed;
    public float _cooldDownTime;
    public float _lifeTime;
}
