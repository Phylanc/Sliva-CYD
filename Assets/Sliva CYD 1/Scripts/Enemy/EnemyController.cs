using System;
using UnityEngine;

namespace SlivaCYD1
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class EnemyController:MonoBehaviour
    {
        [Header("Патруль")] 
        [SerializeField] private float _moveSpeed = 3f;
        [SerializeField] private Transform _pointA;
        [SerializeField] private Transform _pointB;

        [Header("Нахождение игрока")] 
        [SerializeField] private float _detectionRadius = 5f;
        // [SerializeField] private float _loseRadius = 7f;
        [SerializeField] private float _attackRange = 1f;
        [SerializeField] private LayerMask _playerLayer;
        
        [Header("Зависимости")] 
        [SerializeField] private EnemyCombat _eCombat;
        [SerializeField] private EnemyAnimatorHandler _eAnimHandler;
        
        
        
        private Transform _player;
        
        private Rigidbody2D _rb;
        private Transform _patrolTarget;


        private void Update()
        {
            if (_player == null)
            {
                Collider2D hit = Physics2D.OverlapCircle(transform.position, _detectionRadius, _playerLayer);
                _player = hit != null ? hit.GetComponentInParent<PlayerHp>()?.transform ?? hit.transform.root : null;
            }

            else
            {
                float dist = Vector2.Distance(transform.position, _player.position);
                // if (dist> _loseRadius) _player = null;
            }

            _eAnimHandler.SetSpeed(Mathf.Abs(_rb.velocity.x));
        }
        
        

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _patrolTarget = _pointB;
        }

        private void FixedUpdate()
        {
            if (_player == null)
            {
                MoveTowards(_patrolTarget.position);
                return;
                
            }
            
            float disstanceToPlayer = Vector2.Distance(transform.position, _player.position);

            if (disstanceToPlayer <= _attackRange)
            {
                _rb.velocity = new Vector2(0, _rb.velocity.y);
                _eCombat.TryAttack();
            }

            else
            {
                MoveTowards(_player.position);
            }
        }

        private void MoveTowards(Vector3 targetPosition)
        {
            float direction = Mathf.Sign(targetPosition.x - transform.position.x);
            _rb.velocity = new Vector2(direction * _moveSpeed, _rb.velocity.y);
            transform.localScale = new Vector3(direction > 0 ? 1 : -1, 1, 1);

            if (Vector2.Distance(transform.position, targetPosition) < 0.2f)
            {
                _patrolTarget = _patrolTarget == _pointA ? _pointB : _pointA;
            }
        }
    }
}