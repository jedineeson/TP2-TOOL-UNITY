using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEditor;

[CustomEditor(typeof(Environment))]
public class EnvironmentEditor : Editor
{
	private Environment m_Environment;

	private float m_MinOffset;
	private float m_MaxOffset;
	private float m_MinRotation;
	private float m_MaxRotation;
	private float m_MinScale;
	private float m_MaxScale;
	//private int m_Health = 50;
	private bool m_DrawBounds;
	private Color m_BoundsColor;

	private void OnEnable()
	{
		m_Environment = (Environment)target;
	}

	public override void OnInspectorGUI()
	{        
		GUI.color = Color.white;
		EditorGUILayout.BeginVertical(GUI.skin.box);
        GUI.contentColor = Color.white;		
		EditorGUILayout.LabelField("Random Position Offset", EditorStyles.centeredGreyMiniLabel);
		GUI.contentColor = Color.black;
		m_MinOffset = EditorGUILayout.FloatField("Min. Offset", m_MinOffset);
		m_MaxOffset = EditorGUILayout.FloatField("Max. Offset", m_MaxOffset);
		GUI.color = Color.cyan;
		if(GUILayout.Button("Random Offset"))
		{
			Vector3 vec3 = new Vector3();
            vec3.x = Random.Range(m_MinOffset, m_MaxOffset);
			vec3.y = Random.Range(m_MinOffset, m_MaxOffset);
			vec3.z = Random.Range(m_MinOffset, m_MaxOffset);
			m_Environment.transform.localPosition = vec3;
		}
		GUI.color = Color.white;
		EditorGUILayout.EndVertical();
		
		EditorGUILayout.BeginVertical(GUI.skin.box);
		GUI.contentColor = Color.white;
		EditorGUILayout.LabelField("Random Rotation", EditorStyles.centeredGreyMiniLabel);
		GUI.contentColor = Color.black;
		m_MinRotation = EditorGUILayout.FloatField("Min. Offset", m_MinRotation);
		m_MaxRotation = EditorGUILayout.FloatField("Max. Offset", m_MaxRotation);
		GUI.color = Color.magenta;
		if(GUILayout.Button("Random Rotation"))
		{
			Vector3 vec3 = new Vector3();
            vec3.x = Random.Range(m_MinRotation, m_MaxRotation);
			vec3.y = Random.Range(m_MinRotation, m_MaxRotation);
			vec3.z = Random.Range(m_MinRotation, m_MaxRotation);
			m_Environment.transform.rotation = Quaternion.Euler(vec3);
		}
		GUI.color = Color.white;
		EditorGUILayout.EndVertical();
		
		EditorGUILayout.BeginVertical(GUI.skin.box);
		GUI.contentColor = Color.white;
		EditorGUILayout.LabelField("Random Scale", EditorStyles.centeredGreyMiniLabel);
		GUI.contentColor = Color.black;
		m_MinScale = EditorGUILayout.FloatField("Min. Scale", m_MinScale);
		m_MaxScale = EditorGUILayout.FloatField("Max. Scale", m_MaxScale);
		GUI.color = Color.yellow;
		if(GUILayout.Button("Random Scale"))
		{
			Vector3 vec3 = new Vector3();
			vec3.x = Random.Range(m_MinScale, m_MaxScale);
			vec3.y = Random.Range(m_MinScale, m_MaxScale);
			vec3.z = Random.Range(m_MinScale, m_MaxScale);
           	m_Environment.transform.localScale = vec3;
		}
		GUI.color = Color.white;
		EditorGUILayout.EndVertical();
		
		SerializedProperty health = serializedObject.FindProperty("m_Health");
		health.intValue = EditorGUILayout.IntSlider("Health", health.intValue, 0, 100);
		
		EditorGUILayout.Toggle("Draw Bounds", m_DrawBounds);
		EditorGUILayout.ColorField("Bounds Color", m_BoundsColor);
		
		serializedObject.ApplyModifiedProperties();
	}


	// Pour Afficher l'inspecteur par defaut sauf certains parametres, veuillez utiliser DrawPropertiesExcluding(serializedObject, "m_MyVariable");
}
