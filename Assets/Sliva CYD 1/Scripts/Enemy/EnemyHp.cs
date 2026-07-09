using System;
using UnityEngine;

namespace SlivaCYD1
{
    public class EnemyHp : MonoBehaviour, IDamageable
    {
        [Header("ХП Врага")]
        [SerializeField] private int _maxhp = 50;
        private int _currenthp;

        
        public event Action<float> EnemyHpChanged;
        public event Action OnDead;
        
        
        private void Start()
        {
            _currenthp = _maxhp;
            
            
        }
        
        public void TakeDamage(int amount)
        {
            Debug.Log("TakeDamage called, amount=" + amount);
            _currenthp -= Mathf.Clamp(_currenthp -amount, 0, _maxhp);
            
            float pct = (float) _currenthp / _maxhp;
            EnemyHpChanged?.Invoke(pct);
            
            if (_currenthp <= 0)
                OnDead?.Invoke();
        }

    }
}
