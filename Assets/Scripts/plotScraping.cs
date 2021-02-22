using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class plotScraping
{
    PlotGeneration Gen;
    public bool isPlotted = false;
    GameObject plotPos;

    public plotScraping() 
    {
        Gen = GameObject.FindWithTag("grid").GetComponent<PlotGeneration>();
        plotPos = GameObject.FindWithTag("Player");
    }

    


    public void hoeingPlot() 
    {
        if (isPlotted == false)
        {


            for (int i = 0; i < Gen.plots.Count; i++)
            {
               
                if (Gen.gridpos(Gen.plots[i].transform.position.x, Gen.plots[i].transform.position.y) == Gen.gridpos(plotPos.transform.position.x, plotPos.transform.position.y))
                {
                    if (Gen.plots[i].GetComponent<CheckIsScraped>().isFinishedGrowing != true)
                    {



                        if (Gen.plots[i].GetComponent<SpriteRenderer>().sprite == Gen.plotTiles[0])
                        {
                            Gen.plots[i].GetComponent<SpriteRenderer>().sprite = Gen.plotTiles[1];
                            Gen.plots[i].GetComponent<CheckIsScraped>().isScraped = true;
                            isPlotted = true;
                        }
                        else if (Gen.plots[i].GetComponent<CheckIsScraped>().isScraped == true)
                        {
                            Gen.plots[i].GetComponent<SpriteRenderer>().sprite = Gen.plotTiles[0];
                            Gen.plots[i].GetComponent<CheckIsScraped>().isScraped = false;
                            Gen.plots[i].GetComponent<CheckIsScraped>().isPlanted = false;
                            Gen.plots[i].GetComponent<CheckIsScraped>().isWatered = false;
                            Gen.plots[i].GetComponent<CheckIsScraped>().wateredCount = 0;
                            isPlotted = true;
                        }
                    }
                    
                        
                }
               
            }
        }
    }

    public void highlightPlot() 
    {
        for (int i = 0; i < Gen.plots.Count; i++)
        {
            if (Gen.gridpos(Gen.plots[i].transform.position.x, Gen.plots[i].transform.position.y) == Gen.gridpos(plotPos.transform.position.x, plotPos.transform.position.y))
            {
                if(Gamepad.current.aButton.ReadValue() == 1)
                    Gen.plots[i].GetComponent<SpriteRenderer>().color = new Color32(169, 169, 169, 200);



            }
            if(Gamepad.current.aButton.ReadValue() == 0)
                Gen.plots[i].GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
            
            
        }
    }

    

}
