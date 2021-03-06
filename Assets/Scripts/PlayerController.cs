﻿using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PlayerController : MonoBehaviour
{

    //Movement vars
    private Rigidbody2D rb;
    public GameObject uiPre;
    private LevelSupervisor LevelSel;
    private UIScript ui;
    float accel = 10f;
    float decel = 1.0f;
    public float maxSpeed;
    public bool allowMove = true;
    public Animator Animator;
    public AnimatorClipInfo[] ClipInfo;


    //Shoot vars
    ShootBullet sb;
    public bool allowShoot = true;
    float lastShotTime = 0.0f;
    float lastTimeHit = 0.0f;
    public float shotDelay = 0.5f;
    public float hitDelay = 0.5f;

    public struct PlayerInput
    {
        public bool moving; // Is there either horizontal or vertical axis input
        public float angle; //Angle in radians of input (WASD or Joy)
        public bool fire;
        public PlayerInput(bool m, float ia, bool f1)
        {
            this.moving = m;
            this.angle = ia;
            this.fire = f1;
        }
    }

    PlayerInput pi;

    // Use this for initialization
    void Start()
    {
        this.rb = this.GetComponent<Rigidbody2D>();
        this.sb = GetComponent<ShootBullet>();
        this.lastShotTime = Time.time; // Doing this so that when a wave loads the player doesn't double shoot
        this.ui = uiPre.GetComponent<UIScript>();
        this.LevelSel = GameObject.FindGameObjectWithTag("LevelSupervisor").GetComponent<LevelSupervisor>();
    }

    // Update is called once per frame
    void Update()
    {
        this.pi = getPlayerInput();
        DeassertTriggers();
    }

    void FixedUpdate()
    {
        movementCalc();
        fireShot();
      
    }

    void movementCalc() {
        if (this.pi.moving && this.allowMove)
        {
            //Debug.Log (string.Format("x:{0}, y:{1}",Mathf.Cos (pi.angle), Mathf.Sin (pi.angle)));
            float x_mult = Mathf.Cos(pi.angle);
            x_mult = (Mathf.Abs(x_mult) > 0.001f) ? x_mult : 0;
            float y_mult = Mathf.Sin(pi.angle);
            y_mult = (Mathf.Abs(y_mult) > 0.001f) ? y_mult : 0;
            this.rb.velocity = new Vector2(this.maxSpeed * x_mult, this.maxSpeed * y_mult);

        }
        else
        { // SLOW DOWN
            this.rb.velocity = new Vector2(0, 0);
        }
    }

    void fireShot() {

        if (this.pi.fire && Time.time - this.lastShotTime > this.shotDelay && this.allowShoot)
        {

            sb.Shoot();
            this.lastShotTime = Time.time;
        }
    }

    float getVectorSpeed(Vector2 xyvel)
    {
        return Mathf.Sqrt(Mathf.Pow(xyvel.x, 2) + Mathf.Pow(xyvel.y, 2));
    }

    bool valOppZero(float val1, float val2)
    {
        return ((val1 > 0 && val2 < 0) || (val1 < 0 && val2 > 0)) ? true : false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 12 && Time.time - this.lastTimeHit > this.hitDelay)
        {
        Debug.Log("I'm Hit!");
            ui.Damage();
            Animator.SetBool("HitAnimation", true);
            other.GetComponent<BulletScript>().Hit();
            if (ui.currentHealth == 0)
            {
                Destroy(this.gameObject, 1.0f);
                LevelSel.LevelDone(false);
            }
            this.lastTimeHit = Time.time;
            
        }
        if (other.gameObject.layer == 13)
        {
            other.GetComponent<PickupScript>().Hit();
            if (ui.currentHealth != 3) {
                Debug.Log("I'm Healing!");
                ui.Heal();
            }
        }
        if (other.gameObject.layer == 11)
        {
            Debug.Log("Collision with enemy");
            if (Time.time - this.lastTimeHit > this.hitDelay && other.gameObject.GetComponent<EnemyScript>().alive)
            {
                ui.Damage();
                this.lastTimeHit = Time.time;
                Animator.SetBool("HitAnimation", true);

            }
            if (ui.currentHealth == 0)
            {
                Destroy(this.gameObject, 1.0f);
            }
        }
    }
    

    PlayerInput getPlayerInput()
    {
        PlayerInput ret_pi = new PlayerInput(false, 0, false);
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        ret_pi.angle = Mathf.Atan2(y, x);
        ret_pi.moving = (!(x == 0 && y == 0)) ? true : false;
        ret_pi.fire = (Input.GetAxis("Fire") != 0) ? true : false;
        return ret_pi;
    }

    void DeassertTriggers()
    {
        ClipInfo = Animator.GetCurrentAnimatorClipInfo(0);
        if (ClipInfo[0].clip.name == "Hit")
        {
            Animator.SetBool("HitAnimation", false);
        }
        
    }
}
