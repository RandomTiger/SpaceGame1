using UnityEngine;
using System.Collections;

// Creepy cat particle systems need to have the following changes applied:
// Start lifetime to
// Play on awake: false
// Emission: Rate: 0
// Emission: + Bursts 0.00 1
public class ParticleSystemWrapperCreepyCat : ParticleSystemAccessor
{
    private ParticleSystem mParticleSystem;
    private float mSizeMult = 10.0f;

    // Use this for initialization
    void Awake()
    {
        // Have to be set on Awake so they are applied when played (PlayOnAwake=true)
        mParticleSystem = transform.GetChild(0).GetComponent<ParticleSystem>();
        mParticleSystem.loop = false;
        mParticleSystem.startLifetime = 2;
        mParticleSystem.startSize = mSizeMult;
    }

    public override ParticleSystem GetParticleSystem()
    {
        return mParticleSystem;
    }

    // public abstract 
    public override void SetSize(float size)
    {
        mParticleSystem.startSize = mSizeMult * size;
    }
}
