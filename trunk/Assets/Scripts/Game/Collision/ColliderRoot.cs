using UnityEngine;
using System.Collections.Generic;

public class ColliderRoot : MonoBehaviour 
{
	List<Collider2D> colliderList = new List<Collider2D>();

	void Awake()
	{
		Collider2D [] colliders = GetComponentsInChildren<Collider2D>();
		if (colliders != null) 
		{
			colliderList.AddRange(colliders);
		}

		ColliderQueryChild [] colliderQueryChildren = GetComponentsInChildren<ColliderQueryChild>();
		foreach (ColliderQueryChild child in colliderQueryChildren) 
		{
			child.SetRoot(this);
		}
	}

	public List<Collider2D> GetColliderList()
	{
		return colliderList;
	}
}