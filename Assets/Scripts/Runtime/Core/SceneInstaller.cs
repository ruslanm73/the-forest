using Runtime.Level;
using Runtime.Player;
using Runtime.Player.Components;
using UnityEngine;
using Zenject;

namespace Runtime.Core
{
    public class SceneInstaller : MonoInstaller
    {
        [SerializeField] private GameObject _levelPrefab;
        [SerializeField] private GameObject _uiPrefab;

        public override void InstallBindings()
        {
            InstallLevel();
            InstallUi();
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
    }
}