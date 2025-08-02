using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class Player_Brain : Entity_Brain
{
    [Header("Hero Visual Settings")]
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
    [SerializeField] private GameObject secretIdentityVisuals;
    [SerializeField] private GameObject heroIdentityVisuals;

    public PlayerInputActions playerInputActions;
    public Vector2 mousePosition { get; private set; }

    public Player_Movement playerMovement { get; private set; }
    public Player_SkillManager skillManager { get; private set; }
    public Player_VFX vfx { get; private set; }
    public Player_Health health { get; private set; }
    public Entity_StatusHandler statusHandler { get; private set; }

    private UI ui;

    [Header("Hero Settings")]
    [SerializeField] private bool isHero = false;
    [SerializeField] private OriginType originType;
    public OriginType OriginType => originType;

    #region States
    public Player_IdleState idleState { get; private set; }
    public Player_MoveState moveState { get; private set; }
    public Player_DashState dashState { get; private set; }
    public Player_TransformationState transformationState { get; private set; }
    public Player_PrimaryAttackState primaryAttackState { get; private set; }
    public Player_DeathState deathState { get; private set; }
    public Player_CounterAttackState counterAttackState { get; private set; }
    public Player_ThrowingState throwingState { get; private set; }
    #endregion



    protected override void Awake()
    {
        base.Awake();

        ui = FindAnyObjectByType<UI>(); // heavy on preformance, NEVER Do in the update. ok to do once in the awake or start.
        playerMovement = GetComponent<Player_Movement>();
        skillManager = GetComponent<Player_SkillManager>();
        vfx = GetComponent<Player_VFX>();
        statusHandler = GetComponent<Entity_StatusHandler>();
        health = GetComponent<Player_Health>();

        playerInputActions = new PlayerInputActions();

        idleState = new Player_IdleState(this, stateMachine, "IsIdle");
        moveState = new Player_MoveState(this, stateMachine, "IsMoving");
        dashState = new Player_DashState(this, stateMachine, "IsDashing");
        transformationState = new Player_TransformationState(this, stateMachine, "IsTransforming");
        primaryAttackState = new Player_PrimaryAttackState(this, stateMachine, "IsAttacking");
        deathState = new Player_DeathState(this, stateMachine, "IsDead");
        counterAttackState = new Player_CounterAttackState(this, stateMachine, "TryCounterAttack");
        throwingState = new Player_ThrowingState(this, stateMachine, "IsThrowing");

        heroIdentityVisuals.SetActive(isHero); // Show hero visuals if isHero is true
        secretIdentityVisuals.SetActive(!isHero); // Show secret identity visuals if isHero is false
    }

    private void OnEnable()
    {
        playerInputActions.Player.Enable();

        playerInputActions.Player.Mouse.performed += ctx => mousePosition = ctx.ReadValue<Vector2>();

        playerInputActions.Player.ToggleSkillTreeUI.performed += ctx => ui.ToggleSkillTreeUI();

        playerInputActions.Player.SkillOne.performed += ctx => skillManager.deployableBomb.TryUseSkill();
        playerInputActions.Player.SkillTwo.performed += ctx => skillManager.portal.TryUseSkill();
    }

    private void OnDisable()
    {
        playerInputActions.Player.Disable();

        //Dont need a cancel because should always have position of

        playerInputActions.Player.ToggleSkillTreeUI.performed -= ctx => ui.ToggleSkillTreeUI();

        playerInputActions.Player.SkillOne.performed -= ctx => skillManager.deployableBomb.TryUseSkill();
        playerInputActions.Player.SkillTwo.performed -= ctx => skillManager.portal.TryUseSkill();
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

    public void TeleportPlayer(Vector3 position) => transform.position = position;

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

    public void HandleFacing(Vector2 directionToAnimate)
    {
        if (directionToAnimate != Vector2.zero)
        {
            // Update animator parameters for facing direction
            animator.SetFloat("MoveX", directionToAnimate.x);
            animator.SetFloat("MoveY", directionToAnimate.y);

            // Update the last movement direction so other systems know which way we're facing
            playerMovement.SetLastMovementDirection(directionToAnimate);
        }
    }

    public override Vector2 GetFacingDirection()
    {
        return playerMovement.GetLastMovementDirection();
    }
    
    public bool CanMoveInCurrentState()
    {
        var currentState = StateMachine.currentState;
        return currentState is Player_MoveState ||
            currentState is Player_ThrowingState;
    }


}
