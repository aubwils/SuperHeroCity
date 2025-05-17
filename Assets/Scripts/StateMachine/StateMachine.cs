using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public EntityState currentState { get; private set; }

    public void Initialize(EntityState startingState)
    {
        currentState = startingState;
        currentState.Enter();
    }
    public void ChangeState(EntityState newState)
    {
        currentState.Exit();
        currentState = newState;
        currentState.Enter();
    }
}
