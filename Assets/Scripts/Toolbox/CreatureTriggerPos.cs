using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureTriggerPos : MonoBehaviour {

    EventManager E;
    public string TriggerTag = "Player";
    private Transform target;

	void Start () {
        bool found = false;
        foreach(Transform child in transform)
        {
            if(child.name == "Target")
            {
                target = child;
                found = true;
            }
        }
        if (!found)
            Debug.LogError(name + " Target not found!");

        E = Toolbox.RegisterComponent<EventManager>();
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == TriggerTag)
        {
            E.TransmitDogDestination(target.position);
            Debug.LogWarning("Dog destination" + target.position);
        }
    }
    void OnTriggerExit(Collider other)
    {
        if(other.tag == TriggerTag)
        {

        }
    }
	
	
}
