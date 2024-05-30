using System.Collections;
using System.Collections.Generic;
using System.Reflection;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

[System.AttributeUsage(System.AttributeTargets.Field)]
public class MMInspectorButtonAttribute : PropertyAttribute
{
    public readonly string MethodName;

    public MMInspectorButtonAttribute(string MethodName)
    {
        this.MethodName = MethodName;
    }
#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(MMInspectorButtonAttribute))]
    public class InspectorButtonPropertyDrawer : PropertyDrawer
    {
        private MethodInfo _eventMethodInfo = null;

        public override void OnGUI(Rect position, SerializedProperty prop, GUIContent label)
        {
            MMInspectorButtonAttribute inspectorButtonAttribute = (MMInspectorButtonAttribute)attribute;
            float buttonLength = position.width;
            Rect buttonRect = new Rect(position.x, position.y, buttonLength, position.height);
            GUI.skin.button.alignment = TextAnchor.MiddleLeft;
            if (GUI.Button(buttonRect, inspectorButtonAttribute.MethodName))
            {
                System.Type eventOwnerType = prop.serializedObject.targetObject.GetType();
                string eventName = inspectorButtonAttribute.MethodName;
                if (_eventMethodInfo == null)
                {
                    //对当前类里面的，public、instance,nonpublic、instance 全都查找
                    _eventMethodInfo = eventOwnerType.GetMethod(eventName,
                        BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
                }

                if (_eventMethodInfo != null)
                {
                    _eventMethodInfo.Invoke(prop.serializedObject.targetObject, null);
                }
                else
                {
                    Debug.LogWarning(string.Format("检查器按钮：找不到方法 {0} 在 {1}", eventName, eventOwnerType));
                }
            }

        }
    }
#endif
}
