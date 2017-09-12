using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockGenerator : MonoBehaviour
{
    // 生成するまでの時間
    public float recastTime;
    private float saveRecast;

    // 生成したか
    public bool isGenerated;

    // 生成する岩
    public GameObject rock;

    // Use this for initialization
    void Start()
    {
        saveRecast = recastTime;
    }

    // Update is called once per frame
    void Update()
    {
        // 子がいるか
        if (transform.childCount == 0)
            isGenerated = false;

        // 生成されていない時だけ
        if (!isGenerated)
        {
            recastTime -= Time.deltaTime;
        }

        // リキャストタイムが０且つ岩が生成されていない時
        if (recastTime < 0 && !isGenerated)
        {
            GenerateRock();
            recastTime = saveRecast;
            isGenerated = true;
        }
    }

    // 岩を生成する
    private void GenerateRock()
    {
        Instantiate(rock, transform.position, Quaternion.identity, transform);

        // 子を取得
        Transform child = transform.GetChild(0);

        child.gameObject.GetComponent<Rigidbody>().isKinematic = true;
    }
}
