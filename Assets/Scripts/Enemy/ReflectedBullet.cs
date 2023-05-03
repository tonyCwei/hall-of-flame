using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectedBullet : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D myRigidbody;
    Animator myAnimator;
    
    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnTriggerEnter2D(Collider2D other) {
      if (other.tag == "Player" || other.tag == "Wall") {
        //Destroy(gameObject, 0f);
        myRigidbody.velocity = new Vector2(0f,0f);
        Vector3 rotationAngles = new Vector3(0, 0, 0);
        transform.rotation = Quaternion.Euler(rotationAngles);
        myAnimator.SetTrigger("blast");
        Destroy(gameObject, 0.3f);
    } 
    }
}
