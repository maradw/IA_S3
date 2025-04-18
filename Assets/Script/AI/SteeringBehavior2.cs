using UnityEngine;

public class SteeringBehavior2 : MonoBehaviour
{
    // Atributo que representa el objetivo al que se desea acercar
    //public Transform Target;

    // Velocidad m�xima del objeto
    public float maxSpeed = 10f;

    // Fuerza m�xima de la aceleraci�n
    public float maxForce = 5f;

    // Velocidad actual del objeto
    public Vector3 velocity;
    // Radio de desaceleraci�n
    public float slowingRadius = 5f;

    public void UpdatePosition()
    {
        transform.position += velocity * Time.deltaTime;
    }
    public void  ClampMagnitude(Vector3 steeringForce)
    {
        // Aplica la fuerza a la velocidad
        velocity = Vector3.ClampMagnitude(velocity + steeringForce * Time.deltaTime, maxSpeed);

    }
    // Funci�n de Seek que calcula la fuerza de direcci�n hacia el objetivo
    public Vector3 Seek(Transform target)
    {
        // Calcula el vector deseado hacia el objetivo
        Vector3 desired = target.position - transform.position;
        desired.Normalize();
        desired *= maxSpeed;

        // Calcula la fuerza de direcci�n
        Vector3 steering = desired - velocity;
        steering = Vector3.ClampMagnitude(steering, maxForce);

        return steering;
    }
    public Vector3 Flee(Transform target)
    {
        // Calcula el vector deseado alej�ndose del objetivo
        Vector3 desired = transform.position - target.position;
        desired.Normalize();
        desired *= maxSpeed;

        // Calcula la fuerza de direcci�n
        Vector3 steering = desired - velocity;
        steering = Vector3.ClampMagnitude(steering, maxForce);

        return steering;
    }
    public Vector3 Arrive(Transform target)
    {
        // Calcula el vector hacia el objetivo
        Vector3 desired = target.position - transform.position;
        float distance = desired.magnitude;

        // Si el objeto est� dentro del radio de desaceleraci�n
        if (distance < slowingRadius)
        {
            // Escala la velocidad proporcionalmente a la distancia
            desired = desired.normalized * maxSpeed * (distance / slowingRadius);
        }
        else
        {
            // Aplica la velocidad m�xima fuera del radio de desaceleraci�n
            desired = desired.normalized * maxSpeed;
        }

        // Calcula la fuerza de direcci�n
        Vector3 steering = desired - velocity;
        steering = Vector3.ClampMagnitude(steering, maxForce);

        return steering;
    }
    //void Update()
    //{
    //    // Calcula la fuerza de direcci�n
    //    Vector3 steeringForce = Seek(Target);

    //    // Aplica la fuerza a la velocidad
    //    velocity = Vector3.ClampMagnitude(velocity + steeringForce * Time.deltaTime, maxSpeed);

    //    // Actualiza la posici�n del objeto
    //    transform.position += velocity * Time.deltaTime;
    //}
}
