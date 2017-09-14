using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomTest : MonoBehaviour {

    Animator A;
	void Start()
    {
        A = transform.GetComponent<Animator>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            A.SetTrigger("Bounce");
        }
    }
}
