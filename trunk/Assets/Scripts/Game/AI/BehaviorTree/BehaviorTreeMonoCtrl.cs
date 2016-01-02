using UnityEngine;
using System.Collections;
using UnityEditor;

public class BehaviorTreeMonoCtrl : MonoBehaviour
{
    public enum TempCtr
    {
        PursueFlee,
    }

    public TempCtr m_TreeType;
    private BehaviorTree m_Tree = new BehaviorTree();

    BehaviorNode CreatePursueFleeTree()
    {
        // Build a test tree
        BehaviorNodeCompositeSequence pursueFleeSequence = new BehaviorNodeCompositeSequence();
        pursueFleeSequence.AddChild(new BehaviorLeafPursue());
        pursueFleeSequence.AddChild(new BehaviorLeafFlee());

        BehaviorNodeDecoratorRepeater repeater = new BehaviorNodeDecoratorRepeater();
        repeater.SetChild(pursueFleeSequence);
        return repeater;
    }

    void Awake()
    {
        switch(m_TreeType)
        {
            case TempCtr.PursueFlee:
                m_Tree.m_Root = CreatePursueFleeTree(); break;
        }

        m_Tree.Reset();
    }

    void Update()
    {
        BehaviourParams param = new BehaviourParams();
        param.m_Actor = gameObject;
        param.m_DeltaTime = Time.deltaTime;

        m_Tree.Update(param);
    }
}
