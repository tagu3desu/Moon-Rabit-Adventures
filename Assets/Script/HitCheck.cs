using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitCheck : MonoBehaviour
{
    public bool isGroundHIt=false;
    public bool isPlayerHit = false;
    public bool isEnemyHit = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Tilemap")
        {
            isGroundHIt = true;
            
        }

        if (collision.gameObject.name == "Player")
        {
            isPlayerHit = true;
            
        }
        if (collision.gameObject.tag == "Enemy")
        {
            isEnemyHit = true;

        }
    }
    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.name == "Tilemap")
        {
            isGroundHIt = false;
        }
        if (collider.gameObject.name == "Player")
        {
            isPlayerHit = false;
        }
        if (collider.gameObject.tag == "Enemy")
        {
            isEnemyHit = false;
        }
    }
}
