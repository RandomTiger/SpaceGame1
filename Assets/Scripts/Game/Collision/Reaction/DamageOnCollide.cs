using UnityEngine;
using System.Collections;

public class DamageOnCollide : MonoBehaviour
{
	void OnCollisionEnter2D(Collision2D collision)
	{
		Payload payload = collision.collider.GetComponent<Payload>();
		Health health = GetComponent<Health> ();

		if(payload != null && health != null)
		{
			health.mAmount -= payload.mHull;
		}
	}
	
	void Update()
	{
	}
}