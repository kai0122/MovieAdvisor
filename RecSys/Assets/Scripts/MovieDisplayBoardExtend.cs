using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovieDisplayBoardExtend : MonoBehaviour
{
    private Touch touch;
    public ExplanationController explanationController;

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
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit, 100.0f))
                {
                    explanationController.PressMovieDisplayBoard();
                }
            }

        }
    }
}
