using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

public class ObjectCreator : EditorWindow
{
    private enum Direction
    {
        Up,
        Down,
        Left,
        Right,
        Forward,
        Back
    }

    private PrimitiveType m_PrimitiveType;
    private Transform m_Transform;
	private Vector3 m_StartPos = new Vector3(0f, 0f, 0f);

    private string m_Name;
    private bool m_UseColor;
    private Color m_StartColor;
    private Color m_EndColor;
    private int m_NbToCreate;
    private float m_Spacing;
    private bool m_AutoCenter;
    private bool m_UseLocalRotation;
    private Direction m_Direction;





    /*private enum PrimitiveType
    {
        Cube = 0,
        Sphere,
        Cylindre,
        Capsule,
        Custom,
    }*/

    [MenuItem("Tool/Object Creator")]
	private static void Init()
	{
		GetWindow<ObjectCreator>().Show();
	}



	private void OnGUI()
	{
		EditorGUILayout.BeginVertical(GUI.skin.box);
		EditorGUILayout.LabelField("Position", EditorStyles.centeredGreyMiniLabel);
		
		EditorGUILayout.BeginVertical(GUI.skin.box);
		EditorGUILayout.TextArea("If none is assigned, then the start position will be (0f, 0f, 0f)", EditorStyles.label);
		EditorGUILayout.EndVertical();
		
		m_Transform = EditorGUILayout.ObjectField("Start Position", m_Transform, typeof(Transform), false) as Transform;
		EditorGUILayout.EndVertical();
		
		EditorGUILayout.BeginVertical(GUI.skin.box);
		EditorGUILayout.LabelField("Object Selection", EditorStyles.centeredGreyMiniLabel);
		if(GUILayout.Button("Use Custom Object"))
		{
            
		}
        EditorGUILayout.EnumPopup("Primitive Object", m_PrimitiveType); 
		EditorGUILayout.EndVertical();

        EditorGUILayout.BeginVertical(GUI.skin.box);
        EditorGUILayout.LabelField("Creation Parameters", EditorStyles.centeredGreyMiniLabel);
        EditorGUILayout.TextField("Name", m_Name);
        EditorGUILayout.Toggle("Use Color?", m_UseColor);
        EditorGUILayout.ColorField("Start Color", m_StartColor);
        EditorGUILayout.ColorField("Start Color", m_EndColor);
        EditorGUILayout.IntField("Nb to Create", m_NbToCreate);
        EditorGUILayout.FloatField("Spacing", m_Spacing);
        EditorGUILayout.EnumPopup("Axis Direction", m_Direction);
        EditorGUILayout.Toggle("Auto-center?", m_AutoCenter);
        EditorGUILayout.Toggle("Use localRotation?", m_UseLocalRotation);
        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginVertical(GUI.skin.box);
        if (GUILayout.Button("Create"))
        {

        }
        EditorGUILayout.EndVertical();
        //Ligne de Pat
        /*m_CustomObject = (GameObject)EditorGUILayout.ObjectField("Object: ", m_CustomObject, typeof(GameObject), true);*/
    }
}
