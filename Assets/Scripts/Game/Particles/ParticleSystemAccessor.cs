using UnityEngine;

public abstract class ParticleSystemAccessor : MonoBehaviour
{
    public abstract ParticleSystem GetParticleSystem();
    public abstract void SetSize(float size);
    // public abstract void Play();
}
