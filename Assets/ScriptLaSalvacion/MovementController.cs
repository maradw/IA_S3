using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;


public class MovementController : MonoBehaviour
{
    public float speed = 5f;
     public NavMeshAgent agent;
    // M�todo para mover el objeto hacia el destino y luego ejecutar un callback
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (agent == null)
        {
            Debug.LogError("NavMeshAgent no encontrado en el GameObject " + gameObject.name);
        }
    }
   
    public void MoveTo(Transform destination, Action onArrived)
    {
        StartCoroutine(MoveRoutine(destination, onArrived));
    }

    private IEnumerator MoveRoutine(Transform destination, Action onArrived)
    {
        while (Vector3.Distance(transform.position, destination.position) > 0.5f)
        {
            agent.SetDestination(destination.position);
           // transform.position = Vector3.MoveTowards(transform.position, destination.position, speed * Time.deltaTime);
            yield return null;
        }
        onArrived?.Invoke();
    }
}
