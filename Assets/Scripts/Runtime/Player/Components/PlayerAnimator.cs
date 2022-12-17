using UnityEngine;

namespace Runtime.Player.Components
{
    public interface IPlayerAnimator
    {
        void MovementState(float value);
    }

    public class PlayerAnimator : IPlayerAnimator
    {
        private static readonly int PlayerSpeed = Animator.StringToHash("PlayerSpeed");
        private readonly Animator _animator;

        public PlayerAnimator(IPlayerMonoBehaviour playerMonoBehaviour)
        {
            _animator = playerMonoBehaviour.References.PlayerAnimator;
        }

        public void MovementState(float value)
        {
            _animator.SetFloat(PlayerSpeed, value);
        }
    }
}
