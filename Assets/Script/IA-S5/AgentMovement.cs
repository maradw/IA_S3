using UnityEngine;
using UnityEngine.AI;

public class AgentMovement : MonoBehaviour
{
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public void MoveToTargetPosition(Vector3 targetPosition)
    {
        if (agent != null)
        {
            agent.SetDestination(targetPosition);
        }
        else
        {
            Debug.LogError("No se encontr� un NavMeshAgent en el GameObject.");
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Verifica si el raycast impacta en alg�n objeto en el NavMesh
            if (Physics.Raycast(ray, out hit))
            {
                // Llama a la funci�n para mover el agente a la posici�n de impacto
                MoveToTargetPosition(hit.point);
            }
        }
    }
}

