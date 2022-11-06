using System.Collections;
using System.Collections.Generic;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Movies;
using TMDbLib.Objects.Reviews;
using TMDbLib.Objects.Search;
using UnityEngine;

public class JustificationController : MonoBehaviour
{
    // 493065, 508442, 464052, 581392, 601434
    public Dictionary<string, int> movieIDPairs = new Dictionary<string, int>()
                                                    {
                                                        {"CutThroatCity", 493065},
                                                        {"Soul", 508442},
                                                        {"WonderWoman1984", 464052},
                                                        {"Peninsula", 581392},
                                                    };
    public TMDBApiController TMDBApiController;
    public ChildObjectsActivator childObjectsActivator;

    // recommendation movie posters
    private string posterImageBase = "https://image.tmdb.org/t/p/original//";
    private string youtubeVideoBase = "https://www.youtube.com/watch?v=";
    public ImageLoader moviePoster1;
    public ImageLoader moviePoster2;
    public ImageLoader moviePoster3;
    private int currentLeftMovieIndex = 0;
    private int currentMiddleMovieIndex = 1;
    private int currentRightMovieIndex = 2;
    public SelectRecommendationMoreMovies selectRecommendationMoreMovies;

    // target movie information
    public MovieInformationController movieInformationController;

    // target movie review
    public IconShowReview iconShowReview;
    public MovieReviewController movieReviewController;
    private int currentReview = 0;

    // target movie justification
    public IconShowJustification iconShowJustification;
    public MovieJustificationController movieJustificationController;
    public SelectTargetMovieReadMore selectTargetMovieReadMore;
    public IconReturnDescription iconReturnDescription;
    public MovieDescriptionController movieDescriptionController;

    // movie recommendation setting
    public IconShowSetting iconShowSetting;
    public SettingController settingController;
    private string currentRecommendationInput = "Movie";
    public SelectRecommendationActor selectRecommendationActor;
    public SelectRecommendationGenre selectRecommendationGenre;
    public SelectRecommendationMovie selectRecommendationMovie;

    // target movie trailer
    public VideoLoader videoLoaderTargetMovie;

    // target movie recommendation
    public RecommendationCalculator recommendationCalculator;
    public ReasonActorController reasonActorController;
    public ReasonSimilarController reasonSimilarController;
    public ReasonRecommendationController reasonRecommendationController;

    // target movie recommendation score
    public SelectRecommendationWhy selectRecommendationWhy;
    public ReasonScoreController reasonScoreController;
    public ReasonSubScoreController reasonSubScoreController;

    // target movie recommendation full list
    public SelectRecommendationFullList selectRecommendationFullListActor;
    public SelectRecommendationFullList selectRecommendationFullListSimilar;
    public SelectRecommendationFullList selectRecommendationFullListRecommendation;
    public ReasonActorFullListController reasonActorFullListController;
    public ReasonSimilarFullListController reasonSimilarFullListController;
    public ReasonRecommendationFullListController reasonRecommendationFullListController;

    // movie watching history
    public MovieWatchingHistoryController movieWatchingHistoryController;
    public SelectMovieWatchingHistory selectMovieWatchingHistory;
    public IconHistoryNextPage iconHistoryNextPage;
    public IconHistoryPrevPage iconHistoryPrevPage;

    // recommendation movie swiping
    public MovieSwipingController movieSwipingController;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    string previousTargetMovieName = "FirstName";
    bool previousHitShowReview = true;
    bool previousHitShowJustification = true;
    bool previousHitShowSetting = true;
    bool previousSettingActor = false;
    bool previousSettingGenre = false;
    bool previousSettingMovie = false;
    bool previousHitTargetMovieReadMore = true;
    bool previousHitReturnDescription = true;
    bool previousHitRecommendationReasonWhy = true;
    bool previousHitRecommendationReasonActor = true;
    bool previousHitRecommendationReasonSimilar = true;
    bool previousHitRecommendationReasonRecommendation = true;
    bool previousHitRecommendationReasonActorFullList = true;
    bool previousHitRecommendationReasonSimilarFullList = true;
    bool previousHitRecommendationReasonRecommendationFullList = true;
    bool previousHitSelectRecommendationMoreMovies = true;
    bool previousHitMovieWatchingHistory = true;
    bool previousHitHistoryNextPage = true;
    bool previousHitHistoryPrevPage = true;
    bool swipeLeft = false;
    bool swipeRight = false;
    // Update is called once per frame
    void Update()
    {
        if (previousTargetMovieName != childObjectsActivator.currentTrackedName)
        {
            // update movie name
            previousTargetMovieName = childObjectsActivator.currentTrackedName;
            UpdateMovieInformation();
            
            weChangedATargetMovie = true;
            UpdateRecommendationScore();
        }

        if (previousHitSelectRecommendationMoreMovies != selectRecommendationMoreMovies.hitRecommendationMoreMovies)
        {
            previousHitSelectRecommendationMoreMovies = selectRecommendationMoreMovies.hitRecommendationMoreMovies;
            if (previousHitSelectRecommendationMoreMovies == true)
            {
                movieSwipingController.gameObject.SetActive(true);

                // show movie recommendation list
                if (currentRecommendationInput == "Actor")
                {
                    UpdateRecommendationActor();
                }
                else if (currentRecommendationInput == "Genre")
                {
                    UpdateRecommendationGenre();
                }
                else
                {
                    UpdateRecommendationMovie();
                }
            }
            else
            {
                movieSwipingController.gameObject.SetActive(false);
            }
        }

        if (previousHitShowReview != iconShowReview.hitShowReviewIcon)
        {
            // update
            previousHitShowReview = iconShowReview.hitShowReviewIcon;

            UpdateReview();
        }

        if (previousHitShowJustification != iconShowJustification.hitShowJustificationIcon)
        {
            // update
            previousHitShowJustification = iconShowJustification.hitShowJustificationIcon;

            UpdateJustification();
        }

        if (previousHitShowSetting != iconShowSetting.hitShowSettingIcon)
        {
            // update
            previousHitShowSetting = iconShowSetting.hitShowSettingIcon;

            ShowSetting();
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

            ShowRecommendationSubScore();
        }

#pragma warning disable CS0618 // 類型或成員已經過時
        if (settingController.gameObject.active)
#pragma warning restore
        {
            if (previousSettingActor != selectRecommendationActor.hitSettingActor)
            {
                currentRecommendationInput = "Actor";

                previousSettingActor = false;
                previousSettingGenre = false;
                previousSettingMovie = false;
                selectRecommendationActor.hitSettingActor = false;
                selectRecommendationGenre.hitSettingGenre = false;
                selectRecommendationMovie.hitSettingMovie = false;

                // update
                previousHitShowSetting = false;
                iconShowSetting.hitShowSettingIcon = false;
                ShowSetting();

                previousHitShowJustification = false;
                iconShowJustification.hitShowJustificationIcon = false;
                UpdateJustification();

                previousHitShowReview = false;
                iconShowReview.hitShowReviewIcon = false;
                UpdateReview();

                previousHitTargetMovieReadMore = false;
                selectTargetMovieReadMore.hitTargetMovieReadMore = false;
                ShowTargetMovieDescription();

                UpdateRecommendationActor();

            }
            if (previousSettingGenre != selectRecommendationGenre.hitSettingGenre)
            {
                currentRecommendationInput = "Genre";

                previousSettingActor = false;
                previousSettingGenre = false;
                previousSettingMovie = false;
                selectRecommendationActor.hitSettingActor = false;
                selectRecommendationGenre.hitSettingGenre = false;
                selectRecommendationMovie.hitSettingMovie = false;

                // update
                previousHitShowSetting = false;
                iconShowSetting.hitShowSettingIcon = false;
                ShowSetting();

                previousHitShowJustification = false;
                iconShowJustification.hitShowJustificationIcon = false;
                UpdateJustification();

                previousHitShowReview = false;
                iconShowReview.hitShowReviewIcon = false;
                UpdateReview();

                previousHitTargetMovieReadMore = false;
                selectTargetMovieReadMore.hitTargetMovieReadMore = false;
                ShowTargetMovieDescription();

                UpdateRecommendationGenre();
            }
            if (previousSettingMovie != selectRecommendationMovie.hitSettingMovie)
            {
                currentRecommendationInput = "Movie";

                previousSettingActor = false;
                previousSettingGenre = false;
                previousSettingMovie = false;
                selectRecommendationActor.hitSettingActor = false;
                selectRecommendationGenre.hitSettingGenre = false;
                selectRecommendationMovie.hitSettingMovie = false;

                // update
                previousHitShowSetting = false;
                iconShowSetting.hitShowSettingIcon = false;
                ShowSetting();

                previousHitShowJustification = false;
                iconShowJustification.hitShowJustificationIcon = false;
                UpdateJustification();

                previousHitShowReview = false;
                iconShowReview.hitShowReviewIcon = false;
                UpdateReview();

                previousHitTargetMovieReadMore = false;
                selectTargetMovieReadMore.hitTargetMovieReadMore = false;
                ShowTargetMovieDescription();

                UpdateRecommendationMovie();
            }
        }

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

        if (previousHitMovieWatchingHistory != selectMovieWatchingHistory.hitMovieWatchingHistory)
        {
            previousHitMovieWatchingHistory = selectMovieWatchingHistory.hitMovieWatchingHistory;
            ShowMovieWatchingHistory();
        }

        if (previousHitHistoryNextPage != iconHistoryNextPage.hitHistoryNextPage)
        {
            previousHitHistoryNextPage = iconHistoryNextPage.hitHistoryNextPage;
            UpdateMovieWatchingHistory("Next");
        }

        if (previousHitHistoryPrevPage != iconHistoryPrevPage.hitHistoryPrevPage)
        {
            previousHitHistoryPrevPage = iconHistoryPrevPage.hitHistoryPrevPage;
            UpdateMovieWatchingHistory("Prev");
        }

        if (movieSwipingController.deltaXRecommendationMovie > 0)
        {
            swipeLeft = true;
        }
        else if (movieSwipingController.deltaXRecommendationMovie < 0)
        {
            swipeRight = true;
        }
        else
        {
            if (swipeLeft)
            {
                swipeLeft = false;
                RecommendationMovieSwipeLeft();
            }
            else if (swipeRight)
            {
                swipeRight = false;
                RecommendationMovieSwipeRight();
            }
        }
    }

    private void RecommendationMovieSwipeLeft()
    {
        currentLeftMovieIndex = currentRightMovieIndex;
        currentMiddleMovieIndex = currentRightMovieIndex + 1 >= recommendationMovieList.Count ? 0 : currentRightMovieIndex + 1;
        currentRightMovieIndex = currentRightMovieIndex + 2 >= recommendationMovieList.Count ? 1 : currentRightMovieIndex + 2;

        // update recommended movies
        moviePoster1.ChangeImage(posterImageBase + recommendationMovieList[currentLeftMovieIndex].PosterPath);
        moviePoster2.ChangeImage(posterImageBase + recommendationMovieList[currentMiddleMovieIndex].PosterPath);
        moviePoster3.ChangeImage(posterImageBase + recommendationMovieList[currentRightMovieIndex].PosterPath);
    }

    private void RecommendationMovieSwipeRight()
    {
        currentRightMovieIndex = currentLeftMovieIndex;
        currentMiddleMovieIndex = currentLeftMovieIndex - 1 < 0 ? recommendationMovieList.Count - 1 : currentLeftMovieIndex - 1;
        currentLeftMovieIndex = currentLeftMovieIndex - 2 < 0 ? recommendationMovieList.Count - 2 : currentLeftMovieIndex - 2;
        
        // update recommended movies
        moviePoster1.ChangeImage(posterImageBase + recommendationMovieList[currentLeftMovieIndex].PosterPath);
        moviePoster2.ChangeImage(posterImageBase + recommendationMovieList[currentMiddleMovieIndex].PosterPath);
        moviePoster3.ChangeImage(posterImageBase + recommendationMovieList[currentRightMovieIndex].PosterPath);
    }

    List<SearchMovie> recommendationMovieList;
    private void UpdateRecommendationMovie()
    {
        recommendationMovieList = TMDBApiController.GetMovieRecommendationsWithID(movieIDPairs[previousTargetMovieName]);
        foreach(SearchMovie movie in recommendationMovieList)
        {
            Debug.Log(movie.PosterPath);
        }

        // update recommended movies
        moviePoster1.ChangeImage(posterImageBase + recommendationMovieList[currentLeftMovieIndex].PosterPath);
        moviePoster2.ChangeImage(posterImageBase + recommendationMovieList[currentMiddleMovieIndex].PosterPath);
        moviePoster3.ChangeImage(posterImageBase + recommendationMovieList[currentRightMovieIndex].PosterPath);

        /*
        foreach (string key in TMDBApiController.GetMovieTrailer(movieIDPairs[previousTargetMovieName]))
        {
            if (videoLoaderTargetMovie.ChangeVideo(youtubeVideoBase + key))
            {
                break;
            }
        }
        */
    }

    private void UpdateMovieInformation()
    {
        // update movie information
        movieInformationController.ChangeMovieName(TMDBApiController.GetMovieName(movieIDPairs[previousTargetMovieName]));
        List<Cast> casts = TMDBApiController.GetMovieCasts(movieIDPairs[previousTargetMovieName]);
        List<Genre> genres = TMDBApiController.GetMovieGenres(movieIDPairs[previousTargetMovieName]);
        double rating = TMDBApiController.GetMovieRating(movieIDPairs[previousTargetMovieName]);
        int votingCount = TMDBApiController.GetMovieVoteCount(movieIDPairs[previousTargetMovieName]);
        movieInformationController.ChangeMovieInfo(rating, votingCount, genres, casts, new List<string>(), "");
    }

    private void UpdateRecommendationGenre()
    {
        List<SearchMovie> recommendationMovieList = TMDBApiController.GetMovieSimilarWithID(movieIDPairs[previousTargetMovieName]);
        foreach (SearchMovie movie in recommendationMovieList)
        {
            Debug.Log(movie.PosterPath);
        }

        // update recommended movies
        moviePoster1.ChangeImage(posterImageBase + recommendationMovieList[currentLeftMovieIndex].PosterPath);
        moviePoster2.ChangeImage(posterImageBase + recommendationMovieList[currentMiddleMovieIndex].PosterPath);
        moviePoster3.ChangeImage(posterImageBase + recommendationMovieList[currentRightMovieIndex].PosterPath);
    }

    private void UpdateRecommendationActor()
    {
        List<Movie> recommendationMovieList = TMDBApiController.GetMovieWithActorName(movieIDPairs[previousTargetMovieName]);
        foreach (Movie movie in recommendationMovieList)
        {
            Debug.Log(movie.PosterPath);
        }

        // update recommended movies
        moviePoster1.ChangeImage(posterImageBase + recommendationMovieList[currentLeftMovieIndex].PosterPath);
        moviePoster2.ChangeImage(posterImageBase + recommendationMovieList[currentMiddleMovieIndex].PosterPath);
        moviePoster3.ChangeImage(posterImageBase + recommendationMovieList[currentRightMovieIndex].PosterPath);
    }

    private void UpdateReview()
    {
        if (previousHitShowReview == true)
        {
            movieReviewController.gameObject.SetActive(true);
            List<ReviewBase> reviews = TMDBApiController.GetMovieReview(movieIDPairs[previousTargetMovieName]);

            // 250 words
            string result = "";
            foreach (string sentence in reviews[currentReview].Content.Split('.'))
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

            movieReviewController.ChangeReviewName(reviews[currentReview].Author);
            movieReviewController.ChangeReviewInfo(result);
        }
        else
        {
            movieReviewController.gameObject.SetActive(false);
        }
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

    private void UpdateJustification()
    {
        if (previousHitShowJustification == true)
        {
            movieJustificationController.gameObject.SetActive(true);
            movieJustificationController.ChangeJustification(currentRecommendationInput);
        }
        else
        {
            movieJustificationController.gameObject.SetActive(false);
        }
    }

    private void ShowSetting()
    {
        if (previousHitShowSetting == true)
        {
            settingController.gameObject.SetActive(true);
            settingController.ChangeTextColor(currentRecommendationInput);
            settingController.ChangeIconPosition(currentRecommendationInput);
        }
        else
        {
            settingController.gameObject.SetActive(false);
        }
    }

    List<Cast> reasonCasts = null;
    List<int> reasonSimilarMovies = null;
    List<int> reasonUserLikedMovies = null;
    bool weChangedATargetMovie = true;
    double actorScore;
    double similarScore;
    double recommendationScore;
    private void UpdateRecommendationScore()
    {
        // get reasons
        if (weChangedATargetMovie)
        {
            reasonCasts = recommendationCalculator.GetSameActorMovies(movieIDPairs[previousTargetMovieName]);
            reasonCasts = reasonCasts.Count > 10 ? reasonCasts.GetRange(0, 10) : reasonCasts;
            reasonSimilarMovies = recommendationCalculator.GetSameGenreMovies(movieIDPairs[previousTargetMovieName]);
            reasonUserLikedMovies = recommendationCalculator.GetLeadToRecommendationMovieIDs(movieIDPairs[previousTargetMovieName]);
            weChangedATargetMovie = false;
        }

        Debug.Log(recommendationCalculator.GetUserLikedMovieNum());
        Debug.Log("Liked Actor Num: " + reasonCasts.Count.ToString());
        Debug.Log("Similar Movie Num: " + reasonSimilarMovies.Count.ToString());
        Debug.Log("Recommendation Movie Num: " + reasonUserLikedMovies.Count.ToString());

        actorScore = (double) reasonCasts.Count / 10 > 1 ? 1 : (double) reasonCasts.Count / 10;
        similarScore = (double) reasonSimilarMovies.Count / 60;
        recommendationScore = (double) reasonUserLikedMovies.Count / 60;
        Debug.Log("Liked Actor Score: " + actorScore.ToString());
        Debug.Log("Similar Movie Score: " + similarScore.ToString());
        Debug.Log("Recommendation Movie Score: " + recommendationScore.ToString());

        int score = (int) (100.0f * (actorScore + similarScore + recommendationScore)/3.0f);
        reasonScoreController.ChangeRecommendationScore(score);
    }

    private void ShowRecommendationSubScore()
    {
        if (previousHitRecommendationReasonWhy == true)
        {
            reasonSubScoreController.gameObject.SetActive(true);
            reasonSubScoreController.ChangeRecommendationSubScore(actorScore, similarScore, recommendationScore);
        }
        else
        {
            reasonSubScoreController.gameObject.SetActive(false);
            reasonSubScoreController.actorStarController.hitScoreStarControllerActor = false;
            reasonSubScoreController.similarStarController.hitScoreStarControllerSimilar = false;
            reasonSubScoreController.recommendationStarController.hitScoreStarControllerRecommendation = false;
        }
        
    }

    private void ShowRecommendationReason(string _reasonMode)
    {

        if (_reasonMode == "Actor")
        {
            if (previousHitRecommendationReasonActor == true)
            {
                reasonActorController.gameObject.SetActive(true);
                reasonSimilarController.gameObject.SetActive(false);
                reasonRecommendationController.gameObject.SetActive(false);

                if (reasonCasts.Count == 1)
                {
                    reasonActorController.UpdatePoster(posterImageBase + TMDBApiController.GetActorPosters(reasonCasts[0].Id).Profiles[0].FilePath);
                }
                else if (reasonCasts.Count == 2)
                {
                    reasonActorController.UpdatePoster(posterImageBase + TMDBApiController.GetActorPosters(reasonCasts[0].Id).Profiles[0].FilePath,
                                                       posterImageBase + TMDBApiController.GetActorPosters(reasonCasts[1].Id).Profiles[0].FilePath);
                }
                else if (reasonCasts.Count >= 3)
                {
                    reasonActorController.UpdatePoster(posterImageBase + TMDBApiController.GetActorPosters(reasonCasts[0].Id).Profiles[0].FilePath,
                                                       posterImageBase + TMDBApiController.GetActorPosters(reasonCasts[1].Id).Profiles[0].FilePath,
                                                       posterImageBase + TMDBApiController.GetActorPosters(reasonCasts[2].Id).Profiles[0].FilePath);
                }
            }
            else
            {
                reasonActorController.gameObject.SetActive(false);
            }
        }
        else if (_reasonMode == "ActorFullList")
        {
            if (previousHitRecommendationReasonActorFullList == true)
            {
                reasonActorFullListController.gameObject.SetActive(true);
                reasonSimilarFullListController.gameObject.SetActive(false);
                reasonRecommendationFullListController.gameObject.SetActive(false);

                List<string> urls = new List<string>();
                List<string> names = new List<string>();
                for(int i = 0;  i < 10;  i++)
                {
                    if (i < reasonCasts.Count)
                    {
                        urls.Add(posterImageBase + TMDBApiController.GetActorPosters(reasonCasts[i].Id).Profiles[0].FilePath);
                        names.Add(reasonCasts[i].Name);
                    }
                    else
                    {
                        urls.Add("");
                        names.Add("");
                    }
                }

                reasonActorFullListController.UpdateActorFullList(urls, names);
            }
            else
            {
                reasonActorFullListController.gameObject.SetActive(false);
            }
        }
        else if (_reasonMode == "Similar")
        {
            if (previousHitRecommendationReasonSimilar == true)
            {
                reasonActorController.gameObject.SetActive(false);
                reasonSimilarController.gameObject.SetActive(true);
                reasonRecommendationController.gameObject.SetActive(false);

                if (reasonSimilarMovies.Count == 1)
                {
                    reasonSimilarController.UpdatePoster(posterImageBase + TMDBApiController.GetMoviePosters(reasonSimilarMovies[0]).Posters[0].FilePath);
                }
                else if (reasonSimilarMovies.Count == 2)
                {
                    reasonSimilarController.UpdatePoster(posterImageBase + TMDBApiController.GetMoviePosters(reasonSimilarMovies[0]).Posters[0].FilePath,
                                                         posterImageBase + TMDBApiController.GetMoviePosters(reasonSimilarMovies[1]).Posters[0].FilePath);
                }
                else if (reasonSimilarMovies.Count >= 3)
                {
                    reasonSimilarController.UpdatePoster(posterImageBase + TMDBApiController.GetMoviePosters(reasonSimilarMovies[0]).Posters[0].FilePath,
                                                     posterImageBase + TMDBApiController.GetMoviePosters(reasonSimilarMovies[1]).Posters[0].FilePath,
                                                     posterImageBase + TMDBApiController.GetMoviePosters(reasonSimilarMovies[2]).Posters[0].FilePath);
                }
            }
            else
            {
                reasonSimilarController.gameObject.SetActive(false);
            }
        }
        else if (_reasonMode == "SimilarFullList")
        {
            if (previousHitRecommendationReasonSimilarFullList == true)
            {
                reasonActorFullListController.gameObject.SetActive(false);
                reasonSimilarFullListController.gameObject.SetActive(true);
                reasonRecommendationFullListController.gameObject.SetActive(false);

                List<string> names = new List<string>();
                foreach(int movieID in reasonSimilarMovies)
                {
                    names.Add(TMDBApiController.GetMovieName(movieID));
                }

                reasonSimilarFullListController.UpdateSimilarFullList(names);
            }
            else
            {
                reasonSimilarFullListController.gameObject.SetActive(false);
            }
        }
        else if (_reasonMode == "Recommendation")
        {
            if (previousHitRecommendationReasonRecommendation == true)
            {
                reasonActorController.gameObject.SetActive(false);
                reasonSimilarController.gameObject.SetActive(false);
                reasonRecommendationController.gameObject.SetActive(true);

                if (reasonUserLikedMovies.Count == 1)
                {
                    reasonRecommendationController.UpdatePoster(posterImageBase + TMDBApiController.GetMoviePosters(reasonUserLikedMovies[0]).Posters[0].FilePath);
                }
                else if (reasonUserLikedMovies.Count == 2)
                {
                    reasonRecommendationController.UpdatePoster(posterImageBase + TMDBApiController.GetMoviePosters(reasonUserLikedMovies[0]).Posters[0].FilePath,
                                                                posterImageBase + TMDBApiController.GetMoviePosters(reasonUserLikedMovies[1]).Posters[0].FilePath);
                }
                else if (reasonUserLikedMovies.Count >= 3)
                {
                    reasonRecommendationController.UpdatePoster(posterImageBase + TMDBApiController.GetMoviePosters(reasonUserLikedMovies[0]).Posters[0].FilePath,
                                                                posterImageBase + TMDBApiController.GetMoviePosters(reasonUserLikedMovies[1]).Posters[0].FilePath,
                                                                posterImageBase + TMDBApiController.GetMoviePosters(reasonUserLikedMovies[2]).Posters[0].FilePath);
                }

                reasonRecommendationController.ChangeReasonTargetMovieName(TMDBApiController.GetMovieName(movieIDPairs[previousTargetMovieName]));
            }
            else
            {
                reasonRecommendationController.gameObject.SetActive(false);
            }
        }
        else
        {
            if (previousHitRecommendationReasonRecommendationFullList == true)
            {
                reasonActorFullListController.gameObject.SetActive(false);
                reasonSimilarFullListController.gameObject.SetActive(false);
                reasonRecommendationFullListController.gameObject.SetActive(true);

                List<string> names = new List<string>();
                foreach (int movieID in reasonUserLikedMovies)
                {
                    names.Add(TMDBApiController.GetMovieName(movieID));
                }

                reasonRecommendationFullListController.UpdateRecommendationFullList(names);
            }
            else
            {
                reasonRecommendationFullListController.gameObject.SetActive(false);
            }
        }
    }

    private int currentMovieWatchingHistoryPageNum = 1;
    private int totalMovieWatchingHistoryPageNum = 1;
    Dictionary<int, string> keyValuePairs;
    private void ShowMovieWatchingHistory()
    {
        if (previousHitMovieWatchingHistory == true)
        {
            movieWatchingHistoryController.gameObject.SetActive(true);

            List<string> urls = new List<string>();
            List<string> names = new List<string>();
            keyValuePairs = recommendationCalculator.GetUserLikedMoviePairs();
            int[] movieIds = new int[keyValuePairs.Count];
            keyValuePairs.Keys.CopyTo(movieIds, 0);
            totalMovieWatchingHistoryPageNum = 1 + keyValuePairs.Count / 10;

            for (int i = 0; i < 10; i++)
            {
                if (i < keyValuePairs.Keys.Count)
                {
                    List<ImageData> images = TMDBApiController.GetMoviePosters(movieIds[i]).Posters;
                    if (images.Count > 0)
                    {
                        urls.Add(posterImageBase + images[0].FilePath);
                    }
                    else
                    {
                        urls.Add("https://www.24reel.com/static/assets/template/24reel/images/no_img.jpg");
                    }
                    names.Add(TMDBApiController.GetMovieName(movieIds[i]));
                }
                else
                {
                    urls.Add("");
                    names.Add("");
                }
            }

            movieWatchingHistoryController.UpdateList(urls, names);
            movieWatchingHistoryController.UpdatePageNum(currentMovieWatchingHistoryPageNum, totalMovieWatchingHistoryPageNum);
        }
        else
        {
            currentMovieWatchingHistoryPageNum = 1;
            movieWatchingHistoryController.gameObject.SetActive(false);
        }
    }

    private void UpdateMovieWatchingHistory(string _mode)
    {
        if (_mode == "Next")
        {
            if (currentMovieWatchingHistoryPageNum < totalMovieWatchingHistoryPageNum)
            {
                currentMovieWatchingHistoryPageNum++;

                List<string> urls = new List<string>();
                List<string> names = new List<string>();

                int[] movieIds = new int[keyValuePairs.Count];
                keyValuePairs.Keys.CopyTo(movieIds, 0);

                for (int i = 10 * (currentMovieWatchingHistoryPageNum - 1); i < 10 * currentMovieWatchingHistoryPageNum; i++)
                {
                    if (i < keyValuePairs.Keys.Count)
                    {
                        List<ImageData> images = TMDBApiController.GetMoviePosters(movieIds[i]).Posters;
                        if (images.Count > 0)
                        {
                            urls.Add(posterImageBase + images[0].FilePath);
                        }
                        else
                        {
                            urls.Add("https://www.24reel.com/static/assets/template/24reel/images/no_img.jpg");
                        }
                        
                        names.Add(TMDBApiController.GetMovieName(movieIds[i]));
                    }
                    else
                    {
                        urls.Add("");
                        names.Add("");
                    }
                }

                movieWatchingHistoryController.UpdateList(urls, names);
                movieWatchingHistoryController.UpdatePageNum(currentMovieWatchingHistoryPageNum, totalMovieWatchingHistoryPageNum);
            }
        }
        else
        {
            if (currentMovieWatchingHistoryPageNum > 1)
            {
                currentMovieWatchingHistoryPageNum--;

                List<string> urls = new List<string>();
                List<string> names = new List<string>();

                int[] movieIds = new int[keyValuePairs.Count];
                keyValuePairs.Keys.CopyTo(movieIds, 0);

                for (int i = 10 * (currentMovieWatchingHistoryPageNum - 1); i < 10 * currentMovieWatchingHistoryPageNum; i++)
                {
                    if (i < keyValuePairs.Keys.Count)
                    {
                        urls.Add(posterImageBase + TMDBApiController.GetMoviePosters(movieIds[i]).Posters[0].FilePath);
                        names.Add(TMDBApiController.GetMovieName(movieIds[i]));
                    }
                    else
                    {
                        urls.Add("");
                        names.Add("");
                    }
                }

                movieWatchingHistoryController.UpdateList(urls, names);
                movieWatchingHistoryController.UpdatePageNum(currentMovieWatchingHistoryPageNum, totalMovieWatchingHistoryPageNum);
            }
        }
    }
}
