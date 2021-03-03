using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase
{

    List<itemDefault> database;


    public ItemDatabase(Sprite[] images)
    {
        
        database = new List<itemDefault>()
        {
             new itemDefault(itemDefault.itemType.crops, "Corn Seed", 0, 2,1,50, images[0]),
             new itemDefault(itemDefault.itemType.plant, "Corn", 0, 0,4,0, images[1]),
        };



    }

    public List<itemDefault> getItemDatabase() 
    {
        return database;
    }





}
