using Darkmatter.Core;
using System;
using UnityEngine;

namespace Darkmatter.Domain
{
    public class LocomotionState : State<PlayerStateMachine>
    {
        public LocomotionState(PlayerStateMachine runner) : base(runner) { }
        private IInputReader inputReader => runner.inputReader;
        private PlayerConfigSO playerConfig => runner.playerConfig;
        private IPlayerAnim playerAnim => runner.playerAnim;
        public override void Enter()
        {
            Debug.Log("Starting player Locomotion");
            inputReader.OnJumpPerformed += HandlePlayerJump;
        }

        public override void Update()
        {
            HandlePlayerMovement();
            CheckForStateBreak();
           
        }

        public override void LateUpdate()
        {
            HandlePlayerRotation();
        }

        public override void Exit()
        {
            Debug.Log("Exiting Player Locomotion State");
            inputReader.OnJumpPerformed -= HandlePlayerJump;
        }


        //Locomotion Functions

        private void CheckForStateBreak()
        {
            if (!runner.playerController.isGrounded)
            {
                runner.ChangeState(new AirboneState(runner));
            }
        }

        private void HandlePlayerRotation()
        {
            runner.RotateCamera(inputReader.lookInput);
        }

        private void HandlePlayerMovement()
        {
            runner.Move(inputReader.moveInput, playerConfig.moveSpeed);
        }

        private void HandlePlayerJump()
        {
            runner.playerController.Jump(playerConfig.jumpForce);
            playerAnim.PlayJumpAnim();
        }




    }
}
