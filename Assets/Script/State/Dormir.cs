using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dormir : Humano
{
    private void Awake()
    {
        typestate = TypeState.Dormir;
        LocadComponent();
    }
    public override void LocadComponent()
    {
        base.LocadComponent();

    }
    public override void Enter()
    {

    }
    public override void Execute()
    {
       // Jugar
        if (_DataAgent.Energy.value > 0.15f)
        {
            _StateMachine.ChangeState(TypeState.Jugar);
            return;
        }
        else
        {
            _DataAgent.LoadEnergy();
        }

        base.Execute();
    }
    public override void Exit()
    {

    }
}
