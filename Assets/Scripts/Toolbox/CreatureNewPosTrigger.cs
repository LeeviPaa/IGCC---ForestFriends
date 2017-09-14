﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureNewPosTrigger : MonoBehaviour {

    EventManager E;

    public Transform dogDestination;

    void OnEnable () {
        E = Toolbox.RegisterComponent<EventManager>();
	}
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            E.TransmitDogDestination(dogDestination.position);
        }
    }
	
}