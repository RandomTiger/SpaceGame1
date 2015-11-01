using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Payload : MonoBehaviour {

    public float mHull = 15;
    public float mShield = 10;

    public List<Shield> mHitShieldList;
    public List<Health> mHitHullList;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        // Dish out shield damage first
        foreach(Shield shield in mHitShieldList)
        {
            mShield = shield.Damage(mShield);
        }

        // Dish out hull damage onto the shields
        foreach (Shield shield in mHitShieldList)
        {
            mHull = shield.Damage(mHull);
        }

        // If there is any left deal out damage onto the hull
        foreach (Health hull in mHitHullList)
        {
            mHull = hull.Damage(mHull);
        }

        if ((mHull + mShield) < 0.1f)
        {
            DestroyObject(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(mShield > 0)
        {
            Shield shield = other.GetComponent<Shield>();
            if (shield != null)
            {
                mHitShieldList.Add(shield);
            }
        }

        if (mHull > 0)
        {
            Health hull = other.GetComponent<Health>();
            if (hull != null)
            {
                mHitHullList.Add(hull);
            }
        }
    }
}
