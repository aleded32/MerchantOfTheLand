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
    bool isPressedAdd = false;
    bool isPressedSub = false;
    bool isPressed = false;
    int inventoryFillValue = 0;

    public Text[] slotText;
    public Text[] amountText;
    public Image arrow;
    public GameObject inventoryGameObject;

    int currentSlot = 0;


    void Start()
    {

        arrow.rectTransform.position = new Vector3(arrow.rectTransform.position.x, arrow.rectTransform.position.y, slotText[currentSlot].rectTransform.position.z);



        inventory = new List<itemDefault>()
        {
            new itemDefault(itemDefault.itemType.none, "", 0, 0,0,0, null),
            new itemDefault(itemDefault.itemType.none, "", 0, 0,0,0, null),
            new itemDefault(itemDefault.itemType.none, "", 0, 0,0,0, null),
            new itemDefault(itemDefault.itemType.none, "", 0, 0,0,0, null),
            new itemDefault(itemDefault.itemType.none, "", 0, 0,0,0, null),
            new itemDefault(itemDefault.itemType.none, "", 0, 0,0,0, null),
            new itemDefault(itemDefault.itemType.none, "", 0, 0,0,0, null),
            new itemDefault(itemDefault.itemType.none, "", 0, 0,0,0, null),
            new itemDefault(itemDefault.itemType.none, "", 0, 0,0,0, null),
            new itemDefault(itemDefault.itemType.none, "", 0, 0,0,0, null),

        };
        itemDatabase = new ItemDatabase(imageItem);

        addToInventory("Corn Seed");
        addToInventory("Corn");

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(currentSlot);



        inventoryControls();




        //TESTING PURPOSE
        if (Gamepad.current.xButton.ReadValue() == 1 && isPressedAdd == false)
        {
            addToInventory("Corn Seed");

        }



        if (Gamepad.current.rightShoulder.ReadValue() == 1 && isPressedAdd == false)
        {
            addToInventory("Corn");

        }

        if (Gamepad.current.xButton.ReadValue() == 0 && Gamepad.current.rightShoulder.ReadValue() == 0)
        {
            isPressedAdd = false;

        }


        if (Gamepad.current.yButton.ReadValue() == 1 && isPressedSub == false)
        {
            subtractFromInventory("Corn Seed");
        }

        else if (Gamepad.current.yButton.ReadValue() == 0)
        {

            isPressedSub = false;

        }
        //end of testing

        Debug.Log("amount " + inventory[0].amount + " name " + inventory[0].name);
        Debug.Log(inventoryFillValue);
    }

    void addToInventory(string itemName)
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

        isPressedAdd = true;

    }

    void subtractFromInventory(string itemName)
    {
        if (inventoryGameObject.active == true)
        {
            if (inventory[currentSlot].name != "")
            {
                if (inventory.Find(x => x.name == inventory[currentSlot].name).amount > 0)
                {
                    inventory.Find(x => x.name == inventory[currentSlot].name).amount--;
                    amountText[currentSlot].text = inventory[currentSlot].amount.ToString();

                }
                if (inventory.Find(x => x.name == inventory[currentSlot].name).amount <= 0)
                {
                    inventory[currentSlot] = new itemDefault(itemDefault.itemType.none, "", 0, 0, 0, 0, null);
                    amountText[currentSlot].text = "";
                    slotText[currentSlot].text = inventory[currentSlot].name;
                    inventoryFillValue--;
                }

            }
        }
        else
        {
            if (inventory.Find(x => x.name == itemName) != null)
            {
                if (inventory.Find(x => x.name == itemName).amount > 0)
                {
                    inventory.Find(x => x.name == itemName).amount--;
                    amountText[currentSlot].text = inventory[currentSlot].amount.ToString();

                }
                if (inventory.Find(x => x.name == itemName).amount <= 0)
                {
                    inventory[inventory.FindIndex(x => x.name == itemName)] = new itemDefault(itemDefault.itemType.none, "", 0, 0, 0, 0, null);
                    amountText[inventory.FindIndex(x => x.name == itemName)].text = "";
                    slotText[inventory.FindIndex(x => x.name == itemName)].text = inventory[currentSlot].name;
                    inventoryFillValue--;
                }

            }
        }


        isPressedSub = true;
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
