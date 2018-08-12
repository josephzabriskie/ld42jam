using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneShotParticle : MonoBehaviour {
	public int part = 15;
	public int delay = 3;

	// Use this for initialization
	void Start () {
		GetComponent<ParticleSystem>().Emit(this.part);
		Destroy(gameObject, this.delay);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
