using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(BoxCollider))]
public class AttributesExemple : MonoBehaviour 
{
	#region Nested classes
	[System.Serializable]
	public class Data
	{
		public int m_ID;
		public GameObject m_GO;
	}
	#endregion

	[Header("Group 1")]
	public float m_MyFloat;

	[SerializeField]
	[Clear]
	private float m_SerializedFloat;

	[HideInInspector]
	public float m_HideFloat;

	[Range(0, 10)]
	[Tooltip("This is used for nothing")]
	public int m_Range;
	
	[Header("Group 2")]
	[Space(10f)]
	[Multiline(5)]
	public string m_Multiline;

	[Clear]
	public string m_ClearString;

	public Data m_Data;

	private void OnGUI()
	{
		GUI.Label(new Rect(0f, 0f, 100f, 20f), "My Label");
	}
}
