using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Runtime.Player.Components
{
    public interface IPlayerControl
    {
    }

    public class PlayerControl : IPlayerControl
    {
        private const string PlayerSpeedValueName = "PlayerSpeed";
        private readonly MonoBehaviour _monoBehaviour;
        private readonly UltimateJoystick _ultimateJoystick;
        private readonly GameObject _playerGameObject;
        private readonly Animator _playerAnimator;

        private IEnumerator _updatePlayerTransformEnumerator;

        public PlayerControl(UltimateJoystick ultimateJoystick, IPlayerMonoBehaviour playerMonoBehaviour)
        {
            _ultimateJoystick = ultimateJoystick;
            _monoBehaviour = playerMonoBehaviour.PlayerReferences.PlayerRootTransform.GetComponent<MonoBehaviour>();
            _playerGameObject = playerMonoBehaviour.PlayerReferences.PlayerRootTransform.gameObject;
            _playerAnimator = playerMonoBehaviour.PlayerReferences.PlayerAnimator;

            _ultimateJoystick.OnPointerDownCallback += OnPointerJoystickDown;
            _ultimateJoystick.OnPointerUpCallback += OnPointerJoystickUp;
        }

        private void OnPointerJoystickDown()
        {
            PlayerMovementAvailable = true;

            _updatePlayerTransformEnumerator = PlayerMovementEnumerator();
            _monoBehaviour.StartCoroutine(_updatePlayerTransformEnumerator);
        }

        private void OnPointerJoystickUp()
        {
            PlayerMovementAvailable = false;
            _playerAnimator.SetFloat(PlayerSpeedValueName, default);

            _monoBehaviour.StopCoroutine(_updatePlayerTransformEnumerator);
        }

        private IEnumerator PlayerMovementEnumerator()
        {
            while (PlayerMovementAvailable)
            {
                var verticalAxis = _ultimateJoystick.GetVerticalAxis();
                var horizontalAxis = _ultimateJoystick.GetHorizontalAxis();
                var actualPlayerSpeed = _ultimateJoystick.GetDistance() * PlayerMaxSpeed;
                var transformEulerAngles = new Vector3(0, Mathf.Atan2(horizontalAxis, verticalAxis) * 180 / Mathf.PI, 0);

                _playerGameObject.transform.eulerAngles = transformEulerAngles;
                _playerGameObject.transform.Translate(_playerGameObject.transform.TransformDirection(Vector3.forward) * actualPlayerSpeed, Space.World);

                _playerAnimator.SetFloat("PlayerSpeed", _ultimateJoystick.GetDistance());

                yield return null;
            }
        }

        public bool PlayerMovementAvailable { get; set; }
        public float PlayerMaxSpeed { get; set; } = 0.02f;
    }
}