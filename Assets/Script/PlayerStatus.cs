using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    [Header("ステータス")]
    [Tooltip("HP")]
    public float hp = 10.0f;

    [Tooltip("攻撃力")]
    public float atk = 1.0f;
}
