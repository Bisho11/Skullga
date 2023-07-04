using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprintState : State
{
    float gravityValue;
    Vector3 currentVelocity;

    bool grounded;
    bool sprint;
    float playerSpeed;
    bool Jump;
    Vector3 cVelocity;


    public SprintState(Character _character, StateMachine _stateMachine) : base(_character, _stateMachine)
    {
        character = _character;
        stateMachine = _stateMachine;
    }

    public override void Enter()
    {
        base.Enter();

        sprint = false;
        Jump = false;
        input = Vector2.zero;
        velocity = Vector3.zero;
        currentVelocity = Vector3.zero;
        gravityVelocity.y = 0;

        playerSpeed = character.sprintSpeed;
        grounded = character.controller.isGrounded;
        gravityValue = character.gravityValue;
    }

    public override void HandleInput()
    {
        base.Enter();
        input = moveAction.ReadValue<Vector2>();
        //velocity = new Vector3(input.x, 0, input.y);

        //velocity = velocity.x * character.cameraTransform.right.normalized + velocity.z * character.cameraTransform.forward.normalized;
        //velocity.y = 0f;
        if (sprintAction.triggered || input.sqrMagnitude == 0f)
        {
            sprint = false;
        }
        else
        {
            sprint = true;
        }
        if (jumpAction.triggered)
        {
            Jump = true;

        }

    }

    public override void LogicUpdate()
    {
        if (sprint)
        {
            character.animator.SetFloat("speed", input.magnitude , character.speedDampTime, Time.deltaTime);
        }
        else
        {
            stateMachine.ChangeState(character.standing);
        }
        if (Jump)
        {
            stateMachine.ChangeState(character.jumping);
        }
        if (input.magnitude < 0.1f)
        {
            // Character has released input, set velocity to zero
            velocity = Vector3.zero;
        }

        else
        {
            // Calculate movement direction based on camera
            Vector3 camForward = character.cameraTransform.forward;
            camForward.y = 0f;
            camForward.Normalize();

            Vector3 camRight = character.cameraTransform.right;
            camRight.y = 0f;
            camRight.Normalize();

            velocity = camForward * input.y + camRight * input.x;
            //velocity.y = 0f;
            velocity.Normalize();

            // Apply speed to the movement
            velocity *= (character.playerSpeed + character.sprintSpeed);

            // Rotate the character towards the movement direction
            if (velocity.sqrMagnitude > 0)
            {
                Quaternion targetRotation = Quaternion.LookRotation(velocity);
                character.transform.rotation = Quaternion.Slerp(character.transform.rotation, targetRotation, character.rotationDampTime * Time.deltaTime);
            }
        }

        if (input.magnitude < 0.1f)
        {
            stateMachine.ChangeState(character.standing);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        gravityVelocity.y += gravityValue * Time.deltaTime;
        grounded = character.controller.isGrounded;
        if (grounded && gravityVelocity.y < 0)
        {
            gravityVelocity.y = 0f;
        }

        /*currentVelocity = Vector3.SmoothDamp(currentVelocity, velocity, ref cVelocity, character.velocityDampTime);

        character.controller.Move(currentVelocity * Time.deltaTime * playerSpeed + gravityVelocity * Time.deltaTime);


        if (velocity.sqrMagnitude > 0)
        {
            character.transform.rotation = Quaternion.Slerp(character.transform.rotation, Quaternion.LookRotation(velocity), character.rotationDampTime);
        }*/


        character.controller.Move(velocity * Time.deltaTime + gravityVelocity * Time.deltaTime * character.gravityMultiplier);

        // Rotate the character towards the movement direction
        if (velocity.sqrMagnitude > 0)
        {
            Quaternion targetRotation = Quaternion.LookRotation(velocity);
            character.transform.rotation = Quaternion.Slerp(character.transform.rotation, targetRotation, 0.8f);
        }
    }
}
