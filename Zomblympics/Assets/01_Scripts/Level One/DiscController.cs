
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscController : MonoBehaviour {

    private Rigidbody2D body;

    private void Start() {
        body = GetComponent<Rigidbody2D>();
        body.simulated = false;
    }
    
    public void Throw(float _velocity) {
        body.simulated = true;
        body.velocity = transform.up * _velocity;
        transform.right = body.velocity.normalized;
    }
    
}