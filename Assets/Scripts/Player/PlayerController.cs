using System;
using TMPro;
using UnityEngine;

namespace Player {
    /// <summary>
    /// Handles controlling the player
    /// </summary>
    public class PlayerController : MonoBehaviour {
        // The static instance of the player controller
        public static PlayerController Instance { get; private set; }
        
        // The player components
        private Rigidbody2D _rigidbody;
        private Transform _groundCheck;
        private SpriteRenderer _spriteRenderer;
        
        // The player stats
        [Header("Groundchecking")] 
        [SerializeField] private float _groundDistance;

        [SerializeField] private LayerMask _groundMask;
        
        [Header("Movement Stats")] 
        [SerializeField] private float _walkingAccel;

        [SerializeField] private float _sprintingAccel;
        [SerializeField] private float _walkingFriction;
        [SerializeField] private float _jumpForce;
        [SerializeField] private float _maxJumpTime;
        private bool _canJump;
        private float _startJumpTime;
        
        private void Start() {
            // Get the components
            _rigidbody = GetComponent<Rigidbody2D>();
            _groundCheck = GameObject.Find("GroundCheck").transform;
            _spriteRenderer = GetComponent<SpriteRenderer>();
            
            // Set the static instance
            Instance = this;
        }

        private void Update() {
            MovePlayer(Time.deltaTime);
            Animate();
        }
    
        /// <summary>
        /// Handle the player movement
        /// </summary>
        private void MovePlayer(float delta) {
            // Get the player x input
            var xmove = Input.GetAxisRaw("Horizontal");
            var isSprinting = Input.GetButton("Sprint");
                        
            // Handle player jumping
            if (_canJump) {
                if (Input.GetButton("Jump")) {
                    // Apply the jump force
                    _rigidbody.AddForce(new Vector2(.0f, _jumpForce * delta));
                }

                if (!IsOnGround) {
                    // Prevent the player from jumping again if the jump button is released in the air
                    if (Input.GetButtonUp("Jump")) {
                        _canJump = false;
                    }

                    // Handle variable jump-height
                    if (_startJumpTime + _maxJumpTime > Time.time) {
                        _canJump = false;
                    }
                }
            }
            // Check if the player can jump again
            if (IsOnGround && !Input.GetButton("Jump")) {
                _canJump = true;
                _startJumpTime = Time.time;
            }

            // Handle the player movement on the x axis
            _rigidbody.AddForce(new Vector2(xmove * (isSprinting ? _sprintingAccel : _walkingAccel) * delta, .0f));
            _rigidbody.velocity = new Vector2(Mathf.Lerp(_rigidbody.velocity.x, .0f, _walkingFriction * delta),
                _rigidbody.velocity.y);
        }
        
        /// <summary>
        /// Animate the player
        /// </summary>
        private void Animate() {
            // Get the input
            var xmove = Input.GetAxisRaw("Horizontal");
            if (xmove > 0.0) {
                _spriteRenderer.flipX = false;
            }
            if (xmove < 0.0) {
                _spriteRenderer.flipX = true;
            }
        }
        
        /// <summary>
        /// Check if the player is on the ground
        /// </summary>
        public bool IsOnGround 
            => Physics2D.CircleCast(_groundCheck.position, _groundDistance, Vector2.down, _groundDistance, _groundMask);
    }
}
