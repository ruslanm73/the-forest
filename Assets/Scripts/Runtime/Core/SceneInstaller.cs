using Runtime.Player;
using UnityEngine;
using Zenject;

namespace Runtime.Core
{
    public class SceneInstaller : MonoInstaller
    {
        [SerializeField] private GameObject uiPrefab;
        [SerializeField] private GameObject playerPrefab;
        [SerializeField] private GameObject playerCameraPrefab;

        public override void InstallBindings()
        {
            InstallUi();
            InstallPlayer();
            InstallPlayerCamera();
        }

        private void InstallUi()
        {
            var uiGameObject = Container.InstantiatePrefab(uiPrefab);
            var ultimateJoystick = uiGameObject.transform.Find("UICanvas").Find("MainJoystick");
            Container.Bind<UltimateJoystick>().FromComponentOn(ultimateJoystick.gameObject).AsSingle();
        }

        private void InstallPlayer()
        {
            var playerGameObject = Container.InstantiatePrefabForComponent<IPlayerMonoBehaviour>(playerPrefab);
            Container.Bind<IPlayerMonoBehaviour>().FromInstance(playerGameObject).AsSingle();
        }

        private void InstallPlayerCamera()
        {
            var playerCameraGameObject = Container.InstantiatePrefabForComponent<IPlayerCamera>(playerCameraPrefab);
            Container.Bind<IPlayerCamera>().FromInstance(playerCameraGameObject).AsSingle();
            playerCameraGameObject.InitialPositionConstraint();
        }
    }
}
