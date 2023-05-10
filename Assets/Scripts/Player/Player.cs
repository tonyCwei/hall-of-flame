using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

using Random = UnityEngine.Random;

public class Player : MonoBehaviour
{   
    //General
    private Rigidbody2D myRigidbody;
    private BoxCollider2D myBoxCollider;
    private Animator myAnimator;
    private float restartLevelDelay = 1f;
    

    //Player Stats
    public float maxHP;
    public float curHP;
    public int coinAmount;
    public bool isAlive = true;
    public bool isInvin = false;


    //Player Movement
    public float moveSpeed;
    public bool isMoving = false;
    public float dashDuration;
    public float dashSpeed;
    [SerializeField] GameObject dust;
    private Vector2 moveInput;
    private Vector3 mousePos;
    private bool isDashing;
    private bool dashBlocked;


    //Player Attack
    [SerializeField] Transform gun;
    [SerializeField] GameObject bullet;
    public float bulletSpeed;
    public  float minPlayerBulletDamage ;
    public  float maxPlayerBulletDamage ;
    public float attackSpeed; 
    private bool attackBlocked;
    private Vector2 shootDirection;
    private Vector2 bulletVelocity;

    //Audio
    public AudioClip attackSound;
    public AudioClip hitSound;

    void Start()
    {
        //General
        myRigidbody = GetComponent<Rigidbody2D>();
        myBoxCollider = GetComponent<BoxCollider2D>();
        myAnimator = GetComponent<Animator>();
        

        //Player Stats
        maxHP = GameManager.playerMaxHP;
        curHP = GameManager.playerCurHP;
        coinAmount = GameManager.playerCoinAmount;

        //Movement
        moveSpeed = GameManager.playerMoveSpeed;

        //Attack
        bulletSpeed = GameManager.playerBulletSpeed;
        minPlayerBulletDamage = GameManager.minPlayerBulletDamage;
        maxPlayerBulletDamage = GameManager.maxPlayerBulletDamage;
        attackSpeed = GameManager.playerAttackSpeed;

    }

    //on gameend
    private void SaveStats() {
        GameManager.playerMaxHP = maxHP;
        GameManager.playerCurHP = curHP;
        GameManager.playerCoinAmount = coinAmount;

        GameManager.playerBulletSpeed = bulletSpeed;
        GameManager.minPlayerBulletDamage = minPlayerBulletDamage;
        GameManager.maxPlayerBulletDamage = maxPlayerBulletDamage;
        GameManager.playerAttackSpeed = attackSpeed;
        GameManager.playerMoveSpeed = moveSpeed;
    }


    void Update()
    {   
        if (!isAlive || GameManager.inShop) {
            return;
        }
        //General
        





        //Movement
        isMoving = Mathf.Abs(myRigidbody.velocity.x) > 0.1f || Mathf.Abs(myRigidbody.velocity.y) > 0.1f;
        Run();
        FlipSprite();
    }

    private void OnTriggerEnter2D(Collider2D other) {
      if (other.tag == "Arrow" || other.tag == "Enemy Bullet" || other.tag == "Sword") {
         Hit(1f);
      } else if (other.tag == "Hazard" || other.tag == "Guard") {
         Hit(1f);
      } else if (other.tag == "USword") {
        Hit(1f);
      } else if (other.tag == "Coin") {
         AddCoin(1);
         Destroy(other.gameObject,0f);
      } else if (other.tag == "Exit") {
         Invoke ("NextLevel", restartLevelDelay);
         SaveStats();
         myRigidbody.velocity = new Vector2(0,0);
         enabled = false;
      }
    }

    //Player Info Functions
    public void AddMaxHP(float hp) {
        maxHP += hp;
    }

    public void LoseCurHP(float hp) {
        curHP -= hp;
        curHP = curHP < 0 ? 0 : curHP;
    }

    public void AddCurHP(float hp) {
        curHP += hp;
        curHP = curHP > maxHP ? maxHP : curHP;
    }

    public void AddCoin(int amount){
        coinAmount += amount;
    }

    public void LoseCoin(int amount) {
        coinAmount -= amount;
    }

    public bool EnoughCoin(int purchaseAmount) {
        return coinAmount >= purchaseAmount;
    }

    private void CheckIfGameOver() {
        if (curHP == 0) {
            
            Die();
            GameManager.instance.GameOver();
        }
    }

    //Movement Functions
    void OnMove(InputValue value){
        moveInput = value.Get<Vector2>();
    }

    void OnDash(InputValue value){
       if(isDashing || dashBlocked || GameManager.inShop) {
        return;
       }
       StartCoroutine(Dash());
       StartCoroutine(DashBlock());
    }

    IEnumerator Dash() {
      isDashing = true;
      Vector2 dashDirection = myRigidbody.velocity;
      dashDirection.Normalize();
      myRigidbody.velocity = dashDirection * dashSpeed;
      dust.SetActive(true);
      yield return new WaitForSeconds(dashDuration);
      isDashing = false;
      dust.SetActive(false);
    }

    IEnumerator DashBlock() {
      dashBlocked = true;
      yield return new WaitForSeconds(dashDuration + 1f);
      dashBlocked = false;
    }




   void Run(){
      if (isDashing) {
        return;
      }
      myRigidbody.velocity = new Vector2(moveInput.x * moveSpeed , moveInput.y * moveSpeed);
      myAnimator.SetBool("isMoving", isMoving);

    }

   void FlipSprite(){
      mousePos = Mouse.current.position.ReadValue();
        //shootDirection.z = 0.0f;
      mousePos = Camera.main.ScreenToWorldPoint(mousePos);
      Vector2 faceDirection;
      faceDirection = mousePos - transform.position;
      faceDirection.Normalize();
      transform.localScale = new Vector2 (Mathf.Sign(faceDirection.x), 1f);
    }

    //Mortality
    void Hit(float dmg){
      if (isInvin || isDashing) {
        return;
      }
      StartCoroutine(HitInvin());
      myAnimator.SetTrigger("hit");
      SoundManager.instance.PlaySingle(hitSound);
      LoseCurHP(dmg);
      CheckIfGameOver();
    }

    IEnumerator HitInvin(){
      isInvin = true;
      yield return new WaitForSeconds(1f);
      isInvin = false;
    }


    void Die() {
        isAlive = false;
        myBoxCollider.enabled = false;
        myRigidbody.velocity = new Vector2(0f,0f);
        myAnimator.SetTrigger("die");
        
    }


    //Attack Funcitons
    void OnFire(InputValue value){
        if (!isAlive || attackBlocked || isDashing || GameManager.inShop) {
            return;
        }

        mousePos = Mouse.current.position.ReadValue();
        //shootDirection.z = 0.0f;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        shootDirection = mousePos - transform.position;
        shootDirection.Normalize();
        bulletVelocity = shootDirection * bulletSpeed;
        float rotZ = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg;
        Quaternion initialRotation = Quaternion.Euler(0f, 0f, rotZ);
        GameObject bulletInstance = Instantiate(bullet, gun.position, initialRotation);
        bulletInstance.GetComponent<Rigidbody2D>().velocity = bulletVelocity;
        myAnimator.SetTrigger("attack");
        SoundManager.instance.PlaySingle(attackSound);
        StartCoroutine(BlockAttack());
        //Instantiate(bullet, gun.position, rotation);
    }

    IEnumerator BlockAttack(){
      attackBlocked = true;
      yield return new WaitForSeconds(1/attackSpeed);
      attackBlocked = false;
    }

    public float GetPlayerDMG () {
        float playerBulletDamage = Random.Range (minPlayerBulletDamage, maxPlayerBulletDamage);
        playerBulletDamage = Mathf.RoundToInt(playerBulletDamage);
        Debug.Log(playerBulletDamage);
        return playerBulletDamage;
    }

    //Level Management
    private void NextLevel() {
      GameManager.level++;
      if (GameManager.level % 5 == 0) {
        SceneManager.LoadScene("ShopScene");
      } else {
        SceneManager.LoadScene("GameScene");
      }
      
      
    }

    


}
