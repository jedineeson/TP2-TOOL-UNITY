using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Character))]
public class CharacterEditor : Editor 
{
	private const char UP_ARROW = (char)9650;
	private const char DOWN_ARROW = (char)9660;

	private bool m_ListFoldout;
	private Character m_Character;

	private void OnEnable()
	{
		m_Character = (Character)target;
	}

	public override void OnInspectorGUI()
	{
		if(GUILayout.Button("Progress Bar"))
		{
			int count = 1000000;
			for (int i = 0; i < count; i++)
			{
				Debug.Log(i);
				float progress = 1/(float)count;
				if(EditorUtility.DisplayCancelableProgressBar("Log", "Logging..." + i.ToString() + "/" + count.ToString(), progress))
				{
					break;
				}
			}

			EditorUtility.ClearProgressBar();
		}

		m_Character.m_Health = EditorGUILayout.Slider("Health", m_Character.m_Health, 0f, 100f);
		
		SerializedProperty nameProp = serializedObject.FindProperty("m_Name");
		
		EditorGUILayout.PropertyField(nameProp);

		SerializedProperty tiers = serializedObject.FindProperty("m_Tiers");
		EditorGUILayout.PropertyField(tiers, true);

		ShowCustonList(tiers);

		serializedObject.ApplyModifiedProperties();

		//DrawDefaultInspector();
	}

	private void ShowCustonList(SerializedProperty aProp)
	{
		EditorGUILayout.BeginHorizontal();
		m_ListFoldout = EditorGUILayout.Foldout(m_ListFoldout, "Tiers", true);
		//EditorGUILayout.LabelField("Tiers");
		
		GUI.color = Color.green;
		if(GUILayout.Button("+", EditorStyles.toolbarButton, GUILayout.Width(20f)))
		{
			aProp.arraySize++;
		}

		GUI.color = Color.magenta;
		if(GUILayout.Button("X", EditorStyles.toolbarButton,  GUILayout.Width(20f)))
		{
			aProp.arraySize = 0;
		}
		GUI.color = Color.white;

		EditorGUILayout.EndHorizontal();

		if(m_ListFoldout)
		{
			EditorGUI.indentLevel++;
			EditorGUILayout.PropertyField(aProp.FindPropertyRelative("Array.size"));
			//aProp.arraySize = EditorGUILayout.IntField("Size", aProp.arraySize);
			for (int i = 0; i < aProp.arraySize; i++)
			{
				EditorGUILayout.BeginHorizontal();
				EditorGUILayout.PropertyField(aProp.GetArrayElementAtIndex(i));
				
				GUI.color = Color.cyan;
				if(GUILayout.Button(UP_ARROW.ToString(), EditorStyles.toolbarButton, GUILayout.Width(20f)))
				{
					aProp.MoveArrayElement(i, i-1);
				}	
				GUI.color = Color.yellow;
				if(GUILayout.Button(DOWN_ARROW.ToString(), EditorStyles.toolbarButton, GUILayout.Width(20f)))
				{
					aProp.MoveArrayElement(i, i+1);
				}	
				GUI.color = Color.red;
				if(GUILayout.Button("-", EditorStyles.toolbarButton, GUILayout.Width(20f)))
				{
					aProp.DeleteArrayElementAtIndex(i);
					i--;
				}		
				GUI.color = Color.white;
				EditorGUILayout.EndHorizontal();
			}
			EditorGUI.indentLevel--;
		}
	}
}
