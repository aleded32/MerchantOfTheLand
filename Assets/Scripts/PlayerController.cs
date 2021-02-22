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

    plotScraping scraping;
    plotSeeding seeding;
    PlantGrowing growing;
    public PlotGeneration Gen;

    private void Start()
    {
        scraping = new plotScraping();
        seeding = new plotSeeding();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        growing = GameObject.FindWithTag("plot").GetComponent<PlantGrowing>();
        GamePadControls();
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

    [System.Obsolete]
    void GamePadControls() 
    {
        if (Gamepad.current.aButton.ReadValue() == 0 && Gamepad.current.bButton.ReadValue() == 0)
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
            else if (Gamepad.current.dpad.ReadValue() == new Vector2(0, -1))
            {
                selections[2].color = new Color32(168, 168, 168, 255);
                seeding.plantingSeed();
            }
            else if (Gamepad.current.dpad.ReadValue() == new Vector2(-1, 0))
            {
                selections[1].color = new Color32(168, 168, 168, 255);
                growing.wateringPlant();

            }
            else
            {
                growing.isBeingPressed = false;
                foreach (Image image in selections)
                {
                    image.color = new Color32(255, 255, 255, 255);
                }


            }

        }
        else if (Gamepad.current.bButton.ReadValue() == 1) 
        {
            growing.pickupVeg();
        }

        scraping.highlightPlot();
    }
}
