using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SlivaCYD1
{
    public class EnemyCombat : MonoBehaviour
    {
        [Header("Атака")]
        [SerializeField] private Transform _attackCheck;

        [SerializeField] private float _attackRadius = 0.8f;
        [SerializeField] private int _attackDamage = 10;
        [SerializeField] private LayerMask _playerLayer;

        [Header("Тайминги")]
        [SerializeField] private float _attackAnimDlina = 1.5f;
        [SerializeField] private float _damageDelay = 0.35f;

        private bool _canAttack = true;
        private bool _damageAppliedThisAttack;

        public event System.Action OnAttackStart;

        public void TryAttack()
        {
            if(!_canAttack) return;

            _canAttack = false;
            _damageAppliedThisAttack = false;
            OnAttackStart?.Invoke();
            StartCoroutine(DealDamageAfterDelay());
            StartCoroutine(AttackCooldown());
        }

        private IEnumerator AttackCooldown()
        {
            yield return new WaitForSeconds(_attackAnimDlina);
            _canAttack = true;
        }

        public void DealAttackDamage()
        {
            if (_damageAppliedThisAttack) return;
            _damageAppliedThisAttack = true;

            Collider2D[] hits = Physics2D.OverlapCircleAll(_attackCheck.position, _attackRadius, _playerLayer);
            foreach (var hit in hits)
            {
                IDamageable damageable = hit.GetComponentInParent<IDamageable>();
                if (damageable != null)
                    damageable.TakeDamage(_attackDamage);
            }
        }

        private IEnumerator DealDamageAfterDelay()
        {
            yield return new WaitForSeconds(_damageDelay);
            DealAttackDamage();
        }
    }
}
