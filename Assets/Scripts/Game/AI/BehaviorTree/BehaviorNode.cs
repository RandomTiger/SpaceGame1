using UnityEngine;
using System.Collections;
[System.Serializable]
public abstract class BehaviorNode
{
    [SerializeField]
    private bool m_IsInitialised;

    public enum Flow
    {
        Success,
        Failure,
        Running,
    }

    protected virtual void Init(BehaviourParams param) { }
    protected abstract Flow Process(BehaviourParams param);

    public void Reset()
    {
        m_IsInitialised = false;
    }

    public Flow Update(BehaviourParams param)
    {
        if(m_IsInitialised == false)
        {
            Init(param);
            m_IsInitialised = true;
        }

        return Process(param);
    }
}
