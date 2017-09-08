using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is meant as a game wide event highway
/// </summary>
public class EventManager : MonoBehaviour {

    public delegate void TransmitDogDirections(Vector3 TargetMovePoint);
    public delegate void ExampleEvent();

    /// <summary>
    /// This is an example event. You can subscribe to this and whenever it is called here, you will get the event flag.
    /// Events use delegates to convey variables in the event. This event uses void delegate so it will not return any variables.
    /// </summary>
    public event ExampleEvent Example;

    public event TransmitDogDirections GiveDogDestination;

    public void CallExampleEvent()
    {
        //Calls the event
        Example();
    }

    public void TransmitDogDestination(Vector3 destination)
    {
        if (GiveDogDestination != null)
            GiveDogDestination(destination);
    }
}
