using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeftKinematic : MonoBehaviour {
	public float speed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position += new Vector3(-speed * Time.deltaTime, 0, 0);
	}
}
