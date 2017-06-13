using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {

	// Audio toggle button
	public GameObject audioOnIcon;
	public GameObject audioOffIcon;

	// Highscore label
	public Text txtHighscore;

	// Use this for initialization
	void Start () {
		SetAudioState ();

		// Load the highscore
		txtHighscore.text = PlayerPrefs.GetFloat ("Highscore", 0).ToString("0.0");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void StartGame() {
		SceneManager.LoadScene("BunnyHop");
	}

	public void QuitGame() {
		Application.Quit();
	}

	/// <summary>
	/// Using the PlayerPrefs to keep track of the audio state (on or off).
	/// 
	/// </summary>
	public void ToggleSound() {
		// Not muted
		if (PlayerPrefs.GetInt ("Muted", 0) == 0) {
			// Mute
			PlayerPrefs.SetInt ("Muted", 1);
		} 
		// Muted
		else {
			// Unmute
			PlayerPrefs.SetInt ("Muted", 0);
		}

		SetAudioState ();
	}

	public void SetAudioState() {
		// Not muted
		if (PlayerPrefs.GetInt ("Muted", 0) == 0) {
			AudioListener.volume = 1;
			audioOnIcon.SetActive (true);
			audioOffIcon.SetActive (false);
		} 
		// Muted
		else {
			AudioListener.volume = 0;
			audioOnIcon.SetActive (false);
			audioOffIcon.SetActive (true);
		}
	}
}
