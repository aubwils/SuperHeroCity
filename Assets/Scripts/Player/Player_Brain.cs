using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Brain : Entity_Brain
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

    public PlayerInputActions playerInputActions;

    public Player_Movement playerMovement { get; private set; }


    [SerializeField] private GameObject secretIdentityVisuals;
    [SerializeField] private GameObject heroIdentityVisuals;

    #endregion

    #region Player Stats
    [SerializeField] private bool isHero = false;
    #endregion


    #region States
    public Player_IdleState idleState { get; private set; }
    public Player_MoveState moveState { get; private set; }
    public Player_DashState dashState { get; private set; }
    public Player_TransformationState transformationState { get; private set; }
    public Player_PrimaryAttackState primaryAttackState { get; private set; }
    public Player_DeathState deathState { get; private set; }
    public Player_CounterAttackState counterAttackState { get; private set; }


    #endregion



    protected override void Awake()
    {
        base.Awake();
        playerMovement = GetComponent<Player_Movement>();
        playerInputActions = new PlayerInputActions();

        idleState = new Player_IdleState(this, stateMachine, "IsIdle");
        moveState = new Player_MoveState(this, stateMachine, "IsMoving");
        dashState = new Player_DashState(this, stateMachine, "IsDashing");
        transformationState = new Player_TransformationState(this, stateMachine, "IsTransforming");
        primaryAttackState = new Player_PrimaryAttackState(this, stateMachine, "IsAttacking");
        deathState = new Player_DeathState(this, stateMachine, "IsDead");
        counterAttackState = new Player_CounterAttackState(this, stateMachine, "TryCounterAttack");

        heroIdentityVisuals.SetActive(isHero); // Show hero visuals if isHero is true
        secretIdentityVisuals.SetActive(!isHero); // Show secret identity visuals if isHero is false
    }

    private void OnEnable()
    {
        playerInputActions.Player.Enable();

    }

    private void OnDisable()
    {

        playerInputActions.Player.Disable();
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
    protected override IEnumerator SlowDownEntityRoutine(float duration, float slowMultiplier)
    {
        float origionalMoveSpeed = playerMovement.moveSpeed;
        float origionalDashSpeed = playerMovement.dashSpeed;
        float origionalAnimationSpeed = animator.speed;

        float speedMultiplier = 1 - slowMultiplier;

        playerMovement.moveSpeed *= speedMultiplier;
        playerMovement.dashSpeed *= speedMultiplier;
        animator.speed *= speedMultiplier;

        yield return new WaitForSeconds(duration);

        playerMovement.moveSpeed = origionalMoveSpeed;
        playerMovement.dashSpeed = origionalDashSpeed;
        animator.speed = origionalAnimationSpeed;
    }

    public bool GetPlayerIdentity()
    {
        return isHero; // Return the current identity of the player
    }
    public override void CallAnimationFinishTrigger()
    {
        base.CallAnimationFinishTrigger();
    }

    public override Vector2 GetFacingDirection()
    {
        return playerMovement.GetLastMovementDirection();
    }
}
