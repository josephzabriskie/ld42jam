using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSupervisor : MonoBehaviour {

	//Make sure that you add every level you want in the correct order in levelNames
	//Set in editor
	public List<string> levelNames = new List<string>(new string[]{});
	//Make sure that you also set the length of levelsAccessible to the same as number of levels that you have
	//Set in editor
	public List<bool> levelsAccessible = new List<bool>();
	// Name of scene to go to when we run out of levels
	public string victoryScene;
	// Make these public if you need them, READ ONLY
	int startLevel = 0;
	int currLevel;

	// Use this for initialization
	void Awake () {
		DontDestroyOnLoad(this.gameObject);
		GameObject[] lsarray = GameObject.FindGameObjectsWithTag("LevelSupervisor");
		Debug.Log(lsarray.ToString());
		if (lsarray.Length > 1){
			Debug.Log("Kill LevelSupervisor, array: " + lsarray.Length.ToString());
			Destroy(this.gameObject);
		}
		else{
			Debug.Log("I'm the only LevelSupervisor");
		}
	}

	//These functions are for the main menu controller
	public void StartGame(){
		this.currLevel = this.startLevel;
		this.levelsAccessible[this.startLevel] = true;
		SceneManager.LoadScene(this.levelNames[this.startLevel]);	
	}

	public void SelectLevelStart(int levelSelet){
		this.currLevel = levelSelet;
		SceneManager.LoadScene(this.levelNames[this.currLevel]);
	}
	//!! This function is the one that matters!
	// Call this in other scenes to say"Level won/lost" with true/false. Restart scene on false, go to next scene on true
	// A script should do a "GameObject.FindGameObjectWithTag("LevelSupervisor")" to get this persistant object, then call
	// Level done whenever the level logic says we're done. e.g. on player kill, say LevelDone(false) to restart it.

	// Player should be able to go back to any level they've started.
	public void LevelDone(bool victory){
		if (victory){
			this.StartNextLevel();
		}
		else{
			this.RetryCurrentLevel();
		}
	}

	//These two are internal functions
	void StartNextLevel(){
		this.currLevel++;
		if (this.currLevel > this.levelNames.Count){
			SceneManager.LoadScene(this.victoryScene);
		}
		this.levelsAccessible[this.currLevel] = true;
		SceneManager.LoadScene(this.levelNames[this.currLevel]);
	}

	void RetryCurrentLevel(){
		SceneManager.LoadScene(this.levelNames[this.currLevel]);
	}
	
}
