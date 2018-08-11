using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour {

	public int maxHealth;
	public int currentHealth;
	public GameObject healthUIIconPrefab;
	public GameObject healthPanel;
	public List<HealthUI> hUIList;

	void Start () {
		this.currentHealth = this.maxHealth;
		this.hUIList = new List<HealthUI>();
		//Instantiate Health UI
		this.SetMaxHealth(this.maxHealth);

	}

	public void SetMaxHealth(int health){
		this.maxHealth = health;
		if (this.currentHealth > this.maxHealth){
			this.currentHealth = this.maxHealth;
		}
		foreach(HealthUI hui in this.hUIList){
			hui.DestroyGameObj();
			this.hUIList.Remove(hui);
		}
		for(int i = 0; i < health; i++){
			GameObject h = Instantiate(this.healthUIIconPrefab, this.healthPanel.transform);
			this.hUIList.Add(h.GetComponent<HealthUI>());
		}
		this.UpdateHealth(this.currentHealth);
		
	}

	public void Damage(){
		if (this.currentHealth > 0)
			this.UpdateHealth(this.currentHealth - 1);
		else
			this.UpdateHealth(0);
	}

	public void Heal(){
		if (this.currentHealth < this.maxHealth)
			this.UpdateHealth(++this.currentHealth);
		else
			this.UpdateHealth(this.maxHealth);
	}

	public void UpdateHealth(int newHealth){
		this.currentHealth = newHealth;
		for(int i = 0; i < this.maxHealth; i++){
			if(i < this.currentHealth){
				//Debug.Log("Set " + i.ToString() + " to true");
				this.hUIList[i].SetOn(true);
			}
			else{
				//Debug.Log("Set " + i.ToString() + " to False");
				this.hUIList[i].SetOn(false);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
