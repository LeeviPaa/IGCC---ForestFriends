using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbableObject_General : A_GrabbableObject {

    private GameObject particles;
    private GameObject On;
    private GameObject Off;
    bool gate = false;
    AudioSource AS;
    public override void GetGrabbed()
    {
        if (!gate)
        {
            particles.SetActive(false);
            particles.SetActive(true);
            On.SetActive(true);
            Off.SetActive(false);
            gate = true;
        }
    }
    // Use this for initialization
    public override void Start () {
        base.Start();
        foreach(Transform child in transform)
        {
            if(child.name == "Rock_On")
            {
                On = child.gameObject;
            }
            if(child.name == "Rock_Off")
            {
                Off = child.gameObject;
            }
            if(child.name == "ParticleSystems")
            {
                particles = child.gameObject;
                particles.SetActive(false);
            }
        }
        AS = GetComponent<AudioSource>();
        On.SetActive(false);
        Off.SetActive(true);
	}

    void OnCollisionEnter(Collision other)
    {
        if(other.relativeVelocity.magnitude > 1 && AS != null)
            AS.Play();
    }
	
	// Update is called once per frame
	public override void Update () {
        base.Update();
	}
}
