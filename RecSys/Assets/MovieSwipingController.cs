using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovieSwipingController : MonoBehaviour
{
    private Touch touch;
    public bool hitRecommendedMovieSwipe;
    public Vector2 touchDeltaPosition;
    public float speed = 0.1f;
    public float deltaXRecommendationMovie = 0;
    public float deltaYRecommendationMovie = 0;


    // Start is called before the first frame update
    void Start()
    {
        hitRecommendedMovieSwipe = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved)
            {
                Debug.Log("Swiping");
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.touches[0].position);

                if (Physics.Raycast(ray, out hit))
                {
                    //explanationController.ShowNextMovieForSelection();
#pragma warning disable CS0618 // 類型或成員已經過時
                    if (hit.transform.gameObject.active)
                    {
#pragma warning restore
                        if (hit.transform.gameObject.name == "RecommendedMovies")
                        {
                            if (hitRecommendedMovieSwipe == true)
                            {
                                hitRecommendedMovieSwipe = true;

                                // Get movement of the finger since last frame
                                touchDeltaPosition = Input.GetTouch(0).deltaPosition;
                                deltaXRecommendationMovie = -touchDeltaPosition.x * speed;
                                deltaYRecommendationMovie = -touchDeltaPosition.y * speed;
                            }
                        }
                    }
                }
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                hitRecommendedMovieSwipe = false;

                touchDeltaPosition = new Vector2(0, 0);
                deltaXRecommendationMovie = 0f;
                deltaYRecommendationMovie = 0f;
            }
            else if (touch.phase == TouchPhase.Began)
            {
                Debug.Log("Swiping");
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.touches[0].position);

                if (Physics.Raycast(ray, out hit))
                {
                    //explanationController.ShowNextMovieForSelection();
#pragma warning disable CS0618 // 類型或成員已經過時
                    if (hit.transform.gameObject.active)
                    {
#pragma warning restore
                        if (hit.transform.gameObject.name == "RecommendedMovies")
                        {
                            if (hitRecommendedMovieSwipe == false)
                            {
                                hitRecommendedMovieSwipe = true;
                            }
                        }
                    }
                }
            }

        }


    }
}
