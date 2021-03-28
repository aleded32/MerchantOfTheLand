using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
public class startMenuNav : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject startFirstButton;
    public GameObject optionsButton, QuitButton;

     
    void Start()
    {
        //EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(startFirstButton);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {

        Debug.Log(EventSystem.current.currentSelectedGameObject);
    }

    public void startOnlclick() 
    {
        SceneManager.LoadScene(1);
    }

    public void backToStartOnlclick()
    {
        SceneManager.LoadScene(0);
    }
    public void optionsOnlclick()
    {
        SceneManager.LoadScene(2);
    }
    public void quitOnlclick()
    {
        Application.Quit();
    }




}
