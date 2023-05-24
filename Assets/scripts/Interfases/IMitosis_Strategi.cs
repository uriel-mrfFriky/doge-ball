using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMitosis_Strategi 
{
    void Mitosis(int amound,GameObject prefav,Vector3 spawnpoint,float rotation,List<GameObject> childrens);
}
