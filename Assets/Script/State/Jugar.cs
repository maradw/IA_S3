using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jugar : Humano
{
    private void Awake()
    {
        typestate = TypeState.Jugar;
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
        //comer
        if (_DataAgent.Energy.value < 0.15f)
        {
            _StateMachine.ChangeState(TypeState.Comer);
            return;
        }
        else {
            _DataAgent.DiscountEnergy();
        }
        //dormir
        if (_DataAgent.Energy.value < 0.15f)
        {
            _StateMachine.ChangeState(TypeState.Dormir);
            return;
        }
        else
        {
            _DataAgent.DiscountEnergy();
        }


        base.Execute();
    }
    public override void Exit()
    {

    }
}
