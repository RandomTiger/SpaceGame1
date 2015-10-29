using UnityEngine;
using System.Collections.Generic;

public class LaunchVectorAI : MonoBehaviour, ILaunchVector 
{
	public Vector2 GetVec()
	{
        if (Random.value > 0.5)
        {
            return transform.up;
        }
        
        return new Vector2();
	}
}

