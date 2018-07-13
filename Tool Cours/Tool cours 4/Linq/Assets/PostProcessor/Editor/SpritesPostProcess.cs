using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class SpritesPostProcess : AssetPostprocessor 
{
	private void OnPostprocessSprites(Texture2D aTexture2D, Sprite[] aSprites)
	{
		Debug.Log(aSprites.Length);
	}

	private void OnPostprocessTexture(Texture2D aTexture)
	{
		Debug.Log(aTexture.width + ", " + aTexture.height);
	}
}
