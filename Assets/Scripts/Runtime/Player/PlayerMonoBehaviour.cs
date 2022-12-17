using Runtime.Player.Components;
using UnityEngine;
using Zenject;

namespace Runtime.Player
{
    public interface IPlayerMonoBehaviour
    {
        IPlayerReferences PlayerReferences { get; set; }
    }

    public class PlayerMonoBehaviour : MonoBehaviour, IPlayerMonoBehaviour
    {
        private UltimateJoystick _ultimateJoystick;
        private IPlayerControl _playerControl;

        [Inject]
        public void Constructor(UltimateJoystick ultimateJoystick)
        {
            _ultimateJoystick = ultimateJoystick;
            PlayerReferences = GetComponent<IPlayerReferences>();
            _playerControl = new PlayerControl(this, ultimateJoystick, gameObject, PlayerReferences.PlayerAnimator);
        }

        public IPlayerReferences PlayerReferences { get; set; }
    }
}