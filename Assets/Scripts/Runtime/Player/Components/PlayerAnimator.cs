using UnityEngine;
using Zenject;

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

        [Inject]
        public PlayerAnimator(IPlayerReferences playerReferences)
        {
            _animator = playerReferences.PlayerAnimator;
        }

        public void MovementState(float value)
        {
            _animator.SetFloat(PlayerSpeed, value);
        }
    }
}