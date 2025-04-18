using UnityEngine;

public class DataSoundBase
{
    [Header("----- Sound Detection Settings -----")]
    public float hearingRange = 10f;    // Distancia máxima de escucha
    public float minSoundIntensity = 1f; // Intensidad mínima para detectar el sonido
    public Color soundColor = Color.green; // Color para debug
    public bool IsDrawGizmo = false;    // Dibujar Gizmo en escena

    public ViewObject Owner; // El objeto que escucha

    public virtual bool CanHearSound(Transform soundSource, float soundIntensity)
    {
        if (soundSource == null) return false;

        // Calcular la distancia entre el NPC y la fuente de sonido
        Vector3 origin = Owner.transform.position;
        Vector3 direction = soundSource.position - origin;
        float distanceToSound = direction.magnitude;

        // Verificar si la fuente de sonido está dentro del rango de escucha y si la intensidad es suficiente
        if (distanceToSound <= hearingRange && soundIntensity >= minSoundIntensity)
        {
            return true;
        }

        return false;
    }

    public virtual void OnDrawGizmos()
    {
        if (!IsDrawGizmo || Owner == null) return;

        Gizmos.color = soundColor;
        Gizmos.DrawWireSphere(Owner.transform.position, hearingRange);
    }
}

