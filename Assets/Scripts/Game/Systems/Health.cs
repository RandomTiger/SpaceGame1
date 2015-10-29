using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {

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

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Payload>())
        {
            mAmount -= 20.0f;
        }
    }
}
