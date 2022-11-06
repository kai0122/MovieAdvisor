using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectGenreShowNextMoremovies : MonoBehaviour
{
    private Touch touch;
    public bool hitGenreMoreMovies;


    // Start is called before the first frame update
    void Start()
    {
        hitGenreMoreMovies = false;
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
                        if (hit.transform.gameObject.name == "GenreMoreMovies")
                        {
                            if (hitGenreMoreMovies == true)
                            {
                                hitGenreMoreMovies = false;
                            }
                            else
                            {
                                hitGenreMoreMovies = true;
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
                    if (hit.transform.gameObject.name == "GenreMoreMovies")
                    {
                        if (hitGenreMoreMovies == true)
                        {
                            hitGenreMoreMovies = false;
                        }
                        else
                        {
                            hitGenreMoreMovies = true;
                        }
                    }
                }
            }
        }
#endif
    }
}
