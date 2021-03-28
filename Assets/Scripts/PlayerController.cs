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
    public Sprite[] equipSprites;
    public GameObject pauseMenu;
    public bool isPausePressed;
    bool isPressed;

    enum selectionTypes 
    {
        watering,
        hoeing,
        seeding,
        none
    }

    selectionTypes type;

    plotScraping scraping;
    plotSeeding seeding;
    PlantGrowing growing;
    public PlotGeneration Gen;
    public GameObject inventory;
    public SpriteRenderer equippedIcon;
    

    private void Start()
    {
        scraping = new plotScraping();
        seeding = new plotSeeding();
        type = selectionTypes.none;
        isPausePressed = false;
        isPressed = false;
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       
        growing = GameObject.FindWithTag("plot").GetComponent<PlantGrowing>();
        GamePadControls();

    }

    private void Update()
    {
        if (Gamepad.current.bButton.ReadValue() == 1)
        {
            if (inventory.active == true)
            {
                Time.timeScale = 1.0f;
                inventory.SetActive(false);

            }
            else if(inventory.active == false)
            {
                growing.pickupVeg();
            }

        }
        

        scraping.highlightPlot();
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

    public void stopAnim() 
    {
        anim.SetInteger("speed", 0);
    }

    Vector3 move(Vector3 direction) 
    {
        return direction * speed * Time.deltaTime;
    }

    
    void GamePadControls() 
    {
        if (Gamepad.current.yButton.ReadValue() == 0 && Gamepad.current.bButton.ReadValue() == 0 && inventory.active == false && !pauseMenu.activeSelf)
        {
            if (Gamepad.current.dpad.left.isPressed)
            {
                moveLeft();
            }
            else if (Gamepad.current.dpad.right.isPressed)
            {
                moveRight();
            }
            else if (Gamepad.current.dpad.up.isPressed)
            {
                moveUp();

            }
            else if (!Gamepad.current.dpad.IsPressed())
            {
                anim.SetInteger("speed", 0);
            }
            else if (Gamepad.current.dpad.down.isPressed)
            {
                moveDown();
            }

            selectionWheel.SetActive(false);
            scraping.isPlotted = false;


        }
        else if (Gamepad.current.yButton.ReadValue() == 1 && inventory.active == false && !pauseMenu.activeSelf)
        {
            if (inventory.active == false)
            {
                selectionWheel.SetActive(true);

                anim.SetInteger("speed", 0);



                if (Gamepad.current.dpad.ReadValue() == new Vector2(0, 1))
                {
                    selections[0].color = new Color32(168, 168, 168, 255);
                    type = selectionTypes.hoeing;


                }
                else if (Gamepad.current.dpad.ReadValue() == new Vector2(0, -1))
                {
                    selections[2].color = new Color32(168, 168, 168, 255);
                    type = selectionTypes.seeding;

                }
                else if (Gamepad.current.dpad.ReadValue() == new Vector2(-1, 0))
                {
                    selections[1].color = new Color32(168, 168, 168, 255);
                    type = selectionTypes.watering;


                }
                else if (Gamepad.current.dpad.ReadValue() == new Vector2(1,0))
                {
                    selections[3].color = new Color32(168, 168, 168, 255);
                    type = selectionTypes.none;
                }
                else
                {

                    foreach (Image image in selections)
                    {
                        image.color = new Color32(255, 255, 255, 255);
                    }


                }
            }
            

          
            
           

        }

        if (Gamepad.current.xButton.ReadValue() == 1 && selectionWheel.activeSelf == false && pauseMenu.activeSelf == false)
        {
            Time.timeScale = 0.0f;
            inventory.SetActive(true);

        }

        if (Gamepad.current.startButton.isPressed && isPausePressed == false)
        {
            if (pauseMenu.activeSelf == false)
            {
                Time.timeScale = 0.0f;
                pauseMenu.SetActive(true);
               
            }
            isPausePressed = true;
            

        }
        else if (!Gamepad.current.startButton.isPressed && isPausePressed == true) 
        {
            isPausePressed = false;
        }
        




        if (inventory.active == false)
        {
            if (type == selectionTypes.hoeing) 
            {
                scraping.noHighlight = false;
                equippedIcon.sprite = equipSprites[0];
                if (Gamepad.current.aButton.ReadValue() == 1 && !isPressed)
                {

                    scraping.hoeingPlot();
                    isPressed = true;
                }
                else if (Gamepad.current.aButton.ReadValue() == 0 && isPressed)
                    isPressed = false;

            }
            else if (type == selectionTypes.seeding)
            {
                scraping.noHighlight = false;
                equippedIcon.sprite = equipSprites[2];
                if (Gamepad.current.aButton.ReadValue() == 1)
                {
                   
                    seeding.plantingSeed();
                }

            }
            else if (type == selectionTypes.watering)
            {
                scraping.noHighlight = false;
                equippedIcon.sprite = equipSprites[1];
                if (Gamepad.current.aButton.ReadValue() == 1)
                {

                    growing.wateringPlant();
                }
                else 
                {
                    growing.isBeingPressed = false;
                }

            }
            else if (type == selectionTypes.none)
            {
                scraping.noHighlight = true;
                equippedIcon.sprite = equipSprites[3];
            }

            

        }
    }

    public void closeShop(GameObject shopUI) 
    {
        if (Gamepad.current.bButton.ReadValue() == 1 && shopUI.active == true) 
        {
            shopUI.SetActive(false);
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 0.5f, gameObject.transform.position.z);
            
        }
    }
}
