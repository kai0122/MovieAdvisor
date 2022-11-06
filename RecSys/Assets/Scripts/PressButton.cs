using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressButton : MonoBehaviour
{
    private Touch touch;
    public GameObject explanation;
    public ExplanationController explanationController;
    private bool showExplantion;

    // Start is called before the first frame update
    void Start()
    {
        showExplantion = false;
        explanation.SetActive(false);
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
                    if (hit.transform.gameObject.active){
#pragma warning restore
                        if (hit.transform.gameObject.GetComponent<PressButton>().showExplantion)
                        {
                            hit.transform.gameObject.GetComponent<PressButton>().showExplantion = false;
                            hit.transform.gameObject.GetComponent<PressButton>().explanation.SetActive(false);
                        }
                        else
                        {
                            hit.transform.gameObject.GetComponent<PressButton>().showExplantion = true;
                            hit.transform.gameObject.GetComponent<PressButton>().explanation.SetActive(true);
                            hit.transform.gameObject.GetComponent<PressButton>().explanationController.CheckChangeExplanationMode();
                        }
                    }
                }
            }

        }
    }
}
