using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouncer : MonoBehaviour {
    Animator mushroomAnim;

    void Start()
    {
        mushroomAnim = transform.parent.GetComponent<Animator>();
    }
	void OnCollisionEnter(Collision other)
    {
        mushroomAnim.SetTrigger("Bounce");
    }
}
