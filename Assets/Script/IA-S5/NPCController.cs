using UnityEngine;
using UnityEngine.AI;

public class NPCController : MonoBehaviour
{
    private NavMeshAgent agent;  // Referencia al NavMeshAgent
    public Transform target;     // Objetivo a seguir (para Evade o MoveToPosition)
    public float wanderRadius = 10f;  // Radio de búsqueda para Wander
    public float evadeDistance = 5f;  // Distancia a mantener para Evade

    void Start()
    {
        // Inicializa el componente NavMeshAgent en el NPC
        agent = GetComponent<NavMeshAgent>();
    }

    // Función para mover al NPC a una posición específica
    public void MoveToPosition(Vector3 position)
    {
        if (agent != null)
        {
            agent.SetDestination(position);
            Debug.Log("Moviendo al NPC a la posición: " + position);
        }
    }

    // Función para hacer que el NPC deambule (wander) a una posición aleatoria
    public void Wander()
    {
        Vector3 randomPoint;
        if (RandomPosition(transform.position, wanderRadius, out randomPoint))
        {
            agent.SetDestination(randomPoint);
            Debug.Log("Deambulando hacia: " + randomPoint);
        }
    }

    // Función auxiliar que calcula una posición aleatoria en el NavMesh
    public bool RandomPosition(Vector3 center, float range, out Vector3 result)
    {
        Vector3 randomDirection = Random.insideUnitSphere * range;  // Calcula una dirección aleatoria
        randomDirection += center;  // Ajusta la dirección al centro dado

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, range, NavMesh.AllAreas))
        {
            result = hit.position;
            return true;
        }
        result = Vector3.zero;
        return false;
    }

    // Función para que el NPC evada un objetivo (alejándose de él)
    public void Evade()
    {
        if (target != null)
        {
            Vector3 directionAwayFromTarget = transform.position - target.position;  // Dirección opuesta al objetivo
            Vector3 evadePosition = transform.position + directionAwayFromTarget.normalized * evadeDistance;  // Calcula la posición de evasión

            // Mueve al NPC a la posición de evasión
            MoveToPosition(evadePosition);
            Debug.Log("Evadir hacia: " + evadePosition);
        }
        else
        {
            Debug.LogWarning("El objetivo para evadir no está definido.");
        }
    }

    void Update()
    {
        // Solo a modo de prueba: Movimientos con teclas
        if (Input.GetKeyDown(KeyCode.W))  // Tecla "W" para Wander
        {
            Wander();
        }

        if (Input.GetKeyDown(KeyCode.M))  // Tecla "M" para MoveToPosition a una posición fija (ejemplo)
        {
            Vector3 targetPosition = new Vector3(10, 0, 10);  // Ejemplo de una posición específica
            MoveToPosition(targetPosition);
        }

        if (Input.GetKeyDown(KeyCode.E))  // Tecla "E" para Evade
        {
            Evade();
        }
    }
}
