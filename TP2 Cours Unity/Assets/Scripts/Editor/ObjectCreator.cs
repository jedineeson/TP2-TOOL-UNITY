using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ObjectCreator : EditorWindow
{
	private Transform m_Transform;
	private Vector3 m_StartPos = new Vector3(0f, 0f, 0f);

	private enum Direction
	{
		Up,
		Down,
		Left,
		Right,
		Forward,
		Back
	}

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

		EditorGUILayout.EndVertical();

	}
}
