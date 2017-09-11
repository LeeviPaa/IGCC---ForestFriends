using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoivotPoint : MonoBehaviour {

    public GameObject MainCamHolder;
    public float cameraLerpSpeed = 0.5f;
    private float elapsedLerpTime = 0;
    private bool lerpRunning = false;

    private float smoothTransValue = 0;

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
        if (MainCamHolder )
        {
            if (smoothTransValue < 1 && lerpRunning)
            {
                smoothTransValue += Time.deltaTime*cameraLerpSpeed;
                Vector3 tempPos = Vector3.zero;
                tempPos.x = Mathf.SmoothStep(PreviousTargetPosition.x, TargetPosition.x, smoothTransValue);
                tempPos.z = Mathf.SmoothStep(PreviousTargetPosition.z, TargetPosition.z, smoothTransValue);
                Target.position = tempPos;
            }
            else if(smoothTransValue != 0)
            {
                lerpRunning = false;
                smoothTransValue = 0;
            }

            Target.position = new Vector3(Target.transform.position.x, MainCamHolder.transform.position.y, Target.transform.position.z);
            MainCamHolder.transform.LookAt(Target);
        }
	}
    public void ChangeTarget(Vector3 newTarget)
    {
        PreviousTargetPosition = Target.position;
        TargetPosition = newTarget;
        resetLerpVariables();
    }

    public void ResetTarget()
    {
        PreviousTargetPosition = Target.position;
        TargetPosition = transform.position;
        resetLerpVariables();
        Debug.Log("Target resetted" + TargetPosition);
    }
    private void resetLerpVariables()
    {
        lerpRunning = true;
        smoothTransValue = 0;
    }
}
