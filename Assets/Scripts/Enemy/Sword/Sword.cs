using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : EnemyMovement
{   

    [SerializeField] GameObject sword;
    public bool isUpgraded = false;

    public Transform circleOrigin;
    public float attckRadius;
    public float attackSpeed;

    public float attackDelay = 1f;
    public float chargeSpeed = 5f;
    public float chargeDuration = 1f;

    private bool attackBlocked = false;




    protected override void Start()
    {
        base.Start();
        //General

        //Stats

    }

    
    protected override void Update()
    {
        
        base.Update();
        Patrol();
    }

    protected override void OnTriggerEnter2D(Collider2D other) {
        base.OnTriggerEnter2D(other);
        if (other.tag == "Wall") {
            myRigidbody.velocity = new Vector2(0f,0f);
        } if (other.tag == "Player") {
            sword.SetActive(false); //Make sure only hit once
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.blue;
        Vector3 position = circleOrigin == null ? Vector3.zero : circleOrigin.position;
        Gizmos.DrawWireSphere(position, attckRadius);
    }

    public bool PlayerDetected() {
        foreach (Collider2D collider in Physics2D.OverlapCircleAll(circleOrigin.position, attckRadius)) {
            if (collider.name == "Player") {
                return true;
            }
        }
        return false;
    }

    private void Patrol() {
        if (attackBlocked || !isAlive) {
            return;
        }
        
        if (PlayerDetected()) {
            Debug.Log("player detected");
            myRigidbody.velocity = new Vector2(0f,0f);
            isAttacking = true;
            Invoke ("Attack", attackDelay);
            StartCoroutine(BlockAttack());
        }
    }

    IEnumerator BlockAttack(){
      attackBlocked = true;
      yield return new WaitForSeconds(1/attackSpeed);
      attackBlocked = false;
    }

    private void Attack(){
        if (!isAlive) {
            return;
        }
        myAnimator.SetTrigger("doAttack");
        StartCoroutine(Charge());
    }

    IEnumerator Charge(){
      sword.SetActive(true);
      Vector3 playerPos = player.transform.position;
      Vector3 chargeDirection = playerPos - transform.position;
      chargeDirection.Normalize();
      myRigidbody.velocity = chargeDirection * chargeSpeed;
      yield return new WaitForSeconds (chargeDuration);
      sword.SetActive(false);
      isAttacking = false;

    }

}
