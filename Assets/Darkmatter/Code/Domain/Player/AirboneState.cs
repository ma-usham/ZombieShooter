using Darkmatter.Core;
using UnityEngine;

namespace Darkmatter.Domain
{
    public class AirboneState : State<PlayerStateMachine>
    {
        public AirboneState(PlayerStateMachine runner) : base(runner) { }
        private IInputReader inputReader => runner.inputReader;
        private PlayerConfigSO playerConfig => runner.playerConfig;


        public override void Enter()
        {
            Debug.Log("Entering Player AirboneState ");
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
            Debug.Log("Exiting Player AriboneState");
        }


        //Airbone Functions
        private void HandlePlayerMovement()
        {
            runner.Move(inputReader.moveInput, playerConfig.moveSpeed);
        }

        private void HandlePlayerRotation()
        {
            runner.RotateCamera(inputReader.lookInput);
        }

        private void CheckForStateBreak()
        {
            if (runner.playerController.isGrounded)
            {
                runner.ChangeState(new LocomotionState(runner));
            }
        }
    }
}
