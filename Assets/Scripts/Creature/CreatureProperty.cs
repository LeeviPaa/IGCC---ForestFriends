using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CreatureProperty : MonoBehaviour {
    private Creature creature;
    private NavMeshAgent agent;
    [SerializeField]
    private Vector3 destination;
    [SerializeField]
    private float remainingDistance;
	// Use this for initialization
	void Start () {
        creature = GetComponent<Creature>();
        agent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
        if (agent.pathStatus != NavMeshPathStatus.PathComplete)
            destination = agent.destination;
        else
            destination = Vector3.zero;

        remainingDistance = agent.remainingDistance;
	}
}
