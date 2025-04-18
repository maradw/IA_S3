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

    // Funci�n que calcula el camino hacia una posici�n objetivo y desplaza el agente a lo largo del camino
    public void CalculateAndMovePath(Vector3 targetPosition)
    {
        // Calcula el camino hacia la posici�n objetivo
        if (agent.CalculatePath(targetPosition, path))
        {
            // Verifica si el camino calculado es v�lido
            if (path.status == NavMeshPathStatus.PathComplete)
            {
                // Mueve el agente usando el camino calculado
                agent.SetPath(path);
            }
            else
            {
                Debug.LogWarning("No se encontr� un camino completo hacia la posici�n objetivo.");
            }
        }
        else
        {
            Debug.LogWarning("Fall� al calcular un camino hacia la posici�n objetivo.");
        }
    }

    // Ejemplo para calcular el camino hacia una posici�n con clic izquierdo
    void Update()
    {
        // Detecci�n del clic izquierdo del rat�n
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Verifica si el raycast impacta en alg�n objeto en el NavMesh
            if (Physics.Raycast(ray, out hit))
            {
                // Calcula el camino hacia la posici�n donde se hizo clic y mueve el agente
                CalculateAndMovePath(hit.point);
            }
        }
    }
}
