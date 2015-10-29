using UnityEngine;
using System.Collections;

public class DeathOnTimeOut : MonoBehaviour
{
	public float mTimeout = 5.0f;
	
	void Update()
	{
		mTimeout -= Time.deltaTime;
		if(mTimeout < 0)
		{
			Destroy(gameObject);
		}
	}
}