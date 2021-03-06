﻿using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody2D))]
[RequireComponent (typeof (IMovementVector))]
[RequireComponent (typeof (IThrusterScalar))]
public class Movement : MonoBehaviour
{
    IMovementVector mMovementVector;
    IThrusterScalar mThruster;
    Rigidbody2D mRigidbody;
    ParticleSystem mThrusterParticles;

    [SerializeField]private float mPushForceFactor = 100.0f;

    [SerializeField]private float mTorqueForceMult = 8;
    [SerializeField]private float mTorqueForceMax = 20;

    float threshold = 0.001f;

    // Use this for initialization
    void Start () 
	{
		mMovementVector = GetComponent<IMovementVector> ();
        mThruster = GetComponent<IThrusterScalar>();
		mRigidbody = GetComponent<Rigidbody2D> ();

        mThrusterParticles = GetComponentInChildren<ParticleSystem>();
        if(mThrusterParticles != null)
        {
            mThrusterParticles.enableEmission = false;
        }
    }

    void Update()
    {
        float thrust = mThruster.GetValue();
        if(mThrusterParticles != null)
        {
            mThrusterParticles.enableEmission = thrust > 0.0f;
            mThrusterParticles.Play();
        }
    }

    // Update is called once per frame
    void FixedUpdate () 
	{
        float thrust = mThruster.GetValue();

        // Push forward
        Vector2 force = transform.up * thrust * mPushForceFactor;
        mRigidbody.AddForce(force);

        // Get thumbstick dir
        Vector2 src = transform.up;
        Vector2 dst = mMovementVector.GetVec();
        float dstMag = dst.magnitude;

        // Deadzone for applying torque, dont apply if we dont really have direction
        if (dst.sqrMagnitude > threshold)
        {
            float signedAngle = Maths2D.CalcSignedAngle(src, dst);

            float finalTorque = Mathf.Clamp(signedAngle * mTorqueForceMult, -mTorqueForceMax, mTorqueForceMax);
            mRigidbody.AddTorque(finalTorque);
        }
    }
}
