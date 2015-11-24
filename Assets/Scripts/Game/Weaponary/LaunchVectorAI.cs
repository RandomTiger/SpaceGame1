using UnityEngine;
using System.Collections.Generic;

public class LaunchVectorAI : MonoBehaviour, ILaunchVector 
{
    public bool mEnabled = true;

	public Vector2 GetVec()
	{
        if (mEnabled && Random.value > 0.5)
        {
            return transform.up;
        }
        
        return Vector2.zero;
	}
}

