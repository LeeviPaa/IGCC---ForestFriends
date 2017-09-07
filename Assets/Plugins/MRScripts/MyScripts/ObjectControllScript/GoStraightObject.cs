using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Author Masujima Ryohei
/// </summary>
public class GoStraightObject : MovableObject
{

    [SerializeField]
    public bool isUse = true;
    [SerializeField]
    private Vector3 targetPosition; // ターゲットの座標

    //private bool dontMoveFirst = true;

    //private float startTime;
    //private Vector3 startPosition;      // 自オブジェクトの開始位置

    void OnEnable()
    {

    }

    public void Initialize()
    {
        if (time <= 0)
        {
            this.transform.position = targetPosition;
            enabled = false;
            return;
        }


        //startTime = Time.timeSinceLevelLoad;
        //startPosition = this.transform.position;
    }

    protected new void Start()
    {
        //GameObject p = GameObject.FindGameObjectWithTag("Player");

        //if(p && tagetPosition == null)
        //    tagetPosition = p.transform.position;
    }

    protected void Update()
    {
        if (!isUse)
            return;

       // Move();
    }

    private void Move()
    {
        //isMove = true;


        //float diff = Time.timeSinceLevelLoad - startTime;
        //if (diff > time)
        //{
        //    this.transform.position = targetPosition;
        //    enabled = false;
        //}

        //float rate = diff / time;

        //this.transform.position = Vector3.Lerp(startPosition, targetPosition, rate);

        //if (this.transform.position == targetPosition)
        //    isMove = false;

    }

 

    public void SetTargetPosition(Vector3 newTargetPosition)
    {
        targetPosition = newTargetPosition;
        Initialize();
    }
    public void SimpleMove(Vector3 pos)
    {
        var moveHash = new Hashtable();
        moveHash.Add("position", pos);
        moveHash.Add("time", 1f);
        moveHash.Add("delay", 0f);
        moveHash.Add("easeType", "easeInOutBack");
        moveHash.Add("oncomplete", "AnimationEnd");
        moveHash.Add("oncompletetarget", this.gameObject);
        iTween.MoveTo(this.gameObject, moveHash);
    }
}

