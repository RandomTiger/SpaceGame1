using UnityEngine;
using System.Collections;

public class Hull : MonoBehaviour, IDamageable
{
    [SerializeField] private float mHealth = 100;

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

        // Return any unused damage
        return -Mathf.Min(mHealth, 0.0f);
    }
}
