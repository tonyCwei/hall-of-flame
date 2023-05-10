using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : EnemyMovement
{
    [SerializeField] Transform bow;
    [SerializeField] GameObject arrow;
    [SerializeField] float baseArrowSpeed;
    [SerializeField] float baseArrowDamage;
    [SerializeField] float baseAttackSpeed;

    public float arrowSpeed;
    public float arrowDamage;
    public float attackSpeed;

    public bool attackBlocked;

    public bool isUpgraded = false;
    public AudioClip archerSound;
   
    protected override void Start()
    {
        base.Start();
        //General

        //Stats
        float arrowSpeedGrowth = 1 + ((float)GameManager.level - 1) / 30;
        float attackSpeedGrowth = 1 + ((float)GameManager.level - 1) / 20;
        float arrowDamageGrowth = 1 + ((float)GameManager.level - 1) / 20;

        arrowSpeed = baseArrowSpeed * arrowSpeedGrowth;
        attackSpeed = baseAttackSpeed * attackSpeedGrowth;
        arrowDamage = baseArrowDamage * arrowDamageGrowth;
        
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();


        if (!isUpgraded) {
        ShootArrow();
        } else {
        ShootMultiArrow();
        }
    }

    void ShootArrow() {
        if (attackBlocked || !isAlive) {
            return;
        }
        Vector3 playerPos = player.transform.position;
        Vector2 middleDirection = playerPos - transform.position;
        middleDirection.Normalize();
        Vector2 arrowVelocity = middleDirection * arrowSpeed;
        float rotZ = Mathf.Atan2(middleDirection.y, middleDirection.x) * Mathf.Rad2Deg;
        Quaternion initialRotation = Quaternion.Euler(0f, 0f, rotZ);
        GameObject arrowInstance = Instantiate(arrow, bow.position, initialRotation);
        arrowInstance.GetComponent<Rigidbody2D>().velocity = arrowVelocity;
        myAnimator.SetTrigger("doAttack");
        SoundManager.instance.PlaySingle(archerSound);
        StartCoroutine(BlockAttack());
    }

    void ShootMultiArrow() {
        if (attackBlocked || !isAlive) {
            return;
        }
        Vector3 playerPos = player.transform.position;
        Vector3 middleDirection = playerPos - transform.position;
        middleDirection.Normalize();
        float rotZ = Mathf.Atan2(middleDirection.y, middleDirection.x) * Mathf.Rad2Deg;
        Quaternion initialRotation = Quaternion.Euler(0f, 0f, rotZ);
        GameObject arrowMid = Instantiate(arrow, bow.position, initialRotation);
        arrowMid.GetComponent<Rigidbody2D>().velocity = middleDirection * arrowSpeed;

        
        Vector3 leftDirection = Quaternion.AngleAxis(-30, Vector3.forward) * middleDirection;
        Quaternion leftRotation = Quaternion.Euler(0f, 0f, rotZ - 30);
        GameObject leftArrow = Instantiate(arrow, bow.position, leftRotation);
        leftArrow.GetComponent<Rigidbody2D>().velocity = leftDirection * arrowSpeed;

        Vector3 rightDirection = Quaternion.AngleAxis(30, Vector3.forward) * middleDirection;
        Quaternion rightRotation = Quaternion.Euler(0f, 0f, rotZ + 30);
        GameObject rightArrow = Instantiate(arrow, bow.position, rightRotation);
        rightArrow.GetComponent<Rigidbody2D>().velocity = rightDirection * arrowSpeed;



        //Vector2 arrowVelocity = middleDirection * arrowSpeed;
        // float rotZ = Mathf.Atan2(middleDirection.y, middleDirection.x) * Mathf.Rad2Deg;
        // Quaternion initialRotation = Quaternion.Euler(0f, 0f, rotZ);
        // GameObject arrowMid = Instantiate(arrow, bow.position, initialRotation);
        // arrowMid.GetComponent<Rigidbody2D>().velocity = arrowVelocity;

  



        myAnimator.SetTrigger("doAttack");
        SoundManager.instance.PlaySingle(archerSound);
        StartCoroutine(BlockAttack());
    }

    IEnumerator BlockAttack(){
      attackBlocked = true;
      yield return new WaitForSeconds(1/attackSpeed);
      attackBlocked = false;
    }
}
