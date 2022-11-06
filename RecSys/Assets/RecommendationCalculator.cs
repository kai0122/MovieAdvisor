using System.Collections;
using System.Collections.Generic;
using TMDbLib.Objects.Movies;
using TMDbLib.Objects.Search;
using UnityEngine;
using IMDbApiLib;
using System;

public class RecommendationCalculator : MonoBehaviour
{
    public UserLikedMoviesController userLikedMoviesController;

    public TMDBApiController tMDBApiController;
    public IMDBController iMDBController;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetUserLikedMovieNum()
    {
        return userLikedMoviesController.GetUserLikedMovieAmount();
    }

    public int GetActorsTotalActedMovieNum(int movieID)
    {
        if (movieID == 508442)
        {
            return tMDBApiController.GetActorsTotalActedMovieNum()[1];
        }
        if (movieID == 464052)
        {
            return tMDBApiController.GetActorsTotalActedMovieNum()[0];
        }
        return 0;
    }

    public Dictionary<int, string> GetUserLikedMoviePairs()
    {
        return userLikedMoviesController.userLikedMoviesName;
    }

    bool notYetGetList = false;
    public void GetMovieRatedList(bool updateUserLikedList = false)
    {
        if (notYetGetList || updateUserLikedList)
        {
            List<int> targets = new List<int>() { 508442, 464052 };
            foreach (SearchMovie movie in tMDBApiController.GetMovieWatchList(new List<int>() { 508442, 464052 }))
            {
                if (!userLikedMoviesController.userLikedMoviesName.ContainsKey(movie.Id) && !targets.Contains(movie.Id))
                {
                    userLikedMoviesController.userLikedMoviesName.Add(movie.Id, movie.Title);
                }
            }
            foreach (int id in tMDBApiController.GetActorActingMovie())
            {
                if (!userLikedMoviesController.userLikedMoviesName.ContainsKey(id) && !targets.Contains(id))
                {
                    userLikedMoviesController.userLikedMoviesName.Add(id, tMDBApiController.GetMovieName(id));
                }
            }
            notYetGetList = false;
        }
    }

    public List<int> GetLeadToRecommendationMovieIDs(int targetMovieID)
    {
        /*
        List<int> userLikedMovieIDs = new List<int>();
        
        foreach (int userLikedMovieID in userLikedMoviesName.Keys)
        {
            if (tMDBApiController.CompareMoreMovieRecommendationsWithID(userLikedMovieID, targetMovieID))
            {
                userLikedMovieIDs.Add(userLikedMovieID);
            }
        }

        return userLikedMovieIDs;
        */

        List<int> ids = new List<int>();
        foreach (int id in userLikedMoviesController.userLikedMoviesName.Keys) ids.Add(id);
        return tMDBApiController.CompareMoreMovieRecommendationsWithID2(ids, targetMovieID);
    }

    private Dictionary<string, List<int>> actorMoviePairs = new Dictionary<string, List<int>>();
    public List<Cast> GetSameActorMovies(int targetMovieID)
    {
        actorMoviePairs = new Dictionary<string, List<int>>();

        List<Cast> targetMovieCasts = tMDBApiController.GetMovieCasts(targetMovieID);
        targetMovieCasts = targetMovieCasts.GetRange(0, 10);

        List<Cast> userLikedCasts = new List<Cast>();
        foreach (int userLikedMovieID in userLikedMoviesController.userLikedMoviesName.Keys)
        {
            List<Cast> casts = tMDBApiController.GetMovieCasts(userLikedMovieID);

            int castMaxNum = 10;
            if (casts.Count < 10) castMaxNum = casts.Count;

            foreach(Cast person in casts.GetRange(0, castMaxNum))
            {
                if (!actorMoviePairs.ContainsKey(person.Name))
                {
                    userLikedCasts.Add(person);
                    actorMoviePairs.Add(person.Name, new List<int>() { userLikedMovieID });
                }
                else
                {
                    actorMoviePairs[person.Name].Add(userLikedMovieID);
                }
            }
        }

        userLikedCasts = InsertionSort(userLikedCasts);

        List<Cast> result = new List<Cast>();
        foreach (Cast person1 in userLikedCasts)
        {
            foreach (Cast person2 in targetMovieCasts)
            {
                if (person1.Id == person2.Id)
                {
                    result.Add(person1);
                }
            }
        }
        

        return result;
    }

    private List<Cast> InsertionSort(List<Cast> inputArray)
    {
        for (int i = 0; i < inputArray.Count - 1; i++)
        {
            for (int j = i + 1; j > 0; j--)
            {
                if (actorMoviePairs[inputArray[j - 1].Name].Count < actorMoviePairs[inputArray[j].Name].Count)
                {
                    Cast temp = inputArray[j - 1];
                    inputArray[j - 1] = inputArray[j];
                    inputArray[j] = temp;
                }
            }
        }
        return inputArray;
    }

    public List<string> GetUserLikedActorMovies(string actorName)
    {
        if (actorMoviePairs.ContainsKey(actorName))
        {
            List<string> names = new List<string>();
            foreach(int movieId in actorMoviePairs[actorName])
            {
                names.Add(tMDBApiController.GetMovieName(movieId));
            }
            return names;
        }
        else
        {
            return new List<string>();
        }
    }

    public List<string> GetUserLikedActorMovieUrls(string actorName)
    {
        if (actorMoviePairs.ContainsKey(actorName))
        {
            List<string> urls = new List<string>();
            foreach (int movieId in actorMoviePairs[actorName])
            {
                urls.Add(tMDBApiController.GetMoviePosters(movieId).Posters[0].FilePath);
            }
            return urls;
        }
        else
        {
            return new List<string>();
        }
    }

    public List<int> GetUserLikedGenreMovies(string genreName)
    {
        return tMDBApiController.GetUserLikedGenreMovies(genreName);
    }

    public List<string> GetUserLikedGenreMovieNames(string genreName)
    {
        List<string> names = new List<string>();
        foreach(int movieId in tMDBApiController.GetUserLikedGenreMovies(genreName))
        {
            names.Add(tMDBApiController.GetMovieName(movieId));
        }
        return names;
    }

    public List<string> GetUserLikedGenreMovieUrls(string genreName)
    {
        List<string> names = new List<string>();
        foreach (int movieId in tMDBApiController.GetUserLikedGenreMovies(genreName))
        {
            names.Add(tMDBApiController.GetMoviePosters(movieId).Posters[0].FilePath);
        }
        return names;
    }

    public Dictionary<string, int> GetGenreCounts(int targetMovieID)
    {
        /*
        List<int> userLikedMovieIDs = new List<int>();
        
        foreach (int userLikedMovieID in userLikedMoviesName.Keys)
        {
            if (tMDBApiController.CompareMoreMovieSimilarWithID(userLikedMovieID, targetMovieID))
            {
                userLikedMovieIDs.Add(userLikedMovieID);
            }
        }

        return userLikedMovieIDs;
        */

        List<int> ids = new List<int>();
        foreach (int id in userLikedMoviesController.userLikedMoviesName.Keys) ids.Add(id);
        return tMDBApiController.CompareMoreMovieSimilarWithID3(ids, targetMovieID);
    }

    public List<int> GetSameGenreMovies(int targetMovieID)
    {
        /*
        List<int> userLikedMovieIDs = new List<int>();
        
        foreach (int userLikedMovieID in userLikedMoviesName.Keys)
        {
            if (tMDBApiController.CompareMoreMovieSimilarWithID(userLikedMovieID, targetMovieID))
            {
                userLikedMovieIDs.Add(userLikedMovieID);
            }
        }

        return userLikedMovieIDs;
        */

        List<int> ids = new List<int>();
        return ids;
    }

    public double GetRecommendationRating(int movieId)
    {
        string IMDbId = tMDBApiController.GetMovieIMDbID(movieId);
        double TMDBRating = (double) tMDBApiController.GetMovieRating(movieId) / 10.0f;

        double RottenTomatoRating;
        double IMDbRating;
        double MetacriticRating;
        if (iMDBController.successfullyUpdate && iMDBController.HasCreatedList())
        {
            RottenTomatoRating = (double)Int16.Parse(iMDBController.GetMovieRatingRottenTomato(IMDbId)) / 100.0f;
            IMDbRating = Convert.ToDouble(iMDBController.GetMovieRatingIMDb(IMDbId)) / 10.0f;
            MetacriticRating = (double)Int16.Parse(iMDBController.GetMovieRatingMetacritic(IMDbId)) / 100.0f;
        }
        else
        {
            RottenTomatoRating = TMDBRating;
            IMDbRating = TMDBRating;
            MetacriticRating = TMDBRating;
        }

        Debug.Log(TMDBRating);
        Debug.Log(RottenTomatoRating);
        Debug.Log(IMDbRating);
        Debug.Log(MetacriticRating);

        return (TMDBRating + RottenTomatoRating + IMDbRating) / 3.0f;
    }

    public int GetMovieRatingRottenTomato(int movieId)
    {
        if (iMDBController.successfullyUpdate)
        {
            string IMDbId = tMDBApiController.GetMovieIMDbID(movieId);
            return Int16.Parse(iMDBController.GetMovieRatingRottenTomato(IMDbId));
        }
        else
        {
            return (int)(GetMovieRatingTMDb(movieId) * 10);
        }
        
    }

    public int GetMovieRatingMetacritic(int movieId)
    {
        if (iMDBController.successfullyUpdate)
        {
            string IMDbId = tMDBApiController.GetMovieIMDbID(movieId);
            return Int16.Parse(iMDBController.GetMovieRatingMetacritic(IMDbId));
        }
        else
        {
            return (int) (GetMovieRatingTMDb(movieId) * 10);
        }
        
    }

    public double GetMovieRatingIMDb(int movieId)
    {
        if (iMDBController.successfullyUpdate)
        {
            string IMDbId = tMDBApiController.GetMovieIMDbID(movieId);
            return Convert.ToDouble(iMDBController.GetMovieRatingIMDb(IMDbId));
        }
        else
        {
            return GetMovieRatingTMDb(movieId);
        }
        
    }

    public double GetMovieRatingTMDb(int movieId)
    {
        return tMDBApiController.GetMovieRating(movieId);
    }

    public int GetMovieRatingIMDbVotes(int movieId)
    {
        if (iMDBController.successfullyUpdate)
        {
            string IMDbId = tMDBApiController.GetMovieIMDbID(movieId);
            return Int32.Parse(iMDBController.GetMovieRatingIMDbVotes(IMDbId));
        }
        else
        {
            return GetMovieRatingTMDbVotes(movieId);
        }
    }

    public int GetMovieRatingIMDbPercentCount(int movieId, int score)
    {
        if (iMDBController.successfullyUpdate)
        {
            string IMDbId = tMDBApiController.GetMovieIMDbID(movieId);
            return Int32.Parse(iMDBController.GetMovieRatingIMDbDetail(IMDbId)[10 - score].Votes);
        }
        else
        {
            return GetMovieRatingTMDbPercentCount(movieId, score);
        }
    }

    public string GetMovieRatingIMDbPercent(int movieId, int score)
    {
        if (iMDBController.successfullyUpdate)
        {
            string IMDbId = tMDBApiController.GetMovieIMDbID(movieId);
            return iMDBController.GetMovieRatingIMDbDetail(IMDbId)[10 - score].Percent;
        }
        else
        {
            return String.Format("{0:0.0}", GetMovieRatingTMDbPercent(movieId, score).ToString());
        }
    }

    public int GetMovieRatingTMDbVotes(int movieId)
    {
        return tMDBApiController.GetMovieVoteCount(movieId);
    }

    public int GetMovieRatingTMDbPercentCount(int movieId, int score)
    {
        return tMDBApiController.GetMovieRatingTMDbPercentCount(movieId)[10 - score];
    }

    public double GetMovieRatingTMDbPercent(int movieId, int score)
    {
        return tMDBApiController.GetMovieRatingTMDbPercent(movieId)[10 - score];
    }

    public int GetMovieRatingRottenVotes(int movieId)
    {
        return tMDBApiController.GetMovieRottenVoteCount(movieId);
    }

    public int GetMovieRatingRottenPercentCount(int movieId, int score)
    {
        return tMDBApiController.GetMovieRatingRottenPercentCount(movieId)[10 - score];
    }

    public double GetMovieRatingRottenPercent(int movieId, int score)
    {
        return tMDBApiController.GetMovieRatingRottenPercent(movieId)[10 - score];
    }
}
