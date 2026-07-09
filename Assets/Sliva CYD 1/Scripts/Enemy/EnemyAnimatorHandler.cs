using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SlivaCYD1
{
    [RequireComponent(typeof(Animator))]
    public class EnemyAnimatorHandler : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private EnemyCombat _eCombat;
        [SerializeField] private EnemyHp _enemyHp;

        private void OnEnable()
        {
            _eCombat.OnAttackStart += PlayAttack;
            _enemyHp.OnDead += PlayDead;
        }
        
        private void OnDisable()
        {
            _eCombat.OnAttackStart -= PlayAttack;
            _enemyHp.OnDead -= PlayDead;
        }
        
        public void SetSpeed(float speed)
        {
            _animator.SetFloat("Speed", Mathf.Abs(speed) < 0.01f ? 0f : Mathf.Abs(speed));
        }
        private void PlayAttack() => _animator.SetTrigger("Attack");
        private void PlayDead() => _animator.SetTrigger("Dead");
    }
}
