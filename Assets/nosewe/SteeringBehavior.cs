using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum BehaviorType
{
    Seek,
    Flee,
    Pursuit,
    Evade,
    Wander,
    PathFollowing,
    Arrive,
    ObstacleAvoidance
}
public class SteeringBehavior : MonoBehaviour
{
    public float velocity;
    Vector3 tarjetPosition;
    Vector3 direction;
    Vector3 lastTargetPosition;
    public float speed;
    [SerializeField] GameObject b;
    float timer = 0f;
    [SerializeField] private List<GameObject> waypoints;
    private int currentWaypointIndex = 0;
    public float waypointThreshold = 1.0f;

    public BehaviorType currentBehavior = BehaviorType.Seek;



    void LookFollow(Vector3 targetPos,float _speed)
    {
      transform.rotation= Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(targetPos),Time.deltaTime*_speed) ;
    }
    void Seek()
    {
        Vector3 desiredVelocity = (b.transform.position- transform.position).normalized * velocity;
        LookFollow(desiredVelocity,2f);
        transform.position = transform.position + transform.forward*Time.deltaTime*speed;

    }
    void Flee()
    {
        Vector3 direction = (transform.position - b.transform.position).normalized * velocity;
        LookFollow(direction, 4f);
        transform.position = transform.position + transform.forward * Time.deltaTime * speed;
    }
    

    void Persuit()
    {
        if (speed <= 0) return;

        Vector3 targetVelocity = (b.transform.position - lastTargetPosition) / Time.deltaTime;
        lastTargetPosition = b.transform.position;

        Vector3 targetPos = b.transform.position + targetVelocity * Mathf.Max(Vector3.Distance(transform.position, b.transform.position) / (speed + targetVelocity.magnitude), 0.1f);

        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
        LookFollow(targetPos - transform.position, 5f);
    }

    void Evade()
    {
        Vector3 direction = b.transform.position - transform.position;
        float distance = direction.magnitude;
        if (speed <= 0) return;
        float predictionTime = distance / (speed + velocity * 0.5f);

        Vector3 futurePosition = b.transform.position + (b.transform.forward * predictionTime * velocity);
        Vector3 fleeDirection = (transform.position - futurePosition).normalized * velocity;
        LookFollow(fleeDirection, 2f);
        transform.position += transform.forward * Time.deltaTime * speed;

    }
    void Wander()
    {
        timer += Time.deltaTime; 

        if (timer >= 1f) 
        {
            timer = 0f;

            float x = Random.Range(-1f, 1f);
            float z = Random.Range(-1f, 1f);
            Vector3 randomDir = new Vector3(x, 0, z).normalized;

            transform.rotation = Quaternion.LookRotation(randomDir);
        }
        transform.position += transform.forward * Time.deltaTime * speed;
    }
    void PathFollowing()
    {
        if (waypoints.Count == 0) return;
        Vector3 targetPosition = waypoints[currentWaypointIndex].transform.position;
        Vector3 desiredVelocity = (targetPosition - transform.position).normalized * velocity;
        LookFollow(desiredVelocity, 2f);
        transform.position += transform.forward * Time.deltaTime * speed;
        if (Vector3.Distance(transform.position, targetPosition) < waypointThreshold)
        {
            currentWaypointIndex++;
            if (currentWaypointIndex >= waypoints.Count)
            {
                currentWaypointIndex = 0;
            }
        }
    }
    void Update()
    {
        Debug.Log("nosewe");
        HandleInput();
        switch (currentBehavior)
        {
            case BehaviorType.Seek:
                Debug.Log("Behavior is Seek");
                Seek();
                break;
            case BehaviorType.Flee:
                Debug.Log("Behavior is Flee");
                Flee();
                break;
            case BehaviorType.Pursuit:
                Debug.Log("Behavior is Pursuit");
                Persuit();
                break;
            case BehaviorType.Evade:
                Debug.Log("Behavior is Evade");
                Evade();
                break;
            case BehaviorType.Wander:
                Debug.Log("Behavior is Wander");
                Wander();
                break;
            case BehaviorType.PathFollowing:
                Debug.Log("Behavior is PathFollowing");
                PathFollowing();
                break;
            case BehaviorType.Arrive:
                Debug.Log("Behavior is Arrive");
                Arrive();
                break;
            case BehaviorType.ObstacleAvoidance:
                Debug.Log("Behavior is ObstacleAvoidance");
                ObstacleAvoidance();
                break;
        }


    }
    void HandleInput()
    {
        Debug.Log("ola");
        if (Input.GetKeyDown(KeyCode.Alpha1))
            currentBehavior = BehaviorType.Seek;

        if (Input.GetKeyDown(KeyCode.Alpha2))
            currentBehavior = BehaviorType.Flee;

        if (Input.GetKeyDown(KeyCode.Alpha3))
            currentBehavior = BehaviorType.Evade;

        if (Input.GetKeyDown(KeyCode.Alpha4))
            currentBehavior = BehaviorType.Arrive;

        if (Input.GetKeyDown(KeyCode.Alpha5))
            currentBehavior = BehaviorType.Pursuit;

        if (Input.GetKeyDown(KeyCode.Alpha6))
            currentBehavior = BehaviorType.Wander;

        if (Input.GetKeyDown(KeyCode.Alpha7))
            currentBehavior = BehaviorType.PathFollowing;

        if (Input.GetKeyDown(KeyCode.Alpha8))
            currentBehavior = BehaviorType.ObstacleAvoidance;
    }


    void Arrive()
    {
        float slowingDistance = 3f;
        float distance = Vector3.Distance(transform.position, b.transform.position);
        float rampedSpeed = speed * (distance / slowingDistance);
        float clippedSpeed = Mathf.Min(rampedSpeed, speed);
        Vector3 desiredVelocity = (b.transform.position - transform.position).normalized * clippedSpeed;
        LookFollow(desiredVelocity, 5f);
        transform.position += desiredVelocity * Time.deltaTime;

    }
    void ObstacleAvoidance()
    {
        float detectionDistance = 3.5f;
        Debug.DrawLine(transform.position, transform.position + transform.forward * detectionDistance, Color.red);
        if (Physics.Raycast(transform.position, transform.forward, detectionDistance))
        {
            if (!Physics.Raycast(transform.position, -transform.right, detectionDistance))
            {
                Debug.DrawLine(transform.position, transform.position - transform.right * detectionDistance, Color.green);
                transform.Rotate(0, -45, 0);
            }
            else if (!Physics.Raycast(transform.position, transform.right, detectionDistance))
            {
                Debug.DrawLine(transform.position, transform.position + transform.right * detectionDistance, Color.green);
                transform.Rotate(0, 45, 0);
            }
            else
            {
                transform.Rotate(0, 180, 0);
            }
        }
        transform.position += transform.forward * Time.deltaTime * speed;
    }
}
