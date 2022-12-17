using Runtime.Player.Components;
using UnityEngine;
using Zenject;

namespace Runtime.Player
{
    public interface IPlayerMonoBehaviour
    {
        IPlayerReferences PlayerReferences { get; set; }
        IPlayerControl PlayerControl { get; set; }
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
            PlayerControl = new PlayerControl(ultimateJoystick, this);
        }

        public IPlayerReferences PlayerReferences { get; set; }
        public IPlayerControl PlayerControl { get; set; }
    }
}