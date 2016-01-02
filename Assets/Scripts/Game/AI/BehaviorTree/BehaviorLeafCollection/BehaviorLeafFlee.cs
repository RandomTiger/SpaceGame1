using System.Collections;

[System.Serializable]
public class BehaviorLeafFlee : BehaviorLeaf
{
    float m_TestTimeOut = 5.0f;
    float m_TimeOut;

    protected override void Init(BehaviourParams param)
    {
        m_TimeOut = m_TestTimeOut;
        DebugLog.Add("Start Flee");
    }

    protected override Flow Process(BehaviourParams param)
	{
        m_TimeOut -= param.m_DeltaTime; // todo. pass as param
        if (m_TimeOut < 0)
        {
            return Flow.Success;
        }

        param.m_Actor.GetComponent<MovementVectorAI>().SetMode(MovementVectorAI.Mode.Flee);

        return Flow.Running;
	}
}
