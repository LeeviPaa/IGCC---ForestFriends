using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableObject : MonoBehaviour
{

    [SerializeField, Range(0, 10)]
    protected float time = 1;                 // 目的に到着するまでの時間
    protected float tempTime;
    protected bool isMove;
    protected void Start()
    {
        tempTime = time;
    }
    public void SetReachTime(float time)
    {
        this.time = time;
    }
    protected void StartMove()
    {
        isMove = true;
    }

    protected void EndMove()
    {
        isMove = false;
    }
    public bool GetIsMove()
    {
        return isMove;
    }
}
