/// <summary>
/// ver1.0.
/// </summary>
namespace MasujimaRyohei
{
    using UnityEngine;
    using System.Collections;
    using System;
    using System.Reflection;

    public abstract class SceneBase : MonoBehaviour
    {
        protected bool isTouchable = true;
        protected bool isPlayableSounds = true;

        //public AudioClip currentlyBGM;

        protected void Awake()
        {
            UseBasicManagers();
        }
        protected GameObject UseManager(string managerName)
        {
            if (GameObject.Find(managerName + "(Clone)") || GameObject.Find(managerName.ToString()))
                return null;

            string managerPath = "Prefabs/Managers/AutomaticPutting/" + managerName;
            GameObject manager = Resources.Load(managerPath) as GameObject;
            manager.name = managerName.ToString();
            return manager;
        }
        public static GameObject UseManager(Type className)
        {
            if (GameObject.Find(className.ToString() + "(Clone)") || GameObject.Find(className.ToString()))
                return null;

            GameObject manager = new GameObject();
            manager.AddComponent(className);
            manager.name = className.ToString();
            return manager;
        }
        private void UseBasicManagers()
        {
            DontDestroyOnLoad(UseManager(typeof(FadeManager)));
            DontDestroyOnLoad(UseManager(typeof(AudioManager)));
            DontDestroyOnLoad(UseManager(typeof(TimeManager)));
        }
       
    }
}