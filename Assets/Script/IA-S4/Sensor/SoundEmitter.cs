using UnityEngine;

public class SoundEmitter : MonoBehaviour
{
    public float soundIntensity = 5f;   // Intensidad del sonido emitido
    public bool IsMakingSound = false;  // Verifica si está emitiendo sonido

    private AudioSource audioSource;    // Para reproducir el sonido

    private void Start()
    {
        // Agregar o encontrar el AudioSource en el GameObject
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void Update()
    {
        // Detectar si el jugador presiona la tecla "E"
        if (Input.GetKeyDown(KeyCode.E))
        {
            EmitSound();
        }
    }

    // Método para emitir sonido
    public void EmitSound()
    {
        IsMakingSound = true;

        // Aquí puedes agregar el sonido que quieras reproducir
        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.Play();  // Reproduce el sonido
        }

        Debug.Log("Sound emitted from " + gameObject.name);
    }

    // Detener el sonido si es necesario
    public void StopSound()
    {
        IsMakingSound = false;

        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();  // Detiene el sonido
        }
    }
}

