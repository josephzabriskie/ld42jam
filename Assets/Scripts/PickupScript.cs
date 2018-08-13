using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupScript : MonoBehaviour {

	public void Hit(){
		Destroy(this.gameObject);
	}
}
