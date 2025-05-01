using System;

[Serializable]
public class EnemyFSMState 
{
    public string ID;
    public EnemyFSMAction[] Actions; //moveaction | attackaction | ...
    public EnemyFSMTransition[] Transitions;

    public void UpdateState(EnemyBrain enemyBrain)
    {
        ExecuteActions();
        ExecuteTransitions(enemyBrain);
    }
    

    private void ExecuteActions()
    {
        for (int i = 0; i < Actions.Length; i++)
        {
            Actions[i].Act();
        }
    }

    private void ExecuteTransitions(EnemyBrain enemyBrain)
    {
        if(Transitions == null || Transitions.Length <= 0) return;
        for (int i = 0; i < Transitions.Length; i++)
        {
           bool value = Transitions[i].decision.Decide();
              if(value)
              {
                enemyBrain.ChangeState(Transitions[i].TrueState);
              }
              else
              {
                enemyBrain.ChangeState(Transitions[i].FalseState);
              }
        }
    }

}
