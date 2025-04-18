using UnityEngine;
using UnityEngine.AI;

public class NPCController : MonoBehaviour
{
    private NavMeshAgent agent;  // Referencia al NavMeshAgent
    public Transform target;     // Objetivo a seguir (para Evade o MoveToPosition)
    public float wanderRadius = 10f;  // Radio de b�squeda para Wander
    public float evadeDistance = 5f;  // Distancia a mantener para Evade

    void Start()
    {
        // Inicializa el componente NavMeshAgent en el NPC
        agent = GetComponent<NavMeshAgent>();
    }

    // Funci�n para mover al NPC a una posici�n espec�fica
    public void MoveToPosition(Vector3 position)
    {
        if (agent != null)
        {
            agent.SetDestination(position);
            Debug.Log("Moviendo al NPC a la posici�n: " + position);
        }
    }

    // Funci�n para hacer que el NPC deambule (wander) a una posici�n aleatoria
    public void Wander()
    {
        Vector3 randomPoint;
        if (RandomPosition(transform.position, wanderRadius, out randomPoint))
        {
            agent.SetDestination(randomPoint);
            Debug.Log("Deambulando hacia: " + randomPoint);
        }
    }

    // Funci�n auxiliar que calcula una posici�n aleatoria en el NavMesh
    public bool RandomPosition(Vector3 center, float range, out Vector3 result)
    {
        Vector3 randomDirection = Random.insideUnitSphere * range;  // Calcula una direcci�n aleatoria
        randomDirection += center;  // Ajusta la direcci�n al centro dado

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, range, NavMesh.AllAreas))
        {
            result = hit.position;
            return true;
        }
        result = Vector3.zero;
        return false;
    }

    // Funci�n para que el NPC evada un objetivo (alej�ndose de �l)
    public void Evade()
    {
        if (target != null)
        {
            Vector3 directionAwayFromTarget = transform.position - target.position;  // Direcci�n opuesta al objetivo
            Vector3 evadePosition = transform.position + directionAwayFromTarget.normalized * evadeDistance;  // Calcula la posici�n de evasi�n

            // Mueve al NPC a la posici�n de evasi�n
            MoveToPosition(evadePosition);
            Debug.Log("Evadir hacia: " + evadePosition);
        }
        else
        {
            Debug.LogWarning("El objetivo para evadir no est� definido.");
        }
    }

    void Update()
    {
        // Solo a modo de prueba: Movimientos con teclas
        if (Input.GetKeyDown(KeyCode.W))  // Tecla "W" para Wander
        {
            Wander();
        }

        if (Input.GetKeyDown(KeyCode.M))  // Tecla "M" para MoveToPosition a una posici�n fija (ejemplo)
        {
            Vector3 targetPosition = new Vector3(10, 0, 10);  // Ejemplo de una posici�n espec�fica
            MoveToPosition(targetPosition);
        }

        if (Input.GetKeyDown(KeyCode.E))  // Tecla "E" para Evade
        {
            Evade();
        }
    }
}
