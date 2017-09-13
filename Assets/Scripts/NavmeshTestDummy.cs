using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavmeshTestDummy : MonoBehaviour {
    Ray screenpointToRay;
    NavMeshAgent ThisNavAgent;
    RaycastHit hit;
    EventManager E;
    Toolbox T;
    public LayerMask L;
    void Start()
    {
        E = Toolbox.RegisterComponent<EventManager>();
        E.GiveDogDestination += ListenToEventManagerDestination;

        ThisNavAgent = gameObject.GetComponent<NavMeshAgent>();
    }
    void OnDisable()
    {
        E.GiveDogDestination -= ListenToEventManagerDestination;
    }

	void Update () {
		if(Input.GetButtonDown("Fire1"))
        {
            screenpointToRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(screenpointToRay,out hit, Mathf.Infinity, L, QueryTriggerInteraction.Ignore))
            {
                E.TransmitDogDestination(hit.point);
            }
        }
	}

    void ListenToEventManagerDestination(Vector3 target)
    {
        ThisNavAgent.SetDestination(target);
    }
}
