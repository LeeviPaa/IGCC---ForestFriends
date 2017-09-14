using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle3Controller : MonoBehaviour {

	public List<GameObject> NeedToActivate;
    private List<ActivateThis> Scripts = new List<ActivateThis>();
    public GameObject ActivateThis;

    void Start()
    {
        foreach (GameObject GO in NeedToActivate)
        {
            if(GO.GetComponent<ActivateThis>())
            {
                Scripts.Add(GO.GetComponent<ActivateThis>());
            }
        }
    }

	void Update () {

        bool allActive = true;
		foreach(ActivateThis AT in Scripts)
        {
            if(!AT.Activated)
            {
                allActive = false;
            }
        }

        if(allActive)
        {
            if(ActivateThis.GetComponent<MonoBehaviour>() is I_Triggerable)
            {
                I_Triggerable T = (I_Triggerable)ActivateThis.GetComponent<MonoBehaviour>();
                T.triggerable();
            }
        }
	}
}
