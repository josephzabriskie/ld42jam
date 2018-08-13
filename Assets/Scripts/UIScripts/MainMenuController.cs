using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour {

	//public List<string> levelNames = new List<string>(new string[]{ "-", "JoeRezTest", "SampleScene" });
	//public string firstLevel = "JoeRezTest";
	public Dropdown levelDropDown;
	LevelSupervisor ls;

	// Use this for initialization
	void Start () {
		this.ls = GameObject.FindGameObjectWithTag("LevelSupervisor").GetComponent<LevelSupervisor>();
		this.levelDropDown.ClearOptions();
		this.levelDropDown.AddOptions(new List<string>(new string[]{"-"}));
		for(int i = 0; i < this.ls.levelsBeat.Count; i++){
			if (this.ls.levelsBeat[i])
				this.levelDropDown.AddOptions(new List<string>(new string[]{this.ls.levelNames[i]}));
		}
	}

	public void StartGame(){
		this.LoadScene(this.ls.levelNames[0]);
	}

	public void QuitGame(){
#if UNITY_EDITOR
	if(UnityEditor.EditorApplication.isPlaying) 
	{
		UnityEditor.EditorApplication.isPlaying = false;
	}
#endif
		Application.Quit();
	}

	public void StartLevelSelect(){
		if(this.levelDropDown.value == 0){
			Debug.Log("Level 0 is reserved as a 'no choice' option, do nothing");
			return;
		}
		Debug.Log("Starting Level: " + (levelDropDown.value - 1).ToString());
		this.LoadScene(this.ls.levelNames[levelDropDown.value - 1]);
	}

	void LoadScene(string scene){
		SceneManager.LoadScene(scene);
	}
}
