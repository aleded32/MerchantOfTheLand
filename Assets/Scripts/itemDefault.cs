using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemDefault
{
    public enum itemType 
    {
        none,
        crops,
        plant
    }

    public itemType type;
    public int amount;
    public int buyValue, sellValue;
    public int wateringTime;
    public string name;
    public Sprite image;
    bool isSelected;


    public itemDefault(itemType _type, string _name, int _amount, int _buyValue, int _sellValue, int _wateringTime, Sprite _image, bool _isSelected) 
    {
        type = _type;
        name = _name;
        amount = _amount;
        buyValue = _buyValue;
        sellValue = _sellValue;
        wateringTime = _wateringTime;
        image = _image;
        isSelected = _isSelected;
    }

}
