using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    float maxTime = 5;
    public GameObject ps;
    //public bool seeking = false;
    //public GameObject target = null;

    void Awake()
    {
        Destroy(this.gameObject, this.maxTime);
    }

    public void Hit()
    {
        Instantiate(this.ps, this.transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }

    void FixedUpdate(){
        //if (this.seeking && this.target != null){
        //    Vector2 direction = (Vector2)this.target.transform.position - (Vector2)gameObject.transform.position;
        //    direction.Normalize();
        //    float rotateAmount = Vector3.Cross(direction, transform.up).z;
            
        //}
    }

}