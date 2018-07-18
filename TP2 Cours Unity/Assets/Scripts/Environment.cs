using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment : MonoBehaviour 
{
    [SerializeField]
    private int m_Health = 50;

    private void OnDrawGizmosSelected()
	{
		Color gizmosColor = Color.green;
		//gizmosColor.a = 0.25f;
		Gizmos.color = gizmosColor;

        //if()
		Gizmos.DrawWireCube(transform.position, transform.localScale);
	}
}
