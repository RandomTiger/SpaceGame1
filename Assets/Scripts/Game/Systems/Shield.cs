using UnityEngine;
using System.Collections;

public class Shield : MonoBehaviour
{
    public float mMaxStrength = 100;
    public float mRechargeSpeed = 10;
    public float mPoppedShieldTimeout = 4.0f;

    private float mStrength;
    private float mTimeOut;
    private Color mTempCol = new Color();

    // Use this for initialization
    void Start ()
    {
        mStrength = mMaxStrength;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(mTimeOut > 0)
        {
            mTimeOut -= Time.deltaTime;
        }
        else
        {
            mStrength = Mathf.Clamp(mStrength + mRechargeSpeed * Time.deltaTime, 0.0f, mMaxStrength);
        }

        SpriteRenderer debugRender = GetComponentInChildren<SpriteRenderer>();
        if (debugRender != null)
        {
            mTempCol = debugRender.color;
            mTempCol.a = mStrength / mMaxStrength;
            debugRender.color = mTempCol;
        }

        GetComponent<Collider2D>().enabled = mStrength > 0;
    }

    public float Damage(float amount)
    {
        mStrength -= amount;
        if (mStrength < 0)
        {
            mTimeOut = mPoppedShieldTimeout;
        }

        // Return any unused damage
        return -Mathf.Min(mStrength, 0.0f);
    }
}
