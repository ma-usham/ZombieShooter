using Darkmatter.Core;
using UnityEngine;

namespace Darkmatter.Presentation
{
    public class PlayerAnimController : HumonoidAnim, IPlayerAnim
    {
        private readonly int shootHash = Animator.StringToHash("IsShooting");
        public void PlayShootAnim()
        {
            Debug.Log("player Shoot");
        }
    }
}
