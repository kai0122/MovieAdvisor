using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPressARDemo : MonoBehaviour
{
    private Touch touch;
    
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<MeshRenderer>().material.color = Color.white;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit, 100.0f))
                {
                    //explanationController.ShowNextMovieForSelection();
#pragma warning disable CS0618 // 類型或成員已經過時
                    if (hit.transform.gameObject.active)
                    {
#pragma warning restore
                        if (hit.transform.gameObject.GetComponent<ButtonPressARDemo>().gameObject.GetComponent<MeshRenderer>().material.color == Color.white)
                        {
                            hit.transform.gameObject.GetComponent<ButtonPressARDemo>().gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
                        }
                    }
                }
            }

        }
    }
}
