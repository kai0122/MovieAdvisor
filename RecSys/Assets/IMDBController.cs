using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IMDbApiLib;
using System.Threading.Tasks;
using IMDbApiLib.Models;

public class IMDBController : MonoBehaviour
{
    Dictionary<string, string> movie_IMDb_Rating = new Dictionary<string, string>();
    Dictionary<string, string> movie_IMDb_Rating_Votes = new Dictionary<string, string>();
    Dictionary<string, List<UserRatingDataDetail>> movie_IMDb_Rating_Votes_Detail = new Dictionary<string, List<UserRatingDataDetail>>();
    Dictionary<string, string> movie_RottenTomato_Rating = new Dictionary<string, string>();
    //Dictionary<string, string> movie_RottenTomato_Rating_Votes = new Dictionary<string, string>();
    Dictionary<string, string> movie_Metacritic_Rating = new Dictionary<string, string>();
    //Dictionary<string, string> movie_Metacritic_Rating_Votes = new Dictionary<string, string>();

    public bool started = false;

    // Start is called before the first frame update
    void Start()
    {
        // IMDbApiLib Package on Nuget : https://nuget.org/packages/IMDbApiLib
        //GetRatingsAsync("tt1375666");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool successfullyUpdate = false;
    private async void GetRatingsAsync(string movieID)
    {
        var apiLib = new ApiLib("k_931s3yak");
        var data = await apiLib.TitleAsync(movieID);
        var data2 = await apiLib.RatingsAsync(movieID);
        var data3 = await apiLib.UserRatingAsync(movieID);
        
        if (!movie_IMDb_Rating.ContainsKey(movieID))
        {
            Debug.Log(data.Title + ": " + data.IMDbRating.ToString() + " " + data.IMDbRatingVotes.ToString());
            movie_IMDb_Rating.Add(movieID, data.IMDbRating);
            movie_IMDb_Rating_Votes.Add(movieID, data.IMDbRatingVotes);
            movie_IMDb_Rating_Votes_Detail.Add(movieID, data3.Ratings);
            movie_Metacritic_Rating.Add(movieID, data.MetacriticRating);
        }

        if (!movie_RottenTomato_Rating.ContainsKey(movieID))
        {
            Debug.Log(data.Title + ": " + data2.RottenTomatoes.ToString() + " " + data2.Metacritic.ToString());
            if (data2.RottenTomatoes == "")
            {
                if (data2.Metacritic == "")
                {
                    movie_RottenTomato_Rating.Add(movieID, "22");
                }
                else
                {
                    movie_RottenTomato_Rating.Add(movieID, data2.Metacritic);
                }
            }
            else
            {
                movie_RottenTomato_Rating.Add(movieID, data2.RottenTomatoes);
            }
        }

        if (data.ErrorMessage.Length != 0 && data2.ErrorMessage.Length != 0 && data3.ErrorMessage.Length != 0)
        {
            successfullyUpdate = false;
        }

        started = true;
    }

    public TMDBApiController tMDBApiController;
    public void FakeWithTBDb()
    {

    }

    public void UpdateRating(string movieID)
    {
        GetRatingsAsync(movieID);
    }

    public string GetMovieRatingIMDb(string movieID)
    {
        return movie_IMDb_Rating[movieID];
    }

    public string GetMovieRatingIMDbVotes(string movieID)
    {
        return movie_IMDb_Rating_Votes[movieID];
    }

    public List<UserRatingDataDetail> GetMovieRatingIMDbDetail(string movieID)
    {
        return movie_IMDb_Rating_Votes_Detail[movieID];
    }

    public string GetMovieRatingMetacritic(string movieID)
    {
        return movie_Metacritic_Rating[movieID];
    }

    public string GetMovieRatingRottenTomato(string movieID)
    {
        return movie_RottenTomato_Rating[movieID];
    }

    public bool HasCreatedList()
    {
        if (movie_RottenTomato_Rating.Count != 0 && movie_IMDb_Rating.Count != 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
