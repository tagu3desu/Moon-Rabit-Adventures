using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundingDetermination : MonoBehaviour
{
    //�ڒn����X�N���v�g
    [Tooltip("�W�����v������")]
    public int jumpCount = 0;
    [Tooltip("�ڒn����")]
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
