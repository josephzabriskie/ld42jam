using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {
	// Update is called once per frame
	public bool menuOpen = false;
	public string mainMenuScene;
	Canvas cvs;

	void Start(){
		this.cvs = GetComponentInChildren<Canvas>();
		this.CloseMenu();
	}


	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (this.menuOpen)
				this.CloseMenu();
			else
				this.OpenMenu();
        }
	}

	public void OpenMenu(){
		Debug.Log("Open pause menu");
		this.menuOpen = true;
		this.cvs.enabled = true;
        Time.timeScale = 0;
	}

	public void CloseMenu(){
		Debug.Log("Close pause menu");
		this.menuOpen = false;
		this.cvs.enabled = false;
        Time.timeScale = 1;
	}

	public void GoToMainMenu(){
		Debug.Log("Go back to Main Menu");
		SceneManager.LoadScene(this.mainMenuScene);
        Time.timeScale = 1;
	}

	public void ExitToDesktop(){
#if UNITY_EDITOR
	if(UnityEditor.EditorApplication.isPlaying) 
	{
		UnityEditor.EditorApplication.isPlaying = false;
	}
#endif
		Application.Quit();
	}
}
