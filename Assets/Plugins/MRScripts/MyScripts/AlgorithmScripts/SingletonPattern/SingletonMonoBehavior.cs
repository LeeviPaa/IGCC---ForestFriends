// Ver.1.5

namespace MasujimaRyohei
{
    using UnityEngine;

    public class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;
        public static T instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = (T)FindObjectOfType(typeof(T));

                    if (_instance == null)
                    {
                        Debug.LogError(typeof(T) + " is nothing.");
                    }
                }
                return _instance;
            }
        }

        protected void Awake()
        {
            if (this != instance)
            {
                Debug.Log(name.ToString() + "generated.");
                Destroy(this);
                return;
            }
        }

        public void DontDestroyThisOnLoad()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}