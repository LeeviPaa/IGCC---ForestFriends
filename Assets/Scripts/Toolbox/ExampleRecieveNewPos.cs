using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleRecieveNewPos : MonoBehaviour {

    EventManager E;

	void OnEnable()
    {
        E = Toolbox.RegisterComponent<EventManager>();
        E.GiveDogDestination += RecievePos;
    }
    void OnDisable()
    {
        E.GiveDogDestination -= RecievePos;
    }

    void RecievePos(Vector3 pos)
    {
        Debug.LogWarning("Position recieved" + pos);
    }
	
}
