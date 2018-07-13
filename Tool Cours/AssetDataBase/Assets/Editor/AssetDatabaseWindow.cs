using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class AssetDatabaseWindow : EditorWindow
{
	private enum AssetAction
	{
		Copy,
		Move,
		Open,
		Rename
	}

	private const string TEXTURE_PATH = "Assets/Art/BadGuy.gif";
	private const string SPRITE_SHEET_PATH = "Assets/Art/woodman.png";

	private bool m_ShowTexture;
	private Texture m_Texture;

	private AssetAction m_Action = AssetAction.Copy;
	private Object m_Asset;
	private Object m_Directory; 

	private string m_Rename;
	private string m_Animal;
	private string[] m_PopupExample = new string[3] { "Dog", "Cat", "Giraffe"};

	[MenuItem("Tools/Asset Manipulator...")]
	private static void Init()
	{
		GetWindow<AssetDatabaseWindow>().Show();
	}

	private void OnEnable()
	{
		m_Texture = AssetDatabase.LoadAssetAtPath<Texture>(TEXTURE_PATH);
	}

	private void OnGUI()
	{
		ShowAsset();
		ShowActions();
		ShowMisc();
	}

	private void ShowAsset()
	{
		m_Asset = EditorGUILayout.ObjectField("Asset", m_Asset, typeof(Object), false);

		//Si la condition est vrai tout ce qui se trouve à l'intérieur du begin end sera grisé
		EditorGUI.BeginDisabledGroup(m_Asset == null);
		if(GUILayout.Button("Show Asset Path"))
		{
			//Sert à nous donner le chemin d'accès d'un objet
			string assetPath = AssetDatabase.GetAssetPath(m_Asset);
			// *Permet de nous donner l'extension d'un chemin d'accès
			string extension = Path.GetExtension(assetPath);
			Debug.Log("Asset Path: " + assetPath + " | Extension: " + extension);
		}
		EditorGUI.EndDisabledGroup();
	}

	private void ShowActions()
	{
		EditorGUILayout.BeginVertical(GUI.skin.box);
		EditorGUILayout.LabelField("Action", EditorStyles.centeredGreyMiniLabel);
		//Sert à montrer en liste déroulante/dropdown un enum
		m_Action = (AssetAction)EditorGUILayout.EnumPopup("Action", m_Action);
		//Sert à montrer en liste déroulante/dropdown un string à partir d'un array de string
		int index = ArrayUtility.IndexOf(m_PopupExample, m_Animal);
		index = EditorGUILayout.Popup("Animal", index, m_PopupExample);
		if(index != -1)
		{
			m_Animal = m_PopupExample[index];
		}
		ShowParameters();
		ShowActionButton();
		EditorGUILayout.EndVertical();
	}

	private void ShowParameters()
	{
		switch(m_Action)
		{
			case AssetAction.Copy:
			case AssetAction.Move:
				EditorGUI.BeginChangeCheck();
				m_Directory = EditorGUILayout.ObjectField("Directory", m_Directory, typeof(Object), false);
				if(EditorGUI.EndChangeCheck())
				{
					if(m_Directory != null)
					{
						string assetPath = AssetDatabase.GetAssetPath(m_Directory);
						bool isValid = AssetDatabase.IsValidFolder(assetPath);
						if(!isValid)
						{
							Debug.LogWarning("This isn't a directory!");
							m_Directory = null;
						}
					}
				}
				break;
			case AssetAction.Rename:
				m_Rename = EditorGUILayout.TextField("Rename", m_Rename);
				break;			
		}
	}

	private void ShowActionButton()
	{
		EditorGUI.BeginDisabledGroup(m_Asset == null);
		if(GUILayout.Button(m_Action.ToString() + " asset"))
		{
			string assetPath = AssetDatabase.GetAssetPath(m_Asset);
			bool needSave = false;

			switch(m_Action)
			{
				case AssetAction.Copy:
				case AssetAction.Move:
					if(m_Directory != null)
					{
						string directoryPath = AssetDatabase.GetAssetPath(m_Directory);
						directoryPath += "/" + GetAssetFullName(assetPath);
						//assetPath == directoryPath
						if(assetPath.Equals(directoryPath))
						{
							Debug.LogError("Cannot " + m_Action.ToString() + " since path are the same!");
							return;
						}

						if(m_Action == AssetAction.Copy)
						{ 
							needSave = AssetDatabase.CopyAsset(assetPath, directoryPath);
						}
						else
						{
							needSave = string.IsNullOrEmpty(AssetDatabase.MoveAsset(assetPath, directoryPath));
						}
					}
					break;
				case AssetAction.Open:
					//quel asset? Quelle ligne(facultatif)
					AssetDatabase.OpenAsset(m_Asset, 50);
					break;
				case AssetAction.Rename:
					if(!string.IsNullOrEmpty(m_Rename))
					{
						needSave = string.IsNullOrEmpty(AssetDatabase.RenameAsset(assetPath, m_Rename));
					}
					break;
			}

			if(needSave)
			{
				AssetDatabase.SaveAssets();
				AssetDatabase.Refresh();
			}
		}
		EditorGUI.EndDisabledGroup();
	}

	private void ShowMisc()
	{
		EditorGUILayout.BeginVertical(GUI.skin.box);
		EditorGUILayout.LabelField("Misc", EditorStyles.centeredGreyMiniLabel);

		m_ShowTexture = EditorGUILayout.Toggle("Show Message?", m_ShowTexture);
		if(m_ShowTexture)
		{
			GUILayout.Label(m_Texture);
		}
		if(GUILayout.Button("Load Sprites"))
		{
			Object[] allObjects = AssetDatabase.LoadAllAssetsAtPath(SPRITE_SHEET_PATH);
			foreach(Object obj in allObjects)
			{
				Debug.Log("Name: " + obj.name + " | Type: " + obj.GetType().Name);
			}
		}

		EditorGUI.BeginDisabledGroup(m_Asset == null);
		if(GUILayout.Button("Get Dependencies"))
		{
			string assetPath = AssetDatabase.GetAssetPath(m_Asset);
			string[] dependencies = AssetDatabase.GetDependencies(assetPath, true);
			foreach(string s in dependencies)
			{
				Debug.Log(s);
			}
		}
		EditorGUI.EndDisabledGroup();

		EditorGUILayout.EndVertical();
	}

	private string GetAssetFullName(string aAssetPath)
	{
		string[] splits = aAssetPath.Split('/');
		return splits[splits.Length-1];
	}
}
