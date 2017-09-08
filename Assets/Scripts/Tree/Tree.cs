﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Tree Script
public class Tree : MonoBehaviour
{
    // stratPosition
    [SerializeField]
    private Vector3 startPos;
    // endPosition
    [SerializeField]
    private Vector3 endPos;
    // time
    [SerializeField]
    private float time;
    // Amount of movement per second
    private Vector3 deltaPos;
    // elapsedtime
    private float elapsedTime;
    // control flag
    private bool isStartToEnd;

    // isShake
    [SerializeField]
    private bool isShake;
    // for Tree
    [SerializeField]
    private float shakeTime;
    private float savetime;

    [SerializeField]
    private float crashVelocity;

    public GameObject instanceObject;

    // for Apple
    public int CreateAmount = 4;
    [SerializeField]
    private bool isGrowing;
    public float growingTime;
    private float growSaveTime;

    public GameObject[] responPoint;

    // Use this for initialization
    void Start()
    {
        MadeFruit();

        this.transform.position = startPos;

        deltaPos = (endPos - startPos) / time;
        elapsedTime = 0;
        crashVelocity = 0.0f;
        savetime = shakeTime;
        growSaveTime = growingTime;
        isGrowing = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(isShake)
        {
            Shake();
        }

        if (!isGrowing)
        {
            foreach (Transform child in transform)
            {
                if (child.CompareTag("Food"))
                {
                    Debug.Log("ここまできた");
                    child.gameObject.GetComponent<Rigidbody>().useGravity = true;
                    // appleの方である速度床に落ちたら親を解除
                }
            }

            foreach (Transform child in transform)
            {
                if (child.CompareTag("Food"))
                {
                    child.transform.parent = null;
                }
            }

            growingTime -= Time.deltaTime;

            Debug.Log(growingTime);

            if (growingTime < 0.0f)
            {
                print("growed");
                MadeFruit();
                growingTime = growSaveTime;
                isGrowing = true;
            }
        }
    }

    private void Shake()
    {
        transform.position += deltaPos * Time.deltaTime;

        elapsedTime += Time.deltaTime;

        if (elapsedTime > time)
        {
            if (isStartToEnd)
            {
                deltaPos = (startPos - endPos) / time;

                transform.position = endPos;
            }
            else
            {
                deltaPos = (endPos - startPos) / time;

                transform.position = startPos;
            }

            isStartToEnd = !isStartToEnd;

            elapsedTime = 0;
        }

        shakeTime -= Time.deltaTime;

        if (shakeTime < 0)
        {
            print("Shaked");
            isGrowing = false;
            isShake = false;

            shakeTime = savetime;
        }
    }

    private void MadeFruit()
    {
        for (int i = 0; i < CreateAmount; i++)
        {
            Instantiate(instanceObject, responPoint[i].transform.position, Quaternion.identity, transform);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.magnitude > crashVelocity)
            isShake = true;
    }
}