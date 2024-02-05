
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Almost every game project includes some kind of Game Manager class that handles the overall flow of the game, namely loading and transitioning through scenes.
/// </summary>
public class GameManager : MonoBehaviour {
	public static GameManager Instance;
	public Monkey monkey;
	public float timer = 300f;
    private void Awake()
    {
		Instance = this;
    }

    /// <summary>
    /// This GameManager will check for input to restart the scene
    /// </summary>
    void Update(){
		if (Input.GetKeyDown (KeyCode.Escape) || Input.GetKeyDown (KeyCode.R) || Input.GetKeyDown (KeyCode.Return)) {
			RestartTheGame ();
		}
		timer -= Time.deltaTime;
	}
		
	/// <summary>
	/// Reloads the current scene.
	/// </summary>
	public void RestartTheGame(){
		SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
	}

}

