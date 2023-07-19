
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelTwo {

    public class PlayerController : MonoBehaviour {
        [Header("Settings")]
        [SerializeField]
        private float movementSpeed;
        [SerializeField]
        private float jumpingPower;
        [SerializeField]
        private bool drawGizmos;

        [Header("Components")]
        [SerializeField]
        private Rigidbody2D rb;
        [SerializeField]
        private Animator animator;
        [SerializeField]
        public Camera cam;
        [SerializeField]
        public Transform camPos;

        [Header("Debug")]
        [ReadOnly, SerializeField]
        private bool isGrounded;
        [ReadOnly, SerializeField]
        private bool isJumping;
        [ReadOnly, SerializeField]
        public bool canMove;


        private void Start() {
            //canMove = true;
        }

        private void Update() {
            Animate();
            if (canMove) {
                GetInput();
               // Accelerate();
            }
        }

        private void FixedUpdate() {
            if (canMove) {
                rb.velocity = new Vector2(movementSpeed, rb.velocity.y);
            }
            else {
                rb.velocity = new Vector2(0, rb.velocity.y);
            }

            if (isJumping) {
                rb.AddForce(new Vector2(0, jumpingPower * 100f));  //Apply upwards force to player (jump)
                isGrounded = false;
                isJumping = false;
            }
        }

        private void GetInput() {

            if (Input.GetMouseButtonDown(0) && isGrounded) {
                isJumping = true;
            }

            if (rb.velocity.y < 0) {
                rb.gravityScale = 4;
            }
            else {
                rb.gravityScale = 2;
            }

        }
        private void Accelerate() {
            movementSpeed += 0.001f;
        }


        private void Animate() {
            animator.SetFloat("Horizontal", Mathf.Abs(rb.velocity.x));
            animator.SetFloat("Vertical", rb.velocity.y);
        }

        private void OnTriggerStay2D(Collider2D collision) {

            if (collision.gameObject.layer == 8) {   //If player is on ground,
                isGrounded = true;              // set bool to true	
            }
        }

    }
}
