using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SlivaCYD1
{
    [RequireComponent(typeof(Animator))]
    public class PlayerAnimatorHandler : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private PlayerCombat _pCombat;
        [SerializeField] private PlayerHp _playerHp;
        
        private void OnEnable()
        {
            _pCombat.OnAttackStart += PlayAttack;
            _playerHp.OnDead += PlayDead;
        }

        private void OnDisable()
        {
            _pCombat.OnAttackStart -= PlayAttack;
            _playerHp.OnDead -= PlayDead;
        }

        private void PlayAttack()
        {
            _animator.SetTrigger("Attack");
        }

        private void PlayDead()
        {
            _animator.SetTrigger("IsDead");
        }
        
        public void SetSpeed(float speed)
        {
            _animator.SetFloat("Speed", Mathf.Abs(speed) < 0.01f ? 0f : Mathf.Abs(speed));
        }

        public void SetGrounded(bool isGrounded)
        {
            _animator.SetBool("IsGrounded", isGrounded);
        }
       
    }
}
