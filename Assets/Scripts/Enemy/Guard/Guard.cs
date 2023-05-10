using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : EnemyMovement
{   
    [SerializeField] Transform shield;
    [SerializeField] GameObject bullet;
    public bool isUpgraded = false;
    public bool reflected = false;

    public AudioClip guardSound;
    protected override void Start()
    {
        base.Start();
    }

    
    protected override void Update()
    {
        base.Update();
    }


    protected override void OnTriggerEnter2D(Collider2D other){
        if (other.tag == "Bullet") {
            //if bullet comes from front
            Vector3 directionToBullet = (other.GetComponent<Transform>().position - transform.position).normalized; 
            //directionToPlayer = (player.transform.position - transform.position).normalized;
            if (Mathf.Sign(transform.localScale.x) == Mathf.Sign(directionToBullet.x)){ //if facing the bullet
                ReflectBullet();
            } else {
                base.OnTriggerEnter2D(other);
            }
        } 
    }


    void ReflectBullet() {
        float relectSpeed = GameManager.playerBulletSpeed;
        Vector3 playerPos = player.transform.position;
        Vector3 middleDirection = playerPos - transform.position;
        middleDirection.Normalize();
        float rotZ = Mathf.Atan2(middleDirection.y, middleDirection.x) * Mathf.Rad2Deg;
        Quaternion initialRotation = Quaternion.Euler(0f, 0f, rotZ);
        GameObject bulletMid = Instantiate(bullet, shield.position, initialRotation);
        bulletMid.GetComponent<Rigidbody2D>().velocity = middleDirection * relectSpeed;
        
        if(isUpgraded) {
            Vector3 leftDirection = Quaternion.AngleAxis(-30, Vector3.forward) * middleDirection;
            Quaternion leftRotation = Quaternion.Euler(0f, 0f, rotZ - 30);
            GameObject leftBullet = Instantiate(bullet, shield.position, leftRotation);
            leftBullet.GetComponent<Rigidbody2D>().velocity = leftDirection * relectSpeed;

            Vector3 rightDirection = Quaternion.AngleAxis(30, Vector3.forward) * middleDirection;
            Quaternion rightRotation = Quaternion.Euler(0f, 0f, rotZ + 30);
            GameObject rightBullet = Instantiate(bullet, shield.position, rightRotation);
            rightBullet.GetComponent<Rigidbody2D>().velocity = rightDirection * relectSpeed;
        }
        
        
        
        
        
        
        
        
        myAnimator.SetTrigger("doAttack");
        SoundManager.instance.PlaySingle(guardSound);
    }
    

}

