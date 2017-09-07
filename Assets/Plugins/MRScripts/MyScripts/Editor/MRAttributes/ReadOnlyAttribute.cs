/******************************************************
File   : ReadOnlyAttribute.cs
Author : Masujima Ryohei
Date   : 2017/08/01 ~ 2017/--/--
Summary: For original attribute.It's can make uneditable serialize.
*******************************************************/
namespace MasujimaRyohei
{
    using UnityEngine;
    using UnityEditor;

    public class ReadOnlyAttribute : PropertyAttribute { }

    [CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
    public class ReadOnlyDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, label, true);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            GUI.enabled = false;
            EditorGUI.PropertyField(position, property, label, true);
            GUI.enabled = true;
        }
    }
}

/******************************************************
                      END OF FILE
*******************************************************/


