using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTriggerAnim : MonoBehaviour , I_Triggerable
{
    // OpenCheck
    [SerializeField]
    private bool doorOpen;

    private Animator thisAnim;

    private void Start()
    {
        thisAnim = GetComponentInChildren<Animator>();
        doorOpen = false;
        thisAnim.SetBool("Open", false);
    }

    public void triggerable()
    {
        // open the door
        doorOpen = true;
        thisAnim.SetBool("Open", true);
    }
}
