using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StarManager : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D rb2D;
    private float  upPower = 9;
    bool isGet = false;

    public GameObject fxhit;

    public GameObject canvas;
    
    
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        canvas=GameObject.Find("Canvas");
        Sound.LoadSe("getCoin", "coin03");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       
        if (isGet){ transform.Rotate(0, 20, 0); }
    }

   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            
            isGet = true;
            rb2D.AddForce(upPower * Vector2.up);
            canvas.SendMessage("AddScore", 150);
            GetComponent<BoxCollider2D>().enabled = false; ;
            Instantiate(fxhit, transform.position, transform.rotation);
            Sound.PlaySe("getCoin", 1);
            StartCoroutine("GetItem");
           
        }
    }
    IEnumerator  GetItem()
    {
        yield return new WaitForSeconds(1.0f);
        Destroy(this.gameObject);

    }
}
