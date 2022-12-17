using Runtime.Player.Components;
using UnityEngine;
using Zenject;

namespace Runtime.Player
{
    public interface IPlayerMonoBehaviour
    {
        IPlayerReferences References { get; set; }
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
            Movement = new PlayerMovement(ultimateJoystick, this);
        }

        public IPlayerReferences References { get; set; }
        public IPlayerMovement Movement { get; set; }
    }
}