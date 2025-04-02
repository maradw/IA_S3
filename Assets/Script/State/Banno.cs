using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Banno : Humano
{
    private void Awake()
    {
        typestate = TypeState.Banno;
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
        if (_DataAgent.WC.value > 0.15f)
        {
            _StateMachine.ChangeState(TypeState.Banno);
            return;
        }
        else
        {
            _DataAgent.DiscountWC();
        }

        base.Execute();
    }
    public override void Exit()
    {

    }
}
