using Runtime.Player.Components;
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
        private IPlayerReferences _playerReferences;
        private PositionConstraint _positionConstraint;

        [Inject]
        public void Constructor(IPlayerReferences playerReferences)
        {
            _playerReferences = playerReferences;
            _positionConstraint = GetComponent<PositionConstraint>();

            InitialPositionConstraint();
        }

        public void InitialPositionConstraint()
        {
            var source = new ConstraintSource
            {
                sourceTransform = _playerReferences.PlayerRootTransform,
                weight = 1
            };

            _positionConstraint.AddSource(source);
            _positionConstraint.constraintActive = true;
        }
    }
}