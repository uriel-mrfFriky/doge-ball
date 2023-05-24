using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstractFactory <T> where T : IFactorizable
{
   public T factoryObjet;
   public void Create(Transform spawnPoint)
    {
        factoryObjet.Create(spawnPoint);
    }
}
