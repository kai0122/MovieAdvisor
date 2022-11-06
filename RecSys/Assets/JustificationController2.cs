using System.Collections;
using System.Collections.Generic;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Movies;
using TMDbLib.Objects.Reviews;
using TMDbLib.Objects.Search;
using UnityEngine;

public class JustificationController2 : MonoBehaviour
{
    // 493065, 508442, 464052, 581392, 601434
    public Dictionary<string, int> movieIDPairs = new Dictionary<string, int>()
                                                    {
                                                        {"Soul", 508442},
                                                        {"Peninsula", 581392},
                                                        {"WonderWoman1984", 464052},
                                                    };
    public TMDBApiController TMDBApiController;
    public IMDBController iMDBController;
    public ChildObjectsActivator childObjectsActivator;

    // recommendation movie posters
    private string posterImageBase = "https://image.tmdb.org/t/p/original//";
    private string youtubeVideoBase = "https://www.youtube.com/watch?v=";

    // target movie information
    public MovieInformationController movieInformationController;


    // target movie justification
    public SelectTargetMovieReadMore selectTargetMovieReadMore;
    public IconReturnDescription iconReturnDescription;
    public MovieDescriptionController movieDescriptionController;

    // target movie recommendation reason
    public SelectRecommendationWhy selectRecommendationWhy;
    public MovieRecommendationRatingDetailController movieRecommendationRatingDetailController;
    public SelectRecommendationRatingWhy selectRecommendationRatingWhy;
    public SelectRatingIMDBWhy selectRatingIMDBWhy;
    public SelectRatingTMDBWhy selectRatingTMDBWhy;
    public MovieRecommendationRatingDetailPercentController movieRecommendationRatingDetailPercentController;

    public SelectRecommendationPublicityWhy selectRecommendationPublicityWhy;
    public MovieRecommendationPublicityDetailController movieRecommendationPublicityDetailController;
    public SelectRecommendationPublicityMore selectRecommendationPublicityMore;
    public MovieRecommendationPublicityMoreController movieRecommendationPublicityMoreController;
    public IconPublicityReturn iconPublicityReturn;


    public SelectRecommendationActorWhy selectRecommendationActorWhy;
    public SelectRecommendationGenreWhy selectRecommendationGenreWhy;
    public MovieRecommendationActorDetailController movieRecommendationActorDetailController;
    public MovieRecommendationGenreDetailController movieRecommendationGenreDetailController;
    public MovieRecommendationActorMoreController movieRecommendationActorMoreController;
    public MovieRecommendationGenreMoreController movieRecommendationGenreMoreController;
    public MovieActorFullList1 movieActorFullList1;
    public MovieActorFullList2 movieActorFullList2;
    public MovieActorFullList3 movieActorFullList3;
    public MovieGenreFullList1 movieGenreFullList1;
    public MovieGenreFullList2 movieGenreFullList2;
    public MovieGenreFullList3 movieGenreFullList3;
    public MovieGenreFullList4 movieGenreFullList4;

    // target movie recommendation
    public RecommendationCalculator recommendationCalculator;
    public MovieRecommendationTitleController movieRecommendationTitleController;
    public MovieRecommendationSubTitleController movieRecommendationSubTitleController;

    // target movie recommendation full list
    

    // Start is called before the first frame update
    void Start()
    {
        foreach(int movieID in movieIDPairs.Values)
        {
            string IMDbId = TMDBApiController.GetMovieIMDbID(movieID);
            iMDBController.UpdateRating(IMDbId);
            TMDBApiController.GetMovieRatingFakeBreakdown(movieID);
        }
    }

    string previousTargetMovieName = "FirstName";
    bool previousHitTargetMovieReadMore = true;
    bool previousHitReturnDescription = true;
    bool previousHitRecommendationReasonWhy = true;
    bool previousHitRecommendationRatingWhy = true;
    bool previousHitRatingTMDbWhy = true;
    bool previousHitRatingIMDbWhy = true;
    bool previousHitPublicityWhy = true;
    bool previousHitPublicityMore = true;
    bool previousHitPublicityIconReturn = true;
    bool previousHitRecommendationGenreWhy = true;
    bool previousHitRecommendationActorWhy = true;
    bool previousHitRecommendationActorMore1 = true;
    bool previousHitRecommendationActorMore2 = true;
    bool previousHitRecommendationActorMore3 = true;
    bool previousHitRecommendationGenreMore1 = true;
    bool previousHitRecommendationGenreMore2 = true;
    bool previousHitRecommendationGenreMore3 = true;
    bool previousHitRecommendationGenreMore4 = true;
    bool swipeLeft = false;
    bool swipeRight = false;
    // Update is called once per frame
    void Update()
    {
        UpdateRecommendationBase();

        if (previousTargetMovieName != childObjectsActivator.currentTrackedName && childObjectsActivator.currentTrackedName != "")
        {
            selectTargetMovieReadMore.hitTargetMovieReadMore = false;
            selectRecommendationWhy.hitRecommendationScoreWhy = false;

            // update movie name
            previousTargetMovieName = childObjectsActivator.currentTrackedName;
            UpdateMovieInformation();

            weChangedATargetMovie = true;
            UpdateRecommendation();
        }

        if (previousHitTargetMovieReadMore != selectTargetMovieReadMore.hitTargetMovieReadMore)
        {
            // update
            previousHitTargetMovieReadMore = selectTargetMovieReadMore.hitTargetMovieReadMore;

            ShowTargetMovieDescription();
        }

        if (previousHitReturnDescription != iconReturnDescription.hitReturnDescriptionIcon)
        {
            // update
            previousHitReturnDescription = iconReturnDescription.hitReturnDescriptionIcon;

            ShowTargetMovieDescription();
        }

        if (previousHitRecommendationReasonWhy != selectRecommendationWhy.hitRecommendationScoreWhy)
        {
            // update
            previousHitRecommendationReasonWhy = selectRecommendationWhy.hitRecommendationScoreWhy;

            ShowRecommendationReason();
        }

        if (previousHitRecommendationRatingWhy != selectRecommendationRatingWhy.hitRecommendationRatingWhy)
        {
            previousHitRecommendationRatingWhy = selectRecommendationRatingWhy.hitRecommendationRatingWhy;

            ShowRecommendationRatingDetail();
        }

        if (previousHitRatingIMDbWhy != selectRatingIMDBWhy.hitRatingIMDbWhy)
        {
            previousHitRatingIMDbWhy = selectRatingIMDBWhy.hitRatingIMDbWhy;
            ShowRecommendationRatingDetailPercent("IMDb");
        }

        if (previousHitRatingTMDbWhy != selectRatingTMDBWhy.hitRatingTMDbWhy)
        {
            previousHitRatingTMDbWhy = selectRatingTMDBWhy.hitRatingTMDbWhy;
            ShowRecommendationRatingDetailPercent("TMDb");
        }

        if (previousHitPublicityWhy != selectRecommendationPublicityWhy.hitPublicityWhy)
        {
            previousHitPublicityWhy = selectRecommendationPublicityWhy.hitPublicityWhy;
            ShowRecommendationPublicityDetail();
        }

        if (previousHitPublicityMore != selectRecommendationPublicityMore.hitPublicityMore)
        {
            previousHitPublicityMore = selectRecommendationPublicityMore.hitPublicityMore;
            ShowRecommendationPublicityMoreMovies();
        }

        if (previousHitPublicityIconReturn != iconPublicityReturn.hitPublicityReturnIcon)
        {
            // update
            previousHitPublicityIconReturn = iconPublicityReturn.hitPublicityReturnIcon;

            ShowRecommendationPublicityMoreMovies();
        }

        if (previousHitRecommendationGenreWhy != selectRecommendationGenreWhy.hitRecommendationGenreWhy)
        {
            previousHitRecommendationGenreWhy = selectRecommendationGenreWhy.hitRecommendationGenreWhy;
            ShowRecommendationGenreDetail();
        }

        if (previousHitRecommendationActorWhy != selectRecommendationActorWhy.hitRecommendationActorWhy)
        {
            // update
            previousHitRecommendationActorWhy = selectRecommendationActorWhy.hitRecommendationActorWhy;

            ShowRecommendationActorDetail();
        }

        if (previousHitRecommendationActorMore1 != movieActorFullList1.hitMovieActorFullList)
        {
            // update
            previousHitRecommendationActorMore1 = movieActorFullList1.hitMovieActorFullList;

            ShowRecommendationActorMore(1);
        }

        if (previousHitRecommendationActorMore2 != movieActorFullList2.hitMovieActorFullList)
        {
            // update
            previousHitRecommendationActorMore2 = movieActorFullList2.hitMovieActorFullList;

            ShowRecommendationActorMore(2);
        }

        if (previousHitRecommendationActorMore3 != movieActorFullList3.hitMovieActorFullList)
        {
            // update
            previousHitRecommendationActorMore3 = movieActorFullList3.hitMovieActorFullList;

            ShowRecommendationActorMore(3);
        }

        if (previousHitRecommendationGenreMore1 != movieGenreFullList1.hitMovieGenreFullList)
        {
            // update
            previousHitRecommendationGenreMore1 = movieGenreFullList1.hitMovieGenreFullList;

            ShowRecommendationGenreMore(1);
        }

        if (previousHitRecommendationGenreMore2 != movieGenreFullList2.hitMovieGenreFullList)
        {
            // update
            previousHitRecommendationGenreMore2 = movieGenreFullList2.hitMovieGenreFullList;

            ShowRecommendationGenreMore(2);
        }

        if (previousHitRecommendationGenreMore3 != movieGenreFullList3.hitMovieGenreFullList)
        {
            // update
            previousHitRecommendationGenreMore3 = movieGenreFullList3.hitMovieGenreFullList;

            ShowRecommendationGenreMore(3);
        }

        if (previousHitRecommendationGenreMore4 != movieGenreFullList4.hitMovieGenreFullList)
        {
            // update
            previousHitRecommendationGenreMore4 = movieGenreFullList4.hitMovieGenreFullList;

            ShowRecommendationGenreMore(4);
        }

        /*
        if (previousHitRecommendationReasonActor != reasonSubScoreController.actorStarController.hitScoreStarControllerActor)
        {
            previousHitRecommendationReasonActor = reasonSubScoreController.actorStarController.hitScoreStarControllerActor;
            ShowRecommendationReason("Actor");
        }

        if (previousHitRecommendationReasonSimilar != reasonSubScoreController.similarStarController.hitScoreStarControllerSimilar)
        {
            previousHitRecommendationReasonSimilar = reasonSubScoreController.similarStarController.hitScoreStarControllerSimilar;
            ShowRecommendationReason("Similar");
        }

        if (previousHitRecommendationReasonRecommendation != reasonSubScoreController.recommendationStarController.hitScoreStarControllerRecommendation)
        {
            previousHitRecommendationReasonRecommendation = reasonSubScoreController.recommendationStarController.hitScoreStarControllerRecommendation;
            ShowRecommendationReason("Recommendation");
        }

        if (previousHitRecommendationReasonActorFullList != selectRecommendationFullListActor.hitRecommendationFullListActor)
        {
            previousHitRecommendationReasonActorFullList = selectRecommendationFullListActor.hitRecommendationFullListActor;
            ShowRecommendationReason("ActorFullList");
        }

        if (previousHitRecommendationReasonSimilarFullList != selectRecommendationFullListSimilar.hitRecommendationFullListSimilar)
        {
            previousHitRecommendationReasonSimilarFullList = selectRecommendationFullListSimilar.hitRecommendationFullListSimilar;
            ShowRecommendationReason("SimilarFullList");
        }

        if (previousHitRecommendationReasonRecommendationFullList != selectRecommendationFullListRecommendation.hitRecommendationFullListRecommendation)
        {
            previousHitRecommendationReasonRecommendationFullList = selectRecommendationFullListRecommendation.hitRecommendationFullListRecommendation;
            ShowRecommendationReason("RecommendationFullList");
        }
        */

    }

    List<Genre> genres;
    private void UpdateMovieInformation()
    {
        // update movie information
        movieInformationController.ChangeMovieName(TMDBApiController.GetMovieName(movieIDPairs[previousTargetMovieName]));
        List<Cast> casts = TMDBApiController.GetMovieCasts(movieIDPairs[previousTargetMovieName]);
        List<Genre> genres = TMDBApiController.GetMovieGenres(movieIDPairs[previousTargetMovieName]);
        
        int votingCount = TMDBApiController.GetMovieVoteCount(movieIDPairs[previousTargetMovieName]);
        List<string> urls = new List<string>();
        for (int i = 0; i < 4; i++) urls.Add(posterImageBase + TMDBApiController.GetActorPosters(casts[i].Id).Profiles[0].FilePath);
        movieInformationController.ChangeMovieInfo(TMDBApiController.GetMovieRating(movieIDPairs[previousTargetMovieName]), votingCount, genres, casts, urls, posterImageBase + TMDBApiController.GetMoviePosters(movieIDPairs[previousTargetMovieName]).Posters[0].FilePath);
    }

    private void ShowTargetMovieDescription()
    {
        if (previousHitReturnDescription == true)
        {
            movieDescriptionController.gameObject.SetActive(false);
            previousHitTargetMovieReadMore = false;
            selectTargetMovieReadMore.hitTargetMovieReadMore = false;
            previousHitReturnDescription = false;
            iconReturnDescription.hitReturnDescriptionIcon = false;
        }
        else
        {
            if (previousHitTargetMovieReadMore == true)
            {
                movieDescriptionController.gameObject.SetActive(true);
                string name = TMDBApiController.GetMovieName(movieIDPairs[previousTargetMovieName]);
                string description = TMDBApiController.GetMovieDescription(movieIDPairs[previousTargetMovieName]);

                // 370 words
                string result = "";
                foreach (string sentence in description.Split('.'))
                {
                    if ((result + sentence + ".").Length > 370)
                    {
                        break;
                    }
                    else
                    {
                        result += (sentence + ".");
                    }
                }


                movieDescriptionController.ChangeDescriptionName(name);
                movieDescriptionController.ChangeDescription(result);
            }
            else
            {
                movieDescriptionController.gameObject.SetActive(false);
            }
        }
    }

    private void UpdateRecommendation()
    {
        UpdateRecommendationScore();

        //int score = (int)(100.0f * ((actorScore + genreScore + publicityScore + ratingScore) / 4.0f));
        int sum = (actorScoreBase + genreScoreBase + publicityScoreBase + ratingScoreBase);
        double score = (actorScore * actorScoreBase + genreScore * genreScoreBase + publicityScore * publicityScoreBase + ratingScore * ratingScoreBase) / sum;
        //int passingScore = (actorScoreBase + genreScoreBase + publicityScoreBase + ratingScoreBase) / 4;

        movieRecommendationTitleController.ChangeRecommendationTitle(score, 0.5f);
    }


    List<Cast> reasonCasts = null;
    Dictionary<string, int> totalMovieGenres = null;
    Dictionary<string, int> reasonMovieGenres = new Dictionary<string, int>();
    int totalCastingCount = 0;
    int reasonCastingCount = 0;
    int totalMovieGenresNum = 0;
    int reasonMovieGenresNum = 0;
    List<int> reasonUserLikedMovies = null;
    bool weChangedATargetMovie = true;
    double actorScore;
    double genreScore;
    double publicityScore;
    double ratingScore;
    int actorScoreBase = 50;
    int genreScoreBase = 50;
    int publicityScoreBase = 50;
    int ratingScoreBase = 50;
    private void UpdateRecommendationScore()
    {
        // get reasons
        if (weChangedATargetMovie)
        {
            reasonCastingCount = 0;
            totalMovieGenresNum = 0;
            reasonMovieGenresNum = 0;

            reasonCasts = recommendationCalculator.GetSameActorMovies(movieIDPairs[previousTargetMovieName]);
            totalCastingCount = recommendationCalculator.GetActorsTotalActedMovieNum(movieIDPairs[previousTargetMovieName]);
            foreach (Cast actor in reasonCasts)
            {
                Debug.Log(actor.Name + ": get " + recommendationCalculator.GetUserLikedActorMovies(actor.Name).Count.ToString());
                reasonCastingCount += recommendationCalculator.GetUserLikedActorMovies(actor.Name).Count;
            }

            totalMovieGenres = recommendationCalculator.GetGenreCounts(movieIDPairs[previousTargetMovieName]);
            foreach (string genre in totalMovieGenres.Keys)
            {
                //Debug.Log(genre + ": get " + totalMovieGenres[genre].ToString());
                totalMovieGenresNum += totalMovieGenres[genre];
            }

            List<Genre> genres = TMDBApiController.GetMovieGenres(movieIDPairs[previousTargetMovieName]);
            int genreCount = genres.Count < 4 ? genres.Count : 4;
            foreach(Genre genre in genres.GetRange(0, genreCount))
            {
                if (totalMovieGenres.ContainsKey(genre.Name))
                {
                    reasonMovieGenres.Add(genre.Name, totalMovieGenres[genre.Name]);
                    reasonMovieGenresNum += totalMovieGenres[genre.Name];
                }
                else
                {
                    reasonMovieGenres.Add(genre.Name, 0);
                }
            }

            reasonUserLikedMovies = recommendationCalculator.GetLeadToRecommendationMovieIDs(movieIDPairs[previousTargetMovieName]);
            weChangedATargetMovie = false;
        }


        actorScore = (double)reasonCastingCount / totalCastingCount > 1 ? 1 : (double)reasonCastingCount / totalCastingCount;
        genreScore = (double)reasonMovieGenresNum / totalMovieGenresNum > 1 ? 1 : (double)reasonMovieGenresNum / totalMovieGenresNum;
        publicityScore = (double)reasonUserLikedMovies.Count / 60 > 1 ? 1 : (double)reasonUserLikedMovies.Count / 60;
        ratingScore = recommendationCalculator.GetRecommendationRating(movieIDPairs[previousTargetMovieName]);
        Debug.Log("Actor Score: " + actorScore.ToString());
        Debug.Log("Genre Score: " + genreScore.ToString());
        Debug.Log("Publicity Score: " + publicityScore.ToString());
        Debug.Log("Rating Score: " + ratingScore.ToString());

        //int score = (int)(100.0f * (actorScore + similarScore + recommendationScore) / 3.0f);
        //reasonScoreController.ChangeRecommendationScore(score);
    }

    private void ShowRecommendationReason()
    {
        if (previousHitRecommendationReasonWhy == true)
        {
            movieRecommendationSubTitleController.gameObject.SetActive(true);
            int sum = actorScoreBase + genreScoreBase + publicityScoreBase + ratingScoreBase;
            movieRecommendationSubTitleController.ChangeRecommendationSubTitleActor((int)(100 * actorScore), actorScoreBase, sum);
            movieRecommendationSubTitleController.ChangeRecommendationSubTitleGenre((int)(100 * genreScore), genreScoreBase, sum);
            movieRecommendationSubTitleController.ChangeRecommendationSubTitlePublicity((int)(100 * publicityScore), publicityScoreBase, sum);
            movieRecommendationSubTitleController.ChangeRecommendationSubTitleRating((int)(100 * ratingScore), ratingScoreBase, sum);
        }
        else
        {
            movieRecommendationSubTitleController.gameObject.SetActive(false);
            selectRecommendationActorWhy.hitRecommendationActorWhy = false;
            selectRecommendationGenreWhy.hitRecommendationGenreWhy = false;
            selectRecommendationPublicityWhy.hitPublicityWhy = false;
            selectRecommendationRatingWhy.hitRecommendationRatingWhy = false;
        }
    }

    private void ShowRecommendationRatingDetail()
    {
        if (previousHitRecommendationRatingWhy == true)
        {
            movieRecommendationRatingDetailController.gameObject.SetActive(true);
            movieRecommendationRatingDetailController.ChangeDetailRatingScore(recommendationCalculator.GetMovieRatingTMDb(movieIDPairs[previousTargetMovieName]),
                                                                              recommendationCalculator.GetMovieRatingIMDb(movieIDPairs[previousTargetMovieName]),
                                                                              recommendationCalculator.GetMovieRatingRottenTomato(movieIDPairs[previousTargetMovieName]),
                                                                              recommendationCalculator.GetMovieRatingMetacritic(movieIDPairs[previousTargetMovieName]));
            selectRatingTMDBWhy.gameObject.GetComponent<RatingPeopleController>().UpdateRatedPeople(recommendationCalculator.GetMovieRatingTMDbVotes(movieIDPairs[previousTargetMovieName]));
            selectRatingIMDBWhy.gameObject.GetComponent<RatingPeopleController>().UpdateRatedPeople(recommendationCalculator.GetMovieRatingIMDbVotes(movieIDPairs[previousTargetMovieName]));
        }
        else
        {
            movieRecommendationRatingDetailController.gameObject.SetActive(false);
            movieRecommendationRatingDetailPercentController.gameObject.SetActive(false);
            previousHitRatingIMDbWhy = false;
            previousHitRecommendationRatingWhy = false;
            selectRecommendationRatingWhy.hitRecommendationRatingWhy = false;
            selectRatingTMDBWhy.hitRatingTMDbWhy = false;
            selectRatingIMDBWhy.hitRatingIMDbWhy = false;
        }
    }

    private void ShowRecommendationRatingDetailPercent(string mode)
    {
        if (mode == "IMDb")
        {
            if (previousHitRatingIMDbWhy == true)
            {
                previousHitRatingTMDbWhy = false;
                selectRatingTMDBWhy.hitRatingTMDbWhy = false;
                movieRecommendationRatingDetailPercentController.gameObject.SetActive(true);
                for (int score = 10; score > 0; score--)
                {
                    movieRecommendationRatingDetailPercentController.ChangeRecommendationRatingDetail(score,
                                                                                                      recommendationCalculator.GetMovieRatingIMDbPercentCount(movieIDPairs[previousTargetMovieName], score),
                                                                                                      (double) recommendationCalculator.GetMovieRatingIMDbPercentCount(movieIDPairs[previousTargetMovieName], score) / recommendationCalculator.GetMovieRatingIMDbVotes(movieIDPairs[previousTargetMovieName]) ,
                                                                                                      TMDBApiController.GetMovieName(movieIDPairs[previousTargetMovieName]) + " | IMDb Rating");
                }

            }
            else
            {
                movieRecommendationRatingDetailPercentController.gameObject.SetActive(false);
            }
        }
        else
        {
            if (previousHitRatingTMDbWhy == true)
            {
                previousHitRatingIMDbWhy = false;
                selectRatingIMDBWhy.hitRatingIMDbWhy = false;
                movieRecommendationRatingDetailPercentController.gameObject.SetActive(true);
                for (int score = 10; score > 0; score--)
                {
                    movieRecommendationRatingDetailPercentController.ChangeRecommendationRatingDetail(score,
                                                                                                      recommendationCalculator.GetMovieRatingTMDbPercentCount(movieIDPairs[previousTargetMovieName], score),
                                                                                                      recommendationCalculator.GetMovieRatingTMDbPercent(movieIDPairs[previousTargetMovieName], score),
                                                                                                      TMDBApiController.GetMovieName(movieIDPairs[previousTargetMovieName]) + " | TMDb Rating");
                }

            }
            else
            {
                movieRecommendationRatingDetailPercentController.gameObject.SetActive(false);
            }
        }
    }

    private void ShowRecommendationPublicityDetail()
    {
        if (previousHitPublicityWhy == true)
        {
            movieRecommendationPublicityDetailController.gameObject.SetActive(true);
            if (reasonUserLikedMovies.Count == 1)
            {
                movieRecommendationPublicityDetailController.UpdatePoster(posterImageBase + TMDBApiController.GetMoviePosters(reasonUserLikedMovies[0]).Posters[0].FilePath);
                movieRecommendationPublicityDetailController.ChangeReasonMovies(TMDBApiController.GetMovieName(reasonUserLikedMovies[0]));
            }
            else if (reasonUserLikedMovies.Count == 2)
            {
                movieRecommendationPublicityDetailController.UpdatePoster(posterImageBase + TMDBApiController.GetMoviePosters(reasonUserLikedMovies[0]).Posters[0].FilePath,
                                                                          posterImageBase + TMDBApiController.GetMoviePosters(reasonUserLikedMovies[1]).Posters[0].FilePath);
                movieRecommendationPublicityDetailController.ChangeReasonMovies(TMDBApiController.GetMovieName(reasonUserLikedMovies[0]),
                                                                                TMDBApiController.GetMovieName(reasonUserLikedMovies[1]));
            }
            else if (reasonUserLikedMovies.Count >= 3)
            {
                movieRecommendationPublicityDetailController.UpdatePoster(posterImageBase + TMDBApiController.GetMoviePosters(reasonUserLikedMovies[0]).Posters[0].FilePath,
                                                                          posterImageBase + TMDBApiController.GetMoviePosters(reasonUserLikedMovies[1]).Posters[0].FilePath,
                                                                          posterImageBase + TMDBApiController.GetMoviePosters(reasonUserLikedMovies[2]).Posters[0].FilePath,
                                                                          reasonUserLikedMovies.Count);
                movieRecommendationPublicityDetailController.ChangeReasonMovies(TMDBApiController.GetMovieName(reasonUserLikedMovies[0]),
                                                                                TMDBApiController.GetMovieName(reasonUserLikedMovies[1]),
                                                                                TMDBApiController.GetMovieName(reasonUserLikedMovies[2]));
            }

            movieRecommendationPublicityDetailController.ChangeReasonTargetMovieName(TMDBApiController.GetMovieName(movieIDPairs[previousTargetMovieName]),
                                                                                     reasonUserLikedMovies.Count,
                                                                                     recommendationCalculator.GetUserLikedMovieNum());
        }
        else
        {
            movieRecommendationPublicityDetailController.gameObject.SetActive(false);
            movieRecommendationPublicityMoreController.gameObject.SetActive(false);
            previousHitPublicityIconReturn = false;
            selectRecommendationPublicityMore.hitPublicityMore = false;
            previousHitPublicityMore = false;
            iconPublicityReturn.hitPublicityReturnIcon = false;
        }
    }

    private void ShowRecommendationPublicityMoreMovies()
    {
        if (previousHitPublicityIconReturn == true)
        {
            movieRecommendationPublicityMoreController.gameObject.SetActive(false);
            previousHitPublicityIconReturn = false;
            selectRecommendationPublicityMore.hitPublicityMore = false;
            previousHitPublicityMore = false;
            iconPublicityReturn.hitPublicityReturnIcon = false;
        }
        else
        {
            if (previousHitPublicityMore == true)
            {
                movieRecommendationPublicityMoreController.gameObject.SetActive(true);

                List<string> names = new List<string>();
                foreach (int movieID in reasonUserLikedMovies)
                {
                    names.Add(TMDBApiController.GetMovieName(movieID));
                }

                Debug.Log("Publicity Count: " + names.Count.ToString());
                movieRecommendationPublicityMoreController.UpdateRecommendationFullList(names);
                movieRecommendationPublicityMoreController.UpdateRecommendationPublicityDetailTitle(reasonUserLikedMovies.Count);
            }
            else
            {
                movieRecommendationPublicityMoreController.gameObject.SetActive(false);
            }
        }
    }

    private void ShowRecommendationGenreDetail()
    {
        if (previousHitRecommendationGenreWhy == true)
        {
            movieRecommendationGenreDetailController.gameObject.SetActive(true);

            List<string> genres = new List<string>();
            foreach(string genre in reasonMovieGenres.Keys)
            {
                genres.Add(reasonMovieGenres[genre] + " " + genre + " movies");
            }
            movieRecommendationGenreDetailController.ChangeGenreText(genres, totalMovieGenresNum);
        }
        else
        {
            movieRecommendationGenreDetailController.gameObject.SetActive(false);
            previousHitRecommendationGenreMore1 = false;
            movieGenreFullList1.hitMovieGenreFullList = false;
            previousHitRecommendationGenreMore2 = false;
            movieGenreFullList2.hitMovieGenreFullList = false;
            previousHitRecommendationGenreMore3 = false;
            movieGenreFullList3.hitMovieGenreFullList = false;
            previousHitRecommendationGenreMore4 = false;
            movieGenreFullList4.hitMovieGenreFullList = false;
            movieRecommendationGenreMoreController.gameObject.SetActive(false);
        }
    }

    private void ShowRecommendationActorDetail()
    {
        if (previousHitRecommendationActorWhy == true)
        {
            movieRecommendationActorDetailController.gameObject.SetActive(true);

            if (reasonCasts.Count == 1)
            {
                movieRecommendationActorDetailController.UpdateName(reasonCasts[0].Name);
                movieRecommendationActorDetailController.UpdatePoster(posterImageBase + TMDBApiController.GetActorPosters(reasonCasts[0].Id).Profiles[0].FilePath);
                movieRecommendationActorDetailController.UpdateMovieNumber(recommendationCalculator.GetUserLikedActorMovies(reasonCasts[0].Name).Count.ToString());
            }
            else if (reasonCasts.Count == 2)
            {
                movieRecommendationActorDetailController.UpdateName(reasonCasts[0].Name, reasonCasts[1].Name);
                movieRecommendationActorDetailController.UpdatePoster(posterImageBase + TMDBApiController.GetActorPosters(reasonCasts[0].Id).Profiles[0].FilePath,
                                                                      posterImageBase + TMDBApiController.GetActorPosters(reasonCasts[1].Id).Profiles[0].FilePath);
                movieRecommendationActorDetailController.UpdateMovieNumber(recommendationCalculator.GetUserLikedActorMovies(reasonCasts[0].Name).Count.ToString(),
                                                                           recommendationCalculator.GetUserLikedActorMovies(reasonCasts[1].Name).Count.ToString());
            }
            else if (reasonCasts.Count >= 3)
            {
                movieRecommendationActorDetailController.UpdateName(reasonCasts[0].Name, reasonCasts[1].Name, reasonCasts[2].Name);
                movieRecommendationActorDetailController.UpdatePoster(posterImageBase + TMDBApiController.GetActorPosters(reasonCasts[0].Id).Profiles[0].FilePath,
                                                                      posterImageBase + TMDBApiController.GetActorPosters(reasonCasts[1].Id).Profiles[0].FilePath,
                                                                      posterImageBase + TMDBApiController.GetActorPosters(reasonCasts[2].Id).Profiles[0].FilePath);
                movieRecommendationActorDetailController.UpdateMovieNumber(recommendationCalculator.GetUserLikedActorMovies(reasonCasts[0].Name).Count.ToString(),
                                                                           recommendationCalculator.GetUserLikedActorMovies(reasonCasts[1].Name).Count.ToString(),
                                                                           recommendationCalculator.GetUserLikedActorMovies(reasonCasts[2].Name).Count.ToString());
            }
        }
        else
        {
            movieRecommendationActorDetailController.gameObject.SetActive(false);
            previousHitRecommendationActorMore1 = false;
            movieActorFullList1.hitMovieActorFullList = false;
            previousHitRecommendationActorMore2 = false;
            movieActorFullList2.hitMovieActorFullList = false;
            previousHitRecommendationActorMore3 = false;
            movieActorFullList3.hitMovieActorFullList = false;
            movieRecommendationActorMoreController.gameObject.SetActive(false);
        }
    }

    private void ShowRecommendationActorMore(int mode)
    {
        if (mode == 1)
        {
            if (previousHitRecommendationActorMore1 == true)
            {
                previousHitRecommendationActorMore2 = false;
                movieActorFullList2.hitMovieActorFullList = false;
                previousHitRecommendationActorMore3 = false;
                movieActorFullList3.hitMovieActorFullList = false;
                movieRecommendationActorMoreController.gameObject.SetActive(true);
                movieRecommendationActorMoreController.UpdateRecommendationActorDetailTitle(recommendationCalculator.GetUserLikedActorMovies(reasonCasts[0].Name).Count);
                movieRecommendationActorMoreController.UpdateRecommendationFullList(recommendationCalculator.GetUserLikedActorMovies(reasonCasts[0].Name));
            }
            else
            {
                movieRecommendationActorMoreController.gameObject.SetActive(false);
            }
        }
        else if (mode == 2)
        {
            if (previousHitRecommendationActorMore2 == true)
            {
                previousHitRecommendationActorMore1 = false;
                movieActorFullList1.hitMovieActorFullList = false;
                previousHitRecommendationActorMore3 = false;
                movieActorFullList3.hitMovieActorFullList = false;
                movieRecommendationActorMoreController.gameObject.SetActive(true);
                movieRecommendationActorMoreController.UpdateRecommendationActorDetailTitle(recommendationCalculator.GetUserLikedActorMovies(reasonCasts[1].Name).Count);
                movieRecommendationActorMoreController.UpdateRecommendationFullList(recommendationCalculator.GetUserLikedActorMovies(reasonCasts[1].Name));
            }
            else
            {
                movieRecommendationActorMoreController.gameObject.SetActive(false);
            }
        }
        else
        {
            if (previousHitRecommendationActorMore3 == true)
            {
                previousHitRecommendationActorMore2 = false;
                movieActorFullList2.hitMovieActorFullList = false;
                previousHitRecommendationActorMore1 = false;
                movieActorFullList1.hitMovieActorFullList = false;
                movieRecommendationActorMoreController.gameObject.SetActive(true);
                movieRecommendationActorMoreController.UpdateRecommendationActorDetailTitle(recommendationCalculator.GetUserLikedActorMovies(reasonCasts[2].Name).Count);
                movieRecommendationActorMoreController.UpdateRecommendationFullList(recommendationCalculator.GetUserLikedActorMovies(reasonCasts[2].Name));
            }
            else
            {
                movieRecommendationActorMoreController.gameObject.SetActive(false);
            }
        }
    }

    private void ShowRecommendationGenreMore(int mode)
    {
        if (mode == 1)
        {
            if (previousHitRecommendationGenreMore1 == true)
            {
                previousHitRecommendationGenreMore2 = false;
                movieGenreFullList2.hitMovieGenreFullList = false;
                previousHitRecommendationGenreMore3 = false;
                movieGenreFullList3.hitMovieGenreFullList = false;
                previousHitRecommendationGenreMore4 = false;
                movieGenreFullList4.hitMovieGenreFullList = false;
                movieRecommendationGenreMoreController.gameObject.SetActive(true);

                List<string> genres = new List<string>();
                foreach (string genre in reasonMovieGenres.Keys)
                {
                    genres.Add(genre);
                }
                movieRecommendationGenreMoreController.UpdateRecommendationGenreDetailTitle(reasonMovieGenres[genres[mode - 1]], genres[mode - 1]);
                movieRecommendationGenreMoreController.UpdateRecommendationFullList(recommendationCalculator.GetUserLikedGenreMovieNames(genres[mode - 1]));
            }
            else
            {
                movieRecommendationGenreMoreController.gameObject.SetActive(false);
            }
        }
        else if (mode == 2)
        {
            if (previousHitRecommendationGenreMore2 == true)
            {
                previousHitRecommendationGenreMore1 = false;
                movieGenreFullList1.hitMovieGenreFullList = false;
                previousHitRecommendationGenreMore3 = false;
                movieGenreFullList3.hitMovieGenreFullList = false;
                previousHitRecommendationGenreMore4 = false;
                movieGenreFullList4.hitMovieGenreFullList = false;
                movieRecommendationGenreMoreController.gameObject.SetActive(true);

                List<string> genres = new List<string>();
                foreach (string genre in reasonMovieGenres.Keys)
                {
                    genres.Add(genre);
                }
                movieRecommendationGenreMoreController.UpdateRecommendationGenreDetailTitle(reasonMovieGenres[genres[mode - 1]], genres[mode - 1]);
                movieRecommendationGenreMoreController.UpdateRecommendationFullList(recommendationCalculator.GetUserLikedGenreMovieNames(genres[mode - 1]));
            }
            else
            {
                movieRecommendationGenreMoreController.gameObject.SetActive(false);
            }
        }
        else if (mode == 3)
        {

            if (previousHitRecommendationGenreMore3 == true)
            {
                previousHitRecommendationGenreMore2 = false;
                movieGenreFullList2.hitMovieGenreFullList = false;
                previousHitRecommendationGenreMore1 = false;
                movieGenreFullList1.hitMovieGenreFullList = false;
                previousHitRecommendationGenreMore4 = false;
                movieGenreFullList4.hitMovieGenreFullList = false;
                movieRecommendationGenreMoreController.gameObject.SetActive(true);

                List<string> genres = new List<string>();
                foreach (string genre in reasonMovieGenres.Keys)
                {
                    genres.Add(genre);
                }
                movieRecommendationGenreMoreController.UpdateRecommendationGenreDetailTitle(reasonMovieGenres[genres[mode - 1]], genres[mode - 1]);
                movieRecommendationGenreMoreController.UpdateRecommendationFullList(recommendationCalculator.GetUserLikedGenreMovieNames(genres[mode - 1]));
            }
            else
            {
                movieRecommendationGenreMoreController.gameObject.SetActive(false);
            }
        }
        else
        {

            if (previousHitRecommendationGenreMore4 == true)
            {
                previousHitRecommendationGenreMore2 = false;
                movieGenreFullList2.hitMovieGenreFullList = false;
                previousHitRecommendationGenreMore3 = false;
                movieGenreFullList3.hitMovieGenreFullList = false;
                previousHitRecommendationGenreMore1 = false;
                movieGenreFullList1.hitMovieGenreFullList = false;
                movieRecommendationGenreMoreController.gameObject.SetActive(true);

                List<string> genres = new List<string>();
                foreach (string genre in reasonMovieGenres.Keys)
                {
                    genres.Add(genre);
                }
                movieRecommendationGenreMoreController.UpdateRecommendationGenreDetailTitle(reasonMovieGenres[genres[mode - 1]], genres[mode - 1]);
                movieRecommendationGenreMoreController.UpdateRecommendationFullList(recommendationCalculator.GetUserLikedGenreMovieNames(genres[mode - 1]));
            }
            else
            {
                movieRecommendationGenreMoreController.gameObject.SetActive(false);
            }
        }
    }

    public FilterNumberHolder filterNumberHolder;
    public void UpdateRecommendationBase()
    {
        int new_actorScoreBase = filterNumberHolder.actorBase;
        int new_genreScoreBase = filterNumberHolder.genreBase;
        int new_publicityScoreBase = filterNumberHolder.publicityBase;
        int new_ratingScoreBase = filterNumberHolder.ratingBase;

        if (actorScoreBase != new_actorScoreBase ||
            genreScoreBase != new_genreScoreBase ||
            publicityScoreBase != new_publicityScoreBase ||
            ratingScoreBase != new_ratingScoreBase)
        {
            actorScoreBase = new_actorScoreBase;
            genreScoreBase = new_genreScoreBase;
            publicityScoreBase = new_publicityScoreBase;
            ratingScoreBase = new_ratingScoreBase;

            UpdateRecommendation();
            selectRecommendationWhy.hitRecommendationScoreWhy = false;
        }

        
    }
}
