using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderVolume : MonoBehaviour {
    public Slider mySlider;
    // Use this for initialization
    void Start () {
        mySlider.value = 1;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnValueChanged()
    {
        AudioListener.volume = mySlider.value;
    }
}
