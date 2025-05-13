using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : CombatCharacterBrain
{
    #region Components
    [SerializeField] private Animator heroAnimator;
    [SerializeField] private Animator seceretAnimator;
    
    public PlayerStateMachine stateMachine {get; private set;}
    public PlayerMovement playerMovement {get; private set;} 

    [SerializeField] private GameObject seceretIdentityVisuals;
    [SerializeField] private GameObject heroIdentityVisuals;

    #endregion

    #region Player Stats
    [SerializeField] private bool isHero = false;
    public bool isBusy {get; private set;}
    #endregion





    // #region Come Back to in Future
    // Attack Movement Data for pushing the player during attack. Felt weird in testing will look at again once we have real attack animations
    // Was in Player and Player PrimaryAttackState scripts
    // [System.Serializable]
    // public struct AttackMovementData
    // {
    //     public float pushDistance;
    //     public float pushDuration;
    // }
    // public AttackMovementData[] attackMovement;
    // #endregion
  


    #region States
    public PlayerIdleState idleState {get; private set;}
    public PlayerMoveState moveState {get; private set;}
    public PlayerDashState dashState {get; private set;}
    public PlayerTransformationState transformationState {get; private set;}
    public PlayerPrimaryAttackState primaryAttackState {get; private set;}
    #endregion

        

    public override void Awake()
    {
        base.Awake(); 

        stateMachine = new PlayerStateMachine();
        idleState = new PlayerIdleState(stateMachine, this, "IsIdle");
        moveState = new PlayerMoveState(stateMachine, this, "IsMoving");
        dashState = new PlayerDashState(stateMachine, this, "IsDashing");
        transformationState = new PlayerTransformationState(stateMachine, this, "IsTransforming");
        primaryAttackState = new PlayerPrimaryAttackState(stateMachine, this, "IsAttacking");

        playerMovement = GetComponent<PlayerMovement>();

        animator = isHero ? heroAnimator : seceretAnimator; // Set the animator based on the player's identity
        heroIdentityVisuals.SetActive(isHero); // Show hero visuals if isHero is true
        seceretIdentityVisuals.SetActive(!isHero); // Show secret identity visuals if isHero is false

    }

    private void Start()
    {
        stateMachine.Initialize(idleState);
    }
    private void Update()
    {
        stateMachine.currentState.Update();
    }

     private void FixedUpdate()
    {
        stateMachine.currentState.FixedUpdate();
    }

    public void AnimationTrigger() => stateMachine.currentState.AnimationFinishTrigger();

    public void ToggleHeroIdentity()
    {
       if (isHero)
        {
            isHero = false;
            seceretIdentityVisuals.SetActive(true);
            heroIdentityVisuals.SetActive(false);
            animator = seceretAnimator;
        }
        else
        {
            isHero = true;
            seceretIdentityVisuals.SetActive(false);
            heroIdentityVisuals.SetActive(true);
            animator = heroAnimator;
        }
    }


     public bool GetPlayerIdentity()
    {
        return isHero; // Return the current identity of the player
    }

    public IEnumerator BusyFor(float duration)
    {
        isBusy = true;
        Debug.Log("Player is busy for " + duration + " seconds.");
        yield return new WaitForSeconds(duration);
        Debug.Log("Player is no longer busy.");
        isBusy = false;
    }

    // #region Come Back to in Future
    // Attack Movement Data for pushing the player during attack. Felt weird in testing will look at again once we have real attack animations
    // Was in Player and Player PrimaryAttackState scripts
    // public IEnumerator ApplyAttackPush(Vector2 direction, float distance, float duration)
    // {
    //     float elapsed = 0f;
    //     Vector2 startPos = playerMovement.GetRigidbody().position;
    //     Vector2 targetPos = startPos + direction.normalized * distance;

    //     while (elapsed < duration)
    //     {
    //         elapsed += Time.deltaTime;
    //         Vector2 newPos = Vector2.Lerp(startPos, targetPos, elapsed / duration);
    //         playerMovement.GetRigidbody().MovePosition(newPos);
    //         yield return null;
    //     }
    // }
    //


}
