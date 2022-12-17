using System;
using Runtime.Level.Components;
using UnityEngine;

namespace Runtime.Player.Components
{
    public interface IPlayerTrigger
    {
        Action<GameObject> OnTreeTriggerEnter { get; set; }
        Action<GameObject> OnTreeTriggerExit { get; set; }
    }

    public class PlayerTrigger : MonoBehaviour, IPlayerTrigger
    {
        public Action<GameObject> OnTreeTriggerEnter { get; set; }
        public Action<GameObject> OnTreeTriggerExit { get; set; }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out TreeMonoBehaviour treeMonoBehaviour))
            {
                OnTreeTriggerEnter?.Invoke(treeMonoBehaviour.gameObject);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out TreeMonoBehaviour treeMonoBehaviour))
            {
                OnTreeTriggerExit?.Invoke(treeMonoBehaviour.gameObject);
            }
        }
    }
}