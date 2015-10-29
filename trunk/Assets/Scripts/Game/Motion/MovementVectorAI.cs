using UnityEngine;
using System.Collections.Generic;

public class MovementVectorAI : MonoBehaviour, IMovementVector
{
	public Vector2 GetVec()
	{
		return new Vector2(Random.value * 2.0f - 1.0f, Random.value * 2.0f - 1.0f);
	}
}
