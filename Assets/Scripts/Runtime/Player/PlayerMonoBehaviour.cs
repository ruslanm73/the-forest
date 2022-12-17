using Runtime.Player.Components;
using UnityEngine;
using Zenject;

namespace Runtime.Player
{
    public interface IPlayerMonoBehaviour
    {
        IPlayerReferences References { get; set; }
        IPlayerTrigger Trigger { get; set; }
        IPlayerAnimator Animator { get; set; }
        IPlayerMovement Movement { get; set; }
    }

    public class PlayerMonoBehaviour : MonoBehaviour, IPlayerMonoBehaviour
    {
        private UltimateJoystick _ultimateJoystick;
        private IPlayerMovement _playerControl;

        [Inject]
        public void Constructor(UltimateJoystick ultimateJoystick)
        {
            _ultimateJoystick = ultimateJoystick;
            References = GetComponent<IPlayerReferences>();
            Trigger = GetComponent<IPlayerTrigger>();
            Animator = new PlayerAnimator(this);
            Movement = new PlayerMovement(ultimateJoystick, this);
        }

        public IPlayerReferences References { get; set; }
        public IPlayerTrigger Trigger { get; set; }
        public IPlayerAnimator Animator { get; set; }
        public IPlayerMovement Movement { get; set; }
    }
}