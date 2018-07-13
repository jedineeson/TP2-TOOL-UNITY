using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGUITest : MonoBehaviour
{
    public Texture2D m_BoxBackground;
    public Color m_BoxStartColor = Color.white;
    public Color m_BoxEndColor = Color.white;
    public string[] m_ToolBarString;

    private bool m_CheatsToggled;
    private bool m_MyToggle;
    private float m_MyFloat;
    private string m_MyText;

    private int m_ToolBarInt = -1;
    private Vector2 m_ScrollPosition;

    private Texture2D m_TextureCopy;
    private Color m_PreviousColor;
    private Rect m_MyRect;

    //region dans le code(section dans les fonctions)
    #region Getters
    private Color CurrentColor
    {
        get
        {
            return Color.Lerp(m_BoxStartColor, m_BoxEndColor, m_MyFloat);
        }
    }
    #endregion

    private void Start()
    {
        SetupTexture();
        m_PreviousColor = CurrentColor;
        SetColor();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            m_CheatsToggled = !m_CheatsToggled;
        }

        if (!m_PreviousColor.Equals(CurrentColor))
        {
            SetColor();
        }

        m_PreviousColor = CurrentColor;

    }


    private void OnGUI()
    {
        //if (m_CheatsToggled)
        //{
        //    if (GUI.Button(new Rect(10f, 10f, 150f, 100f), "Button"))
        //    {
        //        Debug.Log("Button click");
        //    }
        //}


        if (!m_CheatsToggled)
        {
            return;
        }


        GUIStyle style = new GUIStyle(GUI.skin.box);

        if (m_TextureCopy != null)
        {
            style.normal.background = m_TextureCopy;
        }

        float relativeWidth = Screen.width * 0.5f;

        m_ScrollPosition = GUI.BeginScrollView(new Rect(10f, 10f, relativeWidth - 50f, 250f), m_ScrollPosition, new Rect(10f, 10f, relativeWidth, 350f));

        //Box
        GUI.Box(new Rect(10f, 10f, relativeWidth, 500f), "", style);

        //Fait un rectangle
        m_MyRect = new Rect(10f, 10f, 150f, 100f);

        //Bouton (Debug)
        if (GUI.Button(new Rect(10f, 10f, 150f, 100f), "Button"))
        {
            Debug.Log("Button click");
        }

        SetNewLine(20f);
        //Nom du Slider
        GUI.Label(m_MyRect, "MySlider");

        SetNewLine(20f);
        //Slider
        m_MyFloat = GUI.HorizontalSlider(m_MyRect, m_MyFloat, 0f, 1f);

        SetNewLine(50f);
        //TextField
        m_MyText = GUI.TextField(m_MyRect, m_MyText);


        SetNewLine(20f);
        //Toggle
        m_MyToggle = GUI.Toggle(m_MyRect, m_MyToggle, "Toggle");

        SetNewLine(20f);
        int currentInt = m_ToolBarInt;
        m_ToolBarInt = GUI.Toolbar(m_MyRect, m_ToolBarInt, m_ToolBarString);

        if (currentInt != m_ToolBarInt)
        {
            OnToolBarChanged();
        }

        GUI.EndScrollView();
    }

    private void OnToolBarChanged()
    {
        Vector3 newScale = Vector3.zero;
        switch (m_ToolBarInt)
        {
            case 0:
                newScale = Vector3.one;
                break;
            case 2:
                newScale = Vector3.one*2;
                break;
        }

        transform.localScale = newScale;
    }

    private void SetNewLine(float i_Height = 100f, float i_Spacing = 15f)
    {
        m_MyRect.y += m_MyRect.height + i_Spacing;
        m_MyRect.height = i_Height;
    }

    private void SetColor()
    {
        if (m_TextureCopy == null)
        {
            return;
        }

        for(int y = 0; y < m_BoxBackground.height; y++)
        {
            for (int x = 0; x < m_BoxBackground.width; x++)
            {
                m_TextureCopy.SetPixel(x, y, CurrentColor);
            }
        }

        m_TextureCopy.Apply();
    }

    private void SetupTexture()
    {
        if (m_BoxBackground != null)
        {
            Color32[] pixels = m_BoxBackground.GetPixels32();
            System.Array.Reverse(pixels);
            m_TextureCopy = new Texture2D(m_BoxBackground.width, m_BoxBackground.height);
            m_TextureCopy.SetPixels32(pixels);
            m_TextureCopy.Apply();
        } 
    }
}
