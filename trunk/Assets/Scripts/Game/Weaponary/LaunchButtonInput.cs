using UnityEngine;
using System.Collections.Generic;

public class LaunchButtonInput : MonoBehaviour, ILaunchVector
{
    public GenericPad mInput = GenericPad.RTrigger;
    PlayerController mController;
	
	// Use this for initialization
	void Start () 
	{
        mController = GetComponent<PlayerController> ();
	}

	public Vector2 GetVec()
	{
        if(mController.m_Controller.GetDown(mInput))
        {
            return transform.up;
        }

        return new Vector2(0,0);
	}
}