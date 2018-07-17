using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DragNDropExample : EditorWindow
{
	[MenuItem("Tools/DragAndDrop...")]
	private static void Init()
	{
		GetWindow<DragNDropExample>().Show();
	}

	private void OnGUI()
	{
		DrawDragZone("Zone 1", aOnDragPerform: OnDragPerform1);
		GUILayout.Space(15f);
		DrawDragZone("Zone 2", aOnDragPerform: OnDragPerform2);
	}

	private void OnDragPerform1(Object[] aObjects)
	{
		Debug.Log(aObjects.Length);
	}
	
	private void OnDragPerform2(Object[] aObjects)
	{
		foreach(Object obj in aObjects)
		{
			Debug.Log(obj.name);
		}
	}


	private void DrawDragZone(string aLabel, float aHeight = 50f, System.Action<Object[]> aOnDragPerform = null)
	{
		Rect dragArea = GUILayoutUtility.GetRect(0f, aHeight);
		GUI.Box(dragArea, aLabel);

		Event curEvent = Event.current;

		if(!dragArea.Contains(curEvent.mousePosition))
		{
			return;
		}

		if(curEvent.type == EventType.DragUpdated || curEvent.type == EventType.DragPerform)
		{
			DragAndDrop.visualMode = DragAndDropVisualMode.Copy;

			if (curEvent.type == EventType.DragPerform)
			{
				if(aOnDragPerform != null)
				{
					aOnDragPerform(DragAndDrop.objectReferences);
				}
			}
		}
	}
}
