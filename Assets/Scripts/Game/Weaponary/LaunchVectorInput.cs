using UnityEngine;
using System.Collections.Generic;

[RequireComponent (typeof (AxisInput))]
public class LaunchVectorInput : MonoBehaviour, ILaunchVector
{
	AxisInput mInput;
	
	// Use this for initialization
	void Start () 
	{
		mInput = GetComponent<AxisInput> ();
	}

	public Vector2 GetVec()
	{
		return new Vector2(mInput.x, mInput.y);
	}
}