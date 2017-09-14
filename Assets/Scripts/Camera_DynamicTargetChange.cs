using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_DynamicTargetChange : MonoBehaviour {
    PoivotPoint P;
    Toolbox T;
    Transform CameraTarget;

	void Start () {
        T = Toolbox.Instance;
        P = T.Pivot;
        Debug.Log(P.name);

        bool targetFound = false;
        foreach(Transform child in transform)
        {
            if (child.name == "Target")
            {
                CameraTarget = child;
                targetFound = true;
            }
        }

        if (!targetFound)
            Debug.LogError(gameObject.name + " TargetNotFound");
	}

    void OnTriggerEnter(Collider other)
    {
        if (P != null && CameraTarget != null && other.tag == "Player")
            P.ChangeTarget(CameraTarget.position);
    }
    void OnTriggerExit(Collider other)
    {
        if (P != null && other.tag == "Player")
            P.ResetTarget();
    }
}
