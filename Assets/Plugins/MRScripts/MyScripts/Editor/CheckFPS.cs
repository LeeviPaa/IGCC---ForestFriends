using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckFPS : MonoBehaviour
{
    public int FrameCount { get; set; }
    public int PrevCount { get; set; }


    // Use this for initialization
    void Start()
    {
        FrameCount = 10;
    }

    // Update is called once per frame
    void Update()
    {

    }
}