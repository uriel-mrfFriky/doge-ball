using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mitosis_strategi : MonoBehaviour, IMitosis_Strategi
{
    float currentRotation = 0;
    public void Mitosis(int amound,GameObject prefav,Vector3 spawnpoint,float rotation, List<GameObject> childrens)
    {
        for (int i = 0; i <amound ; i++)
        {
            var Gobj = prefav;
            Gobj.transform.Rotate(new Vector3(0, 0, currentRotation));
            currentRotation += rotation;
            if (childrens.Count < amound)
                Instantiate(Gobj);
            else
                break;
        }
    }
}
