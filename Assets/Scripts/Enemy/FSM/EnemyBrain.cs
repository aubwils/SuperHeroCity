using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBrain : MonoBehaviour
{
    [SerializeField] private string initialState; // PatrolState
    [SerializeField] private EnemyFSMState[] states;
    private Dictionary<string, EnemyFSMState> stateDictionary;


    public EnemyFSMState currentState {get; private set;}
    public Transform PlayerTarget { get; set; }


    private void Start()
    {
        stateDictionary = new Dictionary<string, EnemyFSMState>();
        foreach (var state in states)
        {
            stateDictionary[state.ID] = state;
        }
        ChangeState(initialState);
    }

    private void Update()
    {
        currentState?.UpdateState(this);    // if currentState is not null, then call UpdateState
    }

    public void ChangeState(string newStateID)
    {
        if (currentState != null && currentState.ID == newStateID) return; // Avoid redundant state changes

        EnemyFSMState newState = GetState(newStateID);
        if (newState == null) return;

        Debug.Log($"Changing state from {currentState?.ID} to {newStateID}");
        currentState = newState;
    }

    private EnemyFSMState GetState(string stateID)
    {
        if (stateDictionary.TryGetValue(stateID, out var state))
        {
            return state;
        }
        return null;
    }

    public void SetPlayerTarget(Transform target)
    {
        PlayerTarget = target;
        Debug.Log($"Player target updated: {target?.name}");
    }

}
