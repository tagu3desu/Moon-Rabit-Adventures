using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Windows;

public class GameManager : MonoBehaviour
{
    GameObject player;
    
    PlayerControler playerControler;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        playerControler = player.GetComponent<PlayerControler>();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if (playerControler.isStageClear)
            {
                SceneManager.LoadScene("Result");
        }
        if(playerControler.isDead)
        {
            Invoke("DeadAfter", 1.5f);
        }
       
    }

    private void DeadAfter()
    {
        SceneManager.LoadScene("Result");
    }
}
