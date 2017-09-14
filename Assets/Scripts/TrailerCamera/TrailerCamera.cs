using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ※Maybe the translation is wrong :'-(

// How to Use
// ●targets ... The target the camera sees.
//    If you want to change the target seen by the camera during the movement, 
//    please change the tag of the passing point to "ChangeTarget"
// ●accels ... Speed between passing points.
// ●wayPoints ... Point where camera passes.
//    Add SphereCollider to the passing point and set Is Trigger to true.
//
// ※ A steep curve is difficult.
// ※ Necessary adjustment.

public class TrailerCamera : MonoBehaviour
{
    // カメラ
    public GameObject camera;
    // タイトル
    public GameObject titleImage;
    // フェード
    public GameObject fade;
    // 動作中か
    private bool isAction;

    // 初期ステータス
    private Vector3 startPosition;
    private Quaternion startRotation;

    // 加速度
    private float accel;
    // 慣性
    public float inertia = 0.1f;
    // 速度制限
    public float speedLimit = 10.0f;
    // 下限速度
    public float minSpeed = 1.0f;
    // 停止時間
    public float stopTime = 1.0f;

    // トレイラーカメラを終了するまでの時間
    public float timeOfFinish;
    private float saveTime;

    // タイトルを表示するまでの時間
    public bool isDisplayTitle;
    public float timeToDisplayTitles;
    private float saveDisTime;

    // 現在の速度
    private float currentSpeed = 0.0f;
    // ステート
    private int state = 0;
    // 加速状態
    private bool accelState;
    // 低速状態
    private bool slowState;
    // ウェイポイント
    private Transform waypoint;
    private float rotationDamping = 6.0f;
    public bool smoothRotation = true;
    // ターゲット
    public Transform[] targets;
    private int targetCounter;
    public float[] accels;
    private int accelCounter;
    public Transform[] wayPoints;
    private int wpIndexCounter;

    // Use this for initialization
    void Start()
    {
        state = 0;
        startPosition = transform.position;
        startRotation = transform.rotation;
        saveDisTime = timeToDisplayTitles;
        saveTime = timeOfFinish;
        isAction = true;
    }

    // Update is called once per frame
    void Update()
    {
        timeOfFinish -= Time.deltaTime;

        // Camera move on or off switch
        if (Input.GetKeyDown(KeyCode.Space))
            isAction = !isAction;

        if (Input.GetKeyDown(KeyCode.R))
            Reset();

        if (isAction)
        {
            if (isDisplayTitle)
                DisplayTitle();

            LookAtTarget();

            if (state == 0)
            {
                Accel();
            }
            if (state == 1)
            {
                Slow();
            }
        }

        accel = accels[accelCounter];
        waypoint = wayPoints[wpIndexCounter];

        Debug.Log("現在のウェイポイントカウンター : " + wpIndexCounter);
        Debug.Log("現在の速度カウンター : " + wpIndexCounter + "　速度 : " + accel);

        // トレイラーカメラを終了
        if (timeOfFinish < 0)
            transform.gameObject.SetActive(false);
    }

    // タイトル表示 =======================================
    private void DisplayTitle()
    {
        timeToDisplayTitles -= Time.deltaTime;

        if (timeToDisplayTitles < 0)
        {
            titleImage.SetActive(true);
        }
    }

    // 指定されたターゲットの方向を向く ===========================
    private void LookAtTarget()
    {
        Vector3 targetDir = targets[targetCounter].position - camera.transform.position;
        Vector3 newDir = Vector3.RotateTowards(camera.transform.forward, targetDir, 0.8f * Time.deltaTime, 0.0f);
        camera.transform.rotation = Quaternion.LookRotation(newDir);
    }

    // 加速 ============================================
    private void Accel()
    {
        if (accelState == false)
        {
            accelState = true;
            slowState = false;
        }
        if (waypoint)
        {
            if (smoothRotation)
            {
                var rotation = Quaternion.LookRotation(waypoint.position - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime);
            }
        }

        currentSpeed = currentSpeed + accels[accelCounter] * accels[accelCounter];
        transform.Translate(0, 0, Time.deltaTime * currentSpeed);

        if (currentSpeed >= speedLimit)
        {
            currentSpeed = speedLimit;
        }
    }

    // 低速 ============================================
    private void Slow()
    {
        if (!slowState)
        {
            accelState = false;
            slowState = true;
        }

        currentSpeed = currentSpeed * inertia;

        transform.Translate(0, 0, Time.deltaTime * currentSpeed);

        if (currentSpeed <= minSpeed)
        {
            currentSpeed = 0.0f;
            state = 0;
        }
    }

    // リセット =========================================
    private void Reset()
    {
        state = 0;
        timeOfFinish = saveTime;
        // PositionReset;
        transform.position = startPosition;
        transform.rotation = startRotation;

        // CountReset
        accelCounter = 0;
        wpIndexCounter = 0;

        // PointReset
        accel = accels[accelCounter];
        waypoint = wayPoints[wpIndexCounter];

        // TitleReset
        titleImage.SetActive(false);
        timeToDisplayTitles = saveDisTime;

        // 一度動きを止めてからリセットする
        isAction = false;
        transform.LookAt(targets[targetCounter]);
    }

    // 衝突判定 =========================================
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ChangeTarget")
        {
            targetCounter++;
        }

        Debug.Log("hit!");
        //state = 1;
        wpIndexCounter++;
        accelCounter++;
        if (wpIndexCounter >= wayPoints.Length)
        {
            wpIndexCounter = 0;
            accelCounter = 0;
        }

        if (targetCounter >= targets.Length)
        {
            targetCounter = 0;
        }
    }
}
