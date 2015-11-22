using UnityEngine;
using System.Collections;

public class ParticleSystemPlayOnHostDestroy : MonoBehaviour
{
    public Object mParticlesDeath = null;
    public float mSize = 1.0f;

    void OnDestroy()
    {
        if (mParticlesDeath)
        {
            GameObject particleInstance = Instantiate(mParticlesDeath, transform.position, Quaternion.identity) as GameObject;
#if UNITY_EDITOR
            particleInstance.name += " from " + gameObject.name;
#endif
            particleInstance.AddComponent<ParticleSystemAutoDestroy>();

            ParticleSystemAccessor accessor = particleInstance.GetComponent<ParticleSystemAccessor>();
            accessor.SetSize(mSize);
        }
    }

    // Stops them getting spawned into the editor when you finish a run
    void OnApplicationQuit()
    {
        mParticlesDeath = null;
    }
}