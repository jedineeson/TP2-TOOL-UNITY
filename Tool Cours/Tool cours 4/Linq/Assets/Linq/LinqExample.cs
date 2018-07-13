using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LinqExample : MonoBehaviour 
{
	public List<GameObject> m_Objects;

	private void Update()
	{	
		//Comment avoir le premier élément de ma liste
		GameObject closestGO = m_Objects
			.OrderBy(pikachu => Vector3.Distance(transform.position, pikachu.transform.position))
			.FirstOrDefault();

		//2 examples pour le dernier élément de ma liste:
		GameObject furthestGO = m_Objects
			.OrderBy(pikachu => Vector3.Distance(transform.position, pikachu.transform.position))
			.LastOrDefault();

		GameObject furthestGO2 = m_Objects
			.OrderByDescending(pikachu => Vector3.Distance(transform.position, pikachu.transform.position))
			.FirstOrDefault();

		//Comment avoir un élément à une position X dans ma liste
		GameObject secondGO = m_Objects
			.OrderBy(pikachu => Vector3.Distance(transform.position, pikachu.transform.position))
			.Skip(1)
			.FirstOrDefault();

		//Comment avoir les X premiers éléments de ma liste
		GameObject[] firstTwoObjects = m_Objects
			.OrderBy(pikachu => Vector3.Distance(transform.position, pikachu.transform.position))
			.Take(2)
			.ToArray();

		//Comment filtrer les éléments selon une condition
		GameObject[] filteredObjects = m_Objects
			.Where(pikachu => pikachu.name.Contains("Cube"))
			.ToArray();

		//Est-ce que je contient un élément selon la condition
		bool hasCube = m_Objects
			.Any(go => go.name == "Cube3");
		
		//Comment enlever les doublons d'une liste
		List<int> nonSenseInts = new List<int>() {1, 2, 3, 4, 3, 2, 1};
		int[] distinctInts = nonSenseInts
		.Distinct()
		.ToArray();




		Debug.Log("Plus proche: " + closestGO.name);
		Debug.Log("Plus loin: " + furthestGO.name);
		Debug.Log("Plus loin: " + furthestGO2.name);
		Debug.Log("Deuxième plus proche: " + secondGO.name);
		foreach(GameObject go in firstTwoObjects)
		{
			Debug.Log("Les 2 plus proches: " + go.name);
		}
		foreach(GameObject go in filteredObjects)
		{
			Debug.Log("Contiennent le mot 'cube': " + go.name);
		}
		Debug.Log("Cube3 exist: " + hasCube);
		foreach(int i in distinctInts)
		{
			Debug.Log("Occurences uniques: " + i);
		}
	}

	private GameObject GetClosestGameObject()
	{
		float minDistance = Mathf.Infinity;
		GameObject closestGO = null;

		foreach(GameObject go in m_Objects)
		{
			float distance = Vector3.Distance(transform.position, go.transform.position);
			if(distance < minDistance)
			{
				minDistance = distance;
				closestGO = go;
			}
		}

		return closestGO;
	}

}
