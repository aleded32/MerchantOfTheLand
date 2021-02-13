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

    // Update is called once per frame
    void Update()
    {
        
    }

    void plotGen(int x, int y) 
    {

       
        
        for (int i = 0; i < x; i++) 
        {
            for (int j = 0; j < y; j++)
            {

                if (i < 12 && j < 6)
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
