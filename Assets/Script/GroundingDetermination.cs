using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundingDetermination : MonoBehaviour
{
    //接地判定スクリプト
    [Tooltip("ジャンプした回数")]
    public int jumpCount = 0;
    [Tooltip("接地判定")]
    private bool isGround;
    private bool isGroundEnter, isGroundStay, isGroundExit;



    public bool IsGround()
    {
        if (isGroundEnter || isGroundStay)
        {
            isGround = true;
        }
        else if (isGroundExit)
        {
            isGround = false;
        }

        isGroundEnter = false;
        isGroundStay = false;
        isGroundExit = false;
        return isGround;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGroundEnter = true;
            jumpCount = 0;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGroundStay = true;
            jumpCount = 0;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGroundExit = true;
        }
    }
}
