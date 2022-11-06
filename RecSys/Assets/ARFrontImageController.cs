using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ARFrontImageController : MonoBehaviour
{
    private Touch touch;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                RawImage arFrontImage = GameObject.Find("ARFrontImage").GetComponent<RawImage>();
                if (arFrontImage.enabled == true)
                {
                    arFrontImage.enabled = false;
                }
            }

        }

#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            RawImage arFrontImage = GameObject.Find("ARFrontImage").GetComponent<RawImage>();
            if (arFrontImage.enabled == true)
            {
                arFrontImage.enabled = false;
            }
        }
#endif
    }
}
