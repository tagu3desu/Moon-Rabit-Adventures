using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelect : MonoBehaviour
{
    public float normalSpeed = 0.0f;
    public GameObject cameraObject;

    Vector2 direction;
    private Rigidbody2D rb2D;
    private GameInput input;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private GameObject canvas;

    private GameObject stagePin1;
    private GameObject stagePin2;

    public bool toStage1 = false;
    public bool toStage2 = false;

   
    // Start is called before the first frame update
    void Start()
    {
        input = Camera.main.GetComponent<GameInput>();
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        cameraObject = GameObject.Find("Main Camera");
        canvas = GameObject.Find("Canvas");
        stagePin1 = GameObject.Find("StagePin1");
        stagePin2 = GameObject.Find("StagePin2");
        stagePin1.SetActive(false);
        stagePin2.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        direction = input.inputActions.Player.Direction.ReadValue<Vector2>();

        if (Mathf.Abs(direction.x) > 0.3f)
        {
            normalSpeed = 15.0f;
            rb2D.AddForce(new Vector2(direction.x * normalSpeed/10, 0f));
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="Stage1")
        {
            toStage1 = true;
            stagePin1.SetActive(true);
           
        }
        else
        {
            stagePin1.SetActive(false);
            toStage1 = false;
        }
        if(collision.gameObject.tag=="Stage2")
        {
            toStage2 = true;
            stagePin2.SetActive(true);
           
        }
        else
        {
            stagePin2.SetActive(false);
            toStage2= false;
        }
    }
}
