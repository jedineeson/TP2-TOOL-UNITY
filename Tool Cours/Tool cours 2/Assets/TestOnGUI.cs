using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestOnGUI : MonoBehaviour 
{
    
	public Texture m_MyTexture;
	public Texture2D m_BoxBackGround;
	public Rect m_WindowRect = new Rect(50f, 50f, 200f, 100f);

	public string[] m_MyStrings;

	private bool m_ShowTexture;	
	private int m_SelectionGridIndex;

	private float m_HorizontalSliderValue = 0.5f;
	private float m_VerticalSliderValue = 0.25f;
	private string m_MyPassword = "";
	private Vector2 m_ScrollPosition;
	private Rect m_BoxRect;

	private void OnGUI()
	{
		m_BoxRect = new Rect(0f, 0f, Screen.width * m_HorizontalSliderValue, Screen.height * m_VerticalSliderValue);
		Rect scrollRect = m_BoxRect;
		scrollRect.height = 300f;
		Rect scrollViewRect = scrollRect;
		scrollViewRect.width += 20f;
		scrollViewRect.height += 20f;
		m_ScrollPosition = GUI.BeginScrollView(scrollRect, m_ScrollPosition, scrollViewRect);

		GUIStyle boxStyle = new GUIStyle(GUI.skin.box);
		if(m_BoxBackGround != null)
		{
			boxStyle.normal.background = m_BoxBackGround;
		}
		GUI.Box(m_BoxRect, "My Box", boxStyle);
		m_BoxRect.height = 20f;

		SetNewLine();
		
		GUIStyle labelStyle = new GUIStyle(GUI.skin.label);
		GUIContent myContent = new GUIContent("My Label", "ThisIsALabel");
		Vector2 labelSize = labelStyle.CalcSize(myContent); 
		//labelStyle.alignment = TextAnchor.MiddleCenter;
		Rect labelRect = GetCenteredRect(m_BoxRect, labelSize.x);

		GUI.Label(labelRect, myContent, labelStyle);

		SetNewLine();

		Rect HorizontalSliderRect = GetCenteredRect(m_BoxRect, m_BoxRect.width*0.5f);
		m_HorizontalSliderValue = GUI.HorizontalSlider(HorizontalSliderRect, m_HorizontalSliderValue, 0.1f, 1f);

		SetNewLine(100f);

		Rect VerticalSliderRect = GetCenteredRect(m_BoxRect, 20f);
		m_VerticalSliderValue = GUI.VerticalSlider(VerticalSliderRect, m_VerticalSliderValue, 0.1f, 1f);

		SetNewLine();

		Rect passwordRect = GetCenteredRect(m_BoxRect);
		m_MyPassword = GUI.PasswordField(passwordRect, m_MyPassword, '*', 10);

		SetNewLine();

		Rect textRect = GetCenteredRect(m_BoxRect);
		m_MyPassword = GUI.TextField(textRect, m_MyPassword); 

		SetNewLine(100f);

		Rect selectionRect = GetCenteredRect(m_BoxRect, m_BoxRect.width*0.5f);
		m_SelectionGridIndex = GUI.SelectionGrid(selectionRect, m_SelectionGridIndex, m_MyStrings, 2);

		GUI.EndScrollView();

		m_WindowRect = GUI.Window(0, m_WindowRect, ShowWindow, "Window"); 

		ShowGroup();		
	}

	private void ShowWindow(int aWindowID)
	{
		if (GUI.Button(new Rect(0f, 20f, 100f, 20f), "Ben Button"))
		{
			Debug.Log("Oppressed Ben Button");
		}

		Rect dragRect = m_WindowRect;
		dragRect.x = 0f;
		dragRect.y = 0f;
		GUI.DragWindow(dragRect);
	}

	private Rect GetCenteredRect(Rect aGroupRect, float aWidth = -1f)
	{
		Rect rect = aGroupRect;
		
		//opérateur ternaire
		//			 condition	 ? if true			  if false
		rect.width = aWidth < 0f ? aGroupRect.width : aWidth;
		

		rect.x += aGroupRect.width*0.5f - rect.width*0.5f;
		return rect;
	}

	private void SetNewLine(float aHeight = 20f, float aSpacing = 5f)
	{
		m_BoxRect.y += m_BoxRect.height + aSpacing;
		m_BoxRect.height = aHeight;
	}

	private void ShowGroup()
	{
		Rect groupRect = new Rect(350f, 10f, m_MyTexture.width, m_MyTexture.height);
		
		GUI.BeginGroup(groupRect);
		{
			groupRect.x = 0f;
			groupRect.y = 0f;
			GUI.Box(groupRect, "");
			Rect buttonRect = new Rect(0f, 0f, 50f, 20f);
			
			bool hasBeenPressed = GUI.Button(buttonRect, "Button");
			
			if(hasBeenPressed)
			{
				m_ShowTexture = !m_ShowTexture;
			}
			//if(GUI.Button(buttonRect, "Button"))
			//{
			//	Debug.Log("Button Pressed");
			//}

			if(m_ShowTexture)
			{
				Rect textureRect = new Rect(0f, 0f, m_MyTexture.width, m_MyTexture.height);
				GUI.DrawTexture(textureRect, m_MyTexture);
			}
		}
		GUI.EndGroup();
	}
}
