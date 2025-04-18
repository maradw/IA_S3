using UnityEngine;
using UnityEngine.AI;

public class AgentRandomMovement : MonoBehaviour
{
    // Referencia al componente NavMeshAgent
    private NavMeshAgent agent;

    void Start()
    {
        // Obtiene el componente NavMeshAgent en el mismo GameObject
        agent = GetComponent<NavMeshAgent>();
    }

    // Función para mover al agente hacia una posición aleatoria dentro del NavMesh
    public void MoveToRandomPosition(Vector3 originPosition, float range)
    {
        Vector3 randomPosition;

        if (GetRandomNavMeshPosition(originPosition, range, out randomPosition))
        {
            // Si encuentra una posición válida en el NavMesh, mover el agente hacia esa posición
            agent.SetDestination(randomPosition);
        }
        else
        {
            Debug.LogWarning("No se encontró una posición válida en el NavMesh dentro del rango especificado.");
        }
    }

    // Función que encuentra una posición aleatoria en el NavMesh dentro de un rango dado
    private bool GetRandomNavMeshPosition(Vector3 origin, float range, out Vector3 result)
    {
        Vector3 randomPoint = origin + Random.insideUnitSphere * range;

        NavMeshHit hit;
        // Samplea la posición en el NavMesh más cercana al punto aleatorio generado
        if (NavMesh.SamplePosition(randomPoint, out hit, range, NavMesh.AllAreas))
        {
            result = hit.position; // Almacena la posición encontrada
            return true; // Retorna verdadero si encuentra una posición válida
        }

        result = Vector3.zero; // En caso de que no encuentre una posición válida
        return false;
    }

    // Ejemplo para mover el agente a una posición aleatoria con clic derecho
    void Update()
    {
        // Detección del clic derecho del ratón
        if (Input.GetMouseButtonDown(1))
        {
            // Llama a la función para mover el agente a una posición aleatoria
            MoveToRandomPosition(transform.position, 100f); // Rango de 10 unidades
        }
    }
}
