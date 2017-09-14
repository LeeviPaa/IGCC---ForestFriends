using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarwBridge : MonoBehaviour, I_Triggerable
{
    // 橋が掛かっているか
    [SerializeField]
    private bool isBuild;
    // 橋が降りる速度
    [SerializeField]
    private float downSpeed;


    // Use this for initialization
    void Start()
    {
        isBuild = false;
    }

    // Update is called once per frame
    void Update()
    {
        DownUpBridge();
    }

    private void DownUpBridge()
    {
        if (isBuild)
        {
            // 橋が降りきるまで
            // Until the bridge gets off
            if (transform.rotation.z > 0.0f)
            {
                transform.Rotate(0, 0, -downSpeed * Time.deltaTime);

                // 橋の角度が 0 を下回った場合、角度を 0 にする
                // When the angle of the bridge falls below 0, set the angle to 0.
                if (transform.rotation.z < 0)
                    transform.rotation = Quaternion.AxisAngle(new Vector3(0, 0, 1), 0);
            }
        }
        else
        {
            // 橋が上がりきるまで
            // Until the bridge rises
            if(transform.rotation.z < 0.70f)
           {
                transform.Rotate(0, 0, downSpeed * Time.deltaTime);
            }
        }
    }

    public void triggerable()
    {
        isBuild = true;
    }
}
