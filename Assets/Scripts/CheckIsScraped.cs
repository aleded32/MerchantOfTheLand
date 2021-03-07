using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckIsScraped : MonoBehaviour
{
    public bool isScraped;
    public bool isPlanted;
    public bool needsWater;
    public bool isWatered;
    public int waterWarningCount;
    public int wateredCount;
    public bool isFinishedGrowing;
    public string nameOfVeg;

    private void Start()
    {
        wateredCount = 0;
        isPlanted = false;
        isScraped = false;
        needsWater = false;
        isWatered = false;
        waterWarningCount = 0;
        isFinishedGrowing = false;
        nameOfVeg = null;
    }

}
