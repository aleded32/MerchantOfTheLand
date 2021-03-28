using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class audioControl : MonoBehaviour
{

    GameObject audio;
    public GameObject Slider;
    // Start is called before the first frame update
    void Start()
    {
        audio = GameObject.FindWithTag("audio");
        Slider.GetComponent<Slider>().value = audio.GetComponent<AudioSource>().volume;
    }

    // Update is called once per frame
    void Update()
    {
        audio.GetComponent<AudioSource>().volume = Slider.GetComponent<Slider>().value;
    }
}
