using UnityEngine;
using System.Collections.Generic;

[RequireComponent (typeof (Coloriser))]
[RequireComponent (typeof (ILaunchVector))]
[RequireComponent (typeof (ColliderQueryChild))]
public class Launcher : MonoBehaviour {

    [SerializeField]
    private GameObject m_Projectile;
    [SerializeField]
    private float m_Cooldown = 0.0f;
    [SerializeField]
    private float m_LaunchForceFactor = 5000.0f;

    [SerializeField]
    private float m_Counter; 
		
	ILaunchVector m_LaunchVector;
	
	// Use this for initialization
	void Start () 
	{
		m_LaunchVector = GetComponent<ILaunchVector> ();
	}
	
	// Fixed update ensures a regular burst of fire without varying gaps
	void FixedUpdate () 
	{
		m_Counter -= Time.fixedDeltaTime;

		Vector2 lanchDir = m_LaunchVector.GetVec ();
		
		float threshold = 0.2f;
		if (Mathf.Abs (lanchDir.x) > threshold || Mathf.Abs (lanchDir.y) > threshold) 
		{
			if(m_Counter <= 0.0f)
			{
				lanchDir.Normalize();
				Fire (lanchDir);
				m_Counter = m_Cooldown;
			}
		}
	}

	void Fire(Vector2 dir)
	{
		GameObject newProjectile = Instantiate(m_Projectile, transform.position, Quaternion.identity) as GameObject;

		// Apply force
		Vector2 force = dir * m_LaunchForceFactor;

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
