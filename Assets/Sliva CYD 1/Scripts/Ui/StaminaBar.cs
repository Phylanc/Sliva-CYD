using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SlivaCYD1
{
    public class StaminaBar : MonoBehaviour
    {
        [SerializeField] private Image _staminaBarFill;
        [SerializeField] private PlayerStamina _playerStamina;

        private void Awake()
        {
            _playerStamina.StaminaChanged += OnStaminaChanged;
        }

        private void OnDestroy()
        {
            _playerStamina.StaminaChanged -= OnStaminaChanged;
        }

        private void OnStaminaChanged(float valuesAsPercentage)
        {
            _staminaBarFill.fillAmount = valuesAsPercentage;
        }
        
        // private void LateUpdate()
        // {
        //     float parentScaleX = transform.parent.localScale.x;
        //     transform.localScale = new Vector3(1f / parentScaleX, 1f, 1f);
        // }
    }
}
