using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Misil/Mitosis_stats")]
public class Mitosis_Stats : ScriptableObject
{
    public float rotateAmound;
    public int childrensAmound;
    public GameObject ChildrenPrefav;
    public Mitosis_strategi Mitosis_Strategi;
    public bool childrensCreated;
    public List<GameObject> Childrens = new List<GameObject>();

}
