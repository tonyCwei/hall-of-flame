using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    Rigidbody2D myRigidbody;
    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
      if (other.tag == "Wall" || other.tag == "Player") {
        //Destroy(gameObject, 0f);
        myRigidbody.velocity = new Vector2(0f,0f);
        Destroy(gameObject, 0.1f);
    }
    }
}
