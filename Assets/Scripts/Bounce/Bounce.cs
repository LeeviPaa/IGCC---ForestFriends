using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour
{
    // 保存用
    private Vector3 saveVelocity;

    // 加速度
    [SerializeField]
    private float speed;

    // 制限
    private float upperLimit = 1000.0f;
    private float lowerLimit = 100.0f;
    [SerializeField]
    private float bounceAmount;

    private void OnCollisionEnter(Collision collision)
    {
        // 衝突対象のVelocityを保存
        saveVelocity = collision.relativeVelocity;

        Debug.Log(saveVelocity);

        var s = saveVelocity * speed;

        Debug.Log("制限前" + s);

        //上限設定
        //if (s.x > upperLimit)
        //    s.x /= 2;
        //if (s.y > upperLimit)
        //    s.y /= 2;
        //if (s.z > upperLimit)
        //    s.z /= 2;

        s *= bounceAmount;

        Debug.Log("制限後" + s);
        collision.gameObject.GetComponent<Rigidbody>().AddForce(s);
    }
}
