using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine  : Player
{
    public State CurrentState { get; set; }

    public void Initialize(State startState)
    { 
        CurrentState = startState;
        CurrentState.Enter();
    }

    public void ChangeState(State newState)
    { 
        CurrentState.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    
    }

}
