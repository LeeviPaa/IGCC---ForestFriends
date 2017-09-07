/******************************************************
File   : GameObjectExtensions.cs
Author : Masujima Ryohei
Date   : 2017/07/13 ~ 2017/--/--
Summary: For extensions of GameObject class.
*******************************************************/

namespace MasujimaRyohei
{
    #region Namespaces
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UniRx;
    #endregion

    public static class GameObjectExtensions
    {
        /// <summary>
        /// Set new positon of gameObject using Vector3.
        /// </summary>
        /// <param name="self"></param>
        /// <param name="position">New position coordinate.</param>
        public static void SetPosition(this GameObject self, Vector3 position)
        {
            self.transform.position = position;
        }

        /// <summary>
        /// Set new position of gameObject using float variables.
        /// </summary>
        /// <param name="self"></param>
        /// <param name="px">X coordinate.</param>
        /// <param name="py">Y coordinate.</param>
        /// <param name="pz">Z coordinate.</param>
        public static void SetPosition(this GameObject self, float px, float py, float pz = 0)
        {
            self.transform.position = new Vector3(px, py, pz);
        }

        /// <summary>
        /// Move position of gameObject using Vector3.
        /// </summary>
        /// <param name="self"></param>
        /// <param name="position">New position coordinate.</param>
        public static void AddPosition(this GameObject self, Vector3 position)
        {
            self.transform.position += position;
        }

        /// <summary>
        /// Move position of gameObject using float variables.
        /// </summary>
        /// <param name="self"></param>
        /// <param name="px">X coordinate.</param>
        /// <param name="py">Y coordinate.</param>
        /// <param name="pz">Z coordinate.</param>
        public static void AddPosition(this GameObject self, float px, float py, float pz = 0)
        {
            self.transform.position += new Vector3(px, py, pz);
        }

        /// <summary>
        /// Get current position of gameObject.
        /// </summary>
        /// <param name="self"></param>
        /// <param name="adjustPosition">If you want to adjust position.</param>
        /// <returns></returns>
        public static Vector3 GetPosition(this GameObject self, Vector3 adjustPosition)
        {
            return self.transform.position + adjustPosition;
        }

        /// <summary>
        /// Get current position of gameObject.
        /// </summary>
        /// <param name="self"></param>
        /// <param name="px"></param>
        /// <param name="py"></param>
        /// <param name="pz"></param>
        /// <returns></returns>
        public static Vector3 GetPosition(this GameObject self, float px = 0, float py = 0, float pz = 0)
        {
            return self.transform.position + new Vector3(px, py, pz);
        }

        /// <summary>
        /// Get component safety.
        /// </summary>
        /// <typeparam name="T">Component's name</typeparam>
        /// <param name="obj"></param>
        /// <param name="alsoAddWhenNull">If you want to do AddComponent().</param>
        /// <returns>Component or error</returns>
        public static T GetSafeComponent<T>(this GameObject obj, bool alsoAddWhenNull = true) where T : Component
        {
            T c = obj.GetComponent<T>();

            if (alsoAddWhenNull && !c) c = obj.AddComponent<T>();

            if (!c)
                Debug.LogError("Expected to find component of type " + typeof(T) + " but found none", obj);

            return c;
        }

        /// <summary>
        /// Find game object using tag. In the case of it's unsuccess, using name.
        /// </summary>
        /// <param name="tag">Tag name.</param>
        /// <param name="name">Object name.</param>
        /// <returns>Finded gameObject</returns>
        public static GameObject FindGameObjectTagAndName(string tag, string name = null)
        {
            GameObject g = null;
            if (g.DoesTagExist(tag))
                g = GameObject.FindGameObjectWithTag(tag);
            else
                g = (System.String.IsNullOrEmpty(name))
                    ? GameObject.Find(tag) : GameObject.Find(name);
            return g;
        }

        /// <summary>
        /// Check if the tag is exist.
        /// </summary>
        /// <param name="self"></param>
        /// <param name="tag">Tag name.</param>
        /// <returns>true or false.</returns>
        public static bool DoesTagExist(this GameObject self, string tag)
        {
            try
            {
                GameObject.FindWithTag(tag);
                Debug.Log("Finded");
                return true;
            }
            catch (UnityEngine.UnityException e)
            {
                Debug.Log("Tag is not exist : " + e);
                return false;
            }
        }

        /// <summary>
        /// Check if the game object is null.
        /// </summary>
        /// <param name="self"></param>
        /// <param name="name">Game object's name you want to check.</param>
        /// <returns>true or false.</returns>
        public static bool CheckNull(this GameObject self, string name = null)
        {
            if (self)
                return true;
            if (name != null)
                if (GameObject.Find(name))
                    return true;
            return false;
        }

        /// <summary>
        /// Check if the object's reference is missing.
        /// </summary>
        /// <param name="self"></param>
        /// <param name="errorLog">Recommend to fill the object name.</param>
        /// <returns></returns>
        public static bool CheckReferenceMiss(this Object self, string errorLog = null)
        {
            if (self)
                return true;

            if (errorLog != null)
                Debug.LogError(errorLog + " is not exist.");
            else
                Debug.LogError("There is not exist object");
            return false;
        }

        public static void SetActiveUseDelay(this GameObject self, bool value, float delay = 1)
        {
            Debug.Log("呼んだ");

            
            Observable.Timer(System.TimeSpan.FromSeconds(delay)).Subscribe(_ => { self.SetActive(value); Debug.Log("発動"); });
        }


    }

}


/******************************************************
                      END OF FILE
*******************************************************/
