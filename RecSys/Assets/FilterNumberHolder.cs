using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FilterNumberHolder : MonoBehaviour
{
    public float actorBaseRatio;
    public float genreBaseRatio;
    public float publicityBaseRatio;
    public float ratingBaseRatio;

    public int actorBase;
    public int genreBase;
    public int publicityBase;
    public int ratingBase;

    public FilterController filterController;
    public UserLikedMoviesController userLikedMoviesController;


    public JustificationController_Final_version justificationController_Final_Version_1;
    // Start is called before the first frame update
    void Start()
    {
        actorBase = (int)(actorBaseRatio * userLikedMoviesController.GetUserLikedMovieAmount());
        genreBase = (int)(genreBaseRatio * userLikedMoviesController.GetUserLikedMovieAmount());
        publicityBase = (int)(publicityBaseRatio * userLikedMoviesController.GetUserLikedMovieAmount());
        ratingBase = (int)(ratingBaseRatio * 5.0f);

        totalTargetMovieNum = justificationController_Final_Version_1.movieIDPairs.Count;
        for (int i = 0; i < totalTargetMovieNum; i++)
        {
            ifUpdateFilter.Add(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnBaseUpdated()
    {
        for (int i = 0; i < totalTargetMovieNum; i++)
        {
            ifUpdateFilter[i] = true;
        }

        actorBaseRatio = (float)filterController.GetActorBase();
        genreBaseRatio = (float)filterController.GetGenreBase();
        publicityBaseRatio = (float)filterController.GetPublicityBase();
        ratingBaseRatio = (float)filterController.GetRatingBase();

        actorBase = (int)(actorBaseRatio * userLikedMoviesController.GetUserLikedMovieAmount());
        genreBase = (int)(genreBaseRatio * userLikedMoviesController.GetUserLikedMovieAmount());
        publicityBase = (int)(publicityBaseRatio * userLikedMoviesController.GetUserLikedMovieAmount());
        ratingBase = (int)(ratingBaseRatio * 5.0f);
    }

    public void UpdateRatio_with_new_userLikedList()
    {
        actorBaseRatio = (float) actorBase / userLikedMoviesController.GetUserLikedMovieAmount();
        actorBaseRatio = actorBaseRatio > 1 ? 1.0f : actorBaseRatio;

        genreBaseRatio = (float) genreBase / userLikedMoviesController.GetUserLikedMovieAmount();
        genreBaseRatio = genreBaseRatio > 1 ? 1.0f : genreBaseRatio;

        publicityBaseRatio = (float) publicityBase / userLikedMoviesController.GetUserLikedMovieAmount();
        publicityBaseRatio = publicityBaseRatio > 1 ? 1.0f : publicityBaseRatio;

        ratingBaseRatio = (float) ratingBase / 5.0f;
        ratingBaseRatio = ratingBaseRatio > 1 ? 1.0f : ratingBaseRatio;
    }

    private int totalTargetMovieNum;
    private List<bool> ifUpdateFilter = new List<bool>();

    public bool GetIfUpdateFilter(string name)
    {
        for (int i = 0; i < totalTargetMovieNum; i++)
        {
            if (name.Contains(i.ToString()))
            {
                if (ifUpdateFilter[i])
                {
                    Debug.Log("NOW, " + name + "has updated filter ...");
                    ifUpdateFilter[i] = false;
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        return false;
    }
}
