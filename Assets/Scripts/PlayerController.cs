using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    //Movement vars
    private Rigidbody2D rb;
    float accel = 10f;
    float decel = 1.0f;
    float maxSpeed = 7.0f;
    public bool allowMove = true;

    //Shoot vars
    ShootBullet sb;
    public bool allowShoot = true;
    float lastShotTime = 0.0f;
    float shotDelay = 0.5f;

    public struct PlayerInput
    {
        public bool moving; // Is there either horizontal or vertical axis input
        public float angle; //Angle in radians of input (WASD or Joy)
        public bool fire1;
        public PlayerInput(bool m, float ia, bool f1)
        {
            this.moving = m;
            this.angle = ia;
            this.fire1 = f1;
        }
    }

    PlayerInput pi;

    // Use this for initialization
    void Start()
    {
        this.rb = this.GetComponent<Rigidbody2D>();
        this.sb = GetComponent<ShootBullet>();
        this.lastShotTime = Time.time; // Doing this so that when a wave loads the player doesn't double shoot
    }

    // Update is called once per frame
    void Update()
    {
        this.pi = getPlayerInput();
    }

    void FixedUpdate()
    {
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
        Debug.Log("Trigger Pull?");
        if (this.pi.fire1 && Time.time - this.lastShotTime > this.shotDelay && this.allowShoot)
        {
            Debug.Log("Shot");
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


    PlayerInput getPlayerInput()
    {
        PlayerInput ret_pi = new PlayerInput(false, 0, false);
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        ret_pi.angle = Mathf.Atan2(y, x);
        ret_pi.moving = (!(x == 0 && y == 0)) ? true : false;
        ret_pi.fire1 = (Input.GetAxis("Fire1") != 0) ? true : false;
        return ret_pi;
    }
}
