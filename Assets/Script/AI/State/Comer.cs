using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Comer : StateMove
{


    // Start is called before the first frame update
    void Awake()
    {
        this.LoadComponent();
    }
    public override void LoadComponent()
    {
       //stateType = StateType.Comer;
        base.LoadComponent();
    }
    public override void Enter()
    {
         
        Debug.Log("Comer Enter ");
    }
    public override void Execute()
    {
        // Calcula la fuerza de direcci�n
        Vector3 steeringForce = _SteeringBehavior.Arrive(place);

        // Aplica la fuerza a la velocidad
        _SteeringBehavior.ClampMagnitude(steeringForce);

        // Actualiza la posici�n del objeto
        _SteeringBehavior.UpdatePosition();
        Debug.Log("Comer Execute ");
    }
    public override void Exit()
    {
        Debug.Log("Comer Exit ");
    }
    // Update is called once per frame
     
}
