using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantGrowing : MonoBehaviour
{
    PlotGeneration Gen;
    playerInventory inventory;
    float timer;
    public GameObject waterWarning;
    public int[] intervalWateringCorn;
    int waterWarningCount;
    GameObject plotPos;
    public bool isBeingPressed;
    bool waterShown;
    bool waterSignInactive;

    

    void Start()
    {
        inventory = GameObject.FindWithTag("Player").GetComponent<playerInventory>();
        Gen = GameObject.FindWithTag("grid").GetComponent<PlotGeneration>();
        plotPos = GameObject.FindWithTag("Player");
        waterWarningCount = 0;
        waterWarning.SetActive(false);
        isBeingPressed = false;
        waterShown = false;
        waterSignInactive = true;
    }

    void Update()
    {

        
            if (gameObject.GetComponent<CheckIsScraped>().isPlanted == true && gameObject.GetComponent<CheckIsScraped>().isFinishedGrowing == false)
            {
                timer += Time.deltaTime;
                needsWatering(itemTimeInterval());
            }
            else
            {
                waterWarning.SetActive(false);
                timer = 0;
            }
            

        

         
    }

    int itemTimeInterval() 
    {
       
        if (gameObject.GetComponent<CheckIsScraped>().nameOfVeg == "Corn") { return intervalWateringCorn[0]; }
        else if (gameObject.GetComponent<CheckIsScraped>().nameOfVeg == "Cabbage") { return intervalWateringCorn[1]; }
        else if (gameObject.GetComponent<CheckIsScraped>().nameOfVeg == "Carrot") { return intervalWateringCorn[2]; }
        else if (gameObject.GetComponent<CheckIsScraped>().nameOfVeg == "Strawberry") { return intervalWateringCorn[3]; }
        else { return 0; }
    }

    public int itemSpriteChange(int i)
    {
        if (Gen.plots[i].GetComponent<CheckIsScraped>().nameOfVeg == "Corn") { return 2; }
        else if (Gen.plots[i].GetComponent<CheckIsScraped>().nameOfVeg == "Cabbage") { return 6; }
        else if (Gen.plots[i].GetComponent<CheckIsScraped>().nameOfVeg == "Carrot") { return 10; }
        else if (Gen.plots[i].GetComponent<CheckIsScraped>().nameOfVeg == "Strawberry") { return 14; }
        else { return 0; }
    }

    public void wateringPlant() 
    {
        for (int i = 0; i < Gen.plots.Count; i++)
        {
           
            if (Gen.gridpos(Gen.plots[i].transform.position.x, Gen.plots[i].transform.position.y) == Gen.gridpos(plotPos.transform.position.x, plotPos.transform.position.y))
            {
                if (Gen.plots[i].GetComponent<CheckIsScraped>().isPlanted == true)
                {

                    if (isBeingPressed == false && Gen.plots[i].GetComponent<CheckIsScraped>().isWatered == true)
                    {
                            Gen.plots[i].GetComponent<CheckIsScraped>().wateredCount++;

                        if (Gen.plots[i].GetComponent<CheckIsScraped>().wateredCount > 3)
                        {
                            Gen.plots[i].GetComponent<CheckIsScraped>().isWatered = false;
                        }
                        else if(Gen.plots[i].GetComponent<CheckIsScraped>().wateredCount < 4 && Gen.plots[i].transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color == new Color32(255, 255, 255, 112))
                        {
                            Gen.plots[i].GetComponent<SpriteRenderer>().sprite = Gen.plotTiles[itemSpriteChange(i) + Gen.plots[i].GetComponent<CheckIsScraped>().wateredCount];
                        }

                        Gen.plots[i].transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = new Color32(0, 0, 0, 0);
                       

                        isBeingPressed = true;

                    }

                    else if (Gen.plots[i].GetComponent<CheckIsScraped>().isWatered == false && Gen.plots[i].GetComponent<CheckIsScraped>().isFinishedGrowing != true)
                    {
                        Gen.plots[i].GetComponent<SpriteRenderer>().sprite = Gen.plotTiles[1];
                        Gen.plots[i].GetComponent<CheckIsScraped>().wateredCount = 0;
                        Gen.plots[i].GetComponent<CheckIsScraped>().needsWater = false;
                        Gen.plots[i].GetComponent<CheckIsScraped>().isFinishedGrowing = false;
                        Gen.plots[i].GetComponent<CheckIsScraped>().isPlanted = false;
                        waterWarningCount = 0;
                    }

                    
                }       
                   
 

            }
        }
        

    }


    void needsWatering(int intervalWateringCorn) 
    {

        if (timer > 0 && timer >= intervalWateringCorn && timer < intervalWateringCorn + 5)
        {
            gameObject.GetComponent<CheckIsScraped>().needsWater = true;
            if (waterShown == false)
            {
                
                waterWarningCount++;
            }

            gameObject.GetComponent<CheckIsScraped>().isWatered = true;
            waterShown = true;

            waterWarning.SetActive(true);
            
        }
        else if (timer > 0 && timer > intervalWateringCorn + 5)
        {


            if (gameObject.GetComponent<CheckIsScraped>().wateredCount != waterWarningCount)
            {

                gameObject.GetComponent<SpriteRenderer>().sprite = Gen.plotTiles[1];
                gameObject.GetComponent<CheckIsScraped>().isPlanted = false;
                gameObject.GetComponent<CheckIsScraped>().needsWater = false;
                gameObject.GetComponent<CheckIsScraped>().isFinishedGrowing = false;
                gameObject.GetComponent<CheckIsScraped>().wateredCount = 0;
                waterWarningCount = 0;
            }
           

            
            waterWarning.SetActive(false);
            gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 112);
            waterShown = false;
            timer = 0;
        }
        
        if (gameObject.GetComponent<CheckIsScraped>().wateredCount > 2 && gameObject.GetComponent<CheckIsScraped>().wateredCount == waterWarningCount)
        {
            gameObject.GetComponent<CheckIsScraped>().isFinishedGrowing = true;
            waterWarningCount = 0;
        }

        if (timer < intervalWateringCorn) 
        {
           
            gameObject.GetComponent<CheckIsScraped>().isWatered = false;
        }
    }

    public void pickupVeg()
    {
        
        for (int i = 0; i < Gen.plots.Count; i++)
        {

            if (Gen.gridpos(Gen.plots[i].transform.position.x, Gen.plots[i].transform.position.y) == Gen.gridpos(plotPos.transform.position.x, plotPos.transform.position.y))
            {
                
                if (Gen.plots[i].GetComponent<CheckIsScraped>().isFinishedGrowing == true)
                {
                    inventory.addToInventory(Gen.plots[i].GetComponent<CheckIsScraped>().nameOfVeg);
                    Gen.plots[i].GetComponent<SpriteRenderer>().sprite = Gen.plotTiles[1];
                    Gen.plots[i].GetComponent<CheckIsScraped>().isPlanted = false;
                    Gen.plots[i].GetComponent<CheckIsScraped>().needsWater = false;
                    Gen.plots[i].GetComponent<CheckIsScraped>().isFinishedGrowing = false;
                    Gen.plots[i].GetComponent<CheckIsScraped>().wateredCount = 0;
                }
            }
        }
    }

}
