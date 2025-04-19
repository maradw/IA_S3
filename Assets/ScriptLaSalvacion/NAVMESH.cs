using UnityEngine;
using UnityEngine.AI;

public class NPCMovementController : MonoBehaviour
{
    // Definici�n del enum para seleccionar la acci�n de movimiento
    public enum MovementType
    {
        SamplePosition,
        MoveToTarget,
        CalculatePath,
        FindClosestEdge,
        RayCast,
    }
    public enum targed
    {
        sala,
        dormitorio,
        ba�o,
        comedor,
        patio,
    }

    [Header("Configuraci�n del Movimiento")]
    // Selecci�n del tipo de movimiento desde el Inspector
    public MovementType movementType;
    // Transform que representa el target (por ejemplo, un objeto en la escena)
    public Transform targetTransform;
    // Rango para la b�squeda de una posici�n v�lida en el NavMesh
    public float sampleRange = 10f;

    private NavMeshAgent agent;
    void Awake()
    {
        // Obtenci�n del componente NavMeshAgent del GameObject
        agent = GetComponent<NavMeshAgent>();
    }
    public bool SamplePosition(Vector3 origin, float range, out Vector3 resultPos)
    {
        NavMeshHit hit;
        if (NavMesh.SamplePosition(origin, out hit, range, NavMesh.AllAreas))
        {
            resultPos = hit.position;
            return true;
        }
        resultPos = Vector3.zero;
        return false;
    }
   
    public void MoveToTargetPosition(Vector3 targetPosition)
    {
        Vector3 validPosition;
        if (SamplePosition(targetPosition, sampleRange, out validPosition))
        {
            agent.SetDestination(validPosition);
            Debug.Log("Moviendo al objetivo v�lido: " + validPosition);
        }
        else
        {
            Debug.LogWarning("No se encontr� posici�n v�lida cerca del target");
        }
    }

    public NavMeshPath CalculatePath(Vector3 targetPosition)
    {
        NavMeshPath path = new NavMeshPath();
        if (agent.CalculatePath(targetPosition, path))
        {
            Debug.Log("Path calculado con " + path.corners.Length + " puntos");

            return path;
        }
        else
        {
            Debug.LogWarning("No se pudo calcular el path hacia el target");
            return null;
        }
    } 
    public Vector3 FindClosestEdge(Vector3 position)
    {
        NavMeshHit hit;
        if (NavMesh.FindClosestEdge(position, out hit, NavMesh.AllAreas))
        {
            Debug.Log("Borde m�s cercano encontrado en: " + hit.position);
            return hit.position;
        }
        else
        {
            Debug.LogWarning("No se encontr� un borde cercano en el NavMesh");
            return position;
        }
    }   
    public bool RayCast(Vector3 startPosition, Vector3 endPosition, out NavMeshHit hitInfo)
    {
        bool collision = NavMesh.Raycast(startPosition, endPosition, out hitInfo, NavMesh.AllAreas);
        if (collision)
        {
            Debug.Log("Colisi�n detectada en: " + hitInfo.position);
        }
        else
        {
            Debug.Log("No se detect� colisi�n en el raycast");
        }
        return collision;
    }
    void Update()
    {
        // Para fines de prueba, se ejecuta la acci�n seleccionada al presionar la tecla Espacio
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ExecuteMovement();
        }
    }
    void ExecuteMovement()
    {
        // Se toma la posici�n del target o se utiliza la posici�n actual si no hay target asignado
        Vector3 targetPos = targetTransform != null ? targetTransform.position : transform.position;

        switch (movementType)
        {
            case MovementType.SamplePosition:
                {
                    Vector3 validPosition;
                    if (SamplePosition(targetPos, sampleRange, out validPosition))
                        Debug.Log("SamplePosition: " + validPosition);
                    else
                        Debug.LogWarning("No se encontr� posici�n v�lida con SamplePosition");
                    break;
                }
            case MovementType.MoveToTarget:
                {
                    MoveToTargetPosition(targetPos);
                    break;
                }
            case MovementType.CalculatePath:
                {
                    NavMeshPath path = CalculatePath(targetPos);
                    if (path != null && path.corners.Length > 1)
                    {
                        for (int i = 0; i < path.corners.Length - 1; i++)
                        {
                            Debug.DrawLine(path.corners[i], path.corners[i + 1], Color.green, 22f);
                        }
                    }
                    break;
                }
            case MovementType.FindClosestEdge:
                {
                    Vector3 edgePosition = FindClosestEdge(transform.position);
                    Debug.Log("Punto de borde: " + edgePosition);
                    break;
                }
            case MovementType.RayCast:
                {
                    NavMeshHit hitInfo;
                    bool hit = RayCast(transform.position, targetPos, out hitInfo);
                    Debug.Log("Resultado de RayCast: " + hit);
                    break;
                }
        }
    }
}
