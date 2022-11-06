using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomButtonController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ZoomSliderActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject ZoomSlider;
    private bool ZoomSliderActive;

    public void OnZoomButtonPress()
    {
        if (ZoomSliderActive)
        {
            ZoomSliderActive = false;
        }
        else
        {
            ZoomSliderActive = true;
        }
        ZoomSlider.SetActive(ZoomSliderActive);
    }
}
