#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(MMInformationAttribute))]
public class MMInformationDrawer : PropertyDrawer//在属性窗口编写信息
{
    const int spaceBeforeTheTextBox = 5;
    const int spaceAfterTheTextBox = 10;
    const int iconWidth = 55;
    MMInformationAttribute informationAttribute
    {
        get { return ((MMInformationAttribute)attribute); }
    }

    public override void OnGUI(Rect rect, SerializedProperty prop, GUIContent label)
    {
        if (HelpEnabled())
        {
            EditorStyles.helpBox.richText = true;
            if (!informationAttribute.MessageAfterProperty)
            {
                rect.height = DetermineTextboxHeight(informationAttribute.Message);
                EditorGUI.HelpBox(rect, informationAttribute.Message, informationAttribute.Type);
                rect.y += rect.height + spaceBeforeTheTextBox;
                EditorGUI.PropertyField(rect, prop, label, true);
            }
            else
            {
                rect.height = GetPropertyHeight(prop, label);
                EditorGUI.PropertyField(rect, prop, label, true);
                rect.height = DetermineTextboxHeight(informationAttribute.Message);
                rect.y += GetPropertyHeight(prop, label) - DetermineTextboxHeight(informationAttribute.Message) -
                          spaceAfterTheTextBox;
                EditorGUI.HelpBox(rect, informationAttribute.Message, informationAttribute.Type);
            }

        }
        else
        {
            EditorGUI.PropertyField(rect, prop, label, true);
        }
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        if (HelpEnabled())
        {
            return EditorGUI.GetPropertyHeight(property) + DetermineTextboxHeight(informationAttribute.Message) + spaceAfterTheTextBox + spaceBeforeTheTextBox;
        }
        else
        {
            return EditorGUI.GetPropertyHeight(property);
        }
    }
    
    protected virtual bool HelpEnabled()
    {
        bool helpEnabled = false;
        if (EditorPrefs.HasKey("MMShowHelpInInspectors"))
        {
            if (EditorPrefs.GetBool("MMShowHelpInInspectors"))
            {
                helpEnabled = true;
            }
        }

        return helpEnabled;
    }

    protected virtual float DetermineTextboxHeight(string message)
    {
        GUIStyle style = new GUIStyle(EditorStyles.helpBox);
        style.richText = true;
        float newHeight = style.CalcHeight(new GUIContent(message), EditorGUIUtility.currentViewWidth - iconWidth);
        return newHeight;
    }
}
#endif