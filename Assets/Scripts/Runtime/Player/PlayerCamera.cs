using UnityEngine;
using UnityEngine.Animations;
using Zenject;

namespace Runtime.Player
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
            var playerCameraPositionConstraint = gameObject.GetComponent<PositionConstraint>();
            var source = new ConstraintSource
            {
                sourceTransform = _playerMonoBehaviour.References.PlayerRootTransform,
                weight = 1
            };

            playerCameraPositionConstraint.AddSource(source);
            playerCameraPositionConstraint.constraintActive = true;
        }
    }
}