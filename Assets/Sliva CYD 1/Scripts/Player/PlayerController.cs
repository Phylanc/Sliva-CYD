using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SlivaCYD1
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private float jump = 20f;

        private PlayerInputHandler _input;
        private Rigidbody2D _rb;
        
        [SerializeField] private Transform groundCheck;
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private float groundCheckRadius = 0.2f;
        
        private bool _isGrounded;

        private void Awake()
        {
            _rb=GetComponent<Rigidbody2D>();
            _input = GetComponent<PlayerInputHandler>();
        }

        private void Update()
        {
            _isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
            if (_input.JumpPressed && _isGrounded)
            {
                _rb.velocity=new Vector2(_rb.velocity.x,jump);
            }
        }

        private void FixedUpdate()
        {
            _rb.velocity=new Vector2(_input.MoveInput.x * moveSpeed, _rb.velocity.y);
        }


    }
}
