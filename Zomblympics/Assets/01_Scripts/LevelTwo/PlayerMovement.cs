
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    [Header ("Settings")]
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

    [Header ("Debug")]
    [ReadOnly, SerializeField]
    private float horizontalInput;
    [ReadOnly, SerializeField]
    private float verticalInput;
    [ReadOnly, SerializeField]
    private bool isGrounded;
    [ReadOnly, SerializeField]
    private bool isJumping;
    [ReadOnly, SerializeField]
    private bool isFacingRight = true;


    private void Start() {
        
    }

    private void Update() {
        GetInput();
        Animate();
    }

    private void FixedUpdate() {

        rb.velocity = new Vector2(horizontalInput * movementSpeed, rb.velocity.y);

        if ((horizontalInput > 0 && !isFacingRight) ||    //If player is moving towards right but is facing left,
            (horizontalInput < 0 && isFacingRight)) {       // or if player is moving towards left but is facing right,
            isFacingRight = !isFacingRight;
            Vector3 tempScale = transform.localScale;   // get player scale,
            tempScale.x *= -1;                          // flip x-axis [-1 (left) or 1 (right)],
            transform.localScale = tempScale;           // apply new scale to player.
        }

        if (isJumping) {
            rb.AddForce(new Vector2(0, jumpingPower * 100f));  //Apply upwards force to player (jump)
            isGrounded = false;
            isJumping = false;
        }
    }

    private void GetInput() {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        if (verticalInput > 0 && isGrounded) {
            isJumping = true;
        }

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
