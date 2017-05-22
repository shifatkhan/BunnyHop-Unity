using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunnyController : MonoBehaviour {

    private Rigidbody2D bunnyRigidBody;

    public float bunnyJumpForce = 200f;

    private Animator bunnyAnimator;

	// Use this for initialization
	void Start () {
        // Get reference for RigitBody2D
        bunnyRigidBody = GetComponent<Rigidbody2D>();

        // Get reference for Animator
        bunnyAnimator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Jump"))
        {
            bunnyRigidBody.AddForce(transform.up * bunnyJumpForce);
        }

        bunnyAnimator.SetFloat("vVelocity", bunnyRigidBody.velocity.y);
	}
}
