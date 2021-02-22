using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlotGeneration : MonoBehaviour
{
    // Start is called before the first frame update

    public Sprite[] plotTiles;
    public GameObject plot;
    public List<GameObject> plots;
   

    void Start()
    {
        plots = new List<GameObject>();
        plot.GetComponent<SpriteRenderer>().sprite = plotTiles[0];
        plotGen(26,16);
    }

    public Vector3 gridpos(float objX, float objY)
    {
        int x = (int)(objX - 14);
        int y = (int)(objY - 9);
        return new Vector3(x, y, 0);
    }


    void plotGen(int x, int y) 
    {

       
        
        for (int i = 0; i < x; i++) 
        {
            for (int j = 0; j < y; j++)
            {

                if (i <= 11 && j < 6)
                {
                    plots.Add(Instantiate(plot, new Vector3(i - 14.5f, j - 9.5f), Quaternion.identity));
                }
                else if (i > 11) 
                {
                    plots.Add(Instantiate(plot, new Vector3(i - 14.5f, j - 9.5f), Quaternion.identity));
                }
            }
        }

        
    }


    
    

}
