using System.Collections;
using System.Collections.Generic;
//using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControler : MonoBehaviour
{
    [Header("移動")]
    [Tooltip("プレイヤーの移動速度")]
    public float normalSpeed = 0.0f;
    public float dashSpeed;
    [Tooltip("ジャンプ力")]
    public float jumpPower = 5;

    public GameObject cameraObject;

    // 地面に着地したかどうかのフラグ
    public bool isGround = false;
    //public GroundingDetermination ground;   

    Vector2 direction;
    private Rigidbody2D rb2D;
    private GameInput input;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private bool isSlope  = false;
    public LayerMask groundLayer;

    public bool isDead = false;
    //攻撃中
    bool isHummer=false;

    private GameObject canvas;

    public bool isStageClear = false;

    void Start()
    {
        input = Camera.main.GetComponent<GameInput>();
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        cameraObject = GameObject.Find("Main Camera");
        canvas=GameObject.Find("Canvas");

        //input.playerInputActions.Player.Direction.canceled += MoveStop;
        input.inputActions.Player.Jump.started += Jump;
        input.inputActions.Player.Punch.started += Punch;
        //input.inputActions.Player.Hummer.started += Hummer;
        dashSpeed = normalSpeed * 2;

        Sound.LoadSe("dead", "「きゃああーー！」");//0
        Sound.LoadSe("enemyKill", "「たあっ！」"); //1
        Sound.LoadSe("Clear", "maou_se_jingle02");//2
        Sound.LoadSe("HummerAtk", "「スキあり！」");//3
        Sound.LoadSe("damage", "「きゃっ！」");//4
        Sound.LoadSe("clearVoice", "「先を急ぎましょう」");//5
       
       
        
    }

    void FixedUpdate()
    {
        //今の接地状態
        //isGround = ground.IsGround();

        
        //向き取得
        direction = input.inputActions.Player.Direction.ReadValue<Vector2>();
        //坂道関係の処理
        {
            //自分の立っている場所
            Vector2 groundpos = new Vector2(transform.position.x, transform.position.y);

            //坂道判定
            Vector2 groundArea = new Vector2(0.5f, 0.4f);

            Vector2 wallArea1 = new Vector2(Mathf.Abs(direction.x) * 0.8f, 1.5f);
            Vector2 wallArea2 = new Vector2(Mathf.Abs(direction.x) * 0.3f, 1.0f);

            Vector2 wallArea3 = new Vector2(Mathf.Abs(direction.x) * 1.5f, 0.6f);
            Vector2 wallArea4 = new Vector2(Mathf.Abs(direction.x) * 1.0f, 0.1f);

            Vector2 wallArea5 = new Vector2(Mathf.Abs(direction.x) * -0.8f, 1.5f);
            Vector2 wallArea6 = new Vector2(Mathf.Abs(direction.x) * -0.3f, 1.0f);

            Vector2 wallArea7 = new Vector2(Mathf.Abs(direction.x) * -1.5f, 0.6f);
            Vector2 wallArea8 = new Vector2(Mathf.Abs(direction.x) * -1.0f, 0.1f);

            isGround = Physics2D.OverlapArea(groundpos + groundArea, groundpos - groundArea, groundLayer);

            bool area1 = false;
            bool area2 = false;
            bool area3 = false;
            bool area4 = false;

            area1 = Physics2D.OverlapArea(groundpos + wallArea1, groundpos + wallArea2, groundLayer);
            area2 = Physics2D.OverlapArea(groundpos + wallArea3, groundpos + wallArea4, groundLayer);

            area3 = Physics2D.OverlapArea(groundpos + wallArea5, groundpos + wallArea6, groundLayer);
            area4 = Physics2D.OverlapArea(groundpos + wallArea7, groundpos + wallArea8, groundLayer);


            if (!area1 & area2 || !area3 & area4) { isSlope = true; }
            else { isSlope = false; }
            if (isSlope) { this.gameObject.transform.Translate(0.12f * direction.x, 0.0f, 0.0f); }
        }


        //移動入力
        if(!isStageClear){
            if (Mathf.Abs(direction.x) > 0.3f)
            {
                normalSpeed = 15.0f;
                rb2D.AddForce(new Vector2(direction.x * normalSpeed, 0f));
                animator.SetFloat("speed", normalSpeed);
            }
            else
            {
                normalSpeed = 0;
                animator.SetFloat("speed", normalSpeed);
            }

            if (direction.x < 0)
            {
                spriteRenderer.flipX = true;
            }
            else if (direction.x > 0)
            {
                spriteRenderer.flipX = false;
            }
        }
       

        //地面にいるときはジャンプモーションをオフにする
        if(isGround){
            animator.SetBool("isJump", false);
            animator.SetBool("isFall", false);
        }

        float velX=rb2D.velocity.x;
        float velY=rb2D.velocity.y;

        if (velY >  0.5f) { animator.SetBool("isJump", true); }
        if (velY < -0.1f) { animator.SetBool("isFall", true); }

        animator.SetBool("isPunchi", false);
        animator.SetBool("isHummer", false);
        //ダッシュ状態(実装予定)
        //else if (Mathf.Abs(direction.x) > 0.9f)
        //{
        //    rb2D.AddForce(new Vector2(direction.x * dashSpeed, 0f));
        //}

        if (isHummer) { Invoke("HummerReset", 1); }

    }

   private void HummerReset() { isHummer = false; } 

    private void Jump(InputAction.CallbackContext context)
    {
        if (context.started && /*ground.jumpCount == 0 &&*/ isGround)
        {
            rb2D.AddForce(jumpPower * Vector2.up/1.3f, ForceMode2D.Impulse);
            //ground.jumpCount = 1;
            animator.SetBool("isJump",true);
            isGround = false;
            
        }
    }


    //ハンマーモーション
    private void Punch(InputAction.CallbackContext context)
    {
        if(context.started && !isHummer)
        {
            
            animator.SetBool("isPunchi", true);
            isHummer = true;
            Sound.PlaySe("HummerAtk", 3);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        
    }
    //ハンマーモーション
    //private void Hummer(InputAction.CallbackContext context)
    //{
    //    if (context.started && isGround)
    //    {

    //        animator.SetBool("isHummer", true);
    //    }
    //}

    IEnumerator Dead()
    {
        animator.SetBool("isFallDamage", true);

        yield return new WaitForSeconds(0.5f);

        canvas.SendMessage("DisplayTelop", "gameover");

        cameraObject.GetComponent<CameraFollow>().enabled = false;
        rb2D.AddForce(jumpPower/2.5f * Vector2.up,ForceMode2D.Impulse);
        
        GetComponent<CircleCollider2D>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        
        yield return new WaitForSeconds(1.5f);

        canvas.SendMessage("FadeOut");
        isDead = true;
    }

    //落下死ダメージ
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "DamegeFlore" && !isDead)
        {
            rb2D.AddForce(jumpPower * Vector2.up / 1.3f, ForceMode2D.Impulse);
            StartCoroutine("Dead");
            Sound.PlaySe("dead", 0);
            isDead = true;
        }

        if (collision.gameObject.tag == "Enemy"){
            animator.SetBool("isJump", true);
            rb2D.AddForce(jumpPower * Vector2.up / 1.3f, ForceMode2D.Impulse);
            Sound.PlaySe("enemyKill", 1);

        }
    }



    void OnTriggerEnter2D(Collider2D collision) //敵との判定
    {
        if (collision.gameObject.tag == "Enemy" && !isDead &&!isHummer)
        {
            
            Sound.PlaySe("damage",4);
            StartCoroutine("Dead");

        }

        if (collision.gameObject.tag == "Finish")
        {
            GameObject.Find("Goal").GetComponent<BoxCollider2D>().enabled=false;
            animator.SetBool("StageClear", true);
            canvas.SendMessage("DisplayTelop", "clear");
            Sound.PlaySe("Clear", 2);
            Sound.PlaySe("clearVoice", 5);
            StartCoroutine("FadeOut");
            

        }

    }
    IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(4.0f);

        canvas.SendMessage("FadeOut");
        isStageClear = true;
    }

}
