using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteriodRotate : MonoBehaviour 
{
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
        transform.Rotate(Vector3.up * (1.5f * Time.deltaTime), Space.World);
    }
}