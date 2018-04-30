using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour
{
    public List<MovementInterface> behaviours = new List<MovementInterface>();

    public float maxSpeed = 8.0f;
    [HideInInspector]
    public float maxForce = 8.0f;
    public float fleeDistance = 5.0f;
    public float mass = 1.0f;

    [HideInInspector]
    public Vector3 velocity = Vector3.zero;
    [HideInInspector]
    public Vector3 acceleration = Vector3.zero;
    [HideInInspector]
    public Vector3 force = Vector3.zero;
    [HideInInspector]
    public Vector3 target;
    
    void Start()
    {
        MovementInterface[] behaviours = GetComponents<MovementInterface>();

        foreach (MovementInterface m in behaviours)
        {
            this.behaviours.Add(m);
        }
    }

    void Update()
    {
        force = CalculateForce();
        acceleration = force / mass;
        velocity += acceleration * Time.deltaTime;
        transform.position += velocity * Time.deltaTime;

        if (velocity.magnitude > float.Epsilon)
        {
            transform.forward = velocity;
        }
    }

    public Vector3 SeekVector(Vector3 target)
    {
        Vector3 desiredVelocity = target - transform.position;

        desiredVelocity.Normalize();
        desiredVelocity *= maxSpeed;

        Vector3 steeringVector = desiredVelocity - velocity;

        steeringVector = steeringVector / mass;

        return steeringVector;
    }

    public Vector3 FleeVector(Vector3 target)
    {
        Vector3 desiredVelocity = transform.position - target;

        if (desiredVelocity.magnitude > fleeDistance)
        {
            return Vector3.zero;
        } else {
            desiredVelocity.Normalize();
            desiredVelocity *= maxSpeed;
            Vector3 steeringVector = velocity - desiredVelocity;
            steeringVector = (velocity - desiredVelocity) / mass;

            return velocity - desiredVelocity;
        }
    }

    public Vector3 ArrivalVector(Vector3 target, float arrivalRadius = 10.0f)
    {
        Vector3 desiredVelocity = target - transform.position;

        float distance = (target - transform.position).magnitude;

        if (distance < arrivalRadius)
        {
            (target - transform.position).Normalize();
            desiredVelocity *= maxSpeed * (distance / arrivalRadius);
        } else {
            (target - transform.position).Normalize();
            desiredVelocity *= maxSpeed;
        }

        return target - transform.position - velocity;
    }

    Vector3 CalculateForce()
    {
        force = Vector3.zero;

        foreach (MovementInterface m in behaviours)
        {
            if (m.isActiveAndEnabled)
            {
                force += m.CalculateForce() * m.weight;
            }
        }

        return force;
    }
}