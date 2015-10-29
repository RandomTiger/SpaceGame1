using UnityEngine;
using System.Collections;

public class AttachPos : MonoBehaviour {

	public GameObject target;

	// Use this for initialization
	void Start () 
	{
	
	}
	
    // Changing camera pos so use late update
	void LateUpdate () {
	
		if (target != null) 
		{
			gameObject.transform.position = 
				new Vector3(target.transform.position.x, target.transform.position.y, gameObject.transform.position.z);
		}
	}
}
