using UnityEngine;
using System.Collections.Generic;

public class MovementVectorAI : MonoBehaviour, IMovementVector
{
    public enum Mode
    {
        Pursue,
        Flee,
        Wander
    }

    public bool mEnable = true;
    [SerializeField] private Mode m_Mode;
    [SerializeField] private GameObject m_Target;

    private Vector3 m_SteerForDebug;

    public Vector2 GetVec()
	{
        if (mEnable == false)
        {
            return Vector2.zero;
        }

        m_SteerForDebug = GetDesiredSteerVelocity();
        return m_SteerForDebug;
    }

    public void SetMode(Mode mode)
    {
        m_Mode = mode;
    }

    public void SetTarget(GameObject target)
    {
        m_Target = target;
    }

    Vector3 GetDesiredSteerVelocity()
    {
        SteeringBehaviours steeringBehaviours = GetComponent<SteeringBehaviours>();
        Debug.Assert(steeringBehaviours);
        Debug.Assert(m_Target);

        Steering.SteeringActor thisAI = new Steering.SteeringActor(gameObject);
        Steering.SteeringActor target = new Steering.SteeringActor(m_Target);

        switch (m_Mode)
        { 
            case Mode.Pursue:
                return Steering.Pursuit(thisAI, target);
            case Mode.Flee:
                return Steering.Evade(thisAI, target);
            case Mode.Wander:
                return Steering.Wander(thisAI);
        }

        Debug.Assert(false, "Enum not handled");
        return Vector3.zero;
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(transform.position, transform.position + (m_SteerForDebug * 10.0f));
    }
}
