using UnityEngine;
using UnityEngine.AI;

public class AgentPathMovement : MonoBehaviour
{
    // Referencia al componente NavMeshAgent
    private NavMeshAgent agent;

    // Variable para almacenar el Path calculado
    private NavMeshPath path;

    void Start()
    {
        // Obtiene el componente NavMeshAgent en el mismo GameObject
        agent = GetComponent<NavMeshAgent>();

        // Inicializa el objeto NavMeshPath para almacenar el camino calculado
        path = new NavMeshPath();
    }

    // Función que calcula el camino hacia una posición objetivo y desplaza el agente a lo largo del camino
    public void CalculateAndMovePath(Vector3 targetPosition)
    {
        // Calcula el camino hacia la posición objetivo
        if (agent.CalculatePath(targetPosition, path))
        {
            // Verifica si el camino calculado es válido
            if (path.status == NavMeshPathStatus.PathComplete)
            {
                // Mueve el agente usando el camino calculado
                agent.SetPath(path);
            }
            else
            {
                Debug.LogWarning("No se encontró un camino completo hacia la posición objetivo.");
            }
        }
        else
        {
            Debug.LogWarning("Falló al calcular un camino hacia la posición objetivo.");
        }
    }

    // Ejemplo para calcular el camino hacia una posición con clic izquierdo
    void Update()
    {
        // Detección del clic izquierdo del ratón
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Verifica si el raycast impacta en algún objeto en el NavMesh
            if (Physics.Raycast(ray, out hit))
            {
                // Calcula el camino hacia la posición donde se hizo clic y mueve el agente
                CalculateAndMovePath(hit.point);
            }
        }
    }
}
