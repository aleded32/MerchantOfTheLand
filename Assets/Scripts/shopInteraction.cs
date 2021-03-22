using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class shopInteraction : MonoBehaviour
{

    public playerInventory inventory;
    public PlayerController controller;
    ItemDatabase database;

    public Text[] slotText;
    public Text[] amountText;
    //public Text selectedText;
    public Image arrow;
    public GameObject shopMenu;
    public GameObject selectedItem;

    bool isPressed = false;
    bool isPressedSelect = false;

    List<itemDefault> shopList;
    int currentSlot = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            shopMenu.SetActive(true);
            inventory.inventoryGameObject.SetActive(true);
            arrow.gameObject.SetActive(false);
            selectedItem.SetActive(false);
            controller.stopAnim();

           
        }
        
    }

    void Start()
    {
        database = new ItemDatabase(inventory.imageItem);
        shopList = new List<itemDefault>()
        {
            database.getItemDatabase()[0],
            database.getItemDatabase()[2],
            database.getItemDatabase()[4],
            database.getItemDatabase()[6],
        };
        setShopUI();
    }

    // Update is called once per frame
    void Update()
    {
        controller.closeShop(shopMenu);
        inventoryControls();
    }

    void setShopUI() 
    {
        for (int i = 0; i < shopList.Count; i++) 
        {
            slotText[i].text = shopList[i].name;
            amountText[i].text = shopList[i].buyValue.ToString() + "G";

        }
    }

    void inventoryControls()
    {
        if (shopMenu.active == true && inventory.inventoryControl == false)
        {
            if (Gamepad.current.dpad.ReadValue() == new Vector2(0, 1) && isPressed == false)
            {
                if (currentSlot > 0)
                {
                    do
                    {
                        currentSlot--;
                    }
                    while (shopList[currentSlot].name == "");

                    arrow.rectTransform.anchoredPosition = new Vector3(arrow.rectTransform.anchoredPosition.x, slotText[currentSlot].rectTransform.anchoredPosition.y);

                    isPressed = true;

                }

            }
            else if (Gamepad.current.dpad.ReadValue() == new Vector2(0, -1) && isPressed == false)
            {
                if (currentSlot >= 0 && currentSlot < shopList.FindLastIndex(x => x.name != ""))
                {

                    do
                    {
                        currentSlot++;
                    }
                    while (shopList[currentSlot].name == "");
                    arrow.rectTransform.anchoredPosition = new Vector3(arrow.rectTransform.anchoredPosition.x, slotText[currentSlot].rectTransform.anchoredPosition.y);

                    isPressed = true;
                }

            }
            else if (Gamepad.current.dpad.ReadValue() == new Vector2(0, 0))
            {
                isPressed = false;
            }

            if (Gamepad.current.aButton.ReadValue() == 1 && !isPressedSelect) 
            {
                if(inventory.money >= shopList[currentSlot].buyValue) 
                {
                    inventory.addToInventory(shopList[currentSlot].name);
                    inventory.money -= shopList[currentSlot].buyValue;
                    isPressedSelect = true;
                }
                
                
            } 
            else if (Gamepad.current.aButton.ReadValue() == 0)
                isPressedSelect = false;




        }



        if (Gamepad.current.dpad.ReadValue() == new Vector2(1, 0))
        {
            arrow.gameObject.SetActive(true);
        }
        else if (Gamepad.current.dpad.ReadValue() == new Vector2(-1, 0))
        {
            inventory.inventoryControl = true;
            arrow.gameObject.SetActive(false);
        }


    }

}
