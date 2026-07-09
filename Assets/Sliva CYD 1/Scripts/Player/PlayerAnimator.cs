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
      }
    }
}
