using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAlgorithm : MonoBehaviour
{
    public Animator animator;
    public  GameObject player;

    public GameObject fxhit;
    // Start is called before the first frame update
    void Start()
    {
       
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 pPos= player.transform.position;   
        Vector2 mypos=this.transform.position;
        float distance = Vector2.Distance(pPos, mypos);

        if(distance < 2 & (pPos.y - mypos.y) < 1)
        {
            animator.SetTrigger("Trigger");
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine("Dead");
            animator.SetTrigger("TriggerDead");
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<CircleCollider2D>().enabled = false;
        }
        
    }

    IEnumerator Dead()
    {
        yield return new WaitForSeconds(1.5f);
        Instantiate(fxhit, transform.position, transform.rotation);
        Destroy(this.gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine("Dead");
            animator.SetTrigger("TriggerDead");
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<CircleCollider2D>().enabled = false;
        }
    }
}
