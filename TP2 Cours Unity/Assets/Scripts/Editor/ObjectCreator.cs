using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;
using System;

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
    private Transform m_SpawnTransform;
    private Vector3 m_StartVector = new Vector3(0, 0, 0);
    private Vector3 m_SpawnVector = new Vector3(0, 0, 0);

    private string m_Name;
    private bool m_UseColor;
    private Color m_StartColor;
    private Color m_EndColor;
    private int m_NbToCreate;
    private float m_Spacing;
    private bool m_AutoCenter;
    private bool m_UseLocalRotation;
    private Direction m_Direction;
    private GameObject m_GameObject;
    private bool m_CustomObjectBool = false;

    [MenuItem("Tool/Object Creator")]
    private static void Init()
    {
        GetWindow<ObjectCreator>().Show();
    }

    private void OnGUI()
    {
        GUI.color = Color.white;
        EditorGUILayout.BeginVertical(GUI.skin.box);
        GUI.contentColor = Color.white;
        EditorGUILayout.LabelField("Position", EditorStyles.centeredGreyMiniLabel);

        GUI.contentColor = Color.black;
        GUI.color = Color.yellow;
        EditorGUILayout.BeginVertical(GUI.skin.box);
        EditorGUILayout.TextArea("If none is assigned, then the start position will be (0f, 0f, 0f)", EditorStyles.label);
        EditorGUILayout.EndVertical();
        GUI.color = Color.white;

        m_Transform = EditorGUILayout.ObjectField("Start Position", m_Transform, typeof(Transform), false) as Transform;
        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginVertical(GUI.skin.box);
        GUI.contentColor = Color.white;
        EditorGUILayout.LabelField("Object Selection", EditorStyles.centeredGreyMiniLabel);
        GUI.contentColor = Color.black;
        GUI.color = Color.cyan;
        if (GUILayout.Button("Use Custom Object"))
        {
            m_CustomObjectBool = !m_CustomObjectBool;
        }
        GUI.color = Color.white;
        if (m_CustomObjectBool)
        {
            m_GameObject = EditorGUILayout.ObjectField("Custom Object", m_GameObject, typeof(GameObject), false) as GameObject;
        }
        else
        {
            m_GameObject = null;
        }
        m_PrimitiveType = (PrimitiveType)EditorGUILayout.EnumPopup("Primitive Object", m_PrimitiveType);
        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginVertical(GUI.skin.box);
        GUI.contentColor = Color.white;
        EditorGUILayout.LabelField("Creation Parameters", EditorStyles.centeredGreyMiniLabel);
        GUI.contentColor = Color.black;
        m_Name = EditorGUILayout.TextField("Name", m_Name);
        m_UseColor = EditorGUILayout.Toggle("Use Color?", m_UseColor);
        m_StartColor = EditorGUILayout.ColorField("Start Color", m_StartColor);
        m_EndColor = EditorGUILayout.ColorField("Start Color", m_EndColor);
        m_NbToCreate = EditorGUILayout.IntField("Nb to Create", m_NbToCreate);
        m_Spacing = EditorGUILayout.FloatField("Spacing", m_Spacing);
        m_Direction = (Direction)EditorGUILayout.EnumPopup("Axis Direction", m_Direction);
        m_AutoCenter = EditorGUILayout.Toggle("Auto-center?", m_AutoCenter);

        EditorGUI.BeginDisabledGroup(m_Transform == null);
        m_UseLocalRotation = EditorGUILayout.Toggle("Use localRotation?", m_UseLocalRotation);
        if (m_UseLocalRotation)
        {
            m_Transform.localRotation = m_Transform.localRotation;
        }
        EditorGUI.EndDisabledGroup();

        EditorGUILayout.EndVertical();
        GUI.color = Color.green;
        if (GUILayout.Button("Create", EditorStyles.toolbarButton, GUILayout.Height(400f)))
        {
            Create();
        }

        GUI.color = Color.white;
    }

    private void Create()
    {
        if (m_Transform == null)
        {
            m_SpawnVector = m_StartVector;
        }
        else
        {
            m_SpawnVector = m_Transform.position;
        }

        GameObject[] Objects = new GameObject[m_NbToCreate];



        for (int i = 0; i < m_NbToCreate; i++)
        {
            Renderer renderer;

            if (m_GameObject != null)
            {
                GameObject go = Instantiate(m_GameObject, m_SpawnVector, Quaternion.identity);
                Objects[i] = go;
                Undo.RegisterCreatedObjectUndo(go, "allo");
                go.name = m_Name;
                go.AddComponent(typeof(Environment));

                renderer = go.GetComponent<Renderer>();

                if (m_UseLocalRotation)
                {
                    go.transform.rotation = m_Transform.rotation;
                }
            }
            else
            {
                GameObject go = GameObject.CreatePrimitive(m_PrimitiveType);
                Objects[i] = go;
                Undo.RegisterCreatedObjectUndo(go, "allo");
                go.name = m_Name;
                go.AddComponent(typeof(Environment));

                renderer = go.GetComponent<Renderer>();

                go.transform.position = m_SpawnVector;
                if (m_UseLocalRotation)
                {
                    go.transform.rotation = m_Transform.rotation;
                }
                else
                {
                    go.transform.rotation = Quaternion.identity;
                }
            }

            if (m_UseColor)
            {
                Material newMat = new Material(renderer.sharedMaterial);
                newMat.color = Color.Lerp(m_StartColor, m_EndColor, i / (float)m_NbToCreate);
                renderer.sharedMaterial = newMat;
            }


            switch (m_Direction)
            {
                case Direction.Up:
                    m_SpawnVector.y += m_Spacing;
                    break;
                case Direction.Down:
                    m_SpawnVector.y -= m_Spacing;
                    break;
                case Direction.Left:
                    m_SpawnVector.x -= m_Spacing;
                    break;
                case Direction.Right:
                    m_SpawnVector.x += m_Spacing;
                    break;
                case Direction.Forward:
                    m_SpawnVector.z += m_Spacing;
                    break;
                case Direction.Back:
                    m_SpawnVector.z -= m_Spacing;
                    break;
            }

            Selection.objects = Objects;
        }
    }
}
