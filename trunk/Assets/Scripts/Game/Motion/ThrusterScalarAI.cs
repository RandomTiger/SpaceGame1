using UnityEngine;
using System.Collections.Generic;

public class ThrusterScalarAI : MonoBehaviour, IThrusterScalar
{
    public bool enable = true;

    public float GetValue()
    {
        return enable ? (Random.value / 10.0f) : 0.0f;
    }
}