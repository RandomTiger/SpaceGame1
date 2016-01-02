using UnityEngine;
using System.Collections;

[System.Serializable]
public class BehaviorNodeDecoratorRepeater : BehaviorNodeDecorator
{
    public const int RepeatForever = -1;
    public int m_RepeatCount = RepeatForever;
    [SerializeField]
    private int m_Count;

    protected override void Init(BehaviourParams param)
    {
        m_Count = m_RepeatCount;
    }

    protected override Flow Process(BehaviourParams param)
    {
        Flow result = m_Child.Update(param);

        if(result == Flow.Running)
        {
            return Flow.Running;
        }

        Debug.Assert(m_Count > 0 || m_Count == RepeatForever);
        if(m_Count > 0)
        {
            m_Count--;
            if (m_Count == 0)
            {
                // Finished (ignore errors)
                return Flow.Success;
            }
        }
        else if(m_Count == RepeatForever)
        {
            m_Child.Reset();
        }

        return Flow.Running;
    }
}
