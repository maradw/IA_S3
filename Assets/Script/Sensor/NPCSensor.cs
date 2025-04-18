using UnityEngine;

public class NPCSensor : MonoBehaviour
{
    public SphereCollider detectionCollider;
    private bool playerInRange = false;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            Debug.Log("Jugador en rango.");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            Debug.Log("Jugador fuera de rango.");
        }
    }

    public void HearAudio()
    {
        if (playerInRange)
        {
            Debug.Log("NPC: Te escucho");
        }
        else
        {
            Debug.Log("NPC: No hay nadie en el rango.");
        }
    }
}

