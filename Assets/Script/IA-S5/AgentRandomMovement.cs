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

    // Funci�n para mover al agente hacia una posici�n aleatoria dentro del NavMesh
    public void MoveToRandomPosition(Vector3 originPosition, float range)
    {
        Vector3 randomPosition;

        if (GetRandomNavMeshPosition(originPosition, range, out randomPosition))
        {
            // Si encuentra una posici�n v�lida en el NavMesh, mover el agente hacia esa posici�n
            agent.SetDestination(randomPosition);
        }
        else
        {
            Debug.LogWarning("No se encontr� una posici�n v�lida en el NavMesh dentro del rango especificado.");
        }
    }

    // Funci�n que encuentra una posici�n aleatoria en el NavMesh dentro de un rango dado
    private bool GetRandomNavMeshPosition(Vector3 origin, float range, out Vector3 result)
    {
        Vector3 randomPoint = origin + Random.insideUnitSphere * range;

        NavMeshHit hit;
        // Samplea la posici�n en el NavMesh m�s cercana al punto aleatorio generado
        if (NavMesh.SamplePosition(randomPoint, out hit, range, NavMesh.AllAreas))
        {
            result = hit.position; // Almacena la posici�n encontrada
            return true; // Retorna verdadero si encuentra una posici�n v�lida
        }

        result = Vector3.zero; // En caso de que no encuentre una posici�n v�lida
        return false;
    }

    // Ejemplo para mover el agente a una posici�n aleatoria con clic derecho
    void Update()
    {
        // Detecci�n del clic derecho del rat�n
        if (Input.GetMouseButtonDown(1))
        {
            // Llama a la funci�n para mover el agente a una posici�n aleatoria
            MoveToRandomPosition(transform.position, 100f); // Rango de 10 unidades
        }
    }
}
