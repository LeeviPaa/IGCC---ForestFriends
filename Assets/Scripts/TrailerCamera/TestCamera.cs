using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCamera : MonoBehaviour
{
    // カメラ
    public Camera camera;

    // ターゲット
    public Transform target;

    public Transform[] waypoint;
    public int wayPointCounter = 0;
    public float[] moveTime;
    public int moveTimeCounter = 0;

    public float speed;
    private float startTime;
    private float length;

	// Use this for initialization
	void Start ()
    {
        // 最初のWayPointの座標をカメラの初期座標に設定
       transform.position = waypoint[wayPointCounter].position;
	}
	
	// Update is called once per frame
	void Update ()
    {
        LookAtTarget();

        transform.position = Vector3.Lerp(waypoint[wayPointCounter].position, waypoint[wayPointCounter + 1].position, speed);
	}

    private void LookAtTarget()
    {
        camera.transform.LookAt(target);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("hit");
        
    }
}
