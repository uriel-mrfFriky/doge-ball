using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineOfSight : MonoBehaviour
{
    public float angle;
    public float range;
    public LayerMask mask;
    public GameObject target;
    public void Update()
    {
        LookMouse();
        Camera cam = Camera.main;
        Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 destination = new Vector3(mousePos.x, mousePos.y, 0);
       // target.transform.position = destination;
        var x = InSight(target.transform);
        if (x)
        {
            Debug.Log("in range");
        }
        else { Debug.Log("out range"); }
    }
    public bool InSight(Transform target)
    {
       
        Vector3 diff = (target.position - transform.position);
        float distance = diff.magnitude;
        if (distance > range) return false;
        float angleToTarget = Vector3.Angle(transform.right, diff.normalized);
        if (angleToTarget > angle / 2) return false;
        if(Physics.Raycast(transform.position,diff.normalized,distance,mask))
        {
            return false;
        }
        return true;
    }
    void LookMouse()
    {
        var dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        var angle = Mathf.Atan2(dir.x, dir.y)*Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(-angle+90, Vector3.forward);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, transform.forward * range);
        Gizmos.DrawWireSphere(transform.position, range);
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, Quaternion.Euler(0, angle / 2, 0) * transform.right * range);
        Gizmos.DrawRay(transform.position, Quaternion.Euler(0, -angle / 2, 0) * transform.right * range);
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Quaternion.Euler(0, 0, angle / 2) * transform.right * range);
        Gizmos.DrawRay(transform.position, Quaternion.Euler(0, 0, -angle / 2) * transform.right * range);
    }

}
