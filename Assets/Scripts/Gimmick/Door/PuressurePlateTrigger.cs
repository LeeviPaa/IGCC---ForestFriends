using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuressurePlateTrigger : MonoBehaviour
{
    // triggerableObject
    public GameObject triggerObject;
    public string TriggerTag;
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

    void Update()
    {
        if(isTrigger)
        {
            isTrigger = false;
            if (triggerObject.GetComponent<MonoBehaviour>() is I_Triggerable)
            {
                I_Triggerable Interactable = (I_Triggerable)triggerObject.GetComponent<MonoBehaviour>();
                Interactable.triggerable();
            }
        }
    }


    private void OnTriggerStay(Collider collision)
    {
        if (collision.tag == TriggerTag)
        {
            isTrigger = true;
            if (triggerObject.GetComponent<MonoBehaviour>() is I_Triggerable)
            {
                I_Triggerable Interactable = (I_Triggerable)triggerObject.GetComponent<MonoBehaviour>();
                Interactable.triggerable();
            }
            if (pushRestriction < 0.1f)
            {
                Debug.Log(pushRestriction);
                pushRestriction -= -3.0f * Time.deltaTime;
                transform.Translate(0, -3.0f * Time.deltaTime, 0);
            }
        }
    }
}
