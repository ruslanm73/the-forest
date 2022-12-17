using Runtime.Level;
using Runtime.Player;
using UnityEngine;
using Zenject;

namespace Runtime.Core
{
    public class SceneInstaller : MonoInstaller
    {
        [SerializeField] private GameObject _levelPrefab;
        [SerializeField] private GameObject _uiPrefab;
        [SerializeField] private GameObject _playerPrefab;
        [SerializeField] private GameObject _playerCameraPrefab;

        public override void InstallBindings()
        {
            InstallLevel();
            InstallUi();
            InstallPlayer();
            InstallPlayerCamera();
        }

        private void InstallLevel()
        {
            var levelServiceGameObject = Container.InstantiatePrefabForComponent<ILevelService>(_levelPrefab);
            Container.Bind<ILevelService>().FromInstance(levelServiceGameObject).AsSingle();
        }

        private void InstallUi()
        {
            var uiGameObject = Container.InstantiatePrefab(_uiPrefab);
            var ultimateJoystick = uiGameObject.transform.Find("UICanvas").Find("MainJoystick");
            Container.Bind<UltimateJoystick>().FromComponentOn(ultimateJoystick.gameObject).AsSingle();
        }

        private void InstallPlayer()
        {
            var playerGameObject = Container.InstantiatePrefabForComponent<IPlayerMonoBehaviour>(_playerPrefab);
            Container.Bind<IPlayerMonoBehaviour>().FromInstance(playerGameObject).AsSingle();
        }

        private void InstallPlayerCamera()
        {
            var playerCameraGameObject = Container.InstantiatePrefabForComponent<IPlayerCamera>(_playerCameraPrefab);
            Container.Bind<IPlayerCamera>().FromInstance(playerCameraGameObject).AsSingle();
        }
    }
}