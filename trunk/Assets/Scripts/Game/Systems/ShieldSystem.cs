using UnityEngine;
using System.Collections;

public class ShieldSystem : MonoBehaviour {

    Shield[] mShieldList;

	// Use this for initialization
	void Start ()
    {
        mShieldList = GetComponentsInChildren<Shield>(true);
    }
	
	// Update is called once per frame
	void Update ()
    {
        foreach (Shield shield in mShieldList)
        {
            if (shield.enabled == false)
            {
                continue;
            }

            if(shield.GetComponent<IShieldBool>().GetRequest())
            {
                // todo: Boost this shield system at the cost of the others
            }
        }
    }
}
