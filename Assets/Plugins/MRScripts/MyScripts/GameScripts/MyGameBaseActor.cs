/// <summary>
/// Actor class is base class of All actors.
/// </summary>
namespace MasujimaRyohei
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public abstract class MyGameBaseActor : MonoBehaviour
    {
        protected bool isExist;
        // Use this for initialization
        protected void Start()
        {
            isExist = true;
        }

        protected GameObject FindNearGameObjectWithTag(string tagName)
        {
            float tmpDis = 0;           //距離用一時変数
            float nearDis = 0;          //最も近いオブジェクトの距離
            GameObject targetObj = null; //オブジェクト

            //タグ指定されたオブジェクトを配列で取得する
            foreach (GameObject obs in GameObject.FindGameObjectsWithTag(tagName))
            {
                //自身と取得したオブジェクトの距離を取得
                tmpDis = Vector3.Distance(obs.transform.position, transform.position);

                //オブジェクトの距離が近いか、距離0であればオブジェクト名を取得
                //一時変数に距離を格納
                if (nearDis == 0 || nearDis > tmpDis)
                {
                    nearDis = tmpDis;
                    targetObj = obs;
                }

            }
            //最も近かったオブジェクトを返す
            //return GameObject.Find(nearObjName);
            return targetObj;
        }
        //protected void DestroyThis() => Destroy(gameObject);
        //public void SetExist(bool value) => isExist = value;
    }
}
