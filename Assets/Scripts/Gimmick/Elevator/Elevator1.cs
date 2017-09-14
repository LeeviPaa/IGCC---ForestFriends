using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator1 : MonoBehaviour, I_Triggerable
{
    // 搭乗口地点
    public Transform lowerPos;
    public Transform upperPos;

    // 上昇・下降フラグ
    [SerializeField]
    private bool isRise;

    // 移動速度
    [SerializeField]
    private float riseSpeed;
    private float moveSpeed;

    // 移動中か
    [SerializeField]
    private bool isMoving;

    // Use this for initialization
    void Start()
    {
        isRise = true;
    }

    // Update is called once per frame
    void Update()
    {
        MoveElevator();
    }

    private void MoveElevator()
    {
        if (isRise)
        {
            if (!isMoving) return;
            if (transform.position.y < upperPos.transform.position.y)
            {

                transform.Translate(0, riseSpeed, 0);

                if (transform.position.y > upperPos.transform.position.y)
                    isMoving = false;
            }
        }
        else
        {
            if (!isMoving) return;
            if (transform.position.y > lowerPos.transform.position.y)
            {

                transform.Translate(0, -riseSpeed, 0);

                if (transform.position.y < lowerPos.transform.position.y)
                    isMoving = false;
            }
        }
    }

    public void triggerable()
    {
        isRise = !isRise;
        isMoving = true;
    }

    public void OnTriggerStay(Collider other)
    {
        if (isMoving)
        {
            if (!isRise)
                other.transform.Translate(0, -riseSpeed, 0);
            else
                other.transform.Translate(0, riseSpeed, 0);
        }
    }
}
