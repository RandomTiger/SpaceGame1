using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour
{

	public float mAmount = 100;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(mAmount <= 0)
		{
			Destroy(gameObject);
		}
	}

    public float Damage(float amount)
    {
        mAmount -= amount;
        // Return any unused damage
        return -Mathf.Min(mAmount, 0.0f);
    }
}
