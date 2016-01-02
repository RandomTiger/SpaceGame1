using System;
using UnityEngine;

// todo: rewrite to output force?
// todo: reconsile force max's with velocity max's 

// velocity = force * time / mass
// force = (velocity * mass / time)

[RequireComponent(typeof(Rigidbody2D))] // Only needed for velocity
public class SteeringBehaviours : MonoBehaviour
{
    public enum Deceleration
    {
        Slow = 3,
        Normal = 2,
        Fast = 1
    };

    [Serializable]
    public class Forces
    {
        [SerializeField]
        private float m_MinSpeed = 0;
        [SerializeField]
        private float m_MaxSpeed = 10;
        [SerializeField]
        private float m_TurnRateDegrees = 30.0f;
        [SerializeField]
        private float m_PursuitThresholdAngle = 18.0f;

        public float MinSpeed { get { return m_MinSpeed; } }
        public float MaxSpeed { get { return m_MaxSpeed; } }
        public float TurnRateDegrees { get { return m_TurnRateDegrees; } }
        public float PursuitThresholdAngle { get { return m_PursuitThresholdAngle; } }
    }

    [Serializable]
    public class Wander
    {
        public float Radius { get { return m_Radius; } }
        public float Distance { get { return m_Distance; } }
        public float Jitter { get { return m_Jitter; } }
        public Vector3 Target { get { return m_Target; } set { m_Target = value; } }

        [SerializeField]
        private float m_Radius = 1.0f;
        [SerializeField]
        private float m_Distance = 1.0f;
        [SerializeField]
        private float m_Jitter = 0.1f;
        [SerializeField]
        private Vector3 m_Target = new Vector3();

    }

    [Serializable]
    public class Attributes
    {
        [SerializeField]
        private Forces m_Forces = new Forces();
        [SerializeField]
        private Wander m_Wander = new Wander();

        public Forces Forces { get { return m_Forces; } }
        public Wander Wander { get { return m_Wander; } }
    }

    [SerializeField]
    public SteeringBehaviours.Attributes mAttributes;


    public Vector3 GetActualSteer(Vector3 desiredSteer)
    {
        Forces forces = mAttributes.Forces;
        float mag = desiredSteer.magnitude;

        if (mag < forces.MinSpeed)
        {
            desiredSteer.Normalize();
            desiredSteer *= forces.MinSpeed;
        }

        if (mag > forces.MaxSpeed)
        {
            desiredSteer.Normalize();
            desiredSteer *= forces.MaxSpeed;
        }

        float maxRateRadians = Mathf.Deg2Rad * forces.TurnRateDegrees * Time.deltaTime;

        Vector3 actualSteer = Vector3.RotateTowards(transform.forward, desiredSteer, maxRateRadians, forces.MaxSpeed);
        //transform.rotation = Quaternion.LookRotation(actualSteer, transform.up);

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

    public void OnDrawGizmos()
    {
        /*
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
  //      Gizmos.DrawLine(transform.position, transform.position + avoid * 10.0f);*/
    }

}

static class Steering
{
    public class SteeringActor
    {
        public SteeringActor(GameObject actor)
        {
            m_ActorObject = actor;
            Debug.Assert(actor);

            steeringBehaviours = actor.GetComponent<SteeringBehaviours>();
            m_Rigidbody = actor.GetComponent<Rigidbody2D>();
            m_Attributes = steeringBehaviours.mAttributes;

            Debug.Assert(steeringBehaviours);
            Debug.Assert(m_Rigidbody);
            Debug.Assert(m_Attributes != null);
        }

        public SteeringBehaviours.Attributes GetAttributes()
        {
            return m_Attributes;
        }

        public Vector3 GetPosition()
        {
            return m_ActorObject.transform.position;
        }

        public Vector3 GetForward()
        {
            return m_ActorObject.transform.forward;
        }

        public Vector3 GetVelocity() // change to force???
        {
            return m_Rigidbody.velocity;
        }

        private GameObject m_ActorObject;
        private SteeringBehaviours steeringBehaviours;
        private Rigidbody2D m_Rigidbody;
        private SteeringBehaviours.Attributes m_Attributes;
    }

    public static bool IsTargetFront(SteeringActor actor, SteeringActor target)
    {
        Vector3 toTarget = target.GetPosition() - actor.GetPosition();
        toTarget.Normalize();
        return Vector3.Dot(actor.GetForward(), toTarget) > 0;
    }

    #region SteeringBehaviours
    public static Vector3 Seek(SteeringActor actor, Vector3 targetPos)
    {
        SteeringBehaviours.Forces attributes = actor.GetAttributes().Forces;

        Vector3 toTarget = (targetPos - actor.GetPosition());
        toTarget.Normalize();
        Vector3 desiredVelocity = toTarget * attributes.MaxSpeed;
        return desiredVelocity;// - GetVelocity();
    }

    public static Vector3 Flee(SteeringActor actor, Vector3 targetPos)
    {
        SteeringBehaviours.Forces attributes = actor.GetAttributes().Forces;

        Vector3 fromTarget = actor.GetPosition() - targetPos;
        fromTarget.Normalize();
        Vector3 desiredVelocity = fromTarget * attributes.MaxSpeed;
        return desiredVelocity;// - GetVelocity();
    }

    public static Vector3 Arrive(SteeringActor actor, Vector3 targetPos, SteeringBehaviours.Deceleration deceleration)
    {
        SteeringBehaviours.Forces attributes = actor.GetAttributes().Forces;

        Vector3 toTarget = targetPos - actor.GetPosition();

        float dist = toTarget.magnitude;
        if (dist <= 0)
        {
            return new Vector3();
        }

        float decelerationTweaker = 0.3f;
        float speed = dist / ((float)deceleration * decelerationTweaker);

        speed = Mathf.Min(speed, attributes.MaxSpeed);

        Vector3 desiredVelocity = toTarget * speed / dist;

        return desiredVelocity;// - GetVelocity();
    }

    public static Vector3 Pursuit(SteeringActor actor, SteeringActor evader)
    {
        SteeringBehaviours.Forces attributes = actor.GetAttributes().Forces;
        Vector3 toEvader = evader.GetPosition() - actor.GetPosition();

        Vector3 thisHeading = actor.GetVelocity();
        thisHeading.Normalize();

        Vector3 evaderHeading = evader.GetVelocity();
        evaderHeading.Normalize();
        float relativeHeading = Vector3.Dot(thisHeading, evaderHeading);

        float tresholdAngleAcos = Mathf.Acos(attributes.PursuitThresholdAngle);
        if (Vector3.Dot(toEvader, thisHeading) > 0 && (relativeHeading < -tresholdAngleAcos))
        {
            return Seek(actor, evader.GetPosition());
        }

        // the look ahead time is proportional to the distance between the evader
        // and the pursuer; and is inversely proportional to the sum of the agents velocities
        float lookAheadTime = toEvader.magnitude / (attributes.MaxSpeed + evader.GetVelocity().magnitude);

        // Now seek to the predicted future position of the evader
        return Seek(actor, evader.GetPosition() + (Vector3) evader.GetVelocity() * lookAheadTime);
    }

    public static Vector3 Evade(SteeringActor actor, SteeringActor pursuer)
    {
        SteeringBehaviours.Forces attributes = actor.GetAttributes().Forces;

        Vector3 toPursuer = pursuer.GetPosition() - actor.GetPosition();

        float lookAhreadTime = toPursuer.magnitude / (attributes.MaxSpeed + pursuer.GetVelocity().magnitude);
        return Flee(actor, pursuer.GetPosition() + (Vector3)pursuer.GetVelocity() * lookAhreadTime);
    }

    public static float GetObstactleAvoidanceLookAheadDist(SteeringActor actor)
    {
        return 1.0f + actor.GetVelocity().magnitude * 2.0f;
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

    public static Vector3 Wander(SteeringActor actor)
    {
        SteeringBehaviours.Forces attributes = actor.GetAttributes().Forces;
        SteeringBehaviours.Wander wander = actor.GetAttributes().Wander;

        wander.Target = 
            new Vector3(
                wander.Target.x + RandomClamped() * wander.Jitter,
                wander.Target.y + RandomClamped() * wander.Jitter);
        wander.Target.Normalize();

        wander.Target *= wander.Radius;

        return attributes.MaxSpeed * (wander.Target * wander.Distance);
    }

    #endregion

    #region Helper
    private static float RandomClamped()
    {
        return UnityEngine.Random.Range(-1.0f, 1.0f);
    }
    #endregion
}