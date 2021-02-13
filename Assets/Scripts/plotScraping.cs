using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plotScraping : MonoBehaviour
{
    public PlotGeneration Gen;
    public bool isPlotted = false;


    // Update is called once per frame
    void Update()
    {


    }

    Vector3 gridpos(float objX, float objY) 
    {
        int x = (int)objX - 14;
        int y = (int)objY - 9;
        return new Vector3(x, y);
    }


    public void hoeingPlot() 
    {
        if (isPlotted == false)
        {


            for (int i = 0; i < Gen.plots.Count; i++)
            {
                if (gridpos(Gen.plots[i].transform.position.x, Gen.plots[i].transform.position.y) == gridpos(transform.position.x, transform.position.y))
                {

                    if (Gen.plots[i].GetComponent<SpriteRenderer>().sprite == Gen.plotTiles[0])
                    {
                        Gen.plots[i].GetComponent<SpriteRenderer>().sprite = Gen.plotTiles[1];
                        isPlotted = true;
                    }

                    else if (Gen.plots[i].GetComponent<SpriteRenderer>().sprite == Gen.plotTiles[1]) 
                    {
                        Gen.plots[i].GetComponent<SpriteRenderer>().sprite = Gen.plotTiles[0];
                        isPlotted = true;
                    }
                        
                }
            }
        }
    }



}
