using System;
using UnityEngine;

namespace SlivaCYD1
{
    public class EnemyHp : MonoBehaviour, IDamageable
    {
        [SerializeField] private int _maxhp = 50;
        private int _currenthp;

        public event Action OnDead;

        private void Awake()
        {
            _currenthp = _maxhp;
            
            
        }

        public void TakeDamage(int amount)
        {
            _currenthp -= amount;
            if (_currenthp <= 0)
                OnDead?.Invoke();
        }

    }
}
