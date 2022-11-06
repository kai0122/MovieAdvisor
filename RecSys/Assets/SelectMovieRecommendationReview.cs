using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectMovieRecommendationReview : MonoBehaviour
{
    private Touch touch;
    public bool hitRecommendationReview;
    public bool hitRecommendationReviewNext;
    public bool hitRecommendationReviewPrevious;

    public Texture commentIconOpen;
    public Texture commentIconClose;


    // Start is called before the first frame update
    void Start()
    {
        hitRecommendationReview = false;
        hitRecommendationReviewNext = false;
        hitRecommendationReviewPrevious = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (hitRecommendationReview)
        {
            if (gameObject.name == "ReviewIcon")
            {
                gameObject.GetComponent<Renderer>().material = new Material(Shader.Find("Unlit/Transparent"));
                gameObject.GetComponent<Renderer>().material.mainTexture = commentIconOpen;
            }
        }
        else
        {
            if (gameObject.name == "ReviewIcon")
            {
                gameObject.GetComponent<Renderer>().material = new Material(Shader.Find("Unlit/Transparent"));
                gameObject.GetComponent<Renderer>().material.mainTexture = commentIconClose;
            }
        }

        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.touches[0].position);

                if (Physics.Raycast(ray, out hit))
                {
                    //explanationController.ShowNextMovieForSelection();
#pragma warning disable CS0618 // 類型或成員已經過時
                    if (hit.transform.gameObject.active)
                    {
#pragma warning restore
                        if (hit.transform.gameObject.name == "ReviewIcon")
                        {
                            if (hitRecommendationReview == true)
                            {
                                hitRecommendationReview = false;
                            }
                            else
                            {
                                hitRecommendationReview = true;
                            }
                        }

                        if (hit.transform.gameObject.name == "Next")
                        {
                            if (hitRecommendationReviewNext == true)
                            {
                                hitRecommendationReviewNext = false;
                            }
                            else
                            {
                                hitRecommendationReviewNext = true;
                            }
                        }

                        if (hit.transform.gameObject.name == "Previous")
                        {
                            if (hitRecommendationReviewPrevious == true)
                            {
                                hitRecommendationReviewPrevious = false;
                            }
                            else
                            {
                                hitRecommendationReviewPrevious = true;
                            }
                        }
                    }
                }
            }

        }

#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                //explanationController.ShowNextMovieForSelection();
#pragma warning disable CS0618 // 類型或成員已經過時
                if (hit.collider != null)
                {
#pragma warning restore
                    if (hit.transform.gameObject.name == "ReviewIcon")
                    {
                        if (hitRecommendationReview == true)
                        {
                            hitRecommendationReview = false;
                        }
                        else
                        {
                            hitRecommendationReview = true;
                        }
                    }

                    if (hit.transform.gameObject.name == "Next")
                    {
                        if (hitRecommendationReviewNext == true)
                        {
                            hitRecommendationReviewNext = false;
                        }
                        else
                        {
                            hitRecommendationReviewNext = true;
                        }
                    }

                    if (hit.transform.gameObject.name == "Previous")
                    {
                        if (hitRecommendationReviewPrevious == true)
                        {
                            hitRecommendationReviewPrevious = false;
                        }
                        else
                        {
                            hitRecommendationReviewPrevious = true;
                        }
                    }
                }
            }
        }
#endif
    }
}
