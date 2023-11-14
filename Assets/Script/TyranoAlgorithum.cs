using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Processors;

public class TyranoAlgorithum : MonoBehaviour
{
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb2D;
    public GameObject player;
    public float speed = 1.5f;

    public GameObject fxhit;

    //行動制御用フラグ
    private bool isDead ;
    private bool isAttack;
    private bool isIdle;
   
    // Start is called before the first frame update
   


    //当たり判定
    public HitCheck sideCheck; //横の判定
    public HitCheck groundCheck; 
    void Start()
    {
        player = GameObject.Find("Player");
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb2D = GetComponent<Rigidbody2D>();
        Sound.LoadSe("HummerDead", "maou_se_battle06");
        //sideCheck = transform.Find("CollisionSIdeCheck").gameObject.GetComponent<HitCheck>();
        //groundCheck = transform.Find("CollisionGroundCheck").gameObject.GetComponent<HitCheck>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = 1;
        if (this.transform.eulerAngles.y == 180)
        {
            x = -1;
        }
        else
        {
            x = 1;
        }

        

        if (sideCheck.isPlayerHit)
        {

            if (!isAttack)
            {
                StartCoroutine("Attack");
            }
            rb2D.velocity = new Vector2(0, 0);

        }

        
        //rb2D.AddForce(Vector2.right * x * speed);
        if (!isIdle && !isDead && !isAttack)
        {
            animator.SetBool("isWalk", true);
            rb2D.AddForce(Vector2.right * x * speed);
        }
        else
        {
            animator.SetBool("isWalk", false);
            rb2D.velocity = new Vector2(0, 0);
        }

        CheckValue();
    }


    private void CheckValue()
    {
        //地面にヒットしていない時　かつ　待機状態ではない時
        if (!groundCheck.isGroundHIt && !isIdle)
        {
            groundCheck.isGroundHIt = true;
            StartCoroutine("ChangeRotate");
            Debug.Log("あたってる");

        }

        if(sideCheck.isEnemyHit &&  !isIdle)
        {
            sideCheck.isEnemyHit = false;
            StartCoroutine("ChangeRotate");
        }

        if (sideCheck.isGroundHIt && !isIdle)
        {
            sideCheck.isGroundHIt = false;
            StartCoroutine("ChangeRotate");
        }
    }
    IEnumerator ChangeRotate()
    {
        isIdle = true;

        yield return new WaitForSeconds(1.0f);

        if (this.transform.eulerAngles.y == 180.0f)
        {
            this.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            this.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        isIdle = false;
    }
    IEnumerator Attack()
    {
        isAttack = true;
        animator.SetTrigger("TriggerATK");
        yield return new WaitForSeconds(2.5f);
        Destroy(this.gameObject);
    }

    IEnumerator Dead()
    {
        isDead = true;
        yield return new WaitForSeconds(1.5f);

        Instantiate(fxhit, transform.position, transform.rotation);
        Destroy(this.gameObject);

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine("Dead");
            animator.SetTrigger("TiggerDead");
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<CircleCollider2D>().enabled = false;
        }
       
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Sound.PlaySe("HummerDead", 1);
            StartCoroutine("Dead");
            animator.SetTrigger("TiggerDead");
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<CircleCollider2D>().enabled = false;
        }
    }
}
