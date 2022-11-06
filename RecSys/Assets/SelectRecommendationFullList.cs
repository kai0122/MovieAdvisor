using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectRecommendationFullList : MonoBehaviour
{
    private Touch touch;
    public bool hitRecommendationFullListActor;
    public bool hitRecommendationFullListSimilar;
    public bool hitRecommendationFullListRecommendation;


    // Start is called before the first frame update
    void Start()
    {
        hitRecommendationFullListActor = false;
        hitRecommendationFullListSimilar = false;
        hitRecommendationFullListRecommendation = false;
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
                Ray ray = Camera.main.ScreenPointToRay(Input.touches[0].position);

                if (Physics.Raycast(ray, out hit))
                {
                    //explanationController.ShowNextMovieForSelection();
#pragma warning disable CS0618 // 類型或成員已經過時
                    if (hit.transform.gameObject.active)
                    {
#pragma warning restore
                        if (hit.transform.gameObject.name == "ActorFullList")
                        {
                            hitRecommendationFullListActor = true;
                            hitRecommendationFullListSimilar = false;
                            hitRecommendationFullListRecommendation = false;
                        }
                        else if (hit.transform.gameObject.name == "SimilarFullList")
                        {
                            hitRecommendationFullListActor = false;
                            hitRecommendationFullListSimilar = true;
                            hitRecommendationFullListRecommendation = false;
                        }
                        else if (hit.transform.gameObject.name == "RecommendationFullList")
                        {
                            hitRecommendationFullListActor = false;
                            hitRecommendationFullListSimilar = false;
                            hitRecommendationFullListRecommendation = true;
                        }
                    }
                }
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                hitRecommendationFullListActor = false;
                hitRecommendationFullListSimilar = false;
                hitRecommendationFullListRecommendation = false;
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
                    if (hit.transform.gameObject.name == "ActorFullList")
                    {
                        if (hitRecommendationFullListActor == true)
                        {
                            hitRecommendationFullListActor = false;
                        }
                        else
                        {
                            hitRecommendationFullListActor = true;
                            hitRecommendationFullListSimilar = false;
                            hitRecommendationFullListRecommendation = false;
                        }
                    }
                    else if (hit.transform.gameObject.name == "SimilarFullList")
                    {
                        if (hitRecommendationFullListSimilar == true)
                        {
                            hitRecommendationFullListSimilar = false;
                        }
                        else
                        {
                            hitRecommendationFullListActor = false;
                            hitRecommendationFullListSimilar = true;
                            hitRecommendationFullListRecommendation = false;
                        }
                    }
                    else if (hit.transform.gameObject.name == "RecommendationFullList")
                    {
                        if (hitRecommendationFullListRecommendation == true)
                        {
                            hitRecommendationFullListRecommendation = false;
                        }
                        else
                        {
                            hitRecommendationFullListActor = false;
                            hitRecommendationFullListSimilar = false;
                            hitRecommendationFullListRecommendation = true;
                        }
                    }
                }
            }
        }
#endif
    }
}
