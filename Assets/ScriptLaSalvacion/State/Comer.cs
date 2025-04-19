using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Comer : Humano
{
    private MovementController _movementController;
    private bool reachedDestination = false;
    private void Awake()
    {
        typestate = TypeState.Comer;
        LocadComponent();
        _movementController = GetComponent<MovementController>();
    }
    public override void LocadComponent()
    {
        base.LocadComponent();

    }
    private void OnArrival()
    {
        reachedDestination = true;
        _DataAgent.LoadEnergy();
        // Se puede colocar aqu� cualquier inicializaci�n adicional que se quiera ejecutar al llegar
        Debug.Log("He llegado al destino. Empieza la l�gica de Comer.");
    }
  
    public override void Enter()
    {
        reachedDestination = false;
        if (_movementController != null && destination != null)
        {
            _movementController.MoveTo(destination, OnArrival);
        }
        else
        {
            OnArrival();
        }
    }
    public override void Execute()
    {
        if (!reachedDestination)
            return;

        base.Execute();

        _DataAgent.IncreaseSleep(); // Aumenta el sue�o

        // Si el sue�o llega al 75%, ve a dormir
        if (_DataAgent.Sleep.value >= 0.75f)
        {
            _StateMachine.ChangeState(TypeState.Dormir);
        }
    }
    public override void Exit()
    {
        _DataAgent.StopLoadingEnergy();
    }
}
