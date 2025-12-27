using UnityEngine;

namespace Darkmatter.Core
{
    public interface IPlayerPawn
    {
        Camera mainCamera { get; }
        bool isGrounded { get; }

         void Jump(float jumpForce);

         void Move(Vector3 motion);
         void SetCameraRotation(float pitch, float yaw);
    }
}
