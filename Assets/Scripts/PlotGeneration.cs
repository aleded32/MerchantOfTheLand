using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlotGeneration : MonoBehaviour
{
    // Start is called before the first frame update

    public Sprite plotTiles;
    public GameObject plot;
    public GameObject[,] tiles;

    void Start()
    {
        plotGen(26,16);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void plotGen(int x, int y) 
    {

        tiles = new GameObject[x,y];
        
        for (int i = 0; i < x; i++) 
        {
            for (int j = 0; j < y; j++)
            {
                tiles[i, j] = plot;
                tiles[i,j].GetComponent<SpriteRenderer>().sprite = plotTiles;

                if (i < 26 && j < 6)
                {
                    Instantiate(tiles[i, j], new Vector3(i - 14.5f, j - 9.5f), Quaternion.identity);
                }
                else if (i > 11 && j > 5) 
                {
                    Instantiate(tiles[i, j], new Vector3(i - 14.5f, j - 9.5f), Quaternion.identity);
                }
            }
        }
    }


    
    

}
