using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    
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
      if (other.tag == "Wall" || other.tag == "Enemy") {
        //Destroy(gameObject, 0f);
        Blast();
    } else if (other.tag == "Guard") {
        Transform guardTransform = other.GetComponent<Transform>();
        if (Mathf.Sign(myRigidbody.velocity.x) != Mathf.Sign(guardTransform.localScale.x)){ //if facing the guard
            Destroy(gameObject, 0f);
        } else {
            Blast();
        }
        
    }
    }

    private void Blast() {
        myRigidbody.velocity = new Vector2(0f,0f);
        Vector3 rotationAngles = new Vector3(0, 0, 0);
        transform.rotation = Quaternion.Euler(rotationAngles);
        myAnimator.SetTrigger("blast");
        Destroy(gameObject, 0.3f);
    }


}
