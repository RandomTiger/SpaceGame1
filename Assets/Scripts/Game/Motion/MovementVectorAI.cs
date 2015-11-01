using UnityEngine;
using System.Collections.Generic;

public class MovementVectorAI : MonoBehaviour, IMovementVector
{
    public bool mEnable = true;

	public Vector2 GetVec()
	{
        if (mEnable == false)
        {
            return new Vector2(0, 0);
        }

		return new Vector2(Random.value * 2.0f - 1.0f, Random.value * 2.0f - 1.0f);
	}
}
