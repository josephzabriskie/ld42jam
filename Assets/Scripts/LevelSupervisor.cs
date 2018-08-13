using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSupervisor : MonoBehaviour {

	public List<string> levelNames = new List<string>(new string[]{});
	public List<bool> levelsBeat = new List<bool>();
	public int startLevel = 0;
	public int currLevel;

	// Use this for initialization
	void Awake () {
		DontDestroyOnLoad(this.gameObject);
		if (GameObject.FindGameObjectWithTag("LevelSupervisor") == null){
			Debug.Log("Kill LevelSupervisor");
			Destroy(this.gameObject);
		}
		Debug.Log("I'm the only LevelSupervisor");
	}

	void Start(){
	}

	public void StartGame(){
		this.currLevel = this.startLevel;
		SceneManager.LoadScene(this.levelNames[this.startLevel]);	
	}

	public void SelectLevelStart(int levelSelet){
		this.currLevel = levelSelet;
		SceneManager.LoadScene(this.levelNames[this.currLevel]);
	}

	void StartNextLevel(){
		this.levelsBeat[this.currLevel] = true;
		this.currLevel++;
		SceneManager.LoadScene(this.levelNames[this.currLevel]);
	}

	void RetryCurrentLevel(){
		SceneManager.LoadScene(this.levelNames[this.currLevel]);
	}

	public void LevelDone(bool victory){
		if (victory){
			this.StartNextLevel();
		}
		else{
			this.RetryCurrentLevel();
		}

	}
	
}
