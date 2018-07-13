using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class TexturePreProcess : AssetPostprocessor
{
	private void OnPreprocessTexture()
	{
		Debug.Log(assetPath);
		if(assetPath.EndsWith(".png") || assetPath.EndsWith(".jpg"))
		{
			if(assetPath.ToLower().Contains("readwrite"))
			{
				TextureImporter textureImporter = (TextureImporter)assetImporter;
				textureImporter.isReadable = true;
			}
		}
	}
}
