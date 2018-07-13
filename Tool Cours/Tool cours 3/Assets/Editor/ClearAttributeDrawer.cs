using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(ClearAttribute))]
public class ClearAttributeDrawer : PropertyDrawer
{
    private const float BUTTON_SIZE = 20f;

    public override void OnGUI(Rect aPosition, SerializedProperty aProperty, GUIContent aLabel)
    {
        aPosition.width -= BUTTON_SIZE;
        aLabel = EditorGUI.BeginProperty(aPosition, aLabel, aProperty);
    
        EditorGUI.PropertyField(aPosition, aProperty);

        Rect buttonRect = aPosition;
        buttonRect.width = BUTTON_SIZE;
        buttonRect.x += aPosition.width;

        GUI.color = Color.red;
        if (GUI.Button(buttonRect, "x"))
        {
            switch(aProperty.propertyType)
            {
                case SerializedPropertyType.Color:
                    aProperty.colorValue = Color.white;
                    break;
                case SerializedPropertyType.Float:
                    aProperty.floatValue = 0f;
                    break;
                case SerializedPropertyType.Integer:
                    aProperty.intValue = 0;
                    break;
                case SerializedPropertyType.ObjectReference:
                    aProperty.objectReferenceValue = null;
                    break; 
                case SerializedPropertyType.Quaternion:
                    aProperty.quaternionValue = Quaternion.identity;
                    break;
                case SerializedPropertyType.String:
                    aProperty.stringValue = "";
                    break;
                case SerializedPropertyType.Vector2:
                    aProperty.vector2Value = Vector2.zero;
                    break;
                case SerializedPropertyType.Vector3:
                    aProperty.vector3Value = Vector3.zero;
                    break;
            }

            GUI.FocusControl("");
        }
        GUI.color = Color.white;
        

        EditorGUI.EndProperty();
    }
    //public virtual void OnGUI(Rect position, SerializedProperty property, GUIContent label);

}
