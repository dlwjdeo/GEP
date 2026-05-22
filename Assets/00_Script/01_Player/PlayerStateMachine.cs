using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine
{
    public IState currentState;

    private Player player;

    public IState idleState;
    public IState moveState;
    public IState fishingState;
    public IState loggingState;
    public IState eatingState;
    public IState cookingState;

    public PlayerStateMachine(Player player)
    {
        this.player = player;
        idleState = new IdleState(player);
        moveState = new MoveState(player);
        fishingState = new FishingState(player);
        loggingState = new LoggingState(player);
        eatingState = new EatingState(player);
        cookingState = new CookingState(player);
        
        currentState = idleState;
        currentState.Enter();
    }

    public void ChangeState(IState newState)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }

        currentState = newState;

        if (currentState != null)
        {
            currentState.Enter();
        }
    }

    public void Update()
    {
        if (currentState != null)
        {
            currentState.Update();
        }
    }
}
