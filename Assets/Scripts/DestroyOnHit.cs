using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnHit : MonoBehaviour {
    
    /// <summary>
    /// Remove game object that collides with Destroyer's hit box.
    /// </summary>
    /// <param name="other">Game objects that will run into the Destroyer</param>
	void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(other.gameObject);
    }
}
