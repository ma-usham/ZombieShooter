using Darkmatter.Core;
using UnityEngine;
using VContainer;

namespace Darkmatter.Domain
{
    public class PlayerStateMachine : StateMachine
    {
       [Inject] public readonly IPlayerPawn playerController;
       [Inject] public readonly IInputReader inputReader;
       [Inject] public readonly IPlayerAnim playerAnim;
       [Inject] public readonly PlayerConfigSO playerConfig;
       [Inject] public readonly CameraConfigSO cameraConfig;

        private Vector3 moveDir;
        private float Yaw;
        private float pitch;

        public void Move(Vector2 moveInputDir, float moveSpeed)
        {
            //player movement with reference to camera
            Vector3 cameraForward = playerController.mainCamera.transform.forward;
            Vector3 cameraRight = playerController.mainCamera.transform.right;

            cameraForward.y = 0f;
            cameraRight.y = 0f;

            cameraForward.Normalize();
            cameraRight.Normalize();

            moveDir = cameraRight * moveInputDir.x + cameraForward * moveInputDir.y;

            playerController.Move(moveDir * moveSpeed);
            playerAnim.PlayMovementAnim(moveInputDir);
        }

        public void RotateCamera(Vector2 lookInput)
        {
            //camera rotation logic
            if (lookInput.sqrMagnitude > 0.01f)
            {
                Yaw += lookInput.x * cameraConfig.lookSensitivity * Time.deltaTime;
                pitch -= lookInput.y * cameraConfig.lookSensitivity * Time.deltaTime;
            }
            pitch = Mathf.Clamp(pitch, cameraConfig.bottomClampAngle, cameraConfig.topClampAngle);
            playerController.SetCameraRotation(pitch, Yaw);
        }


    }
}
