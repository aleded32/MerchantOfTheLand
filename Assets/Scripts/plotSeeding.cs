using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plotSeeding
{
    PlotGeneration Gen;
    public bool isPlotted = false;
    GameObject plotPos;
    playerInventory inventory;
   


    public plotSeeding() 
    {
        Gen = GameObject.FindWithTag("grid").GetComponent<PlotGeneration>();
        plotPos = GameObject.FindWithTag("Player");
        inventory = GameObject.FindWithTag("Player").GetComponent<playerInventory>();
        
    }

    int itemSpriteChange(int i )
    {
        if (Gen.plots[i].GetComponent<CheckIsScraped>().nameOfVeg == "Corn") { return 2; }
        else if (Gen.plots[i].GetComponent<CheckIsScraped>().nameOfVeg == "Cabbage") { return 6; }
        else if (Gen.plots[i].GetComponent<CheckIsScraped>().nameOfVeg == "Carrot") { return 10; }
        else if (Gen.plots[i].GetComponent<CheckIsScraped>().nameOfVeg == "Strawberry") { return 14; }
        else { return 0; }
    }

    public void plantingSeed()
    {
       


            for (int i = 0; i < Gen.plots.Count; i++)
            {

                if (inventory.getSelected().name != "" && inventory.getSelected().type == itemDefault.itemType.crops)
                {
                    if (Gen.gridpos(Gen.plots[i].transform.position.x, Gen.plots[i].transform.position.y) == Gen.gridpos(plotPos.transform.position.x, plotPos.transform.position.y))
                    {
                        if (Gen.plots[i].GetComponent<CheckIsScraped>().isScraped == true)
                        {


                            if (Gen.plots[i].GetComponent<CheckIsScraped>().isPlanted == false)
                            {
               
                                inventory.displayCurrentAmount();
                                Debug.Log(inventory.inventory[inventory.getCurrentSlot()].amount);
                                Gen.plots[i].GetComponent<CheckIsScraped>().nameOfVeg = inventory.itemNameToBeGround();
                                Gen.plots[i].GetComponent<SpriteRenderer>().sprite = Gen.plotTiles[itemSpriteChange(i)];
                                
                                Gen.plots[i].GetComponent<CheckIsScraped>().needsWater = true;
                                Gen.plots[i].GetComponent<CheckIsScraped>().isPlanted = true;

                            }
                            inventory.subtractFromInventory(Gen.plots[i].GetComponent<CheckIsScraped>().isPlanted = true);
                        }
                   
                    }
                }
            }
                
            
    }

}
