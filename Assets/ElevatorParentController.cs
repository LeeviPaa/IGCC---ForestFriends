using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ElevatorParentController : MonoBehaviour {

    GameObject creature = null;
    bool creatureIsOn = false;
    public GameObject elevator;
    public GameObject holder;
    private Elevator E;
    private Transform prevtransform;

    bool trigger = false;

    void OnEnable()
    {
        E = elevator.GetComponent<Elevator>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Creature")
        {
            creature = other.gameObject;
            creatureIsOn = true;
            prevtransform = creature.transform.parent;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Creature")
        {
            //creature = null;
            creatureIsOn = false;
        }
        
    }

    void Update()
    {
        if(E.IsMoving && !trigger && creature != null)
        {
            Debug.LogWarning("started moving");

            trigger = true;
            creature.GetComponent<NavMeshAgent>().enabled = false;
            creature.GetComponent<Creature>().enabled = false;
            creature.transform.SetParent(holder.transform);
        }

        if (!E.IsMoving && trigger && creature != null)
        {
            Debug.LogWarning("StoppedMoving");

            trigger = false;
            creature.GetComponent<NavMeshAgent>().enabled = true;
            creature.GetComponent<Creature>().enabled = true;
            creature.transform.SetParent(prevtransform);
            //creature.SetActive(false);
            //creature.SetActive(true);
        }
    }
}
