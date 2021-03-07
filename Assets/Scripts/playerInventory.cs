using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.UI;

public class playerInventory : MonoBehaviour
{
    ItemDatabase itemDatabase;
    public Sprite[] imageItem;
    public List<itemDefault> inventory;
    itemDefault selected;
    bool isPressedSelect = false;
    bool isPressedDeselect = false;
    bool isPressed = false;
    int inventoryFillValue = 0;

    public Text[] slotText;
    public Text[] amountText;
    public Text selectedText;
    public Image arrow;
    public GameObject inventoryGameObject;

    int currentSlot = 0;


    void Start()
    {

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

        addToInventory("Strawberry Seed");
        addToInventory("Strawberry Seed");
        addToInventory("Strawberry Seed");
        addToInventory("Corn Seed");
        addToInventory("Corn Seed");
        addToInventory("Corn Seed");
        addToInventory("Corn Seed");
        addToInventory("Carrot Seed");
        addToInventory("Carrot Seed");
        addToInventory("Cabbage Seed");
        addToInventory("Cabbage Seed");
        addToInventory("Cabbage Seed");


    }

    void updateSelectedText() 
    {
        selectedText.text = selected.name;
    }

    // Update is called once per frame
    void Update()
    {


        updateSelectedText();
        inventoryControls();




        //TESTING PURPOSE
      

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
        if (inventoryGameObject.active == true)
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



    void inventoryControls()
    {
        if (inventoryGameObject.active == true)
        {
            if (Gamepad.current.dpad.ReadValue() == new Vector2(0, 1) && isPressed == false)
            {
                if (currentSlot > 0)
                {

                    currentSlot--;
                    arrow.rectTransform.anchoredPosition = new Vector3(arrow.rectTransform.anchoredPosition.x, slotText[currentSlot].rectTransform.anchoredPosition.y);
                  
                    isPressed = true;
                }

            }
            else if (Gamepad.current.dpad.ReadValue() == new Vector2(0, -1) && isPressed == false)
            {
                if (inventory[currentSlot].name != "" && currentSlot >= 0 && currentSlot < inventoryFillValue - 1)
                {

                    currentSlot++;
                    arrow.rectTransform.anchoredPosition = new Vector3(arrow.rectTransform.anchoredPosition.x, slotText[currentSlot].rectTransform.anchoredPosition.y);
                    
                    isPressed = true;
                }

            }
            else if (Gamepad.current.dpad.ReadValue() == new Vector2(0, 0))
            {
                isPressed = false;
            }

            if (Gamepad.current.aButton.isPressed && !isPressedSelect)
                subtractFromInventory(false);
            else
                isPressedSelect = false;

            if (Gamepad.current.xButton.isPressed && !isPressedDeselect)
            {
                selected = new itemDefault(itemDefault.itemType.none, "", 0, 0, 0, 0, null, false);
                isPressedSelect = true;
            }
            else
                isPressedSelect = false;


        }

        if (inventory[currentSlot].name == "" && currentSlot > 0)
        {

            currentSlot--;
            arrow.rectTransform.anchoredPosition = new Vector3(arrow.rectTransform.anchoredPosition.x, slotText[currentSlot].rectTransform.anchoredPosition.y);

        }
        else if (inventory[currentSlot].name == "" && currentSlot <= 0)
        {
            currentSlot++;
            arrow.rectTransform.anchoredPosition = new Vector3(arrow.rectTransform.anchoredPosition.x, slotText[currentSlot].rectTransform.anchoredPosition.y);
        }

        if (inventoryFillValue <= 0)
        {
            arrow.color = new Color32(0, 0, 0, 0);
        }
        else if (inventoryFillValue > 0)
        {
            arrow.color = new Color32(255, 255, 255, 255);
            

        }

        
    }

    
}
