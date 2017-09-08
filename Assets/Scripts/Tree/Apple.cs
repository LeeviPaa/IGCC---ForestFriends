using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : A_GrabbableObject
{
    public override void GetGrabbed()
    {
    }
	
	// Update is called once per frame
	public override void Update ()
    {
        if (transform.localScale.x < 0.4f)
        {
            transform.localScale += new Vector3(0.03f, 0.03f, 0.03f);
        }
    }
}
