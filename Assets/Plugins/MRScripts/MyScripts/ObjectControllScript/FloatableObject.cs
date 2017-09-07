using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class FloatableObject : MovableObject
{
    [SerializeField]
    private float switchTime = 0.5f;
    private new void Start()
    {
        base.Start();
        //transform.position = new Vector3(0.0f, 0.0f, 0.0f);
        StartCoroutine("Float");
    }
    void Update()
    {
       // transform.position = new Vector3(transform.position.x, 2.0f+Mathf.PingPong(Time.time, switchTime), transform.position.z);

    }

    void Float()
    {
        iTween.MoveBy(this.gameObject, iTween.Hash("y", switchTime, "loopType", "pingPong"));
    }
}
