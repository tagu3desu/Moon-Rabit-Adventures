using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManagerpuroto: MonoBehaviour
{
    // Start is called before the first frame update
    //プレイヤーの状態取得
    private GameObject player;
    private PlayerControler playerControler;
    void Start()
    {
        player = GameObject.Find("Player");
        playerControler=player.GetComponent<PlayerControler>();
       
       
        if(SceneManager.GetActiveScene().name== "GameScene1")
        {
            Sound.LoadBgm("Stage1BGM", "時を刻む森");
            Sound.PlayBgm("Stage1BGM");
        }
        if (SceneManager.GetActiveScene().name == "GameScene2")
        {
            Sound.LoadBgm("Stage2BGM", "岩のダンジョン");
            Sound.PlayBgm("Stage2BGM");
        }

    }

    // Update is called once per frame
    void Update()
    {
        
        if (playerControler.isStageClear || playerControler.isDead)
        {
            Sound.StopBgm();
        }
        
    }
}
