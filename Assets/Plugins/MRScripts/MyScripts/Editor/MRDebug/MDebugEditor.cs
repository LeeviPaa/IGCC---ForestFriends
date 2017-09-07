namespace MasujimaRyohei
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEditor;

    [InitializeOnLoad]
    public class MDebugEditor
    {
        public static bool debugMode;
        
        [MenuItem("My Debug/Enable")]
        private static void EnableDebugMode()
        {
            MDebug.debugMode = true;
        }
        [MenuItem("My Debug/Disable")]
        private static void DisableDebugMode()
        {
            MDebug.debugMode = false;
        }
    }
}