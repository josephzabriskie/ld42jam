using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour {
	Image img;
	public Sprite onSprite;
	public Sprite offSprite;

	void Awake(){
		this.img = GetComponent<Image>();
	}

	public void SetOn(bool setVal){
		//Debug.Log("Set HealthUI to: " + setVal.ToString());
		if (setVal)
			this.img.sprite = this.onSprite;
		else
			this.img.sprite = this.offSprite;
	}

	public void DestroyGameObj(){
		Destroy(this.gameObject);
	}

}
