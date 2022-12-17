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
        private PositionConstraint _positionConstraint;

        [Inject]
        public void Constructor(IPlayerMonoBehaviour playerMonoBehaviour)
        {
            _playerMonoBehaviour = playerMonoBehaviour;
            _positionConstraint = GetComponent<PositionConstraint>();

            InitialPositionConstraint();
        }

        public void InitialPositionConstraint()
        {
            var source = new ConstraintSource
            {
                sourceTransform = _playerMonoBehaviour.References.PlayerRootTransform,
                weight = 1
            };

            _positionConstraint.AddSource(source);
            _positionConstraint.constraintActive = true;
        }
    }
}