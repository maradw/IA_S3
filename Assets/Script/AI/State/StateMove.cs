using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMove : StateBase
{
    protected SteeringBehavior2 _SteeringBehavior;
    public Transform place;
    // Start is called before the first frame update
    public override void LoadComponent()
    {
        _SteeringBehavior = GetComponent<SteeringBehavior2>();
        base.LoadComponent();
    }
    public virtual void MoveToPlace()
    {
        Vector3 steeringForce = _SteeringBehavior.Arrive(place);

        _SteeringBehavior.ClampMagnitude(steeringForce);

        _SteeringBehavior.UpdatePosition();
    }
}
