using UnityEngine;

namespace Runtime.Player.Components
{
    public interface IPlayerReferences
    {
        Transform PlayerRootTransform { get; set; }
        Transform PlayerModelTransform { get; set; }
        Transform PlayerMeshTransform { get; set; }

        Animator PlayerAnimator { get; set; }
    }

    public class PlayerReferences : MonoBehaviour, IPlayerReferences
    {
        [field: SerializeField] public Transform PlayerRootTransform { get; set; }
        [field: SerializeField] public Transform PlayerModelTransform { get; set; }
        [field: SerializeField] public Transform PlayerMeshTransform { get; set; }
        [field: SerializeField] public Animator PlayerAnimator { get; set; }
    }
}