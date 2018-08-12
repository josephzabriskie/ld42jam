using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    enum BulletTypes{
        normal,
        seeking
    };

    float maxTime = 5;
    public GameObject ps;
    public BulletType bt;

    void Awake()
    {
        Destroy(this.gameObject, this.maxTime);
    }

    public void Hit()
    {
        Instantiate(this.ps, this.transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }

}