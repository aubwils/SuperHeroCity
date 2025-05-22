using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : CharacterBrain
{
    #region Components
    [SerializeField] private Animator heroAnimator;
    [SerializeField] private Animator seceretAnimator;
    public Animator CurrentAnimator
    {
        get
        {
            if (heroIdentityVisuals.activeSelf)
                return heroIdentityVisuals.GetComponent<Animator>();
            else
                return secretIdentityVisuals.GetComponent<Animator>();
        }
    }

    
    public PlayerMovement playerMovement { get; private set; } 


    [SerializeField] private GameObject secretIdentityVisuals;
    [SerializeField] private GameObject heroIdentityVisuals;

    #endregion
 
    #region Player Stats
    [SerializeField] private bool isHero = false;
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
    public PlayerDeathState deathState {get; private set;}


    #endregion

        

   protected override void Awake()
    {
        base.Awake();
        playerMovement = GetComponent<PlayerMovement>();

        idleState = new PlayerIdleState(this, stateMachine, "IsIdle");
        moveState = new PlayerMoveState(this, stateMachine, "IsMoving");
        dashState = new PlayerDashState(this, stateMachine, "IsDashing");
        transformationState = new PlayerTransformationState(this, stateMachine, "IsTransforming");
        primaryAttackState = new PlayerPrimaryAttackState(this, stateMachine, "IsAttacking");
        deathState = new PlayerDeathState(this, stateMachine, "IsDead");

        heroIdentityVisuals.SetActive(isHero); // Show hero visuals if isHero is true
        secretIdentityVisuals.SetActive(!isHero); // Show secret identity visuals if isHero is false
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
    }
    protected override void Update()
    {
        base.Update();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }


    public void ToggleHeroIdentity()
    {
        if (isHero)
        {
            isHero = false;
            secretIdentityVisuals.SetActive(true);
            heroIdentityVisuals.SetActive(false);
            animator = seceretAnimator;
        }
        else
        {
            isHero = true;
            secretIdentityVisuals.SetActive(false);
            heroIdentityVisuals.SetActive(true);
            animator = heroAnimator;
        }
    }


     public bool GetPlayerIdentity()
    {
        return isHero; // Return the current identity of the player
    }
    public override void CallAnimationFinishTrigger()
    {
        base.CallAnimationFinishTrigger();
    }

  
}
