using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class AllAssetsPostProcess : AssetPostprocessor 
{
	private static void OnPostprocessAllAssets(string[] aImportedAssets, string[] aDeletedAssets, string[] aMovedAssets, string[] aMovedFromAssets)
	{
		foreach(string s in aImportedAssets)
		{
			Debug.Log("Reimported Asset: " + s);
		}
		
		foreach(string s in aDeletedAssets)
		{
			Debug.Log("Deleted Asset: " + s);
		}
		
		for (int i = 0; i < aMovedAssets.Length; i++)
		{
			Debug.Log("Deleted Asset: " + aMovedAssets[i] + " form: " + aMovedFromAssets[i]);
			
		}
	}
}
