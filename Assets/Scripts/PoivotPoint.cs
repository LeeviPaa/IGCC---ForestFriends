using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoivotPoint : MonoBehaviour {

    public GameObject MainCamHolder;

	void Update () {
        if (MainCamHolder)
        {
            MainCamHolder.transform.LookAt(transform);
        }
	}
}
