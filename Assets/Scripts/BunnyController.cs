using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BunnyController : MonoBehaviour {

    private Rigidbody2D bunnyRigidBody;

	private Collider2D bunnyCollider;

    public float bunnyJumpForce = 200f;

    private Animator bunnyAnimator;

	private float bunnyHurtTime = -1f;

	// Use this for initialization
	void Start () {
        // Get reference for RigitBody2D
        bunnyRigidBody = GetComponent<Rigidbody2D>();

		bunnyCollider = GetComponent<Collider2D> ();

        // Get reference for Animator
        bunnyAnimator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

		if (bunnyHurtTime == -1) {
			// Spacebar clicked, on touch
			if ((Input.GetButtonDown ("Jump") || (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Began)) && bunnyRigidBody.position.y < -2.5) {
				// Modify y velocity (upwards)
				bunnyRigidBody.AddForce (transform.up * bunnyJumpForce);
			}

			// Update property vVelocity accordingly.
			bunnyAnimator.SetFloat ("vVelocity", bunnyRigidBody.velocity.y);
		} else {

			// Reset level 2 seconds after getting hurt.
			if (Time.time > bunnyHurtTime + 2) {
				SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
			}
		}
	}

	/// <summary>
	/// Method that will change values for the bunny when it gets hurt.
	/// </summary>
	/// <param name="collision">Collision.</param>
    void OnCollisionEnter2D(Collision2D collision) {

        // Check if the Bunny's collision detection was triggered by an Enemy.
        if(collision.collider.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
			// Disable cactus spawner (they are of type PrefabSpawner)
			foreach(PrefabSpawner spawner in FindObjectsOfType<PrefabSpawner>()) {
				spawner.enabled = false;
			}

			// Disable cactus movement (they are of type MoveLeft)
			foreach(MoveLeft moveLefter in FindObjectsOfType<MoveLeft>()) {
				moveLefter.enabled = false;
			}

			bunnyHurtTime = Time.time;
			bunnyAnimator.SetBool("bunnyHurt", true);

			bunnyRigidBody.velocity = Vector2.zero;

			// Modify y velocity (upwards)
			bunnyRigidBody.AddForce(transform.up * bunnyJumpForce);

			bunnyCollider.enabled = false;
        }
    }
}
