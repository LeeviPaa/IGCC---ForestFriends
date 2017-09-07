/******************************************************
File   : MDebug.cs
Author : Masujima Ryohei
Date   : 2017/07/13 ~ 2017/--/--
Summary: For debug.
*******************************************************/

namespace MasujimaRyohei
{
    #region Namespaces
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;
    #endregion

    public class MDebug : MonoBehaviour
    {
        #region Variables

        public const string ENABLED = "DEBUG_MASUJIMA";
        public const string DISABLED = "UNDEBUG_MASUJIMA";

        public static List<Text> logs = new List<Text>();

        private float lifeTime;
        //private Text text;
        //private RectTransform rectTransform;
        [SerializeField]
        static private GameObject textPrefab;
        static public bool debugMode;
        #endregion

        /// <summary>
        /// 1. Check reference miss
        /// 2. Start coroutine.
        /// </summary>
        private void Start()
        {
            //text.CheckReferenceMiss("Text component");
            //rectTransform.CheckReferenceMiss("RectTransform component");

            // For destroy logs after a few seconds.
            StartCoroutine(DelayDestroy());
        }

        /// <summary>
        /// Create log.
        /// </summary>
        /// <param name="log">The object you want to display.</param>
        /// <param name="lifeTime">The time to disappear.</param>
        public static void Log(object log, float lifeTime = 5.0f)
        {
            textPrefab = Resources.Load("Prefabs/DText") as GameObject;
            GameObject go = Instantiate(textPrefab);
            go.GetComponent<MDebug>().lifeTime = lifeTime;
            Text logText = go.GetComponent<Text>();
            RectTransform rt;
            System.Type logType = log.GetType();
            if (logType == typeof(string) && logType == typeof(int) && logType == typeof(float) && logType == typeof(double))
                logText.text = log.ToString();

            go.transform.SetParent(GameObject.Find("Canvas").transform);
            rt = go.GetComponent<RectTransform>();
            rt.localPosition = new Vector3(0, 0, -1000);
            rt.localScale = new Vector3(0.2f, 0.2f, 0.2f);
            logs.Add(logText);
        }


        // Update is called once per frame
        void Update()
        {
            //rectTransform.localPosition = new Vector3(-300, 150 - 25 * logs.IndexOf(text), 0);

        }
        /// <summary>
        /// Destroy log object when lifeTime to be 0.
        /// </summary>
        /// <returns></returns>
        IEnumerator DelayDestroy()
        {
            while (true)
            {
                yield return new WaitForSeconds(lifeTime);
                Destroy(gameObject);
                //logs.Remove(text);
            }
        }
    }
}