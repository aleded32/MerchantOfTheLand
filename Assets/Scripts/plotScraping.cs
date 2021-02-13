using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class plotScraping : MonoBehaviour
{
    public PlotGeneration Gen;
    public bool isPlotted = false;
    public GameObject plotPos;


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
                if (gridpos(Gen.plots[i].transform.position.x, Gen.plots[i].transform.position.y) == gridpos(plotPos.transform.position.x, plotPos.transform.position.y))
                {
                    Gen.plots[i].GetComponent<SpriteRenderer>().color = new Color32(169, 169, 169, 200);
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
                Gen.plots[i].GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
            }
        }
    }

    public void highlightPlot() 
    {
        for (int i = 0; i < Gen.plots.Count-1; i++)
        {
            if (gridpos(Gen.plots[i].transform.position.x, Gen.plots[i].transform.position.y) == gridpos(plotPos.transform.position.x, plotPos.transform.position.y))
            {
                if(Gamepad.current.aButton.ReadValue() == 1)
                    Gen.plots[i].GetComponent<SpriteRenderer>().color = new Color32(169, 169, 169, 200);



            }
            if(Gamepad.current.aButton.ReadValue() == 0)
                Gen.plots[i].GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
            
            
        }
    }



}
