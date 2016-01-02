using UnityEngine;
using System.Collections;
[System.Serializable]
public class BehaviorTree : BehaviorNode
{
    [SerializeField]
    public BehaviorNode m_Root;

    protected override Flow Process(BehaviourParams param)
    {
        return m_Root.Update(param);
    }

}
