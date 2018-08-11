using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour {

	public List<string> levelNames = new List<string>(new string[]{ "-", "JoeRezTest", "SampleScene" });
	public string firstLevel = "JoeRezTest";
	public Dropdown levelDropDown;

	// Use this for initialization
	void Start () {
		this.levelDropDown.ClearOptions();
		this.levelDropDown.AddOptions(this.levelNames);
	}

	public void StartGame(){
		this.LoadScene(this.firstLevel);
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
		Debug.Log("Starting Level: " + levelDropDown.value.ToString());
		this.LoadScene(this.levelNames[levelDropDown.value]);
	}

	void LoadScene(string scene){
		SceneManager.LoadScene(scene);
	}
}
