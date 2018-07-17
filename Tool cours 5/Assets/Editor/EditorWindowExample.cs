using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

public class EditorWindowExample : EditorWindow 
{
	private GameObject m_Target;

	private string m_Filter;

	private bool[] m_Foldouts;
	private Dictionary<Transform, Component[]> m_Components; 

	[MenuItem("Tools/Editor Example")]
	private static void Init()
	{
		EditorWindowExample example = GetWindow<EditorWindowExample>();
		example.Show();
	}

	private void OnGUI()
	{
		EditorGUI.BeginChangeCheck();
		m_Target = (GameObject)EditorGUILayout.ObjectField("Target", m_Target, typeof(GameObject), true);

		if(EditorGUI.EndChangeCheck() || (m_Target != null && m_Components == null))
		{
			if(m_Target != null)
			{
				Transform[] allTransforms = m_Target.GetComponentsInChildren<Transform>(true);
				m_Components = new Dictionary<Transform, Component[]>();
				
				for (int i = 0; i < allTransforms.Length; i++)
				{
					Component[] components = allTransforms[i].GetComponents<Component>();
					m_Components.Add(allTransforms[i], components);
				}
				m_Foldouts = new bool[allTransforms.Length];
			}
		}

		EditorGUI.BeginDisabledGroup(m_Target == null);

		m_Filter = EditorGUILayout.TextField("Filter", m_Filter);

		EditorGUILayout.BeginHorizontal();
		GUI.color = Color.cyan;		
		if(GUILayout.Button("Expand All"))
		{
			for(int i = 0; i < m_Foldouts.Length; i++)
			{
				m_Foldouts[i] = true;
			}
		}
		GUI.color = Color.magenta;
		if(GUILayout.Button("Collapse All"))
		{
			for(int i = 0; i < m_Foldouts.Length; i++)
			{
				m_Foldouts[i] = false;
			}
		}
		GUI.color = Color.white;
		EditorGUILayout.EndHorizontal();

		EditorGUI.EndDisabledGroup();

		ShowComponents();
	}

	private void ShowComponents()
	{
		if(m_Target == null || m_Components == null)
		{
			return;
		}

		int index = 0;

		foreach(KeyValuePair<Transform, Component[]> kvp in m_Components)
		{
			bool isMissing = isComponentMissing(kvp.Value);

			GUI.color = isMissing ? Color.red : Color.white;

			EditorGUILayout.BeginHorizontal(GUI.skin.box);
			m_Foldouts[index] = EditorGUILayout.Foldout(m_Foldouts[index], kvp.Key.name, true);
			EditorGUILayout.EndHorizontal();
			//EditorGUI.indentLevel++;

			GUI.color = Color.white;

			if(m_Foldouts[index] || !string.IsNullOrEmpty(m_Filter))
			{				
				for(int i = 0; i < kvp.Value.Length; i++)
				{
					Component comp = kvp.Value[i];
					if(!IsComponentContains(comp))
					{
						continue;
					}
					
					EditorGUILayout.BeginHorizontal();
					GUILayout.Space(20f * (EditorGUI.indentLevel+1));

					string compName = GetComponentName(comp);
					
					GUIStyle btnStyle = new GUIStyle(EditorStyles.toolbarButton);
					btnStyle.alignment = TextAnchor.MiddleLeft;
					
					GUI.color = comp == null ? Color.red : Color.white;
					
					if(GUILayout.Button(compName, btnStyle))
					{
						Selection.activeGameObject = kvp.Key.gameObject;
						EditorGUIUtility.PingObject(kvp.Key.gameObject);
					}
					GUILayout.Space(20f * (EditorGUI.indentLevel+1));
					EditorGUILayout.EndHorizontal();
				}				
			}

			//EditorGUI.indentLevel--;

			index++;
		}
	}

	private bool IsComponentContains(Component aComponent)
	{
		if(string.IsNullOrEmpty(m_Filter))
		{
			return true;
		}

		string compName = GetComponentName(aComponent);

		return compName.ToLower().Contains(m_Filter.ToLower());	

	}

	private string GetComponentName(Component aComponent)
	{
		return aComponent == null ? "Missing Script" : aComponent.GetType().Name;
	}

	private bool isComponentMissing(Component[] aComponents)
	{
		return aComponents.Any(comp => comp == null);
	}
}
