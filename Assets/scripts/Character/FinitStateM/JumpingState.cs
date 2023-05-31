using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingState : State
{
    bool grounded;

    float gravityValue;
    float jumpHeight;
    float playerSpeed;

    float timePassed;
    float landingTime;

    Vector3 airVelocity;

    public JumpingState(Character _character, StateMachine _stateMachine) : base(_character, _stateMachine)
    {
        character = _character;
        stateMachine = _stateMachine;
    }

    public override void Enter()
    {
        base.Enter();

        timePassed = 0f;

        grounded = false;
        gravityValue = character.gravityValue;
        jumpHeight = character.jumpHeight;
        playerSpeed = character.playerSpeed;
        gravityVelocity.y = 0;

        character.animator.SetFloat("speed", 0);
        character.animator.SetTrigger("jump");
        Jump();
        landingTime = 1.1f;
    }
    public override void HandleInput()
    {
        base.HandleInput();

        input = moveAction.ReadValue<Vector2>();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (timePassed > landingTime)
        {
            character.animator.SetTrigger("move");
            stateMachine.ChangeState(character.standing);
        }

        timePassed += Time.deltaTime;
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        if (!grounded)
        {

            velocity = character.playerVelocity;
            airVelocity = new Vector3(input.x, 0, input.y);

            velocity = velocity.x * character.cameraTransform.right.normalized + velocity.z * character.cameraTransform.forward.normalized;
            velocity.y = 0f;
            airVelocity = airVelocity.x * character.cameraTransform.right.normalized + airVelocity.z * character.cameraTransform.forward.normalized;
            airVelocity.y = 0f;
            character.controller.Move(gravityVelocity * Time.deltaTime + (airVelocity * character.airControl + velocity * (1 - character.airControl)) * playerSpeed * Time.deltaTime);
        }

        gravityVelocity.y += gravityValue * Time.deltaTime;
        grounded = character.controller.isGrounded;

        //fix rotation in air
        Quaternion targetRotation = Quaternion.LookRotation(velocity);
        character.transform.rotation = Quaternion.Slerp(character.transform.rotation, targetRotation, 0.8f);
    }

    void Jump()
    {
        gravityVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
    }


}
