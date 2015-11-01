using UnityEngine;

// Todo, make static class?
[RequireComponent(typeof(Rigidbody2D))] // Only needed for velocity
public class SteeringBehaviours : MonoBehaviour 
{
	public enum Deceleration
	{
		Slow = 3,
		Normal = 2,
		Fast = 1
	};

	public float m_PursuitThresholdAngle = 18.0f;

    public float m_MinSpeed = 0.0f;
    public float m_MaxSpeed = 40.0f;
    public float m_TurnRateDegrees = 30.0f;

	Rigidbody2D m_Rigidbody;
   // EntityInfoRoot m_EntityRoot;

    void Awake()
    {
		m_Rigidbody = GetComponent<Rigidbody2D>();
     //   m_EntityRoot = GetComponent<EntityInfoNode>().GetRoot();
    }
    
    public void Update()
    {
        Debug.DrawLine(transform.position, transform.position + GetComponent<Rigidbody>().velocity, Color.blue);
    }
	
    public Vector3 GetActualSteer(Vector3 desiredSteer)
	{
        float mag = desiredSteer.magnitude;

        if (mag < m_MinSpeed)
        {
            desiredSteer.Normalize();
            desiredSteer *= m_MinSpeed;
        }

        if (mag > m_MaxSpeed)
        {
            desiredSteer.Normalize();
            desiredSteer *= m_MaxSpeed;
        }
        
        float maxRateRadians = Mathf.Deg2Rad*m_TurnRateDegrees*Time.deltaTime;

        Vector3 actualSteer = Vector3.RotateTowards(transform.forward, desiredSteer, maxRateRadians, m_MaxSpeed);
		transform.rotation = Quaternion.LookRotation(actualSteer, transform.up);

        {
            Vector3 desiredSteerNormalized = desiredSteer;
            desiredSteerNormalized.Normalize();
            Vector3 actualSteerNormalized = actualSteer;
            actualSteerNormalized.Normalize();
            
            const float factor = 50.0f;
            Debug.DrawLine(transform.position, transform.position + (desiredSteerNormalized * factor), Color.red);
            Debug.DrawLine(transform.position, transform.position + (actualSteerNormalized * factor), Color.green);
        }
	
        return actualSteer;
	}

	public bool IsTargetFront(GameObject target)
	{
		Vector3 toTarget = target.transform.position - transform.position;
		toTarget.Normalize();
		return Vector3.Dot(transform.forward,toTarget) > 0;
	}

	#region SteeringBehaviours
	public Vector3 Seek(Vector3 targetPos)
	{
		Vector3 toTarget = (targetPos - transform.position);
		toTarget.Normalize();
		Vector3 desiredVelocity = toTarget * m_MaxSpeed;
        return desiredVelocity;// - GetVelocity();
	}

	public Vector3 Flee(Vector3 targetPos)
	{
		Vector3 fromTarget = transform.position - targetPos;
		fromTarget.Normalize();
		Vector3 desiredVelocity = fromTarget * m_MaxSpeed;
        return desiredVelocity;// - GetVelocity();
	}

	public Vector3 Arrive(Vector3 targetPos, Deceleration deceleration)
	{
		Vector3 toTarget = targetPos - transform.position;

		float dist = toTarget.magnitude;
		if(dist <= 0)
		{
			return new Vector3();
		}

		float decelerationTweaker = 0.3f;
		float speed = dist / ((float) deceleration * decelerationTweaker);

		speed = Mathf.Min(speed, m_MaxSpeed);

		Vector3 desiredVelocity = toTarget * speed / dist;

        return desiredVelocity;// - GetVelocity();
	}

	public Vector3 Pursuit(GameObject evader)
	{
		Rigidbody evaderRigidbody = evader.GetComponent<Rigidbody>();
		if(evaderRigidbody == null)
		{
			return Seek(evader.transform.position);
		}

		Vector3 toEvader = evader.transform.position - transform.position;

        Vector3 thisHeading = m_Rigidbody.velocity;
		thisHeading.Normalize();

		Vector3 evaderHeading = evaderRigidbody.velocity;
		evaderHeading.Normalize();
		float relativeHeading = Vector3.Dot(thisHeading, evaderHeading);

		float tresholdAngleAcos = Mathf.Acos(m_PursuitThresholdAngle);
		if(Vector3.Dot(toEvader, thisHeading) > 0 && (relativeHeading < -tresholdAngleAcos))
		{
			return Seek(evader.transform.position);
		}

		// the look ahead time is proportional to the distance between the evader
		// and the pursuer; and is inversely proportional to the sum of the agents velocities
		float lookAheadTime = toEvader.magnitude / (m_MaxSpeed + evaderRigidbody.velocity.magnitude);

		// Now seek to the predicted future position of the evader
		return Seek (evader.transform.position + evaderRigidbody.velocity * lookAheadTime);
	}

	public Vector3 Evade(GameObject pursuer)
	{
		Vector3 toPursuer = pursuer.transform.position - transform.position;
		Rigidbody pursuerRigidbody = pursuer.GetComponent<Rigidbody>();
		
		float lookAhreadTime = toPursuer.magnitude / (m_MaxSpeed + pursuerRigidbody.velocity.magnitude);
		return Flee(pursuer.transform.position + pursuerRigidbody.velocity * lookAhreadTime);
	}

    public float GetObstactleAvoidanceLookAheadDist()
    {
        return 1.0f + m_Rigidbody.velocity.magnitude * 2.0f;
    }
	/*
    public Vector3 ObstactleAvoidance()
    {
        float lookAhead = GetObstactleAvoidanceLookAheadDist();

        // Need to use a primitive collider, the object running the sweeptest can't use its mesh collidier
        // though the mesh colliders of the objects it tests can be used
      //  m_EntityRoot.GetCollidable().m_Collider.enabled = true;

        Vector3 result = Vector3.zero;
        RaycastHit sweepResult = new RaycastHit();
        if (m_Rigidbody.SweepTest(m_Rigidbody.velocity, out sweepResult, lookAhead))
        {
            GameObject rayHitObject = sweepResult.collider.gameObject;
            Collider rayHitCollider = rayHitObject.GetComponent<Collider>();

            float hitObjRadius = rayHitCollider.bounds.extents.magnitude;
            Vector2 hitObjPos = rayHitCollider.bounds.center; // world or local?

            Vector3 hitObjInMyLocalCoords = transform.worldToLocalMatrix.MultiplyPoint(hitObjPos);

            Vector3 posDiff = hitObjInMyLocalCoords - transform.localPosition;

            // The closer the object , the stronger the steering force
            float mult = 1.0f + (lookAhead - hitObjInMyLocalCoords.x) / lookAhead;
            Vector3 steeringForce = Vector3.zero;

            // Lateral force
            const float breakingWeight = 1.0f;
            steeringForce.y = (hitObjRadius - hitObjInMyLocalCoords.y) * mult;

            steeringForce.x = (hitObjRadius - hitObjInMyLocalCoords.x) * breakingWeight;

            result = transform.localToWorldMatrix.MultiplyPoint(steeringForce);
        }

    //    m_EntityRoot.GetCollidable().m_Collider.enabled = false;

        return result;
    }
*/
	#endregion

    #region Helper

    public void OnDrawGizmos()
    {
        if (m_Rigidbody == null)
        {
            return;
        }
    

        Gizmos.color = Color.yellow;
        Gizmos.matrix = transform.localToWorldMatrix;

        Vector3 pos = new Vector3(0, 0, GetObstactleAvoidanceLookAheadDist() / 2);
                                  Gizmos.DrawCube(pos, new Vector3(10, 10, GetObstactleAvoidanceLookAheadDist()));
        Gizmos.matrix = Matrix4x4.identity;

        Gizmos.color = Color.magenta;
//        Vector3 avoid = ObstactleAvoidance();
  //      Gizmos.DrawLine(transform.position, transform.position + avoid * 10.0f);
    }

	private float RandomClamped()
	{
		return Random.Range(-1.0f, 1.0f);
	}
	#endregion


	float m_wanderRadius = 1.0f;
	float m_wanderDistance = 1.0f;
	float m_wanderJitter = 0.1f;
	Vector3 m_WanderTarget = new Vector3();
	public Vector3 Wander()
	{
		m_WanderTarget += new 
			Vector3(
				RandomClamped() * m_wanderJitter,
				0.0f,//RandomClamped() * m_wanderJitter,
				RandomClamped() * m_wanderJitter);
		m_WanderTarget.Normalize();

		m_WanderTarget *= m_wanderRadius;

		return m_MaxSpeed * (m_WanderTarget * m_wanderDistance);
	}
}