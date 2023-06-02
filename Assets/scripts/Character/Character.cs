using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Character : MonoBehaviour
{

    [Header("Controls")]
    public float playerSpeed = 15f;
    public float sprintSpeed = 7.0f;
    public float jumpHeight = 0.8f;
    public float gravityMultiplier = 2;
    public float rotationSpeed = 5f;
    public GameObject weapon;

    [Header("Animation Smoothing")]
    [Range(0, 1)]
    public float speedDampTime = 0.1f;
    [Range(0, 1)]
    public float velocityDampTime = 0.9f;
    [Range(0, 1)]
    public float rotationDampTime = 0.2f;
    [Range(0, 1)]
    public float airControl = 0.5f; //how much the player can move when he is in the air

    public StateMachine movementSM;
    public StandingState standing;
    public JumpingState jumping;
    public SprintState sprinting;
    public CombatState combatting;
    public AttackState attacking;

    [HideInInspector]
    public float gravityValue = -9.81f;
    [HideInInspector]
    public CharacterController controller;
    [HideInInspector]
    public PlayerInput playerInput;
    [HideInInspector]
    public Transform cameraTransform;
    [HideInInspector]
    public Animator animator;
    [HideInInspector]
    public Vector3 playerVelocity;




    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();
        cameraTransform = Camera.main.transform;

        movementSM = new StateMachine();
        standing = new StandingState(this, movementSM);
        jumping = new JumpingState(this, movementSM);
        sprinting = new SprintState(this, movementSM);
        combatting = new CombatState(this, movementSM);
        attacking = new AttackState(this, movementSM);

        movementSM.Initialize(standing);

        gravityValue *= gravityMultiplier;

        
    }

    // Update is called once per frame
    void Update()
    {

        movementSM.currentState.HandleInput();

        movementSM.currentState.LogicUpdate();

    }

    private void FixedUpdate()
    {
        movementSM.currentState.PhysicsUpdate();
    }
}
