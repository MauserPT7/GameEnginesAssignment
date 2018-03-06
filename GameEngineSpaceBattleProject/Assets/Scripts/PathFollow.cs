using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollow : MonoBehaviour
{
    public PathGenerator pathGeneratorScript;
    [HideInInspector]
    public Vector3 nextWaypoint;
    public float minimumDistance = 0.7f;
    public float speed;
    private float startTime;
    private float journeyLength;

    bool followingPath = false;

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(transform.position, nextWaypoint);
    }

    // Use this for initialization
    void Start()
    {
        startTime = Time.time;
        journeyLength = Vector3.Distance(transform.position, nextWaypoint);
        //nextWaypoint = pathGeneratorScript.GetNextWaypointInList();
        //FollowPath();
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(nextWaypoint);
        //pathGeneratorScript.pathGeneratorScript.NextWaypointCount();();

        Debug.Log("Next to follow " + nextWaypoint);
        //Debug.Log(transform.position);
        Debug.Log(minimumDistance);

        if (!followingPath)
        {
            FollowPath();
        }
    }

    public void FollowPath()
    {
        nextWaypoint = pathGeneratorScript.nextWaypoint;

        float distCovered = (Time.time - startTime) * speed;
        float fracJourney = distCovered / journeyLength;

        transform.position = Vector3.MoveTowards(transform.position, nextWaypoint, (speed * Time.deltaTime));
        //transform.position = Vector3.Lerp(transform.position, nextWaypoint, (Time.deltaTime));

        if ((transform.position - nextWaypoint).sqrMagnitude <= minimumDistance)
        {
            pathGeneratorScript.next++;
            journeyLength = Vector3.Distance(transform.position, nextWaypoint);
            Debug.Log("Get next from script " + nextWaypoint);
        }

        Debug.Log("Following path on " + nextWaypoint);
        //followingPath = true;
    }
}