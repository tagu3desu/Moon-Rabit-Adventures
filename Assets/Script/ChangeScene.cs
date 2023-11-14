using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Windows;

public class ChangeScene : MonoBehaviour
{
    private GameInput input;
    public GameObject cameraObject;
    // Start is called before the first frame update

    private StageSelect stageSelect;
    private GameObject stageSelectTarget;
    void Start()
    {
        cameraObject = GameObject.Find("Main Camera");
        input = Camera.main.gameObject.GetComponent<GameInput>();
        Sound.LoadSe("enterSound", "maou_se_system48");
        stageSelectTarget = GameObject.Find("StageselectTarget");
        stageSelect =stageSelectTarget.gameObject.GetComponent<StageSelect>();
        
    }

    // Update is called once per frame
    void Update()
    {
       
        if (SceneManager.GetActiveScene().name == "Result")
        {
            if (input.inputActions.UI.SceneDecided.ReadValue<float>() > 0.2f)
            {
                Sound.PlaySe("enterSound", 0);
                SceneManager.LoadScene("Title");
            }
        }
        if (SceneManager.GetActiveScene().name == "Title")
        {
            if (input.inputActions.UI.SceneDecided.ReadValue<float>() > 0.2f)
            {
                Sound.PlaySe("enterSound",0);
                SceneManager.LoadScene("StageSelect");
            }
        }
        if (SceneManager.GetActiveScene().name == "StageSelect")
        {
            if (input.inputActions.UI.SceneDecided.ReadValue<float>() > 0.2f && stageSelect.toStage1)
            {
                Sound.PlaySe("enterSound", 0);
                SceneManager.LoadScene("GameScene1");
            }
        }

        if (SceneManager.GetActiveScene().name == "StageSelect")
        {
            if (input.inputActions.UI.SceneDecided.ReadValue<float>() > 0.2f && stageSelect.toStage2)
            {
                Sound.PlaySe("enterSound", 0);
                SceneManager.LoadScene("GameScene2");
            }
        }

    }
}
