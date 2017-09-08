using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (transform.localScale.x < 0.4f)
        {
            transform.localScale += new Vector3(0.03f, 0.03f, 0.03f);
        }
    }
}
