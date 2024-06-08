using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

public class ObstacleAvoidance : ISteeringBehaviors2D
{
    Transform _npc;
    Transform _target;
    float _radius;
    LayerMask _mask;
    float _avoidWeight;
    public Collider2D[] obstacles;
    public Vector3 cellWorldPos;

    Vector3Int auxVector3;
    public ObstacleAvoidance(Transform npc, Transform target, float radius, float avoidWeight, LayerMask mask)
    {
        _mask = mask;
        _npc = npc;
        _radius = radius;
        _target = target;
        _avoidWeight = avoidWeight;
    }
    public Vector2 GetTileDir()
    {
        //get the obstacles
        obstacles = Physics2D.OverlapCircleAll(new Vector2(_npc.position.x, _npc.position.y), _radius, _mask);
        var count = obstacles.Length;
        // Debug.Log(count);
        Vector3 dirObsToNpc = new Vector3(0, 0, 0);
        Vector3 currCellWorldPos = new Vector3(0,0,0);

        //run through all the ostacles to get the neares
        for (int i = 0; i < count; i++)
        {
            var currObs = obstacles[i].transform;

            if (obstacles[i].gameObject.TryGetComponent<Tilemap>(out Tilemap tilemap))
            {
                //get the specific the tile position in order to serch the sprite position of that tile
                //obtengo la posicion del tile especifico para buscar la posicion del sprite perteneciente a ese tile
                var cellpos = currObs.gameObject.GetComponent<Tilemap>().WorldToCell(currObs.position);
                var Spritepos = currObs.gameObject.GetComponent<Tilemap>().GetSprite(cellpos).pivot;
                //save the sprite position to get its world position
                //guardo la posicion del sprite y la convierto a posicion mundial
                auxVector3 = new Vector3Int((int)Spritepos.x, (int)Spritepos.y, 0);
                currCellWorldPos = currObs.gameObject.GetComponent<Tilemap>().CellToWorld(auxVector3);

                if (cellWorldPos == Vector3.zero)
                {
                    cellWorldPos = currCellWorldPos;
                }
                //compare the previous distance with the actual one
                else if (Vector3.Distance(_npc.position, cellWorldPos) > Vector3.Distance(_npc.position, currCellWorldPos))
                {
                    cellWorldPos = currCellWorldPos;
                }
                Debug.Log("currObstacle position" + currObs.position);
                Debug.Log("Cell Sprite position" + Spritepos);
                Debug.Log("World Sprite Position" + cellWorldPos);
            }
            else//if not tile map
            {
                return GetDir();
            }
        }
        //if thears an obstcle add the avoid direcction
        //Si hay un obstaculo, le agregamos a nuestra direccion una direccion de esquive
        if (cellWorldPos != Vector3.zero)
        {
            dirObsToNpc = (_npc.position - cellWorldPos).normalized * _avoidWeight;
        }
        //retornamos la direccion final
        return dirObsToNpc.normalized;
    }

    public Vector2 GetDir()
    {
        //Obtenemos los obstaculos
        obstacles = Physics2D.OverlapCircleAll(new Vector2(_npc.position.x, _npc.position.y), _radius, _mask);
       
        
        Transform obsSave = null;
        var count = obstacles.Length;
       // Debug.Log(count);
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
