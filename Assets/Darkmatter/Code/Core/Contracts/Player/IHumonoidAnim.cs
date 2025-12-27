using UnityEngine;

namespace Darkmatter.Core
{
    public interface IHumonoidAnim
    {
        void PlayJumpAnim();
        void PlayMovementAnim(Vector2 velocity);
    }
}
