using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityInput = UnityEngine.Input;


namespace SlivaCYD1
{
    public class PlayerHp : MonoBehaviour
    {
        [Header("ХП ИГРОКА")]
        [SerializeField] private int maxHp = 100;

        public event Action<float> HpChanged;
        private int _currentHp;
        
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

        private void ChangeHp(int value)
        {
            _currentHp += value;

            if (_currentHp <= 0)
            {
                Death();
            }
            else
            {
                float currentHpAsPercentage = (float)_currentHp / maxHp;
                HpChanged?.Invoke(currentHpAsPercentage);
            }
        }
        
        private void Death()
        {
            HpChanged?.Invoke(0);
            Debug.Log("игрок сдох");
            Destroy(gameObject);
        }
    }
}
