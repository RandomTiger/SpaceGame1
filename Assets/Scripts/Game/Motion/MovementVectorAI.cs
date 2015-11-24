using UnityEngine;
using System.Collections.Generic;

public class MovementVectorAI : MonoBehaviour, IMovementVector
{
    public bool mEnable = true;

	public Vector2 GetVec()
	{
        if (mEnable == false)
        {
            return Vector2.zero;
        }

        return GetDesiredSteerVelocity();
    }
    Vector3 GetDesiredSteerVelocity()
    {
        return GetComponent<SteeringBehaviours>().Wander();
    }
}
