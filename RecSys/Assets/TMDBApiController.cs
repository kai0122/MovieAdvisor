using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Http;
using System;
using TMDbLib.Client;
using TMDbLib.Objects.Movies;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Search;
using UnityEngine.UI;
using System.IO;
using VideoLibrary;
using System.Globalization;
using System.Diagnostics;
using TMDbLib.Objects.Reviews;
using TMDbLib.Objects.People;
using Video = TMDbLib.Objects.General.Video;

public class TMDBApiController : MonoBehaviour
{
    private string _movideDbApiKey = "06be00eafe8419222c854f8db00b8197";
    public string posterImageBase = "https://image.tmdb.org/t/p/original//";

    private TMDbClient client;
    public RecommendationCalculator recommendationCalculator;

    // Start is called before the first frame update
    void Start()
    {
        StartTMDbController();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Dictionary<string, int> movieIDPairs = new Dictionary<string, int>()
                                                    {
                                                        {"Soul", 508442},
                                                        {"DarkPhoenix", 320288},
                                                        {"tomandjerry", 587807},
                                                        {"WrathofMan", 637649},
                                                    };

    public IMDBController iMDBController;
    public void StartTMDbController()
    {
        client = new TMDbClient(_movideDbApiKey);

        foreach (int movieID in movieIDPairs.Values)
        {
            string IMDbId = GetMovieIMDbID(movieID);
            iMDBController.UpdateRating(IMDbId);
            GetMovieRatingFakeBreakdown(movieID);
        }
    }

    private List<SearchMovie> SearchMovieWithName(string movieName)
    {
        SearchContainer<SearchMovie> results = client.SearchMovieAsync(movieName).Result;

        //UnityEngine.Debug.Log($"Got {results.Results.Count:N0} of {results.TotalResults:N0} results");

        List<SearchMovie> movieList = new List<SearchMovie>();
        foreach (SearchMovie result in results.Results)
        {
            movieList.Add(result);
        }
        return movieList;
    }

    public List<SearchMovie> GetMovieRecommendationsWithID(int movieID)
    {
        SearchContainer<SearchMovie> results = client.GetMovieRecommendationsAsync(movieID).Result;

        //UnityEngine.Debug.Log($"Got {results.Results.Count:N0} of {results.TotalResults:N0} results");

        List<SearchMovie> movieList = new List<SearchMovie>();
        foreach (SearchMovie result in results.Results)
        {
            movieList.Add(result);
        }
        return movieList;
    }

    public bool CompareMoreMovieRecommendationsWithID(int movieID, int targetID)
    {
        for (int i = 0; i < 4; i++)
        {
            SearchContainer<SearchMovie> results = client.GetMovieRecommendationsAsync(movieID, page: i).Result;

            //UnityEngine.Debug.Log($"Got {results.Results.Count:N0} of {results.TotalResults:N0} results");

            foreach (SearchMovie result in results.Results)
            {
                if (result.Id == targetID)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public List<int> CompareMoreMovieRecommendationsWithID2(List<int> movieID, int targetID)
    {
        List<int> resultIDs = new List<int>();
        for (int i = 0; i < 10; i++)
        {
            SearchContainer<SearchMovie> results = client.GetMovieRecommendationsAsync(targetID, page: i).Result;

            //UnityEngine.Debug.Log($"Got {results.Results.Count:N0} of {results.TotalResults:N0} results");


            foreach (SearchMovie result in results.Results)
            {
                UnityEngine.Debug.Log("FFFFFound!: " + result.Id.ToString() + ", " + result.Title);
                if (movieID.Contains(result.Id) && !resultIDs.Contains(result.Id))
                {
                    resultIDs.Add(result.Id);
                }
            }
        }

        return resultIDs;
    }

    public bool started = false;
    private Dictionary<string, List<int>> genreMoviePairs = new Dictionary<string, List<int>>();
    public List<int> GetUserLikedGenreMovies(string genre)
    {

        return genreMoviePairs[genre];
    }

    public List<SearchMovie> GetMovieSimilarWithID(int movieID) // genre recommendation
    {
        SearchContainer<SearchMovie> results = client.GetMovieSimilarAsync(movieID).Result;

        //UnityEngine.Debug.Log($"Got {results.Results.Count:N0} of {results.TotalResults:N0} results");

        List<SearchMovie> movieList = new List<SearchMovie>();
        foreach (SearchMovie result in results.Results)
        {
            movieList.Add(result);
        }
        return movieList;
    }

    public bool CompareMoreMovieSimilarWithID(int movieID, int targetID) // genre recommendation
    {
        for (int i = 0; i < 3; i++)
        {
            SearchContainer<SearchMovie> results = client.GetMovieSimilarAsync(movieID, page: i).Result;

            //UnityEngine.Debug.Log($"Got {results.Results.Count:N0} of {results.TotalResults:N0} results");


            foreach (SearchMovie result in results.Results)
            {
                if (result.Id == targetID)
                {
                    return true;
                }
            }
        }
            
        return false;
    }

    public Dictionary<string, int> CompareMoreMovieSimilarWithID2(List<int> movieID, int targetID) // genre recommendation
    {
        started = true;
        genreMoviePairs = new Dictionary<string, List<int>>();

        Dictionary<string, int> genres = new Dictionary<string, int>();
        List<int> addedMovies = new List<int>();
        for (int i = 0; i < 10; i++)
        {
            SearchContainer<SearchMovie> results = client.GetMovieSimilarAsync(targetID, page: i).Result;

            //UnityEngine.Debug.Log($"Got {results.Results.Count:N0} of {results.TotalResults:N0} results");


            foreach (SearchMovie result in results.Results)
            {
                if (movieID.Contains(result.Id) && !addedMovies.Contains(result.Id))
                {
                    List<Genre> genreList = GetMovieGenres(result.Id);
                    int len = genreList.Count > 4 ? 4 : genreList.Count;
                    foreach (Genre genre in genreList.GetRange(0, len))
                    {
                        if (genres.ContainsKey(genre.Name))
                        {
                            genres[genre.Name]++;
                            genreMoviePairs[genre.Name].Add(result.Id);
                        }
                        else
                        {
                            genres.Add(genre.Name, 1);
                            genreMoviePairs.Add(genre.Name, new List<int>() { result.Id });
                        }
                    }
                    addedMovies.Add(result.Id);
                }
            }
        }

        return genres;
    }

    public Dictionary<string, int> CompareMoreMovieSimilarWithID3(List<int> movieID, int targetID) // genre recommendation
    {
        started = true;
        genreMoviePairs.Clear();

        Dictionary<string, int> genres = new Dictionary<string, int>();
        List<int> addedMovies = new List<int>();


        List<Genre> genreList = GetMovieGenres(targetID);
        int len = genreList.Count > 4 ? 4 : genreList.Count;
        List<Genre> targetGenreList = genreList.GetRange(0, len);

        foreach (int userLikedMovieId in movieID)
        {
            List<Genre> userLikedMovieGenreList = GetMovieGenres(userLikedMovieId);

            addedMovies.Clear();
            foreach (Genre genreLike in userLikedMovieGenreList)
            {
                foreach (Genre genreTarget in targetGenreList)
                {
                    if (genreLike.Id == genreTarget.Id)
                    {
                        // genre is the same

                        // add movie to List
                        if (!addedMovies.Contains(userLikedMovieId))
                        {
                            addedMovies.Add(userLikedMovieId);
                        }

                        // add movie to genre accumulated List
                        if (genres.ContainsKey(genreTarget.Name))
                        {
                            genres[genreTarget.Name]++;
                            genreMoviePairs[genreTarget.Name].Add(userLikedMovieId);
                        }
                        else
                        {
                            genres.Add(genreTarget.Name, 1);
                            genreMoviePairs.Add(genreTarget.Name, new List<int>() { userLikedMovieId });
                        }
                    }

                    if (addedMovies.Contains(userLikedMovieId))
                    {
                        break;
                    }
                }

                if (addedMovies.Contains(userLikedMovieId))
                {
                    break;
                }
            }

            // check if there is any genre with no number
            foreach (Genre genreLike in userLikedMovieGenreList)
            {
                if (!genres.ContainsKey(genreLike.Name))
                {
                    genres.Add(genreLike.Name, 0);
                    genreMoviePairs.Add(genreLike.Name, new List<int>());
                }
            }
        }

        return genres;
    }

    public List<Movie> GetMovieWithActorName(int movieID) // actor recommendation
    {
        List<Cast> casts = GetMovieCasts(movieID);
        List<int> personIDs = new List<int>();
        int castNum = 2;
        if (casts.Count < 2) castNum = casts.Count;
        for(int i = 0; i < castNum; i++)
        {
            personIDs.Add(casts[i].Id);
        }

        List<Movie> movieList = new List<Movie>();
        foreach (int personID in personIDs)
        {
            MovieCredits results = client.GetPersonMovieCreditsAsync(personID).Result;

            for(int i = 0; i < 8; i++)
            {
                Movie movie = client.GetMovieAsync(results.Cast[i].Id).Result;
                movieList.Add(movie);
            }
        }
        
        return movieList;
    }

    public List<SearchMovie> GetMoviePopularList()
    {
        SearchContainer<SearchMovie> results = client.GetMoviePopularListAsync().Result;

        //UnityEngine.Debug.Log($"Got {results.Results.Count:N0} of {results.TotalResults:N0} results");

        List<SearchMovie> movieList = new List<SearchMovie>();
        foreach (SearchMovie result in results.Results)
        {
            movieList.Add(result);
        }
        return movieList;
    }

    public List<SearchMovie> GetMovieWatchList(List<int> targetMovieIDs)
    {
        List<SearchMovie> movieList = new List<SearchMovie>();
        for(int movieIndex = 0; movieIndex < targetMovieIDs.Count; movieIndex++)
        {
            for (int i = 0; i < (movieIndex * 3 + 1); i++)
            {
                SearchContainer<SearchMovie> results = client.GetMovieSimilarAsync(targetMovieIDs[movieIndex], page: i).Result;
                //UnityEngine.Debug.Log($"Got {results.Results.Count:N0} of {results.TotalResults:N0} results");

                foreach (SearchMovie result in results.Results)
                {
                    if (result.VoteCount > 2000)
                    {
                        movieList.Add(result);
                    }
                }

                SearchContainer<SearchMovie> results2 = client.GetMovieRecommendationsAsync(targetMovieIDs[movieIndex], page: i).Result;
                //UnityEngine.Debug.Log($"Got {results2.Results.Count:N0} of {results2.TotalResults:N0} results");

                foreach (SearchMovie result in results2.Results)
                {
                    if (result.VoteCount > 100)
                    {
                        movieList.Add(result);
                    }
                }
            }
        }

        return movieList;
    }

    public List<int> GetActorActingMovie()
    {
        //List<Movie> movieList = new List<Movie>();
        //Person gal = client.GetPersonAsync(90633).Result;
        //UnityEngine.Debug.Log($"Got {results.Results.Count:N0} of {results.TotalResults:N0} results");

        /*
        for (int i = 0; i < 10; i++)
        {
            Movie movie = client.GetMovieAsync(gal.MovieCredits.Cast[i].Id).Result;
            movieList.Add(movie);
        }

        // 1253360
        
        Person pedro = client.GetPersonAsync(1253360).Result;
        //UnityEngine.Debug.Log($"Got {results.Results.Count:N0} of {results.TotalResults:N0} results");


        for (int i = 0; i < 10; i++)
        {
            Movie movie = client.GetMovieAsync(pedro.MovieCredits.Cast[i].Id).Result;
            movieList.Add(movie);
        }
        */

        return new List<int>() { 172385, 375588, 556574, 269149, 102382, 8355, 82702, 93456, 10191, 20352, 338766, 13475, 81188, 188927, 297762, 512195 , 505026 , 724209 , 141052 , 209112 , 146198 , 51497 , 302156 , 82992 , 168259 , 13804 , 724779 , 648579 , 464052 , 61537 , 86710 , 680016 , 345887 , 648579 };
    }

    public List<int> GetActorsTotalActedMovieNum()
    {
        return new List<int>() { 33 + 30 + 40, 132 };
    }

    public List<Cast> GetMovieCasts(int movieID)
    {
        Credits results = client.GetMovieCreditsAsync(movieID).Result;
        UnityEngine.Debug.Log(movieID);
        return results.Cast;
    }

    public List<Genre> GetMovieGenres(int movieID)
    {
        //List<Genre> genres = client.GetMovieGenresAsync().Result;
        Movie movie = client.GetMovieAsync(movieID).Result;

        return movie.Genres;
    }

    public double GetMovieRating(int movieID)
    {
        //List<Genre> genres = client.GetMovieGenresAsync().Result;
        Movie movie = client.GetMovieAsync(movieID).Result;

        return movie.VoteAverage;
    }

    public int GetMovieVoteCount(int movieID)
    {
        //List<Genre> genres = client.GetMovieGenresAsync().Result;
        Movie movie = client.GetMovieAsync(movieID).Result;

        return movie.VoteCount;
    }

    public List<ReviewBase> GetMovieReview(int movieID)
    {
        //List<Genre> genres = client.GetMovieGenresAsync().Result;
        SearchContainer<ReviewBase> results = client.GetMovieReviewsAsync(movieID).Result;

        //UnityEngine.Debug.Log($"Got {results.Results.Count:N0} of {results.TotalResults:N0} results");

        List<ReviewBase> review = new List<ReviewBase>();
        foreach (ReviewBase result in results.Results)
        {
            review.Add(result);
        }
        return review;
    }

    public List<SearchMovie> SearchForMoviesToAdd(string inputMovieName)
    {
        SearchContainer<SearchMovie> results = client.SearchMovieAsync(inputMovieName).Result;

        List<SearchMovie> moviesSearched = new List<SearchMovie>();
        foreach (SearchMovie result in results.Results)
        {
            moviesSearched.Add(result);
        }
        return moviesSearched;
    }

    public string GetMovieName(int movieID)
    {
        //List<Genre> genres = client.GetMovieGenresAsync().Result;
        UnityEngine.Debug.Log(movieID);
        Movie movie = client.GetMovieAsync(movieID).Result;
        return movie.Title;
    }

    public List<string> GetMovieTrailer(int movieID)
    {
        ResultContainer<Video> results = client.GetMovieVideosAsync(movieID).Result;
        
        List<string> keyList = new List<string>();
        foreach (Video result in results.Results)
        {
            keyList.Add(result.Key);
        }
        return keyList;
    }

    public string GetMovieDescription(int movieID)
    {
        //List<Genre> genres = client.GetMovieGenresAsync().Result;
        Movie movie = client.GetMovieAsync(movieID).Result;
        return movie.Overview;
    }

    public ProfileImages GetActorPosters(int personID)
    {
        //List<Genre> genres = client.GetMovieGenresAsync().Result;
        ProfileImages images = client.GetPersonImagesAsync(personID).Result;
        return images;
    }

    public ImagesWithId GetMoviePosters(int movieID)
    {
        //List<Genre> genres = client.GetMovieGenresAsync().Result;
        ImagesWithId images = client.GetMovieImagesAsync(movieID).Result;
        return images;
    }

    public Dictionary<int, string> movieIMDbIDPairs = new Dictionary<int, string>()
                                                    {
                                                        {508442, "tt2948372"},
                                                        {581392, "tt8850222"},
                                                        {464052, "tt7126948"},
                                                    };
    public string GetMovieIMDbID(int movieID)
    {
        //UnityEngine.Debug.Log(movieID);
        //List<Genre> genres = client.GetMovieGenresAsync().Result;
        Movie movie = client.GetMovieAsync(movieID).Result;
        return movie.ImdbId;
        //return movieIMDbIDPairs[movieID];
    }

    Dictionary<int, List<int>> movie_TMDb_Rating_Votes = new Dictionary<int, List<int>>();
    Dictionary<int, List<double>> movie_TMDb_Rating_Percent = new Dictionary<int, List<double>>();
    public void GetMovieRatingFakeBreakdown(int movieID)
    {
        double totalScore = GetMovieRating(movieID);
        int totalVotes = GetMovieVoteCount(movieID);

        System.Random rand = new System.Random();
        int[] votes = new int[10];
        int remaining = totalVotes;
        int score = 0;
        for (int i = 0; i < 10; i++)
        {
            if ((10 - i) == (int)totalScore)
            {
                votes[i] = totalVotes / 3;
            }
            else if ((10 - i) >= (int)totalScore)
            {
                int val = rand.Next(0, totalVotes / 5);
                votes[i] = val;
            }
            else 
            {
                int val;
                if (i != 9)
                {
                    int max = (int)(totalVotes * totalScore - score) / (10 - i) > 0 ? (int)(totalVotes * totalScore - score) / (10 - i) : 0;
                    int min = (int)max / 2  < max ? max : (int)max / 2;
                    
                    UnityEngine.Debug.Log(min);
                    UnityEngine.Debug.Log(max);
                    val = rand.Next(min, max);
                }
                else
                {
                    val = (int)(totalVotes * totalScore - score) / (10 - i) > 0 ? (int)(totalVotes * totalScore - score) / (10 - i) : 0;
                }
                votes[i] = val;
            }
            score += (10 - i) * votes[i];
            remaining -= votes[i];
        }

        List<int> voteList = new List<int>();
        List<double> precentList = new List<double>();
        foreach (int vote in votes)
        {
            voteList.Add(vote);
            precentList.Add((double) vote / totalVotes);
        }
        if (!movie_TMDb_Rating_Votes.ContainsKey(movieID))
        {
            movie_TMDb_Rating_Votes.Add(movieID, voteList);
            movie_TMDb_Rating_Percent.Add(movieID, precentList);
        }
    }

    public List<int> GetMovieRatingTMDbPercentCount(int movieID)
    {
        return movie_TMDb_Rating_Votes[movieID];
    }

    public List<double> GetMovieRatingTMDbPercent(int movieID)
    {
        return movie_TMDb_Rating_Percent[movieID];
    }

    Dictionary<int, List<int>> movie_Rotten_Rating_Votes = new Dictionary<int, List<int>>();
    Dictionary<int, List<double>> movie_Rotten_Rating_Percent = new Dictionary<int, List<double>>();
    public int GetMovieRottenVoteCount(int movieID)
    {
        return (int)(recommendationCalculator.GetMovieRatingIMDbVotes(movieID) * (2.0f / 3.0f));
    }
    public void GetMovieRatingFakeBreakdown_Rotten(int movieID)
    {
        double totalScore = recommendationCalculator.GetMovieRatingRottenTomato(movieID)/10.0f;
        int totalVotes = (int)(recommendationCalculator.GetMovieRatingIMDbVotes(movieID) * (2.0f / 3.0f));

        System.Random rand = new System.Random();
        int[] votes = new int[10];
        int remaining = totalVotes;
        int score = 0;
        for (int i = 0; i < 10; i++)
        {
            if ((10 - i) == (int)totalScore)
            {
                votes[i] = totalVotes / 3;
            }
            else if ((10 - i) >= (int)totalScore)
            {
                int val = rand.Next(0, totalVotes / 5);
                votes[i] = val;
            }
            else
            {
                int val;
                if (i != 9)
                {
                    int max = (int)(totalVotes * totalScore - score) / (10 - i) > 0 ? (int)(totalVotes * totalScore - score) / (10 - i) : 0;
                    int min = (int)max / 2 < max ? max : (int)max / 2;

                    UnityEngine.Debug.Log(min);
                    UnityEngine.Debug.Log(max);
                    val = rand.Next(min, max);
                }
                else
                {
                    val = (int)(totalVotes * totalScore - score) / (10 - i) > 0 ? (int)(totalVotes * totalScore - score) / (10 - i) : 0;
                }
                votes[i] = val;
            }
            score += (10 - i) * votes[i];
            remaining -= votes[i];
        }

        List<int> voteList = new List<int>();
        List<double> precentList = new List<double>();
        foreach (int vote in votes)
        {
            voteList.Add(vote);
            precentList.Add((double)vote / totalVotes);
        }
        if (!movie_Rotten_Rating_Votes.ContainsKey(movieID))
        {
            movie_Rotten_Rating_Votes.Add(movieID, voteList);
            movie_Rotten_Rating_Percent.Add(movieID, precentList);
        }
    }

    public List<int> GetMovieRatingRottenPercentCount(int movieID)
    {
        return movie_Rotten_Rating_Votes[movieID];
    }

    public List<double> GetMovieRatingRottenPercent(int movieID)
    {
        return movie_Rotten_Rating_Percent[movieID];
    }
}
