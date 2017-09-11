using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoivotPoint : MonoBehaviour {

    public GameObject MainCamHolder;
    public float cameraLerpSpeed = 0.5f;
    private float elapsedLerpTime = 0;
    private bool lerpRunning = false;

    private Transform Target;
    private Vector3 TargetPosition;
    private Vector3 PreviousTargetPosition;
    private Toolbox T;
    void OnEnable()
    {
        T = Toolbox.Instance;
        T.Pivot = this;
        Target = transform;

        bool targetFound = false;
        foreach(Transform child in transform)
        {
            if(child.name == "Target")
            {
                Target = child;
                targetFound = true;
            }
        }
        if (!targetFound)
            Debug.LogError(transform.name + " Target not found");

        TargetPosition = Target.position;
        PreviousTargetPosition = Target.position;
    }

	void Update () {
        if (MainCamHolder)
        {
            Target.position = Vector3.Lerp(Target.position, TargetPosition, cameraLerpSpeed);
            Target.position = new Vector3(Target.transform.position.x, MainCamHolder.transform.position.y, Target.transform.position.z);
            MainCamHolder.transform.LookAt(Target);
        }
	}
    public void ChangeTarget(Vector3 newTarget)
    {
        PreviousTargetPosition = Target.position;
        TargetPosition = newTarget;
        
        Debug.Log("Target changed "+ newTarget);
    }

    public void ResetTarget()
    {
        PreviousTargetPosition = Target.position;
        TargetPosition = transform.position;
        Debug.Log("Target resetted" + TargetPosition);
    }
}
