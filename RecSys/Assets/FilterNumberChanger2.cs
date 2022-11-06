using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FilterNumberChanger2 : MonoBehaviour
{
    public FilterNumberHolder filterNumberHolder;

    // Start is called before the first frame update
    void Start()
    {
        ChangeActorNum(filterNumberHolder.actorBaseRatio);
        ChangeGenreNum(filterNumberHolder.genreBaseRatio);
        ChangePublicityNum(filterNumberHolder.publicityBaseRatio);
        ChangeRatingNum(filterNumberHolder.ratingBaseRatio);

        gameObject.transform.Find("ScrollbarActor").GetComponent<Scrollbar>().value = filterNumberHolder.actorBaseRatio;
        gameObject.transform.Find("ScrollbarGenre").GetComponent<Scrollbar>().value = filterNumberHolder.genreBaseRatio;
        gameObject.transform.Find("ScrollbarPublicity").GetComponent<Scrollbar>().value = filterNumberHolder.publicityBaseRatio;
        gameObject.transform.Find("ScrollbarRating").GetComponent<Scrollbar>().value = filterNumberHolder.ratingBaseRatio;
    }

    public UserLikedMoviesController userLikedMoviesController;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeNum(float number)
    {
        gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = String.Format("{0:0}", (number * 100));
    }

    public void ChangeActorNum(float number)
    {
        if (number < 1.0f)
        {
            gameObject.transform.Find("ActorNumber").GetComponent<TMPro.TextMeshProUGUI>().text = ((int)(userLikedMoviesController.GetUserLikedMovieAmount() * number)).ToString();
        }
        else
        {
            gameObject.transform.Find("ActorNumber").GetComponent<TMPro.TextMeshProUGUI>().text = userLikedMoviesController.GetUserLikedMovieAmount().ToString() +  "+";
        }
    }

    public void ChangeGenreNum(float number)
    {
        if (number < 1.0f)
        {
            gameObject.transform.Find("GenreNumber").GetComponent<TMPro.TextMeshProUGUI>().text = ((int)(userLikedMoviesController.GetUserLikedMovieAmount() * number)).ToString();
        }
        else
        {
            gameObject.transform.Find("GenreNumber").GetComponent<TMPro.TextMeshProUGUI>().text = userLikedMoviesController.GetUserLikedMovieAmount().ToString() + "+";
        }
    }

    public void ChangePublicityNum(float number)
    {
        if (number < 1.0f)
        {
            gameObject.transform.Find("PublicityNumber").GetComponent<TMPro.TextMeshProUGUI>().text = ((int)(userLikedMoviesController.GetUserLikedMovieAmount() * number)).ToString();
        }
        else
        {
            gameObject.transform.Find("PublicityNumber").GetComponent<TMPro.TextMeshProUGUI>().text = userLikedMoviesController.GetUserLikedMovieAmount().ToString() + "+";
        }
    }

    public void ChangeRatingNum(float number)
    {
        if (number < 1.0f)
        {
            gameObject.transform.Find("RatingNumber").GetComponent<TMPro.TextMeshProUGUI>().text = (5.0f * number).ToString("0.0");
        }
        else
        {
            gameObject.transform.Find("RatingNumber").GetComponent<TMPro.TextMeshProUGUI>().text = "5.0";
        }
    }
}
