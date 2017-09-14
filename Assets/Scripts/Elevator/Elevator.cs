using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour, I_Triggerable
{
    // エレベーター
    [SerializeField]
    private GameObject elevator;

    // 搭乗口地点
    [SerializeField]
    private Transform lowerPos;
    [SerializeField]
    private Transform upperPos;

    // 上昇・下降フラグ
    [SerializeField]
    private bool isRise;

    // 移動速度
    [SerializeField]
    private float riseSpeed;


    private bool isMoving = false;
    public bool IsMoving
    {
        get
        {
            return isMoving;
        }
    }
    AudioSource A;
    // Use this for initialization
    void Start()
    {
        A = GetComponentInChildren<AudioSource>();
        A.volume = 0;
        isRise = true;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(elevator.transform.position.y);

        MoveElevator();
    }

    private void MoveElevator()
    {
        if (isRise)
        {
            if (elevator.transform.position.y < upperPos.transform.position.y)
            {
                elevator.transform.Translate(0, riseSpeed * Time.deltaTime, 0);
                isMoving = true;
                A.volume = 1;
            }
            else
            {
                isMoving = false;
                A.volume = 0;
            }
        }
        else
        {
            if (elevator.transform.position.y > lowerPos.transform.position.y)
            {
                elevator.transform.Translate(0, -riseSpeed * Time.deltaTime, 0);
                isMoving = true;
                A.volume = 1;
            }
            else
            {
                isMoving = false;
                A.volume = 0;
            }
        }
    }

    public void triggerable()
    {
        isRise = !isRise;
    }
}
