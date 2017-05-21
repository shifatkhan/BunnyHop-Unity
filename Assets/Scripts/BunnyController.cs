using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunnyController : MonoBehaviour {

    private Rigidbody2D bunnyRigidBody;
    public float bunnyJumpForce = 200f;

	// Use this for initialization
	void Start () {
        // Get reference for RigitBody2D
        bunnyRigidBody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonUp("Jump"))
        {
            bunnyRigidBody.AddForce(transform.up * bunnyJumpForce);
        }
	}
}
