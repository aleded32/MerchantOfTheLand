using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plotSeeding
{
    PlotGeneration Gen;
    public bool isPlotted = false;
    GameObject plotPos;


    public plotSeeding() 
    {
        Gen = GameObject.FindWithTag("grid").GetComponent<PlotGeneration>();
        plotPos = GameObject.FindWithTag("Player");

    }

    public void plantingSeed()
    {
       


            for (int i = 0; i < Gen.plots.Count; i++)
            {

                if (Gen.gridpos(Gen.plots[i].transform.position.x, Gen.plots[i].transform.position.y) == Gen.gridpos(plotPos.transform.position.x, plotPos.transform.position.y))
                {
                    if (Gen.plots[i].GetComponent<CheckIsScraped>().isScraped == true)
                    {

                        
                        if (Gen.plots[i].GetComponent<SpriteRenderer>().sprite == Gen.plotTiles[1])
                        {
                            Gen.plots[i].GetComponent<SpriteRenderer>().sprite = Gen.plotTiles[2];
                            Gen.plots[i].GetComponent<CheckIsScraped>().isPlanted = true;
                            Gen.plots[i].GetComponent<CheckIsScraped>().needsWater = true;
                        }

                    }
                }
            }
                
            
    }

}
