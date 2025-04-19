using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class SeguirJuguete : Humano
{
    private MovementController _movementController;
    private bool reachedDestination = false;
    private PathMover _pathMover;
    private Transform currentToy;
    private IAEye _iaEye;

    private void Awake()
    {
        typestate = TypeState.SeguirJuguete;
        LocadComponent();
        _movementController = GetComponent<MovementController>();
        _pathMover = GetComponent<PathMover>();

    }
    public override void LocadComponent()
    {
        base.LocadComponent();
        _iaEye = GetComponent<IAEye>();

    }
    // M�todo que se invoca al finalizar el movimiento
    private void OnArrival()
    {

        reachedDestination = true;

        if (_pathMover != null)
        {
            _pathMover.StartPathMovement();
        }
        // Se puede colocar aqu� cualquier inicializaci�n adicional que se quiera ejecutar al llegar
        Debug.Log("He llegado al destino. Empieza la l�gica de SeguirJuguete.");
    }

    public override void Enter()
    {
        reachedDestination = false;

        if (_iaEye != null && _iaEye.ViewToy != null)
        {
            currentToy = _iaEye.ViewToy.transform;
        }

        if (currentToy != null && _movementController != null)
        {
            _movementController.agent.SetDestination(currentToy.position);
        }

        base.Enter();


        /*reachedDestination = false; // Aseg�rate de resetear la bandera al entrar al estado
        // Primero mueve al personaje al destino asignado 
        if (_movementController != null && destination != null)
        {
            _movementController.MoveTo(destination, OnArrival);
        }
        else
        {
            // Si no hay destino asignado o MovementController, ejecuta la l�gica sin desplazarse
            OnArrival();
        }*/
    }
    public override void Execute()
    {
        /*    if (!reachedDestination)
            return;

        // Prioridad 1: Chequear energ�a para comer
        if (_DataAgent.Energy.value < 0.25f)
        {
            _StateMachine.ChangeState(TypeState.Comer);
            return; // Salir inmediatamente despu�s de cambiar estado
        }

        // Prioridad 2: Chequear sue�o para dormir
        if (_DataAgent.Sleep.value >= 0.75f)
        {
            _StateMachine.ChangeState(TypeState.Dormir);
            return;
        }

        // Prioridad 3: Chequear necesidad de ba�o
        if (_DataAgent.WC.value >= 0.6f)
        {
            _StateMachine.ChangeState(TypeState.Banno);
            return;
        }

        // Aumentar necesidad de ba�o mientras juega (agrega esto)
        _DataAgent.IncreaseWC();
        // Si no hay necesidades urgentes, sigue jugando
        _DataAgent.DiscountEnergy();



        base.Execute();*/



        if (_iaEye == null || _iaEye.ViewToy == null)
        {
            _StateMachine.ChangeState(TypeState.Jugar); // O el estado por defecto
            return;
        }

        // Si el juguete sigue visible, actualizar posici�n constantemente
        currentToy = _iaEye.ViewToy.transform;

        if (_movementController != null && currentToy != null)
        {
            _movementController.agent.SetDestination(currentToy.position);
        }

        // Resto de condiciones (energ�a, WC, sue�o...)
        if (_DataAgent.Energy.value < 0.24f)
        {
            _StateMachine.ChangeState(TypeState.Comer);
            return;
        }

        if (_DataAgent.Sleep.value >= 0.75f)
        {
            _StateMachine.ChangeState(TypeState.Dormir);
            return;
        }

        if (_DataAgent.WC.value >= 0.6f)
        {
            _StateMachine.ChangeState(TypeState.Banno);
            return;
        }

        _DataAgent.IncreaseWC();
        _DataAgent.DiscountEnergy();

        base.Execute();
    }

    public override void Exit()
    {

        _DataAgent.StopLoadingEnergy(); // Detiene la recarga de energ�a
        if (_pathMover != null)
        {
            _pathMover.StopPathMovement();
        }
    }
}
