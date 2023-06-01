using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandingState : State
{
    float gravityValue;
    bool jump;
    Vector3 currentVelocity;
    bool grounded;
    bool sprint;
    float playerSpeed;
    bool drawWeapon;

    Vector3 cVelocity;

    public StandingState(Character _character, StateMachine _stateMachine) : base(_character, _stateMachine)
    {
        character = _character;
        stateMachine = _stateMachine;
    }

    public override void Enter()
    {
        base.Enter();

        drawWeapon = false;
        jump = false;
        sprint = false;
        input = Vector2.zero;
        velocity = Vector3.zero;    
        currentVelocity = Vector3.zero;
        gravityVelocity.y = 0;

        playerSpeed = character.playerSpeed;
        grounded = character.controller.isGrounded;
        gravityValue = character.gravityValue;
    }


    public override void HandleInput()
    {
        base.HandleInput();

        if (jumpAction.triggered)
        {
            jump = true;
        }
        
        if (sprintAction.triggered)
        {
            sprint = true;
        }

        if (drawWeaponAction.triggered)
        {
            drawWeapon = true;
        }

        input = moveAction.ReadValue<Vector2>();
        //velocity = new Vector3(input.x, 0, input.y);

        //velocity = velocity.x * character.cameraTransform.right.normalized + velocity.z * character.cameraTransform.forward.normalized;
        //velocity.y = 0f;

    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (character.animator.GetFloat("speed") < 0.3 && input.magnitude > 0.1f)
        { 
            character.animator.SetFloat("speed", input.magnitude, character.speedDampTime, Time.deltaTime);
        }

        else if (character.animator.GetFloat("speed") > 0.4 && input.magnitude > 0.1f && !sprint)
        {
            character.animator.SetFloat("speed", -input.magnitude*(1/1000000), character.speedDampTime, Time.deltaTime);
        }

        if(input.magnitude < 0.1f)
        {
            character.animator.SetFloat("speed", input.magnitude, character.speedDampTime, Time.deltaTime);
        }

        if (sprint)
        {
            stateMachine.ChangeState(character.sprinting);
        }
        if (jump)
        {
            stateMachine.ChangeState(character.jumping);
        }

        if (drawWeapon)
        {
            stateMachine.ChangeState(character.combatting);
            character.animator.SetTrigger("drawWeapon");
            drawWeapon = false;
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
            velocity *= character.playerSpeed;

            // Rotate the character towards the movement direction
            if (velocity.sqrMagnitude > 0)
            {
                Quaternion targetRotation = Quaternion.LookRotation(velocity);
                character.transform.rotation = Quaternion.Slerp(character.transform.rotation, targetRotation, character.rotationDampTime * Time.deltaTime);
            }
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
        /*
        currentVelocity = Vector3.SmoothDamp(currentVelocity, velocity, ref cVelocity, character.velocityDampTime);
        character.controller.Move(currentVelocity * Time.deltaTime * playerSpeed + gravityVelocity * Time.deltaTime);

        if (velocity.sqrMagnitude > 0)
        {
            character.transform.rotation = Quaternion.Slerp(character.transform.rotation, Quaternion.LookRotation(velocity), character.rotationDampTime);
        }
        */
        character.controller.Move(velocity * Time.deltaTime + gravityVelocity * Time.deltaTime);

        // Rotate the character towards the movement direction
        if (velocity.sqrMagnitude > 0)
        {
            Quaternion targetRotation = Quaternion.LookRotation(velocity);
            character.transform.rotation = Quaternion.Slerp(character.transform.rotation, targetRotation,0.3f);
        }

    }

    public override void Exit()
    {
        base.Exit();

        gravityVelocity.y = 0f;
        character.playerVelocity = new Vector3(input.x, 0, input.y);

        if (velocity.sqrMagnitude > 0)
        {
            character.transform.rotation = Quaternion.LookRotation(velocity);
        }
    }
}
