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
        
        // The player stats
        [Header("Groundchecking")] 
        [SerializeField] private float _groundDistance;

        [SerializeField] private LayerMask _groundMask;
        
        [Header("Movement Stats")] 
        [SerializeField] private float _walkingAccel;
        [SerializeField] private float _walkingFriction;
        [SerializeField] private float _jumpForce;
        
        private void Start() {
            // Get the components
            _rigidbody = GetComponent<Rigidbody2D>();
            _groundCheck = GameObject.Find("GroundCheck").transform;
            
            // Set the static instance
            Instance = this;
        }

        private void Update() {
            MovePlayer(Time.deltaTime);
        }
    
        /// <summary>
        /// Handle the player movement
        /// </summary>
        private void MovePlayer(float delta) {
            // Get the player x input
            var xmove = Input.GetAxisRaw("Horizontal");
                        
            // Handle player jumping
            if (Input.GetButtonDown("Jump") && IsOnGround) {
                _rigidbody.AddForce(new Vector2(.0f, _jumpForce));
            }
            
            // Handle the player movement on the x axis
            _rigidbody.AddForce(new Vector2(xmove * _walkingAccel * delta, .0f));
            _rigidbody.velocity = new Vector2(Mathf.Lerp(_rigidbody.velocity.x, .0f, _walkingFriction * delta),
                _rigidbody.velocity.y);
        }
        
        /// <summary>
        /// Check if the player is on the ground
        /// </summary>
        public bool IsOnGround 
            => Physics2D.CircleCast(_groundCheck.position, _groundDistance, Vector2.down, _groundDistance, _groundMask);
    }
}
