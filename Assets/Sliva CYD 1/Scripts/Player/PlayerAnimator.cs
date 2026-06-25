using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SlivaCYD1
{
    public class PlayerAnimator : MonoBehaviour
    {
      [SerializeField] private Animator _animator;
      [SerializeField] private Rigidbody2D _rb;
      [SerializeField] private Transform groundCheck;
      [SerializeField] private LayerMask groundLayer;
      [SerializeField] private float groundCheckRadius = 0.2f;

      private void Update()
      {
          float speed = Mathf.Abs(_rb.velocity.x);
          _animator.SetFloat("Speed", speed);
          
          bool isGrounded = Physics2D.OverlapCircle(
              groundCheck.position, 
              groundCheckRadius, 
              groundLayer);
          _animator.SetBool("IsGrounded", isGrounded);

          if (_rb.velocity.x > 0.1f)
          {
              transform.localScale = new Vector3(1f, 1f, 1f);
          }
          else if (_rb.velocity.x < -0.1f)
          {
              transform.localScale = new Vector3(-1f, 1f, 1f);
          }
      }
    }
}
