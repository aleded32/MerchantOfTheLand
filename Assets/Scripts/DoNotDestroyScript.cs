using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoNotDestroyScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject audio;

    public static DoNotDestroyScript instance;

    void Awake()
    {
        if (instance)
        {
            Destroy(audio);
        }
        else 
        {
            instance = this;
            DontDestroyOnLoad(audio);
           
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
