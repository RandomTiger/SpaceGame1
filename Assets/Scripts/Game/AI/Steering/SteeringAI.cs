using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(SteeringBehaviours))] // ok
public class SteeringAI : MonoBehaviour 
{
    SteeringBehaviours m_SteeringBehaviours;

	Vector2 mForce = new Vector2();

    void Awake()
    {
        m_SteeringBehaviours = GetComponent<SteeringBehaviours>();
    }

	void Update()
	{
        Vector3 actualSteer = m_SteeringBehaviours.GetActualSteer(GetDesiredSteer());

        // todo, refactor
     //   float ForwardForce = actualSteer.magnitude * Time.deltaTime;

		mForce.x = actualSteer.x * Time.deltaTime;
		mForce.y = actualSteer.y * Time.deltaTime;

 /*       Quaternion rotation = Quaternion.LookRotation(actualSteer, transform.up);

        float factor = 250.0f;
        m_ForceController.PitchForce = -rotation.eulerAngles.y / factor;
        m_ForceController.YawForce = -rotation.eulerAngles.x / factor;
        m_ForceController.RollForce = 0;
*/
        // todo, shouldnt be doing this directly
       // transform.rotation = rotation;
    }

    Vector3 GetDesiredSteer()    
    {
       // iPilot pilot = GetComponent(typeof(iPilot)) as iPilot;
       // GameObject target = pilot.GetCurrentTarget();

        // todo, do something more productive, regroup, guard etc
      //  if(target == null)
        {
            return m_SteeringBehaviours.Wander();
        }
		/*
        //bool inFront = steer.IsTargetFront(m_Target);
        return m_SteeringBehaviours.Pursuit(target) + avoid;*/
	}
}