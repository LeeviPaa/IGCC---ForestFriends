using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerEnable : MonoBehaviour {

    public GameObject enableThis;
    public string triggerTag;

	void OnTriggerEnter(Collider other)
    {
        if (other.tag == triggerTag)
        {
            if (enableThis != null)
            {
                enableThis.SetActive(true);
            }
        }
    }
}
