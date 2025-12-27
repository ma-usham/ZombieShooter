using Darkmatter.Core;
using UnityEngine;

namespace Darkmatter.Presentation
{
    public abstract class HumonoidAnim : MonoBehaviour, IHumonoidAnim
    {
        public Animator animator;

        protected readonly int moveXhash = Animator.StringToHash("MoveX");
        protected readonly int moveYhash = Animator.StringToHash("MoveY");
        protected readonly int jumpHash = Animator.StringToHash("Jump");

        public void PlayMovementAnim(Vector2 velocity)
        {
            animator.SetFloat(moveXhash, velocity.x,0.4f,Time.deltaTime);
            animator.SetFloat(moveYhash, velocity.y,0.4f, Time.deltaTime);
        }

        public void PlayJumpAnim()
        {
            animator.SetTrigger(jumpHash);
        }
    }
}
