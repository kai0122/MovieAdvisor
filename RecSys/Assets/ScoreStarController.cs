using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreStarController : MonoBehaviour
{
    public Texture textureFullStar;
    public Texture textureEmptyStar;

    public GameObject star1;
    public GameObject star2;
    public GameObject star3;
    public GameObject star4;
    public GameObject star5;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private Touch touch;
    public bool hitScoreStarControllerActor;
    public bool hitScoreStarControllerSimilar;
    public bool hitScoreStarControllerRecommendation;

    // Update is called once per frame
    void Update()
    {
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
                        if (hit.transform.gameObject.name == "ActorScoreStar")
                        {
                            if (hitScoreStarControllerActor == true)
                            {
                                hitScoreStarControllerActor = false;
                            }
                            else
                            {
                                hitScoreStarControllerActor = true;
                                hitScoreStarControllerSimilar = false;
                                hitScoreStarControllerRecommendation = false;
                            }
                        }

                        if (hit.transform.gameObject.name == "SimilarScoreStar")
                        {
                            if (hitScoreStarControllerSimilar == true)
                            {
                                hitScoreStarControllerSimilar = false;
                            }
                            else
                            {
                                hitScoreStarControllerActor = false;
                                hitScoreStarControllerSimilar = true;
                                hitScoreStarControllerRecommendation = false;
                            }
                        }

                        if (hit.transform.gameObject.name == "RecommendationScoreStar")
                        {
                            if (hitScoreStarControllerRecommendation == true)
                            {
                                hitScoreStarControllerRecommendation = false;
                            }
                            else
                            {
                                hitScoreStarControllerActor = false;
                                hitScoreStarControllerSimilar = false;
                                hitScoreStarControllerRecommendation = true;
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
                    if (hit.transform.gameObject.name == "ActorScoreStar")
                    {
                        if (hitScoreStarControllerActor == true)
                        {
                            hitScoreStarControllerActor = false;
                        }
                        else
                        {
                            hitScoreStarControllerActor = true;
                            hitScoreStarControllerSimilar = false;
                            hitScoreStarControllerRecommendation = false;
                        }
                    }

                    if (hit.transform.gameObject.name == "SimilarScoreStar")
                    {
                        if (hitScoreStarControllerSimilar == true)
                        {
                            hitScoreStarControllerSimilar = false;
                        }
                        else
                        {
                            hitScoreStarControllerActor = false;
                            hitScoreStarControllerSimilar = true;
                            hitScoreStarControllerRecommendation = false;
                        }
                    }

                    if (hit.transform.gameObject.name == "RecommendationScoreStar")
                    {
                        if (hitScoreStarControllerRecommendation == true)
                        {
                            hitScoreStarControllerRecommendation = false;
                        }
                        else
                        {
                            hitScoreStarControllerActor = false;
                            hitScoreStarControllerSimilar = false;
                            hitScoreStarControllerRecommendation = true;
                        }
                    }
                }
            }
        }
#endif
    }

    public void ChangeStarScore(int num)
    {
        if (num == 0)
        {
            star1.GetComponent<Renderer>().material.mainTexture = textureEmptyStar;
            star2.GetComponent<Renderer>().material.mainTexture = textureEmptyStar;
            star3.GetComponent<Renderer>().material.mainTexture = textureEmptyStar;
            star4.GetComponent<Renderer>().material.mainTexture = textureEmptyStar;
            star5.GetComponent<Renderer>().material.mainTexture = textureEmptyStar;
        }
        else if (num == 1)
        {
            star1.GetComponent<Renderer>().material.mainTexture = textureFullStar;
            star2.GetComponent<Renderer>().material.mainTexture = textureEmptyStar;
            star3.GetComponent<Renderer>().material.mainTexture = textureEmptyStar;
            star4.GetComponent<Renderer>().material.mainTexture = textureEmptyStar;
            star5.GetComponent<Renderer>().material.mainTexture = textureEmptyStar;
        }
        else if (num == 2)
        {
            star1.GetComponent<Renderer>().material.mainTexture = textureFullStar;
            star2.GetComponent<Renderer>().material.mainTexture = textureFullStar;
            star3.GetComponent<Renderer>().material.mainTexture = textureEmptyStar;
            star4.GetComponent<Renderer>().material.mainTexture = textureEmptyStar;
            star5.GetComponent<Renderer>().material.mainTexture = textureEmptyStar;
        }
        else if (num == 3)
        {
            star1.GetComponent<Renderer>().material.mainTexture = textureFullStar;
            star2.GetComponent<Renderer>().material.mainTexture = textureFullStar;
            star3.GetComponent<Renderer>().material.mainTexture = textureFullStar;
            star4.GetComponent<Renderer>().material.mainTexture = textureEmptyStar;
            star5.GetComponent<Renderer>().material.mainTexture = textureEmptyStar;
        }
        else if (num == 4)
        {
            star1.GetComponent<Renderer>().material.mainTexture = textureFullStar;
            star2.GetComponent<Renderer>().material.mainTexture = textureFullStar;
            star3.GetComponent<Renderer>().material.mainTexture = textureFullStar;
            star4.GetComponent<Renderer>().material.mainTexture = textureFullStar;
            star5.GetComponent<Renderer>().material.mainTexture = textureEmptyStar;
        }
        else if (num == 5)
        {
            star1.GetComponent<Renderer>().material.mainTexture = textureFullStar;
            star2.GetComponent<Renderer>().material.mainTexture = textureFullStar;
            star3.GetComponent<Renderer>().material.mainTexture = textureFullStar;
            star4.GetComponent<Renderer>().material.mainTexture = textureFullStar;
            star5.GetComponent<Renderer>().material.mainTexture = textureFullStar;
        }
    }
}
