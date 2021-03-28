using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.UI;

public class playerInventory : MonoBehaviour
{
    [HideInInspector]
    public ItemDatabase itemDatabase;
    [HideInInspector]
    public Sprite[] imageItem;
    [HideInInspector]
    public List<itemDefault> inventory;
    itemDefault selected;
    bool isPressedSelect = false;
    bool isPressed = false;
    [HideInInspector]
    public bool inventoryControl = true;

    [HideInInspector]
    public shopInteraction shopInter;

    public GameObject pauseMenu;
    int inventoryFillValue = 0;

    [HideInInspector]
    public int money;
    [HideInInspector]
    public Text moneyText;

    [HideInInspector]
    public Text[] slotText;
    [HideInInspector]
    public Text[] amountText;
    [HideInInspector]
    public Text selectedText;
    [HideInInspector]
    public Image arrow;
    [HideInInspector]
    public GameObject inventoryGameObject;

    public Text sellValueText;

    int currentSlot = 0;


    void Start()
    {
        money = 14;
        arrow.rectTransform.position = new Vector3(arrow.rectTransform.position.x, arrow.rectTransform.position.y, slotText[currentSlot].rectTransform.position.z);

        selected = new itemDefault(itemDefault.itemType.none, "", 0, 0, 0, 0, null, false);

        inventory = new List<itemDefault>()
        {
            new itemDefault(itemDefault.itemType.none, "", 0, 0,0,0, null, false),
            new itemDefault(itemDefault.itemType.none, "", 0, 0,0,0, null, false),
            new itemDefault(itemDefault.itemType.none, "", 0, 0,0,0, null, false),
            new itemDefault(itemDefault.itemType.none, "", 0, 0,0,0, null, false),
            new itemDefault(itemDefault.itemType.none, "", 0, 0,0,0, null, false),
            new itemDefault(itemDefault.itemType.none, "", 0, 0,0,0, null, false),
            new itemDefault(itemDefault.itemType.none, "", 0, 0,0,0, null, false),
            new itemDefault(itemDefault.itemType.none, "", 0, 0,0,0, null, false),
            new itemDefault(itemDefault.itemType.none, "", 0, 0,0,0, null, false),
            new itemDefault(itemDefault.itemType.none, "", 0, 0,0,0, null, false),

        };
        itemDatabase = new ItemDatabase(imageItem);



    }

    public int setCurrentSlot(int newCurrentSlot) 
    {
        currentSlot = newCurrentSlot;
        return currentSlot;
    }

    void updateSelectedText() 
    {
        selectedText.text = selected.name;

        moneyText.text = money.ToString() + "G";
        sellValueText.text = inventory[currentSlot].sellValue.ToString() + "G";

    }

    // Update is called once per frame
    void Update()
    {


        updateSelectedText();

        if(!pauseMenu.activeSelf)
            inventoryControls();


        


    }

    public void addToInventory(string itemName)
    {

        if (inventory.Exists(x => x.name == itemName))
        {

            inventory.Find(x => x.name == itemName).amount++;
            amountText[inventory.FindIndex(x => x.name == itemName)].text = inventory[inventory.FindIndex(x => x.name == itemName)].amount.ToString();


        }

        else if (inventory.Exists(x => x.name != itemName))
        {

            for (int i = 0; i < inventory.Count; i++)
            {
                if (inventory[i].name == "")
                {

                    inventory[i] = itemDatabase.getItemDatabase().Find(x => x.name == itemName);
                    inventory.Find(x => x.name == itemName).amount++;
                    amountText[i].text = inventory[i].amount.ToString();
                    slotText[i].text = inventory[i].name;
                    inventoryFillValue++;
                    break;
                }
            }
        }

       

    }

    public itemDefault getSelected() 
    {
        return selected;
    }

    public int getCurrentSlot()
    {
        return currentSlot;
    }

    

    public string itemNameToBeGround()
    {
        if (getSelected().name == "Corn Seed") { return "Corn"; }
        else if (getSelected().name == "Cabbage Seed") { return "Cabbage"; }
        else if (getSelected().name == "Carrot Seed") { return "Carrot"; }
        else if (getSelected().name == "Strawberry Seed") { return "Strawberry"; }
        else { return ""; }
    }

    public void displayCurrentAmount() 
    {
        if (inventory.Find(x => x.name == inventory[currentSlot].name).amount > 0)
        {
            inventory.Find(x => x.name == inventory[currentSlot].name).amount--;
            amountText[currentSlot].text = inventory[currentSlot].amount.ToString();

        }
    }

    public void subtractFromInventory(bool isPlanted)
    {
        if (inventoryGameObject.active == true && isPlanted == false)
        {
            if (inventory[currentSlot].name != "" || selected.name == "")
            {
                selected = inventory[currentSlot];
            }
        }
        if(isPlanted == true) 
        {
            if (inventory.Find(x => x.name == inventory[currentSlot].name).amount <= 0)
            {
                inventory[currentSlot] = new itemDefault(itemDefault.itemType.none, "", 0, 0, 0, 0, null, false);
                amountText[currentSlot].text = "";
                slotText[currentSlot].text = inventory[currentSlot].name;
                inventoryFillValue--;
                selected = inventory[currentSlot];
                

            }
        }





        isPressedSelect = true;
    }

    void checkEmptyInventory()
    {
        if (inventory[currentSlot].name == "" && inventoryFillValue > 0)
        {
            do
            {
                
                if (currentSlot >= inventory.FindLastIndex(x => x.name != "") || inventory.FindLastIndex(x => x.name != "") <= 0)
                {
                    currentSlot = 0;

                    arrow.rectTransform.anchoredPosition = new Vector3(arrow.rectTransform.anchoredPosition.x, slotText[0].rectTransform.anchoredPosition.y);
                    break;
                }
                currentSlot++;
            }
            while (inventory[currentSlot].name == "");
        }

        if (inventoryFillValue <= 0)
        {
            arrow.color = new Color32(0, 0, 0, 0);
            currentSlot = 0;
            arrow.rectTransform.position = new Vector3(arrow.rectTransform.position.x, arrow.rectTransform.position.y, slotText[currentSlot].rectTransform.position.z);
        }
        else if (inventoryFillValue > 0 && inventoryControl == true)
        {
            arrow.color = new Color32(255, 255, 255, 255);


        }
    }


    void inventoryControls()
    {

        if (shopInter.shopMenu.active == false)
        {
            inventoryControl = true;
            shopInter.selectedItem.SetActive(true);
        }


        if (inventoryGameObject.active == true && inventoryControl == true)
        {

            checkEmptyInventory();

            arrow.rectTransform.anchoredPosition = new Vector3(arrow.rectTransform.anchoredPosition.x, slotText[currentSlot].rectTransform.anchoredPosition.y);



            if (Gamepad.current.dpad.ReadValue() == new Vector2(0, 1) && isPressed == false)
            {
                if (currentSlot > 0)
                {
                    do
                    {
                        currentSlot--;
                    }
                    while (inventory[currentSlot].name == "");

                    arrow.rectTransform.anchoredPosition = new Vector3(arrow.rectTransform.anchoredPosition.x, slotText[currentSlot].rectTransform.anchoredPosition.y);

                    isPressed = true;

                }

            }
            else if (Gamepad.current.dpad.ReadValue() == new Vector2(0, -1) && isPressed == false)
            {
                if (currentSlot >= 0 && currentSlot < inventory.FindLastIndex(x => x.name != ""))
                {

                    do
                    {
                        currentSlot++;
                    }
                    while (inventory[currentSlot].name == "");
                    arrow.rectTransform.anchoredPosition = new Vector3(arrow.rectTransform.anchoredPosition.x, slotText[currentSlot].rectTransform.anchoredPosition.y);

                    isPressed = true;
                }

            }
            else if (Gamepad.current.dpad.ReadValue() == new Vector2(0, 0))
            {

                isPressed = false;
            }
            else if (Gamepad.current.dpad.ReadValue() == new Vector2(1, 0) && shopInter.shopMenu.active == true)
            {
                arrow.color = new Color32(0, 0, 0, 0);
                inventoryControl = false;

            }
            else if (Gamepad.current.dpad.ReadValue() == new Vector2(-1, 0) && shopInter.shopMenu.active == true)
            {
                arrow.color = new Color32(255, 255, 255, 255);
            }


            if (Gamepad.current.aButton.ReadValue() == 1 && !isPressedSelect)
            {
                if (shopInter.shopMenu.active == true)
                {

                    if (inventory[currentSlot].amount > 0)
                    {
                        displayCurrentAmount();

                    }
                    money += inventory[currentSlot].sellValue;

                    subtractFromInventory(true);

                }
                else
                {

                    subtractFromInventory(false);
                }

            }
            else if (Gamepad.current.aButton.ReadValue() == 0)
                isPressedSelect = false;




        }

    }




        

        




}
