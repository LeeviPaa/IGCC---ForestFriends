using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuressurePlate : MonoBehaviour
{
    // triggerableObject
    public GameObject triggerObject;

    // PushCheck
    private bool isPush;
    // MoveRestriction
    private float pushRestriction;

    // DebugSwitch
    [SerializeField]
    private bool isTrigger = false;

    private void Start()
    {
        isPush = false;
        pushRestriction = 0.0f;
    }

    public void Update()
    {
        // On Trigger
        if(isTrigger)
        {
            triggerObject.GetComponent<DoorTrigger>().triggerable();
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        isTrigger = true;

        if (pushRestriction < 0.1f)
        {
            Debug.Log(pushRestriction);
            pushRestriction -= -3.0f * Time.deltaTime;
            transform.Translate(0, -3.0f * Time.deltaTime, 0);
        }
    }
}
