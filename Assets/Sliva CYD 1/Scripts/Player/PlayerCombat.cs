
using System.Collections;
using UnityEngine;

namespace SlivaCYD1
{
    public class PlayerCombat : MonoBehaviour
    {
        [Header("Атака")] 
        [SerializeField] private Transform _attackCheck;
        [SerializeField] private float _attackRadius = 0.9f;
        [SerializeField] private int _attackDamage = 10;
        [SerializeField] private LayerMask _enemyLayer;
        
        [Header("Тайминги")]
        [SerializeField] private float _attackAnimDlina = 3f;
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

            Collider2D[] hits = Physics2D.OverlapCircleAll(_attackCheck.position, _attackRadius, _enemyLayer);
            foreach (var hit in hits)
            {
                IDamageable hp = hit.GetComponentInParent<IDamageable>();
                if (hp != null)
                    hp.TakeDamage(_attackDamage);
            }
        }

        private IEnumerator DealDamageAfterDelay()
        {
            yield return new WaitForSeconds(_damageDelay);
            DealAttackDamage();
        }
    }
}
