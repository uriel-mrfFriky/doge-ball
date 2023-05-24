using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleAvoidance : ISteeringBehaviors2D
{
    Transform _npc;
    Transform _target;
    float _radius;
    LayerMask _mask;
    float _avoidWeight;
    public Collider[] obstacles;
    public ObstacleAvoidance(Transform npc, Transform target, float radius, float avoidWeight, LayerMask mask)
    {
        _mask = mask;
        _npc = npc;
        _radius = radius;
        _target = target;
        _avoidWeight = avoidWeight;
    }

    public Vector2 GetDir()
    {
        //Obtenemos los obstaculos
        obstacles = Physics.OverlapSphere(_npc.position, _radius, _mask);
        Transform obsSave = null;
        var count = obstacles.Length;
        Vector3 dirObsToNpc = new Vector3(0,0,0);

        //Recorremos los obstaculos y determinos cual es el mas cercano
        for (int i = 0; i < count; i++)
        {
            var currObs = obstacles[i].transform;
            if (obsSave == null)
            {
                obsSave = currObs;
            }
            else if (Vector3.Distance(_npc.position, obsSave.position) > Vector3.Distance(_npc.position, currObs.position))
            {
                obsSave = currObs;
            }
        }

        //Si hay un obstaculo, le agregamos a nuestra direccion una direccion de esquive
        if (obsSave != null)
        {
            dirObsToNpc = (_npc.position - obsSave.position).normalized * _avoidWeight;
        }
        //retornamos la direccion final
        return dirObsToNpc.normalized;
    }
}
