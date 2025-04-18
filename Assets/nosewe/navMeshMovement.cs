using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class navMeshMovement : MonoBehaviour
{
    [SerializeField] private NavMeshAgent enemy;
    [SerializeField] private float range;
    [SerializeField] private Transform centerPlane;
    Vector3 target;
    private bool hasTarget = false;

    void Start()
    {

    }

    void Update()
    {
        CalculatePath(target);
    }

    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        Vector3 randomTarget = center + Random.insideUnitSphere * range;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomTarget, out hit, 0.5f, NavMesh.AllAreas))
        {
            result = hit.position;
            return true;
        }
        result = Vector3.zero;
        return false;
    }

    void CallRandom(Vector3 target)
    {

        if (enemy.remainingDistance <= enemy.stoppingDistance && !enemy.pathPending)
        {
            if (RandomPoint(centerPlane.position, range, out target))
            {
                Debug.DrawRay(target, Vector2.up, Color.magenta, 0.8f);
                enemy.SetDestination(target);
            }
        }


    }
    void CalculatePath(Vector3 targetPosition)
    {

        if (!enemy.pathPending && enemy.remainingDistance <= enemy.stoppingDistance)
        {
            hasTarget = false;
        }
        if (!hasTarget)
        {
            
            if (RandomPoint(centerPlane.position, range, out targetPosition))
            {
                NavMeshPath path = new NavMeshPath();

                if (enemy.CalculatePath(targetPosition, path) && path.status == NavMeshPathStatus.PathComplete)
                {
                    target = targetPosition;
                    enemy.SetPath(path);
                    hasTarget = true;

                    Debug.DrawRay(target, Vector3.up * 2f, Color.magenta, 1f);
                }
                else
                {
                    Debug.Log("Camino no válido hacia: " + targetPosition);
                }
            }
        }

    }



}
