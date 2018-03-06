using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathGenerator : MonoBehaviour 
{
    [HideInInspector]
    public List<Vector3> waypoints = new List<Vector3>();
    [HideInInspector]
    public int next = 0;
    public int waypointCount;
    public Vector3 nextWaypoint;

    public void OnDrawGizmos()
    {
        int waypointCount = transform.childCount;
        Gizmos.color = Color.green;

        for (int i = 1; i < waypointCount; i++)
        {
            Transform previousWaypoint = transform.GetChild(i - 1);
            Transform nextWaypointPrivate = transform.GetChild(i % transform.childCount);
            Gizmos.DrawLine(previousWaypoint.transform.position, nextWaypointPrivate.transform.position);
        }
    }

    // Use this for initialization
    void Start () 
	{
		waypoints.Clear();
        waypointCount = transform.childCount;

        for (int i = 0; i < waypointCount; i++)
        {
            waypoints.Add(transform.GetChild(i).position);
            Debug.Log("Waypoint added " + i);
        }
    }
	
	// Update is called once per frame
	void Update () 
	{
        nextWaypoint = waypoints[next];
        Debug.Log("Next in update" + next);
	}

    public Vector3 GetNextWaypointInList ()
    {
        Debug.Log("GetNextWaypointInList " + next);
        return waypoints[next];
    }

    /*
    public void NextWaypointCount ()
    {
        next = (next + 1) % waypoints.Count;
        Debug.Log("NextWaypointCount " + next);

        if (next != waypoints.Count - 1)
        {
            next++;
            Debug.Log("Add to next" + next);
        }
    }
    */

    public bool LastWaypoint ()
    {
        Debug.Log("Return last " + next);
        return next == waypoints.Count - 1;
    }
}