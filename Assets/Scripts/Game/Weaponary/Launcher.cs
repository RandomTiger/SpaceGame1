using UnityEngine;
using System.Collections.Generic;

[RequireComponent (typeof (Coloriser))]
[RequireComponent (typeof (ILaunchVector))]
[RequireComponent (typeof (ColliderQueryChild))]
public class Launcher : MonoBehaviour {

	public GameObject mProjectile;
	public float mCooldown = 0.0f;
    float mLaunchForceFactor = 5000.0f;

    public float mCounter; 
		
	ILaunchVector mLaunchVector;
	
	// Use this for initialization
	void Start () 
	{
		mLaunchVector = GetComponent<ILaunchVector> ();
	}
	
	// Fixed update ensures a regular burst of fire without varying gaps
	void FixedUpdate () 
	{
		mCounter -= Time.fixedDeltaTime;

		Vector2 lanchDir = mLaunchVector.GetVec ();
		
		float threshold = 0.2f;
		if (Mathf.Abs (lanchDir.x) > threshold || Mathf.Abs (lanchDir.y) > threshold) 
		{
			if(mCounter <= 0.0f)
			{
				lanchDir.Normalize();
				Fire (lanchDir);
				mCounter = mCooldown;
			}
		}
	}

	void Fire(Vector2 dir)
	{
		GameObject newProjectile = Instantiate(mProjectile, transform.position, Quaternion.identity) as GameObject;

		// Apply force
		Vector2 force = dir * mLaunchForceFactor;

		// Set colour (faction based only for now)
		newProjectile.GetComponentInChildren<SpriteRenderer> ().color = GetComponent<Coloriser>().color;
		
		//Projectile projectile = newProjectile.GetComponent<Projectile> ();
		newProjectile.GetComponent<Rigidbody2D> ().AddForce (force);

		// Ignore all colliders of whole object instance
		Collider2D projectileCollider = newProjectile.GetComponent<Collider2D> ();
		List<Collider2D> colliderList = GetComponent<ColliderQueryChild> ().GetColliderList ();

		foreach(Collider2D collider in colliderList)
		{
			Physics2D.IgnoreCollision(collider, projectileCollider);
		}
	}
}
