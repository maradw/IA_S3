using UnityEngine;
using UnityEngine.AI;

public class FindClosestNavMeshEdge : MonoBehaviour
{
    // Referencia al componente NavMeshAgent
    private NavMeshAgent agent;

    void Start()
    {
        // Obtiene el componente NavMeshAgent en el mismo GameObject
        agent = GetComponent<NavMeshAgent>();
    }

    // Función para encontrar el borde más cercano en el NavMesh desde una posición
    public Vector3 FindClosestEdge(Vector3 position)
    {
        NavMeshHit hit;

        // Intenta encontrar el borde más cercano del NavMesh desde la posición dada
        if (NavMesh.FindClosestEdge(position, out hit, NavMesh.AllAreas))
        {
            Debug.Log("Encontrado el borde más cercano en el NavMesh.");
            return hit.position; // Devuelve la posición más cercana en el borde
        }
        else
        {
            Debug.LogWarning("No se encontró ningún borde cercano en el NavMesh.");
            return Vector3.zero; // Devuelve un vector vacío si no se encuentra borde
        }
    }

    // Ejemplo para encontrar el borde más cercano al hacer clic derecho
    void Update()
    {
        if (Input.GetMouseButtonDown(1)) // Clic derecho del ratón
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Verifica si el raycast impacta en algún objeto
            if (Physics.Raycast(ray, out hit))
            {
                // Encuentra el borde más cercano en el NavMesh a la posición clicada
                Vector3 closestEdge = FindClosestEdge(hit.point);

                // Muestra visualmente el borde más cercano (por ejemplo, con una esfera)
                if (closestEdge != Vector3.zero)
                {
                    Debug.DrawLine(hit.point, closestEdge, Color.red, 2.0f);
                    Debug.Log("El borde más cercano está en: " + closestEdge);
                }
            }
        }
    }
}
