﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pursue : MovementInterface 
{
    //[HideInInspector]
    public GameObject target;
    [HideInInspector]
    public Vector3 targetPosition;
    public float minimumDistance = 2.0f;
    public float chaseSpeed = 20.0f;
    public float normalSpeed;

    // Use this for initialization
    void Start () 
	{
        normalSpeed = boidScript.maxSpeed;
        target = GameObject.Find("Cruiser");
    }
	
	// Update is called once per frame
	void Update () 
	{
        
	}

    public override Vector3 CalculateForce()
    {
        float pursueDistance = Vector3.Distance(target.transform.position, transform.position);
        float time = pursueDistance / boidScript.maxSpeed;

        if(pursueDistance >= minimumDistance)
        {
            boidScript.maxSpeed = chaseSpeed;
        } else if (pursueDistance <= minimumDistance) {
            boidScript.maxSpeed = normalSpeed;
        }

        targetPosition = target.transform.position + (time * boidScript.velocity);

        return boidScript.SeekVector(target.transform.position);
    }
}