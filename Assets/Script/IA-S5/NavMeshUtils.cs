using UnityEngine;
using UnityEngine.AI;

public class NavMeshUtils : MonoBehaviour
{
    // Referencia al NavMeshAgent
    private NavMeshAgent agent;

    void Start()
    {
        // Obtiene el componente NavMeshAgent
        agent = GetComponent<NavMeshAgent>();
    }

    // Función que encuentra el borde más cercano desde una posición
    public void FindClosestEdge(Vector3 position)
    {
        NavMeshHit hit;

        // Llama a NavMesh.FindClosestEdge para obtener el borde más cercano
        if (NavMesh.FindClosestEdge(position, out hit, NavMesh.AllAreas))
        {
            Debug.Log("Borde más cercano encontrado en: " + hit.position);
            Debug.DrawLine(position, hit.position, Color.red, 2.0f); // Dibuja una línea hacia el borde
        }
        else
        {
            Debug.LogWarning("No se encontró un borde cercano en el NavMesh.");
        }
    }

    // Función que realiza un raycast en el NavMesh entre dos posiciones
    public void PerformNavMeshRaycast(Vector3 startPos, Vector3 endPos)
    {
        NavMeshHit hit;

        // Realiza un raycast en el NavMesh para detectar colisiones entre startPos y endPos
        if (NavMesh.Raycast(startPos, endPos, out hit, NavMesh.AllAreas))
        {
            Debug.Log("Raycast colisionó con un obstáculo en: " + hit.position);
            Debug.DrawLine(startPos, hit.position, Color.yellow, 2.0f); // Dibuja una línea hasta el punto de colisión
        }
        else
        {
            Debug.Log("No se encontraron colisiones, camino libre.");
            Debug.DrawLine(startPos, endPos, Color.green, 2.0f); // Dibuja una línea verde si no hay colisiones
        }
    }

    // Ejemplo para probar ambas funciones con clic izquierdo y derecho
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Clic izquierdo: FindClosestEdge
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                FindClosestEdge(hit.point);
            }
        }

        if (Input.GetMouseButtonDown(1)) // Clic derecho: Raycast entre agente y posición de clic
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                PerformNavMeshRaycast(agent.transform.position, hit.point);
            }
        }
    }
}

