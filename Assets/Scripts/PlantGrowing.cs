using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantGrowing : MonoBehaviour
{
    PlotGeneration Gen;
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
            Debug.Log(waterWarningCount);
            needsWatering(intervalWateringCorn, 0);
        }
        else 
        {
            waterWarning.SetActive(false);
            timer = 0;
        }
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
                            Gen.plots[i].GetComponent<SpriteRenderer>().sprite = Gen.plotTiles[Gen.plots[i].GetComponent<CheckIsScraped>().wateredCount + 2];
                        }

                        Gen.plots[i].transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = new Color32(0, 0, 0, 0);
                       

                        isBeingPressed = true;

                    }

                    else if (Gen.plots[i].GetComponent<CheckIsScraped>().isWatered == false)
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


    void needsWatering(int[] intervalWateringCorn, int i) 
    {

        if (timer > 0 && timer >= intervalWateringCorn[i] && timer < intervalWateringCorn[i] + 5)
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
        else if (timer > 0 && timer > intervalWateringCorn[i] + 5)
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

        if (timer < intervalWateringCorn[0]) 
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
                Debug.Log(22);
                if (Gen.plots[i].GetComponent<CheckIsScraped>().isFinishedGrowing == true)
                {
                    
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
