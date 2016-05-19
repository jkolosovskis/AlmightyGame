using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenuUILogic : MonoBehaviour {
	// Use this for initialization
	void Start () {
	
	}
	
	// Function to be executed when pressing New Game button.
	public void moveToSetupMenu () {
        SceneManager.LoadScene("SetupScene");
	}
}
