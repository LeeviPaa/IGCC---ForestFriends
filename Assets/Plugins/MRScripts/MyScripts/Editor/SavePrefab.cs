#if UNITY_EDITOR

namespace MasujimaRyohei
{
    #region Namespaces
    using UnityEngine;
    using UnityEditor;
    #endregion

    public class Util : Editor
    {
        [UnityEditor.MenuItem("Tools/SavePrefab %&s")]
        static void SavePrefab()
        {
            AssetDatabase.SaveAssets();
            Debug.Log("Save prefabs");
        }
    }
}
#endif