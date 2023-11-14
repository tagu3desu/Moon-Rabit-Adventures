using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    [Header("Controls")]
    public InputActions inputActions;  //入力の制御クラスのインスタンス
    Vector2 direction;


    // Start is called before the first frame update
    void Awake()
    {
       inputActions=new InputActions();
       inputActions.Player.Enable();
        inputActions.UI.Enable();
    }

    void Update()
    {
        direction = inputActions.Player.Direction.ReadValue<Vector2>();
        if ((Mathf.Abs(direction.x) > 0.2f || Mathf.Abs(direction.y) > 0.2f))
        {

        }
    }
    private void Jump(InputAction.CallbackContext context)
    {
        if (context.started)
        {

        }
    }
    private void JumpCancel(InputAction.CallbackContext context)
    {
        if (context.started)
        {

        }
    }
}
