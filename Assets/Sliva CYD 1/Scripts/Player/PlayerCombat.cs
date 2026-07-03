
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

        private bool _canAttack = true;

        public event System.Action OnAttackStart;
        
        
        public void TryAttack()
        {
            if(!_canAttack) return;

            _canAttack = false;
            OnAttackStart?.Invoke();
            StartCoroutine(AttackCooldown());
        }

        private IEnumerator AttackCooldown()
        {
            yield return new WaitForSeconds(_attackAnimDlina);
            _canAttack = true;
        }
        
        public void DealAttackDamage()
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(_attackCheck.position, _attackRadius, _enemyLayer);
            foreach (var hit in hits)
            {
                if (hit.TryGetComponent<IDamageable>(out var hp))
                    hp.TakeDamage(_attackDamage);
            }
        }
    }
}
