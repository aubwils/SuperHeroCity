using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState 
{
    protected PlayerStateMachine stateMachine;
    protected Player player;

    private string animBoolName;

    public PlayerState(PlayerStateMachine stateMachine, Player player, string animBoolName)
    {
        this.stateMachine = stateMachine;
        this.player = player;
        this.animBoolName = animBoolName;
    }
    
    public virtual void Enter() 
    {
        Debug.Log("Entering state: " + animBoolName);
        player.animator.SetBool(animBoolName, true);
        
    }
    
    public virtual void Exit() 
    {
        Debug.Log("Exiting state: " + animBoolName);
        player.animator.SetBool(animBoolName, false);
    }
    
    public virtual void Update() 
    {
         Debug.Log("Updating state: " + animBoolName);
    }
    
}
