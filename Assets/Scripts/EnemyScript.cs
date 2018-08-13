using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public AudioClip deathSound;
    Rigidbody2D rb;
    //SpriteRenderer sr;
    public string movementType;
    //Circle around point
    Vector3 center;
    public GameObject mainChar;
    public float speed = 5.0f;
    public bool isActive = false;
    public float rotateAmount;
    public float activationOffset;
    private float activationTime;
    private Transform playerPosition;
    PolygonCollider2D enemyCollider;
    MoveLeftKinematic lk;
    public float radius = 1.0f;
    public float rotateSpeed = 2.0f;
    private float angle = 0.0f;
    public bool cwPath = true;
    bool alive = true;
    public float Health = 5;
    //Line bool
    public bool linearReverse = false;
    public bool horizPath = true;

    //Shot vars
    ShootBullet sb;
    public bool allowShoot = true;
    public float shotDelay = 1.0f;
    float lastShotTime = 0.0f;

    void Start()
    {
        this.sb = GetComponent<ShootBullet>();
        this.rb = GetComponent<Rigidbody2D>();
        //this.sr = GetComponent<SpriteRenderer>();
         // Set center to where we currently are positioned
        this.enemyCollider = GetComponent<PolygonCollider2D>();
        this.lk = GetComponent<MoveLeftKinematic>();
        this.playerPosition = mainChar.GetComponent<Transform>();
    }

    void Update()
    {
        if (isActive && Time.time - this.activationTime > this.activationOffset)
        {
            if (Time.time - this.lastShotTime > this.shotDelay && this.alive && this.allowShoot)
            {
                sb.Shoot();
                this.lastShotTime = Time.time;
            }
            if (!alive)
            {
                this.transform.Rotate(new Vector3(0, 0, 2));
                //don't need right now, looks alright
                this.transform.localScale = new Vector3(this.transform.localScale.x * 0.99f, this.transform.localScale.y * 0.99f, 1.0f);
            }
            this.enemyCollider.transform.rotation = rb.transform.rotation;
        }
    }

    void FixedUpdate()
    {
        //Add these lines if you want to make the baddie fly away randomly, polish
        //if (!this.alive)
        //	this.rb.AddForce (new Vector2 (0, Random.Range (0, 10)));
        if (isActive && Time.time - this.activationTime > this.activationOffset)
        {
            if (this.movementType == "Circle" && this.alive)
                CircleUpdate();
            if (this.movementType == "Line" && this.alive)
                LineUpdate();
            if (this.movementType == "Follow" && this.alive)
                FollowUpdate();
        }
        //Else do nothing, just sits there
    }

    void CircleUpdate()
    {
        this.angle += this.rotateSpeed * Time.deltaTime;
        if (this.angle > 6.28319f)
            this.angle -= 6.28319f;
        Vector3 offset;
        if (this.cwPath)
        {
            offset = new Vector3(Mathf.Sin(this.angle), Mathf.Cos(this.angle), 0) * this.radius;
        }
        else
        {
            offset = new Vector3(Mathf.Cos(this.angle), Mathf.Sin(this.angle), 0) * this.radius;
        }
        this.transform.position = this.center + offset;
    }

    void LineUpdate()
    { //Still uses same vars as Circle update, radius, rotatespeed just add new bool for horiz/vert
        this.lk.speed = 0;
        this.angle += this.rotateSpeed * Time.deltaTime;
        if (this.angle > 6.28319f)
            this.angle -= 6.28319f;
        Vector3 offset;
        if (this.horizPath)
        {
            if (!this.linearReverse)
            {
                offset = new Vector3(Mathf.Sin(this.angle), 0, 0) * this.radius;
            }
            else
            {
                offset = new Vector3(Mathf.Cos(this.angle), 0, 0) * this.radius;
            }
        }
        else
        {
            if (!this.linearReverse)
            {
                offset = new Vector3(0, Mathf.Cos(this.angle), 0) * this.radius;
            }
            else
            {
                offset = new Vector3(0, Mathf.Sin(this.angle), 0) * this.radius;
            }
        }
        this.transform.position = this.transform.position + offset;
    }

    void FollowUpdate()
    {
        if (playerPosition && playerPosition.position.x < this.transform.position.x)
        {
            Vector2 direction = (Vector2)playerPosition.position - rb.position;

            direction.Normalize();

            rotateAmount = Vector3.Cross(direction, -transform.right).z;

            rb.angularVelocity = -rotateAmount * rotateSpeed;

            rb.velocity = -transform.right * speed;
            this.enemyCollider.transform.rotation = rb.transform.rotation;
        }
        else
        {
            this.enemyCollider.transform.rotation = rb.transform.rotation;

            rb.angularVelocity = 0;
            rotateAmount = 0;

        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 10)
        {
            Health--;
            other.gameObject.GetComponent<BulletScript>().Hit();
            if (Health == 0)
            {
                Destroy(this.gameObject, 1.0f);

                alive = false;
            }
        }
        if (other.gameObject.layer == 15)
        { 
            isActive = true;
            activationTime = Time.time;
        }
        if (other.gameObject.layer == 16)
        { 
            Destroy(this.gameObject, 1.0f);
            alive = false;
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            Physics.IgnoreLayerCollision(8, 11);
        }
    }
}