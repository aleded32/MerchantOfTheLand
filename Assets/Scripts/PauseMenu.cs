using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject slider;
    bool isSelected;
    public PlayerController controller;

    void Start()
    {
        isSelected = false;
    }

    // Update is called once per frame
    void Update()
    {
        pauseMenuActive();
    }

    public void pauseMenuActive() 
    {
        if (gameObject.activeSelf == true && !isSelected) 
        {
            EventSystem.current.SetSelectedGameObject(slider);
            isSelected = true;
        }

        if (Gamepad.current.startButton.isPressed && controller.isPausePressed == false)
        {
            if (gameObject.activeSelf)
            {
                Time.timeScale = 1.0f;
                gameObject.SetActive(false);
               
            }
            controller.isPausePressed = true;
        }
        else if (!Gamepad.current.startButton.isPressed && controller.isPausePressed == true)
        {
            controller.isPausePressed = false;
        }


    }

    public void ExitOnClick() 
    {
        Time.timeScale = 1.0f;
        isSelected = false;
        SceneManager.LoadScene(0);
    }

    public void BackOnClick() 
    {
        isSelected = false;
        gameObject.SetActive(false);
    }

}
