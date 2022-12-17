using Runtime.Player;
using UnityEngine;
using UnityEngine.Animations;
using Zenject;

namespace Assets.Scripts.Runtime.Player
{
    public interface IPlayerCamera
    {
        void InitialPositionConstraint();
    }

    public class PlayerCamera : MonoBehaviour, IPlayerCamera
    {
        private IPlayerMonoBehaviour _playerMonoBehaviour;

        [Inject]
        public void Constructor(IPlayerMonoBehaviour playerMonoBehaviour)
        {
            _playerMonoBehaviour = playerMonoBehaviour;
        }

        public void InitialPositionConstraint()
        {
            var playerCameraPositionContraint = gameObject.GetComponent<PositionConstraint>();
            var source = new ConstraintSource
            {
                sourceTransform = _playerMonoBehaviour.PlayerReferences.PlayerRootTransform,
                weight = 1
            };

            playerCameraPositionContraint.AddSource(source);
            playerCameraPositionContraint.constraintActive = true;
        }
    }
}