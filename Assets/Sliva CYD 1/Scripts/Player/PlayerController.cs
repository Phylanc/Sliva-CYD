using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


namespace SlivaCYD1
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(PlayerStamina))]
    public class PlayerController : MonoBehaviour
    {
        // Start is called before the first frame update
        [Header("Движение")]
        [SerializeField] private float walkSpeed = 5f;
        [SerializeField] private float runSpeed = 8f;
        [SerializeField] private float jumpHeight = 3f;
        
        [Header("Проверка земли")]
        // Пустой дочерний объект
        [SerializeField] private Transform _groundCheck;
        // Радиус круга проверки
        [SerializeField] private float _groundCheckRadius = 0.15f;
        // Слой земли
        [SerializeField] private LayerMask _groundLayer;
        
        [Header("Инпут активы")] [SerializeField]
        private InputActionAsset _inputActions;
        
        private Rigidbody2D _rb;
        private PlayerStamina _ps;
        
        private InputAction _moveAction;
        private InputAction _jumpAction;
        private InputAction _sprintAction;
        
        private bool _isGrounded;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _ps = GetComponent<PlayerStamina>();
            
            var playermap = _inputActions.FindActionMap("Player", throwIfNotFound:true);
            
            _moveAction = playermap.FindAction("Move", throwIfNotFound:true);
            _jumpAction = playermap.FindAction("Jump", throwIfNotFound:true);
            _sprintAction = playermap.FindAction("Sprint", throwIfNotFound:true);
            
            
        }

        private void OnEnable()
        {
            _moveAction.Enable();
            _jumpAction.Enable();
            _sprintAction.Enable();
        }

        private void OnDisable()
        {
            _moveAction.Disable();
            _jumpAction.Disable();
            _sprintAction.Disable();
        }

        private void Update()
        {

            _isGrounded = Physics2D.OverlapCircle(
                _groundCheck.position,
                _groundCheckRadius,
                _groundLayer);
            HandleJump();
        }

        private void FixedUpdate()
        {
            HandleMovement();
        }
        
        //джижение

        private void HandleMovement()
        {
            float inputX = _moveAction.ReadValue<Vector2>().x;

            bool wantsToSprint = _sprintAction.IsPressed() && _ps.HasStamina();
            float speed = wantsToSprint ? runSpeed : walkSpeed;
            
            if(wantsToSprint)
                _ps.Drain(Time.fixedDeltaTime); //FixedUpdate используем fixedDeltaTime
            else
                _ps.StopDraining();
            
            //меняю только x, y не трогаю
            _rb.velocity = new Vector2(inputX * speed, _rb.velocity.y);
            
            // Разворот спрайта 
            if(inputX>0.01f)
                transform.localScale = new Vector3(1, 1, 1);
            else if(inputX<-0.01f)
                transform.localScale = new Vector3(-1, 1, 1);

        }

        private void HandleJump()
        {
            if (_jumpAction.WasPressedThisFrame() && _isGrounded)
            {
                _rb.velocity = new Vector2(_rb.velocity.x, jumpHeight);
            }
        }


    }
}
