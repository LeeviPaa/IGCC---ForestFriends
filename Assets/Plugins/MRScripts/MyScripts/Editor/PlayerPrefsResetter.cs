/******************************************************
File   : PlayerPrefResetter.cs
Author : Masujima Ryohei
Date   : 2017/07/14
Summary: For Reset PlayerPref's data.
*******************************************************/
namespace MasujimaRyohei
{
    #region Namespaces
    using UnityEngine;
    using UnityEditor;
    #endregion

    public static class PlayerPrefsResetter
    {
        [MenuItem("Tools/PlayerPrefs/Reset")]
        public static void ResetPlayerPrefs()
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();

            Debug.Log("Reset PlayerPrefs");
        }

    }
}

/******************************************************
                      END OF FILE
*******************************************************/
