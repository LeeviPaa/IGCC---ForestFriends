using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraTilt : MonoBehaviour {
    Camera mainCam;
    public float maxTiltXAngle = 20;
    public float maxTiltYAngle = 20;
    public float Smoothing = 1f;

    private Vector3 startRotation;
    private Transform Target;
	// Use this for initialization
	void Start () {
        mainCam = Camera.main;

        bool TargetFound = false;
        foreach(Transform child in transform)
        {
            if(child.name == "Target")
            {
                Target = child;
                TargetFound = true;
            }
        }
        if(!TargetFound)
        {
            Debug.LogError(transform.name + " Target transform not found!");
        }

        startRotation = transform.localEulerAngles;
	}
	
	void Update () {
        Vector3 resolution = new Vector2(mainCam.pixelWidth, mainCam.pixelHeight);
        resolution /= 2;
        Vector2 screenPointNormalized = Input.mousePosition - resolution;

        Target.localPosition = Vector3.Lerp(Target.localPosition, new Vector3(screenPointNormalized.x, screenPointNormalized.y, transform.position.z), Smoothing*Time.deltaTime);
        //transform.localEulerAngles = startRotation + new Vector3(
        //    Mathf.Clamp(-Target.localPosition.y,-maxTiltYAngle, maxTiltYAngle),
        //    Mathf.Clamp(-Target.localPosition.x, -maxTiltXAngle, maxTiltXAngle), 0)/30;
        transform.localEulerAngles = startRotation + new Vector3(
            Mathf.Clamp(-Target.localPosition.y, -maxTiltYAngle, maxTiltYAngle),
            Mathf.Clamp(Target.localPosition.x, -maxTiltXAngle, maxTiltXAngle), 0) / 30;
    }
}
