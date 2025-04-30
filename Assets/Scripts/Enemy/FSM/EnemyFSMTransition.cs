using System;

[Serializable]
public class EnemyFSMTransition 
{
    public EnemyFSMDecision decision; // Playerinrangeofattack -- return true or false
    public string TrueState; // current state -> attackstate
    public string FalseState; // current state -> patrolstate

}

