using UnityEngine;
using System.Collections.Generic;

public class ColliderQueryChild : MonoBehaviour 
{
	ColliderRoot mRoot;

	public void SetRoot(ColliderRoot root)
	{
		mRoot = root;
	}

    public ColliderRoot GetRoot()
    {
        return mRoot;
    }

    public List<Collider2D> GetColliderList()
	{
		return mRoot.GetColliderList ();
	}
}