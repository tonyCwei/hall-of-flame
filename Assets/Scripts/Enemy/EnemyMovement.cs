using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    //General
    protected Rigidbody2D myRigidbody;
    protected BoxCollider2D myBoxCollider;
    protected Animator myAnimator;
    [SerializeField] GameObject coin;

    //Enemy Stats
    [SerializeField] public float baseMaxHP;
    [SerializeField] public float hpGrowthDenominator = 20; //Higher the number, slower the growth 
    public float MaxHP;
    public float curHP;
    public float attackDMG;

    

 
    
    //Enemy Movement 
    [SerializeField] public float baseSpeed = 1f;
    [SerializeField] public float speedGrowthDenominator = 20; //Higher the number, slower the growth 
    public float moveSpeed;
    public bool shouldMove;
    public bool isGuard = false;
    public float guardFlipDelay = 1f;
    public bool Fliped = false;
    public Vector3 directionToPlayer;
    protected GameObject player;
    protected Player playerScript;
    protected bool isMoving;  
    protected bool hasDroppedCoin = false;
    protected bool isAlive = true;
    protected bool isPlayerAlive = true;
    protected bool isAttacking = false;
    
    
    

    
    protected virtual void Start()
    {
        //General
        myRigidbody = GetComponent<Rigidbody2D>();
        myBoxCollider = GetComponent<BoxCollider2D>();
        myAnimator = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player");
        playerScript = player.GetComponent<Player>();
        
        //Stats
        float hpGrowth = 1 + ((float)GameManager.level - 1) / hpGrowthDenominator;
        MaxHP = baseMaxHP * hpGrowth;
        curHP = MaxHP;

        
        //Movement
        float moveSpeedGrowth = 1 + ((float)GameManager.level - 1) / speedGrowthDenominator;
        moveSpeed = baseSpeed * moveSpeedGrowth;



        
    }

    
    protected virtual void Update()
    {
        if (!isAlive || !isPlayerAlive) {
            myRigidbody.velocity = new Vector2(0f,0f);
            return;
        }

        isMoving = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon || Mathf.Abs(myRigidbody.velocity.y) > Mathf.Epsilon;
        isPlayerAlive = player.GetComponent<Player>().isAlive;

        Move();
        FlipSprite();
        Die();
        
    }

    protected virtual void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Bullet") {
            float dmg = playerScript.GetPlayerDMG();
            Hit(dmg);
        }
    }
    
    //Stats Function
    private void LoseCurHP(float hp) {
        curHP -= hp;
        curHP = curHP < 0 ? 0 : curHP;
    }



    //Enemy Movement
    public void Move(){
        if(!shouldMove || isAttacking ) {
            return;
        }
        directionToPlayer = (player.transform.position - transform.position).normalized;
        myRigidbody.velocity = new Vector2(directionToPlayer.x, directionToPlayer.y) * moveSpeed;
        myAnimator.SetBool("isMoving", isMoving);

    }
    
    public void FlipSprite() {
        if (!isAlive) {
            return;
        }
        
        directionToPlayer = (player.transform.position - transform.position).normalized;

        if (isGuard) {
            if (Mathf.Sign(transform.localScale.x) != Mathf.Sign(directionToPlayer.x) && !Fliped){ // if not facing player
                 Fliped = true;
                 StartCoroutine(GuardFlipSprite());
                 //StartCoroutine(FlipReset());
            } 
             else if (Mathf.Sign(transform.localScale.x) == Mathf.Sign(directionToPlayer.x) && Fliped) {
                 //StartCoroutine(FlipReset());
                 Fliped = false;
            }
        } else if (!isGuard){
            transform.localScale = new Vector2(Mathf.Sign(directionToPlayer.x),1f);
        }
    }

    IEnumerator GuardFlipSprite(){
      if(!isAlive) {
        yield break;
      }
      yield return new WaitForSeconds(guardFlipDelay);
      directionToPlayer = (player.transform.position - transform.position).normalized;
      transform.localScale = new Vector2(Mathf.Sign(directionToPlayer.x),1f);
    }
    
    //  IEnumerator FlipReset(){
    //   yield return new WaitForSeconds(guardFlipDelay + 1f);
    //   Fliped = false;
    // }
    
    //Mortality
    public void Hit(float dmg){
      myAnimator.SetTrigger("doHit");
      LoseCurHP(dmg);
    }

    public void Die() {
        if (curHP == 0) {
        isAlive = false;
        myBoxCollider.enabled = false;
        myRigidbody.velocity = new Vector2(0f,0f);
        myAnimator.SetTrigger("doDie");
        if (!hasDroppedCoin) {
            Instantiate(coin,transform.position + new Vector3(0f, 0.25f, 0f), transform.rotation);
            hasDroppedCoin = true;
            GameManager.enemyCount--;
        }
        Destroy(gameObject, 1f);
        }
    }





}
