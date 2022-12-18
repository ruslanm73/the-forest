using System;
using Runtime.Player;
using Runtime.Player.Components;
using UnityEngine;
using Zenject;

namespace Runtime.Core
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private GameObject _playerPrefab;
        [SerializeField] private GameObject _playerCameraPrefab;

        private IPlayerReferences _playerReferences;
        private IPlayerTrigger _playerTrigger;
        private IPlayerAnimator _playerAnimator;
        private IPlayerMovement _playerMovement;

        public override void InstallBindings()
        {
            InstallPlayer();
            InstallPlayerCamera();
        }

        private void InstallPlayer()
        {
            var playerGameObject = Container.InstantiatePrefab(_playerPrefab);

            _playerReferences = playerGameObject.GetComponent<IPlayerReferences>();
            Container.Bind<IPlayerReferences>().FromInstance(_playerReferences).AsSingle();

            _playerTrigger = playerGameObject.GetComponent<IPlayerTrigger>();
            Container.Bind<IPlayerTrigger>().FromInstance(_playerTrigger).AsSingle();

            _playerAnimator = Container.Instantiate<PlayerAnimator>();
            Container.Bind<IPlayerAnimator>().FromInstance(_playerAnimator).AsSingle().NonLazy();

            _playerMovement = Container.Instantiate<PlayerMovement>();
            Container.Bind<IPlayerMovement>().FromInstance(_playerMovement).AsSingle().NonLazy();
        }

        private void InstallPlayerCamera()
        {
            var playerCameraGameObject = Container.InstantiatePrefabForComponent<IPlayerCamera>(_playerCameraPrefab);
            Container.Bind<IPlayerCamera>().FromInstance(playerCameraGameObject).AsSingle();
        }
    }
}