using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SlivaCYD1
{
    public class PlayerStamina : MonoBehaviour
    {
        // Start is called before the first frame update
        [Header("СТАМИНА ИГРОКА")]
        [SerializeField] private float maxStamina;
        [SerializeField] private float staminaDownSecond = 20f;
        [SerializeField] private float staminaRegenSecond = 10f;
        [SerializeField]  private float regenDelaySecond = 2f;
        
        public event Action<float> StaminaChanged;
        
        private float _currentStamina;
        private Coroutine _regenCoroutine;
        private bool _isDraining;

        private void Start()
        {
            _currentStamina = maxStamina;
        }

        //--------------------------------------пока игрок бежит
        public void Drain(float deltaTime)
        {
            //если на 0 --- не че не делаем
            if(_currentStamina <= 0f) return;
            
            //остановка если шла стамина
            if (_regenCoroutine != null)
            {
                StopCoroutine(_regenCoroutine);
                _regenCoroutine = null;
            }
            
            _isDraining = true;
            // Отнимаем стамину deltaTime - время прошедшее с прошлого кадра в сек
            _currentStamina -= staminaDownSecond * deltaTime;
            _currentStamina = Mathf.Max(_currentStamina, 0f);
            
            NotifyUI();
        }

        public void StopDraining()
        {
            if (!_isDraining) return;
            _isDraining = false;
            
            if(_regenCoroutine != null)
                {
                StopCoroutine(_regenCoroutine);
                }

            _regenCoroutine = StartCoroutine(RegenRoutine());
        }

        public bool HasStamina() => _currentStamina > 0f;
        
        
        //ВОССТАНОВЛЕНИЕ СТАМИНЫ

        private IEnumerator RegenRoutine()
        {
            yield return new WaitForSeconds(regenDelaySecond);

            while (_currentStamina < maxStamina)
            {
                _currentStamina += staminaRegenSecond * Time.deltaTime;
                _currentStamina = Mathf.Min(_currentStamina, maxStamina);

                NotifyUI();

                yield return null;
            }

            _regenCoroutine = null;
        }

        
        //если нет подписчиков - не упадёт с ошибкой
        private void NotifyUI()
        {
            StaminaChanged?.Invoke(_currentStamina/maxStamina);
        }
    }
}
