﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuizmoExample : MonoBehaviour 
{
	public MeshFilter m_MeshFilter;
	public Vector3 m_CubeSize = Vector3.one;
	public Transform m_Target;
	public float m_SphereRadius = 1f;

	public float m_Fov = 60f;
	public float m_MinRange = 1f;
	public float m_MaxRange = 100f;
	public float m_Aspect = 0.75f;

	private void OnDrawGizmosSelected()
	{
		Color gizmosColor = Color.cyan;
		gizmosColor.a = 0.25f;
		Gizmos.color = gizmosColor;

		Gizmos.DrawCube(transform.position, m_CubeSize);

		Gizmos.DrawWireCube(transform.position + transform.forward * 5f, m_CubeSize);
	
		if(m_Target != null)
		{
			Gizmos.color = Color.magenta;
			Gizmos.DrawLine(transform.position, m_Target.position);	
		}

		if(m_MeshFilter != null)
		{
			Gizmos.color = Color.red;
			Gizmos.DrawMesh(m_MeshFilter.sharedMesh, transform.position + transform.right * 5f, Quaternion.identity);
		}

		Gizmos.color = Color.green;
		Gizmos.DrawRay(transform.position, transform.up*1000f);
		
		Gizmos.color = Color.yellow;
		Gizmos.DrawSphere(transform.position + transform.forward * 2f, m_SphereRadius);

		Gizmos.color = Color.blue;
		Gizmos.matrix = transform.localToWorldMatrix;
		Gizmos.DrawFrustum(transform.position, m_Fov, m_MaxRange, m_MinRange, m_Aspect);
	}

}
