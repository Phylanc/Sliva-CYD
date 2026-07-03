using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityInput = UnityEngine.Input;


namespace SlivaCYD1
{
    public class PlayerHp : MonoBehaviour, IDamageable
    {
        [Header("ХП ИГРОКА")]
        [SerializeField] private int maxHp = 100;

        public event Action<float> HpChanged;
        public event Action OnDead;
        
        private int _currentHp;
        private bool _isDead;
        
        private void Start()
        {
            _currentHp = maxHp;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                ChangeHp(-10);
            }
            
        }

        public void TakeDamage(int amount)
        {
            if (_isDead) return;
            ChangeHp(-amount);
        }
        
        private void ChangeHp(int value)
        {
            _currentHp = Mathf.Clamp(_currentHp + value, 0, maxHp);
            
            float pct =  (float)_currentHp / maxHp;
            HpChanged?.Invoke(pct);
            
            if (_currentHp <= 0)
                Death();
        }
        
        private void Death()
        {
            _isDead = true;
            Debug.Log("игрок сдох");
            OnDead?.Invoke();
            

        }
    }
}
