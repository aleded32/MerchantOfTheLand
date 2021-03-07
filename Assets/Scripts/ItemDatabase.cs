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
             new itemDefault(itemDefault.itemType.crops, "Corn Seed", 0, 2,1,40, images[0], false),
             new itemDefault(itemDefault.itemType.plant, "Corn", 0, 0,4,0, images[1],  false),

             new itemDefault(itemDefault.itemType.crops, "Cabbage Seed", 0, 4,2,30, images[2], false),
             new itemDefault(itemDefault.itemType.plant, "Cabbage", 0, 0,6,0, images[3],  false),

             new itemDefault(itemDefault.itemType.crops, "Carrot Seed", 0, 6,3,20, images[4], false),
             new itemDefault(itemDefault.itemType.plant, "Carrot", 0, 0,10,0, images[5],  false),

             new itemDefault(itemDefault.itemType.crops, "Strawberry Seed", 0, 8,4,10, images[6], false),
             new itemDefault(itemDefault.itemType.plant, "Strawberry", 0, 0,12,0, images[7],  false),
        };



    }

    public List<itemDefault> getItemDatabase() 
    {
        return database;
    }





}
