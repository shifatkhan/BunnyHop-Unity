using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BunnyController : MonoBehaviour {

	// Bunny's sprite objects
    private Rigidbody2D bunnyRigidBody;
	private Collider2D bunnyCollider;
	private Animator bunnyAnimator;

	// Functionality of bunny
    public float bunnyJumpForce = 200f;
	private float bunnyHurtTime = -1f;
	private const int MAX_JUMPS = 1; // 2 for double jump
	private int jumpsLeft = MAX_JUMPS; 

	// UI
	public Text scoreText;
	private float startTime;

	/// <summary>
	/// Use this for initialization
	/// </summary>
	void Start () {
        // Get reference for RigitBody2D
        bunnyRigidBody = GetComponent<Rigidbody2D>();

		// Get reference to the bunny's 2D collider
		bunnyCollider = GetComponent<Collider2D> ();

        // Get reference for Animator
        bunnyAnimator = GetComponent<Animator>();

		startTime = Time.time;
	}// End Start
	
	/// <summary>
	/// Update is called once per frame
	/// </summary>
	void Update () {

		if (bunnyHurtTime == -1) {
			// Spacebar clicked, on touch
			if ((Input.GetButtonDown ("Jump") || (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Began)) && jumpsLeft > 0) {

				// Cancel downwards velocity if falling for second jump
				if(MAX_JUMPS > 1 && bunnyRigidBody.velocity.y < 0){
					
					bunnyRigidBody.velocity = Vector2.zero;
				}

				// Modify y velocity (upwards)
				bunnyRigidBody.AddForce (transform.up * bunnyJumpForce);
				jumpsLeft--;
			}

			// Update property vVelocity accordingly.
			bunnyAnimator.SetFloat ("vVelocity", bunnyRigidBody.velocity.y);

			scoreText.text = (Time.time - startTime).ToString("0.0");
		}
		else {
			// Reset level 2 seconds after getting hurt.
			if (Time.time > bunnyHurtTime + 2) {
				SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
			}
		}
	}// End Update

	/// <summary>
	/// Method that will change values for the bunny when it gets hurt.
	/// </summary>
	/// <param name="collision">Collision.</param>
    void OnCollisionEnter2D(Collision2D collision) {

        // Check if the Bunny's collision detection was triggered by an Enemy.
		if (collision.collider.gameObject.layer == LayerMask.NameToLayer ("Enemy")) {
			// Disable cactus spawner (they are of type PrefabSpawner)
			foreach (PrefabSpawner spawner in FindObjectsOfType<PrefabSpawner>()) {
				spawner.enabled = false;
			}

			// Disable cactus movement (they are of type MoveLeft)
			foreach (MoveLeft moveLefter in FindObjectsOfType<MoveLeft>()) {
				moveLefter.enabled = false;
			}

			bunnyHurtTime = Time.time;
			bunnyAnimator.SetBool ("bunnyHurt", true);

			bunnyRigidBody.velocity = Vector2.zero;

			// Modify y velocity (upwards)
			bunnyRigidBody.AddForce (transform.up * bunnyJumpForce);

			bunnyCollider.enabled = false;
		}
		// Check if the Bunny's collision detection was triggered by an Enemy.
		else if (collision.collider.gameObject.layer == LayerMask.NameToLayer ("Ground")) {
			jumpsLeft = MAX_JUMPS;
		}

    }// End OnCollisionEnter2D

}// End class
