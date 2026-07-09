using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


namespace SlivaCYD1
{
  [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(PlayerStamina))]
    public class PlayerInputController : MonoBehaviour
    {
        [Header("Движение")]
        [SerializeField] private float walkSpeed = 5f;
        [SerializeField] private float runSpeed = 8f;
        [SerializeField] private float jumpHeight = 3f;

        [Header("Проверка земли")]
        [SerializeField] private Transform _groundCheck;
        [SerializeField] private float _groundCheckRadius = 0.15f;
        [SerializeField] private LayerMask _groundLayer;

        [Header("Инпут активы")]
        [SerializeField] private InputActionAsset _inputActions;

        [Header("Зависимости")]
        [SerializeField] private PlayerCombat _combat;
        [SerializeField] private PlayerAnimatorHandler _animatorHandler;

        private Rigidbody2D _rb;
        private PlayerStamina _ps;

        private InputAction _moveAction;
        private InputAction _jumpAction;
        private InputAction _sprintAction;
        private InputAction _attackAction;

        private bool _isGrounded;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _ps = GetComponent<PlayerStamina>();

            var playermap = _inputActions.FindActionMap("Player", throwIfNotFound: true);

            _moveAction = playermap.FindAction("Move", throwIfNotFound: true);
            _jumpAction = playermap.FindAction("Jump", throwIfNotFound: true);
            _sprintAction = playermap.FindAction("Sprint", throwIfNotFound: true);
            _attackAction = playermap.FindAction("Attack", throwIfNotFound: true);

            GetComponent<PlayerHp>().OnDead += Die;
        }

        private void OnEnable()
        {
            _moveAction.Enable();
            _jumpAction.Enable();
            _sprintAction.Enable();
            _attackAction.Enable();
        }

        private void OnDisable()
        {
            _moveAction.Disable();
            _jumpAction.Disable();
            _sprintAction.Disable();
            _attackAction.Disable();
        }

        private void Update()
        {
            _isGrounded = Physics2D.OverlapCircle(
                _groundCheck.position,
                _groundCheckRadius,
                _groundLayer);

            HandleJump();
            HandleAttackInput();
            
            _animatorHandler.SetSpeed(Mathf.Abs(_rb.velocity.x));
            _animatorHandler.SetGrounded(_isGrounded);
        }

        private void FixedUpdate()
        {
            HandleMovement();
        }

        private void HandleMovement()
        {
            float inputX = _moveAction.ReadValue<Vector2>().x;
            bool isMoving = Mathf.Abs(inputX) > 0.01f;
            
            bool wantsToSprint = isMoving && _sprintAction.IsPressed() && _ps.HasStamina();
            float speed = wantsToSprint ? runSpeed : walkSpeed;

            if (wantsToSprint)
                _ps.Drain(Time.fixedDeltaTime);
            else
                _ps.StopDraining();

            _rb.velocity = new Vector2(inputX * speed, _rb.velocity.y);

            if (inputX > 0.01f)
                transform.localScale = new Vector3(1, 1, 1);
            else if (inputX < -0.01f)
                transform.localScale = new Vector3(-1, 1, 1);
        }

        private void HandleJump()
        {
            if (_jumpAction.WasPressedThisFrame() && _isGrounded)
            {
                _rb.velocity = new Vector2(_rb.velocity.x, jumpHeight);
            }
        }

        private void HandleAttackInput()
        {
            if (_attackAction.WasPressedThisFrame())
            {
                // Вся логика "можно ли атаковать" и кулдаун — внутри PlayerCombat.
                _combat.TryAttack();
            }
        }

        public void Die()
        {
            _moveAction.Disable();
            _jumpAction.Disable();
            _attackAction.Disable();
        }
    }  
}
