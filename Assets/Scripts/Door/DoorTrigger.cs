using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour , I_Triggerable
{
    // OpenDoorSpeed
    public float openSpeed;
    // OpenCheck
    [SerializeField]
    private bool doorOpen;
    //MaxMovement
    public float maxMovement = 2;
    // MoveRestriction
    private float openRestriction;

    private void Start()
    {
        doorOpen = false;
        openRestriction = 0.0f;
    }

    private void Update()
    {
        if (doorOpen)
        {
            if (openRestriction < maxMovement)
            {
                //Debug.Log(openRestriction);
                openRestriction -= openSpeed * Time.deltaTime;
                transform.Translate(0, openSpeed * Time.deltaTime, 0);
            }
        }
        else
        {
            if (openRestriction > 0.0f)
            {
                openRestriction += openSpeed * Time.deltaTime;
                transform.Rotate(0, openSpeed * Time.deltaTime, 0);
            }
        }
    }

    public void triggerable()
    {
        // open the door
        doorOpen = true;
    }
}
