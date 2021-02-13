using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    int speed = 5;
    public Animator anim;
    public GameObject selectionWheel;
    public Image[] selections;

    public plotScraping scraping;

    private void Update()
    { 
           
       

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Gamepad.current.aButton.ReadValue() == 0)
        {
            if (Gamepad.current.leftStick.left.isPressed)
            {
                moveLeft();
            }
            else if (Gamepad.current.leftStick.right.isPressed)
            {
                moveRight();
            }
            else if (Gamepad.current.leftStick.up.isPressed)
            {
                moveUp();

            }
            else if (!Gamepad.current.leftStick.IsPressed())
            {
                anim.SetInteger("speed", 0);
            }
            else if (Gamepad.current.leftStick.down.isPressed)
            {
                moveDown();
            }

            selectionWheel.SetActive(false);
            scraping.isPlotted = false;

        }
        else if (Gamepad.current.aButton.ReadValue() == 1) 
        {
            selectionWheel.SetActive(true);
            anim.SetInteger("speed", 0);


            if (Gamepad.current.dpad.ReadValue() == new Vector2(0, 1))
            {
                selections[0].color = new Color32(168, 168, 168, 255);
                scraping.hoeingPlot();
            }
            else 
            {
                selections[0].color = new Color32(255, 255, 255, 255);
            }
            
        }

    }

    void moveLeft()
    {
        GetComponent<SpriteRenderer>().flipX = false;
        anim.SetBool("isUp", false);
        anim.SetBool("isDown", false);
        anim.SetBool("isLeft", true);
        anim.SetInteger("speed", 1);
        transform.Translate(move(Vector3.left));
    }

    void moveUp()
    {
        anim.SetBool("isDown", false);
        anim.SetBool("isLeft", false);
        anim.SetBool("isUp", true);
        anim.SetInteger("speed", 1);
        transform.Translate(move(Vector3.up));
    }

    void moveDown()
    {
        anim.SetBool("isUp", false);
        anim.SetBool("isLeft", false);
        anim.SetBool("isDown", true);
        anim.SetInteger("speed", 1);
        transform.Translate(move(Vector3.down));
    }

    void moveRight()
    {
        GetComponent<SpriteRenderer>().flipX = true;
        anim.SetBool("isUp", false);
        anim.SetBool("isDown", false);
        anim.SetBool("isLeft", true);
        anim.SetInteger("speed", 1);
        transform.Translate(move(Vector3.right));
    }

    Vector3 move(Vector3 direction) 
    {
        return direction * speed * Time.deltaTime;
    }
}
