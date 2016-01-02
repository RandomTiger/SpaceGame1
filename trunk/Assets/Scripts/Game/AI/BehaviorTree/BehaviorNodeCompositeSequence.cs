using UnityEngine;
using System.Collections;
using UnityEditor;

/*
Sequences

The simplest composite node found within behaviour trees, their name says it all. 
A sequence will visit each child in order, starting with the first, and when that 
succeeds will call the second, and so on down the list of children. If any child fails 
it will immediately return failure to the parent. If the last child in the sequence succeeds, 
then the sequence will return success to its parent.
*/

[System.Serializable]
//[CustomEditor(typeof(BehaviorNodeComposite), true)]
public class BehaviorNodeCompositeSequence : BehaviorNodeComposite
{
    protected override Flow Process(BehaviourParams param)
	{
		for(int i = 0; i < m_ChildList.Count; i++)
        {
            Flow result = m_ChildList[i].Update(param);
            if(result != Flow.Success)
            {
                return result; // failure and running will return
            }
        }

        return Flow.Success;
    }
}
