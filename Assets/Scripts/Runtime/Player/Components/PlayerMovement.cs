using System.Collections;
using UnityEngine;
using Zenject;

namespace Runtime.Player.Components
{
    public interface IPlayerMovement
    {
        bool MovementAvailable { get; set; }
        float MaxSpeed { get; set; }
    }

    public class PlayerMovement : IPlayerMovement
    {
        private const string PlayerSpeedValueName = "PlayerSpeed";
        private readonly UltimateJoystick _ultimateJoystick;
        private readonly MonoBehaviour _monoBehaviour;
        private readonly Transform _playerTransform;
        private readonly IPlayerAnimator _playerAnimator;

        private IEnumerator _updatePlayerTransformEnumerator;

        [Inject]
        public PlayerMovement(UltimateJoystick ultimateJoystick, IPlayerReferences playerReferences, IPlayerAnimator playerAnimator)
        {
            _ultimateJoystick = ultimateJoystick;
            _monoBehaviour = playerReferences.PlayerRootTransform.GetComponent<MonoBehaviour>();
            _playerTransform = playerReferences.PlayerRootTransform;
            _playerAnimator = playerAnimator;

            _ultimateJoystick.OnPointerDownCallback += OnPointerJoystickDown;
            _ultimateJoystick.OnPointerUpCallback += OnPointerJoystickUp;
        }

        private void OnPointerJoystickDown()
        {
            MovementAvailable = true;

            _updatePlayerTransformEnumerator = PlayerMovementEnumerator();
            _monoBehaviour.StartCoroutine(_updatePlayerTransformEnumerator);
        }

        private void OnPointerJoystickUp()
        {
            MovementAvailable = false;
            _playerAnimator.MovementState(default);

            _monoBehaviour.StopCoroutine(_updatePlayerTransformEnumerator);
        }

        private IEnumerator PlayerMovementEnumerator()
        {
            while (MovementAvailable)
            {
                var verticalAxis = _ultimateJoystick.GetVerticalAxis();
                var horizontalAxis = _ultimateJoystick.GetHorizontalAxis();
                var actualPlayerSpeed = _ultimateJoystick.GetDistance() * MaxSpeed;
                var transformEulerAngles = new Vector3(0, Mathf.Atan2(horizontalAxis, verticalAxis) * 180 / Mathf.PI, 0);

                _playerTransform.eulerAngles = transformEulerAngles;
                _playerTransform.Translate(_playerTransform.TransformDirection(Vector3.forward) * actualPlayerSpeed, Space.World);

                _playerAnimator.MovementState(_ultimateJoystick.GetDistance());

                yield return null;
            }
        }

        public bool MovementAvailable { get; set; }
        public float MaxSpeed { get; set; } = 0.02f;
    }
}