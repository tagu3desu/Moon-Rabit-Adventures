using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultManager : MonoBehaviour
{

    private GameObject imageObject;
    public GameObject textObject;
    private Text scoreText;
    private int LastScore;
    // Start is called before the first frame update
    void Start()
    {
        imageObject = GameObject.Find("Text");
        scoreText = textObject.GetComponent<Text>();
        scoreText.text =("Score:") + UIManager.g_score.ToString();
            
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
