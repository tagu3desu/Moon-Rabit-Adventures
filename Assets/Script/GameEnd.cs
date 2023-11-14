using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class GameEnd : MonoBehaviour
{
    public GameObject cameraObject;
    private GameInput input;
    // Start is called before the first frame update
    void Start()
    {
        input = Camera.main.GetComponent<GameInput>();
        cameraObject = GameObject.Find("Main Camera");
    }

    // Update is called once per frame
    void Update()
    {
        if(input.inputActions.UI.GameEnd.ReadValue<float>()> 0.2f)
        {
            EndGame();
        }
    }
    private void EndGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;//ゲームプレイ終了
#else
    Application.Quit();//ゲームプレイ終了
#endif
    }
}
