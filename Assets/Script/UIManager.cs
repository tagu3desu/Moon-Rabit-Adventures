//using Microsoft.Unity.VisualStudio.Editor;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private RectTransform rectTransform;

    private GameObject imageObject;

    private Image imageComponent;

    public GameObject textObject;


    private Text scoreText;
    public static int g_score=0;

    private GameObject fadeObject;
    private RectTransform fadeRectT;



    // Start is called before the first frame update
    void Start()
    {
        imageObject = GameObject.Find("Image");
        imageComponent = imageObject.GetComponent<Image>();
        imageComponent.enabled = false;

        imageObject = GameObject.Find("Text");
        scoreText=textObject.GetComponent<Text>();
        scoreText.text = "00000";

        fadeObject = GameObject.Find("Fade");
        fadeRectT=fadeObject.GetComponent<RectTransform>();

        FadeIn();
    }

    void DisplayTelop(string telopName){
        imageComponent.enabled = true;
        //テクスチャを獲得
        Texture2D telopImage= Resources.Load(telopName) as Texture2D;

        //Sprite.Create(画像、画像の大きさ,回転の中心)
        imageComponent.sprite = Sprite.Create(telopImage, new Rect(0, 0, telopImage.width, telopImage.height), Vector2.zero);

        imageComponent.SetNativeSize();
    }

    void AddScore(int score)
    {
        g_score += score;
        scoreText.text=g_score.ToString("00000");
    }

    void FadeIn()
    {
        fadeRectT.DOScale(new Vector3(1, 0, 1), 1.5f).SetEase(Ease.InOutQuint);
    }


    void FadeOut()
    {
        fadeRectT.DOScale(new Vector3(1, 1, 1), 1.5f).SetEase(Ease.InOutQuint);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
