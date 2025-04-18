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

    // Funci�n para encontrar el borde m�s cercano en el NavMesh desde una posici�n
    public Vector3 FindClosestEdge(Vector3 position)
    {
        NavMeshHit hit;

        // Intenta encontrar el borde m�s cercano del NavMesh desde la posici�n dada
        if (NavMesh.FindClosestEdge(position, out hit, NavMesh.AllAreas))
        {
            Debug.Log("Encontrado el borde m�s cercano en el NavMesh.");
            return hit.position; // Devuelve la posici�n m�s cercana en el borde
        }
        else
        {
            Debug.LogWarning("No se encontr� ning�n borde cercano en el NavMesh.");
            return Vector3.zero; // Devuelve un vector vac�o si no se encuentra borde
        }
    }

    // Ejemplo para encontrar el borde m�s cercano al hacer clic derecho
    void Update()
    {
        if (Input.GetMouseButtonDown(1)) // Clic derecho del rat�n
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Verifica si el raycast impacta en alg�n objeto
            if (Physics.Raycast(ray, out hit))
            {
                // Encuentra el borde m�s cercano en el NavMesh a la posici�n clicada
                Vector3 closestEdge = FindClosestEdge(hit.point);

                // Muestra visualmente el borde m�s cercano (por ejemplo, con una esfera)
                if (closestEdge != Vector3.zero)
                {
                    Debug.DrawLine(hit.point, closestEdge, Color.red, 2.0f);
                    Debug.Log("El borde m�s cercano est� en: " + closestEdge);
                }
            }
        }
    }
}
