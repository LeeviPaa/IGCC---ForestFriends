using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrabAndThrow : MonoBehaviour {

    private GameObject RingOutput;
    bool InteractableAround = false;
    GameObject GrabbedObject;
    GameObject lookTarget;
    LineRenderer line;
    Vector3 MouseInputForce;

    public float forceAmount = 100;
    public Transform holdPosition;

    private bool Fire1Down = false;

    bool holdingObject = false;

	void Start () {
        line = GetComponent<LineRenderer>();

		foreach(Transform child in transform)
        {
            if(child.name =="LookTarget")
            {
                lookTarget = child.gameObject;
                
            }
            if(child.name == "Ring")
            {
                RingOutput = child.gameObject;
                child.gameObject.SetActive(false);
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Fire1"))
        {
            if (holdingObject)
            {
                line.positionCount = 15;
                Fire1Down = true;
            }
                
        }
        if(Input.GetButtonUp("Fire1"))
        {
            Fire1Down = false;
            line.positionCount = 0;
            if(holdingObject)
            {
                ThrowObject();
            }
        }

        if(Fire1Down)
        {
            DrawSpline();
        }

        //Check for interactable objects around the player
        CheckAroundForIteractable();
	}
    private void CheckAroundForIteractable()
    {
        InteractableAround = false;

        foreach (Collider col in Physics.OverlapSphere(transform.position, 2.0f))
        {
            if (col.tag == "Interactable")
            {
                InteractableAround = true;
                RingOutput.SetActive(true);
                
            }

            if(col.tag == "Grabbable")
            {
                InteractableAround = true;
                RingOutput.SetActive(true);

                if (Input.GetButtonDown("Interact"))
                {
                    CheckObjectForGrab(col.gameObject);
                }
            }
        }

        if(!InteractableAround)
        {
            RingOutput.SetActive(false);
        }
    }

    private void CheckObjectForGrab(GameObject obj)
    {
        if(!holdingObject)
            GrabObject(obj);
    }

    private void GrabObject(GameObject grabbed)
    {
        holdingObject = true;
        GrabbedObject = grabbed;
        grabbed.transform.parent = holdPosition;
        grabbed.transform.localPosition = Vector3.zero;
        if(grabbed.GetComponent<Rigidbody>())
        {
            grabbed.GetComponent<Rigidbody>().isKinematic = true;
        }

    }
    private void ThrowObject()
    {
        holdingObject = false;
        GrabbedObject.transform.parent = null;
        if (GrabbedObject.GetComponent<Rigidbody>())
        {
            Rigidbody GrabRigidbody = GrabbedObject.GetComponent<Rigidbody>();
            GrabRigidbody.isKinematic = false;
            GrabRigidbody.AddForce(MouseInputForce * forceAmount);
        }

    }

    private void DrawSpline()
    {
        line.positionCount = 15;
        Vector3 lineVector = lookTarget.transform.position - transform.position;
        Vector3 highPoint = transform.position + (lookTarget.transform.position-transform.position)/2 + new Vector3(0, lineVector.magnitude / 4, 0);

        MouseInputForce = (lookTarget.transform.position - transform.position) / 2 + new Vector3(0, lineVector.magnitude / 4, 0);
        float f = 1.0f / 14.0f;
        for(int i = 0; i < 15; i++)
        {
            float I = i;
            Vector3 lerpAB = Vector3.Lerp(transform.position, highPoint, I*f);
            Vector3 lerpBC = Vector3.Lerp(highPoint, lookTarget.transform.position, I * f);
            Vector3 LinePoint = Vector3.Lerp(lerpAB, lerpBC, I * f);
            line.SetPosition(i,LinePoint);
        }
    }
}
