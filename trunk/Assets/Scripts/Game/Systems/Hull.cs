using UnityEngine;
using System.Collections;

public class Hull : MonoBehaviour, IDamageable
{
    public Object mParticlesDeath = null;
	public float mHealth = 100;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(mHealth <= 0)
		{
			Destroy(gameObject);
		}
	}

    public float Damage(float amount)
    {
        float before = mHealth;
        mHealth -= amount;

        if (before > 0 && mHealth <= 0 && mParticlesDeath != null)
        {
            GameObject particleSys = Instantiate(mParticlesDeath, transform.position, Quaternion.identity) as GameObject;
            particleSys.GetComponent<ParticleSystem>().Play();
        }

        // Return any unused damage
        return -Mathf.Min(mHealth, 0.0f);
    }
}
