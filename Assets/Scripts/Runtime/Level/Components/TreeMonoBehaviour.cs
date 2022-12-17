using System;
using UnityEngine;

namespace Runtime.Level.Components
{
    public class TreeMonoBehaviour : MonoBehaviour
    {
        [SerializeField] private TreeConfiguration _treeConfiguration;

        public void InitialTree()
        {
            _treeConfiguration.CurrentHealth = _treeConfiguration.MaxHealth;
        }

        public void TakeDamage(int damage)
        {
            _treeConfiguration.CurrentHealth--;
        }

        public TreeConfiguration Configuration => _treeConfiguration;
    }

    [Serializable]
    public class TreeConfiguration
    {
        [field: SerializeField] public int MaxHealth { get; set; }
        [field: SerializeField] public int CurrentHealth { get; set; }
    }
}