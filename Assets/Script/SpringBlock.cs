using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringBlock : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D playerRB2D;
    private float bounceForce = 10f;
    void Start()
    {
        playerRB2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerRB2D.velocity = new Vector2(playerRB2D.velocity.x, bounceForce);
        }
    }
}
