using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBrain : MonoBehaviour
{
    [SerializeField] private string initialState; // PatrolState
    [SerializeField] private EnemyFSMState[] states;

    public EnemyFSMState currentState {get; private set;}

    private Transform player;
 
    private void Start()
    {
        ChangeState(initialState);
    }

    private void Update()
    {
        currentState?.UpdateState(this);    // if currentState is not null, then call UpdateState
    }

    public void ChangeState(string newStateID)
    {
        EnemyFSMState newState = GetState(newStateID);
        if(newState == null) return;
        
        currentState = newState;
    }

    private EnemyFSMState GetState(string stateID)
    {
        for (int i = 0; i < states.Length; i++)
        {
            if(states[i].ID == stateID)
            {
                return states[i];
            }
        }
        return null;
    }

       public void UpdatePlayerTarget(Transform newPlayer)
    {
        player = newPlayer;
    }

}
