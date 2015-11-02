using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Payload : MonoBehaviour {

    public enum Target
    {
        Shield,
        Hull
    }

    [System.Serializable]
    public struct Damage
    {
        public Target mTarget;
        public float mAmount;
    }

    public Damage [] mDamageList = null;

    private List<Shield> mHitShieldList = new List<Shield>();
    private List<Hull> mHitHullList = new List<Hull>();

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        bool bulletStillGoing = false;

        for(int i = 0; i < mDamageList.Length; i++)
        {
            Damage dam = mDamageList[i];

            if (dam.mTarget == Target.Shield && dam.mAmount > 0)
            {
                // Dish out shield damage first
                foreach (Shield shield in mHitShieldList)
                {
                    dam.mAmount = shield.Damage(dam.mAmount);
                }
            }
            else if (dam.mTarget == Target.Hull && dam.mAmount > 0)
            {
                // Dish out hull damage onto the shields
                foreach (Shield shield in mHitShieldList)
                {
                    dam.mAmount = shield.Damage(dam.mAmount);
                }

                // If there is any left deal out damage onto the hull
                foreach (Hull hull in mHitHullList)
                {
                    dam.mAmount = hull.Damage(dam.mAmount);

                    // We've hit hull, time to stop
                    dam.mAmount = 0; 
                    bulletStillGoing = false;
                }
            }

            bulletStillGoing |= dam.mAmount > 0.1f;
        }

        if (bulletStillGoing == false)
        {
            DestroyObject(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Shield shield = other.GetComponent<Shield>();
        if (shield != null)
        {
            mHitShieldList.Add(shield);
        }

        Hull hull = other.GetComponent<Hull>();
        if (hull != null)
        {
            mHitHullList.Add(hull);
        }
    }
}
