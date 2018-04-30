using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollow : MovementInterface
{
    public PathGenerator pathGeneratorScript;

    [HideInInspector]
    public Vector3 nextWaypoint;
    public float minimumDistance = 0.7f;

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(transform.position, nextWaypoint);
    }

    public override Vector3 CalculateForce()
    {
        nextWaypoint = pathGeneratorScript.GetNextWaypointInList();

        if((transform.position - nextWaypoint).sqrMagnitude <= minimumDistance)
        {
            pathGeneratorScript.NextWaypointCount();
        }
        
        Debug.Log(gameObject.name + " to point " + (pathGeneratorScript.next + 1));
        return boidScript.SeekVector(nextWaypoint);
    }
}