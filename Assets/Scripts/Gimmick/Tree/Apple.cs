using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : A_GrabbableObject
{
    public float scaleSize;
    private bool isScaleComplete;

    private void Start()
    {
        isScaleComplete = false;
    }

    public override void GetGrabbed()
    {
    }
	
	// Update is called once per frame
	public override void Update ()
    {
        if (transform.localScale.x < scaleSize)
        {
            transform.localScale += new Vector3(0.03f, 0.03f, 0.03f);

            if(transform.localScale.x > scaleSize)
            {
                isScaleComplete = true;
            }
        }

        if(isScaleComplete)
        {
            transform.GetComponent<Apple>().enabled = false;
        }
    }
}
