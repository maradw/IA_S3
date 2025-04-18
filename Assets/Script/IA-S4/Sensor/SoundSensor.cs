using UnityEngine;

public class SoundSensor : MonoBehaviour
{
    public float detectionRadius = 10f;     // Rango de detecci�n del sonido
    public LayerMask soundLayer;            // Capa para detectar los objetos emisores de sonido
    private bool hasHeardSound = false;     // Si el NPC ha escuchado un sonido
    private Collider[] soundSources;        // Fuentes de sonido dentro del rango

    private void Update()
    {
        DetectSound();
    }

    private void DetectSound()
    {
        // Detectar todos los objetos con SoundEmitter dentro del rango de detecci�n
        soundSources = Physics.OverlapSphere(transform.position, detectionRadius, soundLayer);

        hasHeardSound = false; // Resetear la detecci�n de sonido en cada frame

        foreach (Collider collider in soundSources)
        {
            SoundEmitter soundEmitter = collider.GetComponent<SoundEmitter>();
            if (soundEmitter != null && soundEmitter.IsMakingSound)
            {
                hasHeardSound = true;
                break;
            }
        }

        // Mandar el mensaje basado en si escuch� o no el sonido
        if (hasHeardSound)
        {
            Debug.Log("Te escucho");
        }
        else
        {
            Debug.Log("No te escucho");
        }
    }

    // Visualizaci�n del rango de detecci�n en el editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}


