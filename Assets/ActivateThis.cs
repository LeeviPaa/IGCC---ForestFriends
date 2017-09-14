using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateThis : MonoBehaviour, I_Triggerable {

    public GameObject activateThis;
    private bool activated = false;

    public bool Activated
    {
        get
        {
            return activated;
        }
        set
        {
            activated = value;
        }
    }

    void Start()
    {
        activateThis.SetActive(false);
    }

	public void triggerable()
    {
        activateThis.SetActive(true);
        activated = true;
    }
}
