﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.layer == 10)
        {
            other.gameObject.GetComponent<BulletScript>().Hit();
        }
    }
}
