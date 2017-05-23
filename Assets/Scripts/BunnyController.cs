using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

        // Spacebar clicked
		if(Input.GetButtonDown("Jump"))
        {
            // Modify y velocity (upwards)
            bunnyRigidBody.AddForce(transform.up * bunnyJumpForce);
        }

        // Update property vVelocity accordingly.
        bunnyAnimator.SetFloat("vVelocity", bunnyRigidBody.velocity.y);
	}

    void OnCollisionEnter2D(Collision2D collision) {

        // Check if the Bunny's collision detection was triggered by an Enemy.
        if(collision.collider.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            //Application.LoadLevel(Application.loadedLevel); <--- DEPRECATED

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
