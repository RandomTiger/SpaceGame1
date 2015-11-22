using UnityEngine;
using System.Collections;

public class ParticleSystemAutoDestroy : MonoBehaviour
{
    private ParticleSystemAccessor mParticleSystemAccessor;

    public void Start()
    {
        mParticleSystemAccessor = GetComponent<ParticleSystemAccessor>();
    }

    public void Update()
    {
        if (mParticleSystemAccessor)
        {
            ParticleSystem particleSystem = mParticleSystemAccessor.GetParticleSystem();
            if (particleSystem && !particleSystem.IsAlive())
            {
                Destroy(gameObject);
            }
        }
    }
}