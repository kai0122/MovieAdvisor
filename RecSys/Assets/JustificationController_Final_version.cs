using System.Collections;
using System.Collections.Generic;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Movies;
using TMDbLib.Objects.Reviews;
using TMDbLib.Objects.Search;
using UnityEngine;

public class JustificationController_Final_version : MonoBehaviour
{
    // 493065, 508442, 464052, 581392, 601434
    public Dictionary<string, int> movieIDPairs = new Dictionary<string, int>()
                                                    {
                                                        {"Soul", 508442},
                                                        {"DarkPhoenix", 320288},
                                                        {"tomandjerry", 587807},
                                                        {"WrathofMan", 637649},
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

    // target movie recommendation reason
    public SelectRecommendationWhy selectRecommendationWhy;
    public MovieRecommendationRatingDetailController movieRecommendationRatingDetailController;
    public SelectRecommendationRatingWhy selectRecommendationRatingWhy;
    public SelectRatingIMDBWhy selectRatingIMDBWhy;
    public SelectRatingTMDBWhy selectRatingTMDBWhy;
    public SelecrRatingRottenTomatoeWhy selecrRatingRottenTomatoeWhy;
    public MovieRecommendationRatingDetailPercentController movieRecommendationRatingDetailPercentController;

    public SelectRecommendationPublicityWhy selectRecommendationPublicityWhy;
    public MovieRecommendationPublicityDetailController movieRecommendationPublicityDetailController;
    public SelectPublicityShowNextMoremovies selectPublicityShowNextMoremovies;


    public SelectRecommendationActorWhy selectRecommendationActorWhy;
    public SelectRecommendationGenreWhy selectRecommendationGenreWhy;
    public MovieRecommendationActorDetailController movieRecommendationActorDetailController;
    public MovieRecommendationGenreDetailController movieRecommendationGenreDetailController;
    public MovieRecommendationActorMoreController movieRecommendationActorMoreController;
    public MovieRecommendationGenreMoreController movieRecommendationGenreMoreController;
    public SelectGenreShowNextMoremovies selectGenreShowNextMoremovies;
    public SelectActorShowNextMoremovies selectActorShowNextMoremovies;
    public MovieActorFullList1 movieActorFullList1;
    public MovieActorFullList2 movieActorFullList2;
    public MovieActorFullList3 movieActorFullList3;
    public MovieGenreFullList1 movieGenreFullList1;
    public MovieGenreFullList2 movieGenreFullList2;
    public MovieGenreFullList3 movieGenreFullList3;
    public MovieGenreFullList4 movieGenreFullList4;

    // target movie recommendation
    public RecommendationCalculator recommendationCalculator;
    public MovieRecommendationTitleController2 movieRecommendationTitleController;
    public MovieRecommendationSubTitleController3 movieRecommendationSubTitleController;


    public SelectActorClose selectActorClose;
    public SelectGenreClose selectGenreClose;
    public SelectPublicityClose selectPublicityClose;
    public SelectRatingClose selectRatingClose;
    public SelectActorClose selectActorClose_Main;
    public SelectGenreClose selectGenreClose_Main;
    public SelectRatingClose selectRatingClose_Main;

    public UserPreferenceStore userPreferenceStore;

    public SelectLikeTargetMovieIcon selectLikeTargetMovieIcon;


    // Start is called before the first frame update
    void Start()
    {
        actorScoreBase = userPreferenceStore.GetActorPreference();
        genreScoreBase = userPreferenceStore.GetGenrePreference();
        publicityScoreBase = userPreferenceStore.GetPublicityPreference();
        ratingScoreBase = userPreferenceStore.GetRatingPreference();


        movieListButtonController.MovieListActive = true;
        movieListButtonController.OnButtonPress();

        //recommendationCalculator.GetMovieRatedList();
        movieListButtonController.MovieListActive = false;
    }

    string previousTargetMovieName = "FirstName";
    bool previousHitRecommendationReasonWhy = true;
    bool previousHitRecommendationRatingWhy = true;
    bool previousHitRatingTMDbWhy = true;
    bool previousHitRatingIMDbWhy = true;
    bool previousHitRatingRottenTomatoeWhy = true;
    bool previousHitPublicityWhy = true;
    bool previousHitPublicityMoreMovies = true;
    bool previousHitRecommendationGenreWhy = true;
    bool previousHitRecommendationActorWhy = true;
    bool previousHitRecommendationActorMore1 = true;
    bool previousHitRecommendationActorMore2 = true;
    bool previousHitRecommendationActorMore3 = true;
    bool previousHitRecommendationGenreMore1 = true;
    bool previousHitRecommendationGenreMore2 = true;
    bool previousHitRecommendationGenreMore3 = true;
    bool previousHitRecommendationGenreMore4 = true;
    bool previousHitGenreMoreMovies = true;
    bool previousHitActorMoreMovies = true;

    bool previousHitActorClose = true;
    bool previousHitGenreClose = true;
    bool previousHitPublicityClose = true;
    bool previousHitRatingClose = true;
    bool previousHitActorClose_Main = true;
    bool previousHitGenreClose_Main = true;
    bool previousHitRatingClose_Main = true;

    bool previousHitLikeUnlikeTargetMovie = false;

    public MovieListButtonController movieListButtonController;


    bool previousHitRecommendationReview = true;
    bool previousHitRecommendationReviewNext = false;
    bool previousHitRecommendationReviewPrevious = false;

    // Shift Justification parameters
    private bool JustificationShiftLeft = false;
    private bool JustificationShiftCenter = false;
    public float startX;
    public float startY;
    public float startZ;
    public float endX;
    public float endY;
    public float endZ;
    private float shiftX = 0.7f;
    private enum CurrentPosition
    {
        Left,
        Middle
    }
    private CurrentPosition currentJustificationInterfacePosition = CurrentPosition.Middle;

    private void ShiftJustificationInterface(string mode = "Open")
    {
        if (mode == "Close" && currentJustificationInterfacePosition == CurrentPosition.Left)
        {
            JustificationShiftCenter = true;
        }

        if (mode == "Open" && currentJustificationInterfacePosition == CurrentPosition.Middle)
        {
            JustificationShiftLeft = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Shift Justification code

        if (JustificationShiftLeft)
        {
            if (Vector3.Distance(movieInformationController.transform.localPosition, new Vector3(endX, endY, endZ)) > shiftX)
            {
                movieInformationController.transform.localPosition = new Vector3(movieInformationController.transform.localPosition.x + shiftX,
                                                                                 movieInformationController.transform.localPosition.y,
                                                                                 movieInformationController.transform.localPosition.z);
                movieRecommendationTitleController.transform.localPosition = new Vector3(movieRecommendationTitleController.transform.localPosition.x + shiftX,
                                                                                         movieRecommendationTitleController.transform.localPosition.y,
                                                                                         movieRecommendationTitleController.transform.localPosition.z);
                movieRecommendationSubTitleController.transform.localPosition = new Vector3(movieRecommendationSubTitleController.transform.localPosition.x + shiftX,
                                                                                            movieRecommendationSubTitleController.transform.localPosition.y,
                                                                                            movieRecommendationSubTitleController.transform.localPosition.z);
            }
            else
            {
                movieInformationController.transform.localPosition = new Vector3(endX, endY, endZ);
                JustificationShiftLeft = false;
                currentJustificationInterfacePosition = CurrentPosition.Left;
            }
        }

        if (JustificationShiftCenter)
        {
            if (Vector3.Distance(movieInformationController.transform.localPosition, new Vector3(startX, startY, startZ)) > shiftX)
            {
                movieInformationController.transform.localPosition = new Vector3(movieInformationController.transform.localPosition.x - shiftX,
                                                                                 movieInformationController.transform.localPosition.y,
                                                                                 movieInformationController.transform.localPosition.z);
                movieRecommendationTitleController.transform.localPosition = new Vector3(movieRecommendationTitleController.transform.localPosition.x - shiftX,
                                                                                         movieRecommendationTitleController.transform.localPosition.y,
                                                                                         movieRecommendationTitleController.transform.localPosition.z);
                movieRecommendationSubTitleController.transform.localPosition = new Vector3(movieRecommendationSubTitleController.transform.localPosition.x - shiftX,
                                                                                            movieRecommendationSubTitleController.transform.localPosition.y,
                                                                                            movieRecommendationSubTitleController.transform.localPosition.z);
            }
            else
            {
                movieInformationController.transform.localPosition = new Vector3(startX, startY, startZ);
                JustificationShiftCenter = false;
                currentJustificationInterfacePosition = CurrentPosition.Middle;
            }
        }

        if (!movieListButtonController.MovieListActive && !movieListButtonController.movieSearchButtonController.SearchListActive)
        {
            //UpdateRecommendationBase();

            if (filterNumberHolder.GetIfUpdateFilter(gameObject.transform.parent.name))
            {
                UpdateRecommendation();
            }

            if (userLikedMoviesController.GetIfUpdateList(gameObject.transform.parent.name))
            {
                UpdateRecommendation();
            }

            if (previousTargetMovieName != childObjectsActivator.currentTrackedName && childObjectsActivator.currentTrackedName != "")
            {
                selectTargetMovieReadMore.hitTargetMovieReadMore = false;
                selectRecommendationWhy.hitRecommendationScoreWhy = false;

                // update movie name
                previousTargetMovieName = childObjectsActivator.currentTrackedName;
                UpdateMovieInformation();

                weChangedATargetMovie = true;
                UpdateRecommendation();
                Debug.Log("REEEEEEE");

                foreach (int movieID in movieIDPairs.Values)
                {
                    TMDBApiController.GetMovieRatingFakeBreakdown_Rotten(movieID);
                }
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

            if (previousHitRatingRottenTomatoeWhy != selecrRatingRottenTomatoeWhy.hitRatingRottenTomatoWhy)
            {   previousHitRatingRottenTomatoeWhy = selecrRatingRottenTomatoeWhy.hitRatingRottenTomatoWhy;
                ShowRecommendationRatingDetailPercent("Rotten");
            }

            if (previousHitPublicityWhy != selectRecommendationPublicityWhy.hitPublicityWhy)
            {
                previousHitPublicityWhy = selectRecommendationPublicityWhy.hitPublicityWhy;
                ShowRecommendationPublicityDetail_together();
            }

            
            if (previousHitRecommendationGenreWhy != selectRecommendationGenreWhy.hitRecommendationGenreWhy)
            {
                previousHitRecommendationGenreWhy = selectRecommendationGenreWhy.hitRecommendationGenreWhy;
                ShowRecommendationGenreDetail();
            }

            if (previousHitGenreMoreMovies != selectGenreShowNextMoremovies.hitGenreMoreMovies)
            {
                previousHitGenreMoreMovies = selectGenreShowNextMoremovies.hitGenreMoreMovies;
                ShowRecommendationGenreDetailMoreMovies();
            }

            if (previousHitActorMoreMovies != selectActorShowNextMoremovies.hitActorMoreMovies)
            {
                previousHitActorMoreMovies = selectActorShowNextMoremovies.hitActorMoreMovies;
                ShowRecommendationActorDetailMoreMovies();
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

            if (previousHitPublicityMoreMovies != selectPublicityShowNextMoremovies.hitPublicityMoreMovies)
            {
                previousHitPublicityMoreMovies = selectPublicityShowNextMoremovies.hitPublicityMoreMovies;
                ShowRecommendationPublicityMoreMoviesNextMovies_together();
            }

            
            if (previousHitActorClose != selectActorClose.hitActorClose)
            {
                previousHitActorClose = selectActorClose.hitActorClose;
                if (previousHitActorClose)
                {
                    movieActorFullList1.hitMovieActorFullList = false;
                    movieActorFullList2.hitMovieActorFullList = false;
                    movieActorFullList3.hitMovieActorFullList = false;

                    previousHitActorClose = false;
                    selectActorClose.hitActorClose = false;
                }
            }

            if (previousHitActorClose_Main != selectActorClose_Main.hitActorClose_Main)
            {
                previousHitActorClose_Main = selectActorClose_Main.hitActorClose_Main;
                if (previousHitActorClose_Main)
                {
                    selectRecommendationActorWhy.hitRecommendationActorWhy = false;

                    previousHitActorClose_Main = false;
                    selectActorClose_Main.hitActorClose_Main = false;
                }
            }

            if (previousHitGenreClose != selectGenreClose.hitGenreClose)
            {
                previousHitGenreClose = selectGenreClose.hitGenreClose;
                if (previousHitGenreClose)
                {
                    movieGenreFullList1.hitMovieGenreFullList = false;
                    movieGenreFullList2.hitMovieGenreFullList = false;
                    movieGenreFullList3.hitMovieGenreFullList = false;
                    movieGenreFullList4.hitMovieGenreFullList = false;

                    previousHitGenreClose = false;
                    selectGenreClose.hitGenreClose = false;
                }
            }

            if (previousHitGenreClose_Main != selectGenreClose_Main.hitGenreClose_Main)
            {
                previousHitGenreClose_Main = selectGenreClose_Main.hitGenreClose_Main;
                if (previousHitGenreClose_Main)
                {
                    selectRecommendationGenreWhy.hitRecommendationGenreWhy = false;

                    previousHitGenreClose_Main = false;
                    selectGenreClose_Main.hitGenreClose_Main = false;
                }
            }

            
            if (previousHitPublicityClose != selectPublicityClose.hitPublicityClose)
            {
                previousHitPublicityClose = selectPublicityClose.hitPublicityClose;
                if (previousHitPublicityClose)
                {
                    selectRecommendationPublicityWhy.hitPublicityWhy = false;

                    previousHitPublicityClose = false;
                    selectPublicityClose.hitPublicityClose = false;
                }
            }
            

            if (previousHitRatingClose != selectRatingClose.hitRatingClose)
            {
                previousHitRatingClose = selectRatingClose.hitRatingClose;
                if (previousHitRatingClose)
                {
                    selectRatingTMDBWhy.hitRatingTMDbWhy = false;
                    selectRatingIMDBWhy.hitRatingIMDbWhy = false;
                    selecrRatingRottenTomatoeWhy.hitRatingRottenTomatoWhy = false;
                }
            }

            if (previousHitRatingClose_Main != selectRatingClose_Main.hitRatingClose_Main)
            {
                previousHitRatingClose_Main = selectRatingClose_Main.hitRatingClose_Main;
                if (previousHitRatingClose_Main)
                {
                    selectRecommendationRatingWhy.hitRecommendationRatingWhy = false;

                    previousHitRatingClose_Main = false;
                    selectRatingClose_Main.hitRatingClose_Main = false;
                }
            }


            if (previousHitLikeUnlikeTargetMovie != selectLikeTargetMovieIcon.hitLikeUnlikeTargetMovie)
            {
                previousHitLikeUnlikeTargetMovie = selectLikeTargetMovieIcon.hitLikeUnlikeTargetMovie;
                if (previousHitLikeUnlikeTargetMovie)
                {
                    // User like the target movie
                    userLikedMoviesController.AddMovieToList(movieIDPairs[previousTargetMovieName], previousTargetMovieName);
                    //movieListButtonController.AddMovie(movieIDPairs[previousTargetMovieName], previousTargetMovieName, posterImageBase);
                }
                else
                {
                    // User dislike the target movie
                    userLikedMoviesController.RemoveMovieFromList(movieIDPairs[previousTargetMovieName]);
                    movieListButtonController.RemoveMovie(movieIDPairs[previousTargetMovieName]);
                }
            }

            if (previousHitLikeUnlikeTargetMovie == true && movieListButtonController.previousRemoveMovieId == movieIDPairs[previousTargetMovieName])
            {
                // if remove target movie from user-liked list in the setting
                previousHitLikeUnlikeTargetMovie = false;
                selectLikeTargetMovieIcon.hitLikeUnlikeTargetMovie = false;

                movieListButtonController.previousRemoveMovieId = 0;
            }

            if (previousHitRecommendationReview != selectMovieRecommendationReview.hitRecommendationReview)
            {
                previousHitRecommendationReview = selectMovieRecommendationReview.hitRecommendationReview;
                ShowRecommendationReview();
            }

            if (previousHitRecommendationReviewNext != selectMovieRecommendationReviewNext.hitRecommendationReviewNext)
            {
                previousHitRecommendationReviewNext = selectMovieRecommendationReviewNext.hitRecommendationReviewNext;
                ShowRecommendationReviewNext();
            }

            if (previousHitRecommendationReviewPrevious != selectMovieRecommendationReviewPrevious.hitRecommendationReviewPrevious)
            {
                previousHitRecommendationReviewPrevious = selectMovieRecommendationReviewPrevious.hitRecommendationReviewPrevious;
                ShowRecommendationReviewPrevious();
            }
        }

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
        movieInformationController.ChangeMovieInfo3(TMDBApiController.GetMovieRating(movieIDPairs[previousTargetMovieName]), votingCount, genres, casts, urls, posterImageBase + TMDBApiController.GetMoviePosters(movieIDPairs[previousTargetMovieName]).Posters[0].FilePath);
        movieInformationController.ChangeMovieStory(TMDBApiController.GetMovieDescription(movieIDPairs[previousTargetMovieName]));
    }

    private void UpdateRecommendation()
    {
        UpdateRecommendationBase();
        UpdateRecommendationScore();

        movieRecommendationTitleController.ChangeRecommendationTitle(actorScore, actorScoreBase,
                                                                     genreScore, genreScoreBase,
                                                                     publicityScore, publicityScoreBase,
                                                                     ratingScore * 100, ratingScoreBase);
#pragma warning disable CS0618 // 類型或成員已經過時
        if (movieRecommendationSubTitleController.transform.gameObject.active)
        {
#pragma warning restore
            movieRecommendationSubTitleController.UpdateRecommendationSubStars(new List<bool>() {(actorScore >= actorScoreBase),
                                                                                                 (genreScore >= genreScoreBase),
                                                                                                 (publicityScore >= publicityScoreBase),
                                                                                                 ((100 * ratingScore) >= ratingScoreBase)});
        }
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
    int actorScore;
    int genreScore;
    int publicityScore;
    double ratingScore;
    int actorScoreBase = 0;
    int genreScoreBase = 0;
    int publicityScoreBase = 0;
    int ratingScoreBase = 0;
    private void UpdateRecommendationScore()
    {
        if (previousTargetMovieName != "FirstName")
        {
            // get reasons
            //if (weChangedATargetMovie)
            if (true)
            {
                reasonCastingCount = 0;
                totalMovieGenresNum = 0;
                reasonMovieGenresNum = 0;

                reasonCasts = recommendationCalculator.GetSameActorMovies(movieIDPairs[previousTargetMovieName]);
                totalCastingCount = recommendationCalculator.GetActorsTotalActedMovieNum(movieIDPairs[previousTargetMovieName]);
                int countActor = 0;
                foreach (Cast actor in reasonCasts)
                {
                    countActor += 1;
                    if (countActor < 4)
                    {
                        reasonCastingCount += recommendationCalculator.GetUserLikedActorMovies(actor.Name).Count; 
                    }
                    else
                    {
                        break;
                    }
                }

                totalMovieGenres = recommendationCalculator.GetGenreCounts(movieIDPairs[previousTargetMovieName]);
                foreach (string genre in totalMovieGenres.Keys)
                {
                    //Debug.Log(genre + ": get " + totalMovieGenres[genre].ToString());
                    totalMovieGenresNum += totalMovieGenres[genre];
                }

                List<Genre> genres = TMDBApiController.GetMovieGenres(movieIDPairs[previousTargetMovieName]);
                int genreCount = genres.Count < 4 ? genres.Count : 4;

                reasonMovieGenres.Clear();
                foreach (Genre genre in genres.GetRange(0, genreCount))
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


            actorScore = reasonCastingCount;
            genreScore = reasonMovieGenresNum;
            publicityScore = reasonUserLikedMovies.Count;
            ratingScore = recommendationCalculator.GetRecommendationRating(movieIDPairs[previousTargetMovieName]);

        }
    }

    private void ShowRecommendationReason()
    {
        if (previousHitRecommendationReasonWhy == true)
        {
            movieRecommendationTitleController.UpdateRecommendationStar(false);

            movieRecommendationSubTitleController.gameObject.SetActive(true);
            int sum = actorScoreBase + genreScoreBase + publicityScoreBase + ratingScoreBase;
            movieRecommendationSubTitleController.ChangeRecommendationSubTitleActor4((int)(actorScore), actorScoreBase, sum);
            movieRecommendationSubTitleController.ChangeRecommendationSubTitleGenre4((int)(genreScore), genreScoreBase, sum);
            movieRecommendationSubTitleController.ChangeRecommendationSubTitlePublicity4((int)(publicityScore), publicityScoreBase, sum);
            movieRecommendationSubTitleController.ChangeRecommendationSubTitleRating4((int)(100 * ratingScore), ratingScoreBase, sum);


            movieRecommendationSubTitleController.UpdateRecommendationSubStars(new List<bool>() {(actorScore >= actorScoreBase),
                                                                                                 (genreScore >= genreScoreBase),
                                                                                                 (publicityScore >= publicityScoreBase),
                                                                                                 ((100 * ratingScore) >= ratingScoreBase)});
            movieInformationController.ExpendRecommendationWhy_2_v2();
        }
        else
        {
            movieRecommendationTitleController.UpdateRecommendationStar(true);

            movieInformationController.CloseRecommendationWhy();
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
            ShiftJustificationInterface();
            movieRecommendationRatingDetailController.OpenRatingJustification();
            movieRecommendationRatingDetailController.ChangeDetailRatingScore(TMDBApiController.GetMovieName(movieIDPairs[previousTargetMovieName]),
                                                                              recommendationCalculator.GetMovieRatingTMDb(movieIDPairs[previousTargetMovieName]),
                                                                              recommendationCalculator.GetMovieRatingIMDb(movieIDPairs[previousTargetMovieName]),
                                                                              recommendationCalculator.GetMovieRatingRottenTomato(movieIDPairs[previousTargetMovieName]),
                                                                              recommendationCalculator.GetMovieRatingMetacritic(movieIDPairs[previousTargetMovieName]));
            /*
            if (!selectRecommendationActorWhy.hitRecommendationActorWhy && !selectRecommendationGenreWhy.hitRecommendationGenreWhy && !selectRecommendationPublicityWhy.hitPublicityWhy)
            {
                movieInformationController.ExpendRecommendationWhyRating_2();
            }
            else
            {
                selectRecommendationActorWhy.hitRecommendationActorWhy = false;
                selectRecommendationGenreWhy.hitRecommendationGenreWhy = false;
                selectRecommendationPublicityWhy.hitPublicityWhy = false;
            }
            */
            selectRecommendationActorWhy.hitRecommendationActorWhy = false;
            selectRecommendationGenreWhy.hitRecommendationGenreWhy = false;
            selectRecommendationPublicityWhy.hitPublicityWhy = false;
        }
        else
        {
            if (previousHitRecommendationRatingWhy == false &&
                previousHitPublicityWhy == false &&
                previousHitRecommendationActorWhy == false &&
                previousHitRecommendationGenreWhy == false)
            {
                ShiftJustificationInterface("Close");
            }
            
            movieRecommendationRatingDetailController.CloseRatingJustification();
            movieRecommendationRatingDetailPercentController.gameObject.SetActive(false);
            selectRecommendationRatingWhy.hitRecommendationRatingWhy = false;
            selectRatingTMDBWhy.hitRatingTMDbWhy = false;
            selectRatingIMDBWhy.hitRatingIMDbWhy = false;
            selecrRatingRottenTomatoeWhy.hitRatingRottenTomatoWhy = false;
            previousHitRatingIMDbWhy = false;
            previousHitRatingTMDbWhy = false;
            previousHitRatingRottenTomatoeWhy = false;

            selectMovieRecommendationReview.hitRecommendationReview = false;
        }
    }

    private void ShowRecommendationRatingDetailPercent(string mode)
    {
        if (mode == "IMDb")
        {
            if (previousHitRatingIMDbWhy == true)
            {
                previousHitRatingTMDbWhy = false;
                previousHitRatingRottenTomatoeWhy = false;
                selectRatingTMDBWhy.hitRatingTMDbWhy = false;
                selecrRatingRottenTomatoeWhy.hitRatingRottenTomatoWhy = false;
                movieRecommendationRatingDetailPercentController.gameObject.SetActive(true);
                for (int score = 5; score > 0; score--)
                {
                    movieRecommendationRatingDetailPercentController.ChangeRecommendationRatingDetail(score,
                                                                                                      recommendationCalculator.GetMovieRatingIMDbPercentCount(movieIDPairs[previousTargetMovieName], score*2) + recommendationCalculator.GetMovieRatingIMDbPercentCount(movieIDPairs[previousTargetMovieName], score*2-1),
                                                                                                      (double)(recommendationCalculator.GetMovieRatingIMDbPercentCount(movieIDPairs[previousTargetMovieName], score * 2) + recommendationCalculator.GetMovieRatingIMDbPercentCount(movieIDPairs[previousTargetMovieName], score * 2 - 1)) / recommendationCalculator.GetMovieRatingIMDbVotes(movieIDPairs[previousTargetMovieName]),
                                                                                                      TMDBApiController.GetMovieName(movieIDPairs[previousTargetMovieName]) + " | IMDb Rating");
                }
                movieRecommendationRatingDetailPercentController.ChangeTitle(mode, recommendationCalculator.GetMovieRatingIMDbVotes(movieIDPairs[previousTargetMovieName]), (float)recommendationCalculator.GetMovieRatingIMDb(movieIDPairs[previousTargetMovieName]));

                //movieInformationController.ExpendRecommendationWhyRatingMore_2();

                movieRecommendationRatingDetailController.gameObject.SetActive(false);
            }
            else
            {
                //movieInformationController.CloseRecommendationWhyRatingMore_2();

                movieRecommendationRatingDetailPercentController.gameObject.SetActive(false);

                movieRecommendationRatingDetailController.gameObject.SetActive(true);
            }
        }
        else if (mode == "TMDb")
        {
            if (previousHitRatingTMDbWhy == true)
            {
                previousHitRatingIMDbWhy = false;
                previousHitRatingRottenTomatoeWhy = false;
                selectRatingIMDBWhy.hitRatingIMDbWhy = false;
                selecrRatingRottenTomatoeWhy.hitRatingRottenTomatoWhy = false;
                movieRecommendationRatingDetailPercentController.gameObject.SetActive(true);
                for (int score = 5; score > 0; score--)
                {
                    movieRecommendationRatingDetailPercentController.ChangeRecommendationRatingDetail(score,
                                                                                                      recommendationCalculator.GetMovieRatingTMDbPercentCount(movieIDPairs[previousTargetMovieName], score*2) + recommendationCalculator.GetMovieRatingTMDbPercentCount(movieIDPairs[previousTargetMovieName], score*2-1),
                                                                                                      recommendationCalculator.GetMovieRatingTMDbPercent(movieIDPairs[previousTargetMovieName], score*2) + recommendationCalculator.GetMovieRatingTMDbPercent(movieIDPairs[previousTargetMovieName], score*2-1),
                                                                                                      TMDBApiController.GetMovieName(movieIDPairs[previousTargetMovieName]) + " | TMDb Rating");
                }

                movieRecommendationRatingDetailPercentController.ChangeTitle(mode, recommendationCalculator.GetMovieRatingTMDbVotes(movieIDPairs[previousTargetMovieName]), (float)recommendationCalculator.GetMovieRatingTMDb(movieIDPairs[previousTargetMovieName]));

                //movieInformationController.ExpendRecommendationWhyRatingMore_2();

                movieRecommendationRatingDetailController.gameObject.SetActive(false);
            }
            else
            {
                //movieInformationController.CloseRecommendationWhyRatingMore_2();

                movieRecommendationRatingDetailPercentController.gameObject.SetActive(false);

                movieRecommendationRatingDetailController.gameObject.SetActive(true);
            }
        }
        else
        {
            if (previousHitRatingRottenTomatoeWhy == true)
            {
                previousHitRatingTMDbWhy = false;
                previousHitRatingIMDbWhy = false;
                selectRatingTMDBWhy.hitRatingTMDbWhy = false;
                selectRatingIMDBWhy.hitRatingIMDbWhy = false;
                movieRecommendationRatingDetailPercentController.gameObject.SetActive(true);
                for (int score = 5; score > 0; score--)
                {
                    movieRecommendationRatingDetailPercentController.ChangeRecommendationRatingDetail(score,
                                                                                                      recommendationCalculator.GetMovieRatingRottenPercentCount(movieIDPairs[previousTargetMovieName], score * 2) + recommendationCalculator.GetMovieRatingRottenPercentCount(movieIDPairs[previousTargetMovieName], score * 2 - 1),
                                                                                                      recommendationCalculator.GetMovieRatingRottenPercent(movieIDPairs[previousTargetMovieName], score * 2) + recommendationCalculator.GetMovieRatingRottenPercent(movieIDPairs[previousTargetMovieName], score * 2 - 1),
                                                                                                      TMDBApiController.GetMovieName(movieIDPairs[previousTargetMovieName]) + " | Rotten Tomatoe Rating");
                }

                movieRecommendationRatingDetailPercentController.ChangeTitle(mode, recommendationCalculator.GetMovieRatingRottenVotes(movieIDPairs[previousTargetMovieName]), (float)recommendationCalculator.GetMovieRatingTMDb(movieIDPairs[previousTargetMovieName]));

                //movieInformationController.ExpendRecommendationWhyRatingMore_2();

                movieRecommendationRatingDetailController.gameObject.SetActive(false);
            }
            else
            {
                //movieInformationController.CloseRecommendationWhyRatingMore_2();

                movieRecommendationRatingDetailPercentController.gameObject.SetActive(false);

                movieRecommendationRatingDetailController.gameObject.SetActive(true);
            }
        }
    }

    private void ShowRecommendationPublicityDetail()
    {
        if (previousHitPublicityWhy == true)
        {
            ShiftJustificationInterface();
            movieRecommendationPublicityDetailController.OpenPublicityJustification();
            movieRecommendationPublicityDetailController.ChangeReasonTargetMovieName(TMDBApiController.GetMovieName(movieIDPairs[previousTargetMovieName]),
                                                                                     reasonUserLikedMovies.Count,
                                                                                     recommendationCalculator.GetUserLikedMovieNum());

            /*
            if (!selectRecommendationActorWhy.hitRecommendationActorWhy && !selectRecommendationGenreWhy.hitRecommendationGenreWhy && !selectRecommendationRatingWhy.hitRecommendationRatingWhy)
            {
                movieInformationController.ExpendRecommendationWhyPublicity_2();
                movieRecommendationSubTitleController.ExpendPublicityWhy_2();
            }
            else
            {
                selectRecommendationActorWhy.hitRecommendationActorWhy = false;
                selectRecommendationGenreWhy.hitRecommendationGenreWhy = false;
                selectRecommendationRatingWhy.hitRecommendationRatingWhy = false;
            }
            */
            selectRecommendationActorWhy.hitRecommendationActorWhy = false;
            selectRecommendationGenreWhy.hitRecommendationGenreWhy = false;
            selectRecommendationRatingWhy.hitRecommendationRatingWhy = false;
        }
        else
        {
            if (previousHitRecommendationRatingWhy == false &&
                previousHitPublicityWhy == false &&
                previousHitRecommendationActorWhy == false &&
                previousHitRecommendationGenreWhy == false)
            {
                ShiftJustificationInterface("Close");
            }
            movieRecommendationPublicityDetailController.ClosePublicityJustification();
            selectPublicityShowNextMoremovies.hitPublicityMoreMovies = false;
            previousHitPublicityMoreMovies = false;
        }
    }

    private void ShowRecommendationPublicityDetail_together()
    {
        if (previousHitPublicityWhy == true)
        {
            ShiftJustificationInterface();
            movieRecommendationPublicityDetailController.OpenPublicityJustification();
            movieRecommendationPublicityDetailController.ChangeReasonTargetMovieName(TMDBApiController.GetMovieName(movieIDPairs[previousTargetMovieName]),
                                                                                     reasonUserLikedMovies.Count,
                                                                                     recommendationCalculator.GetUserLikedMovieNum());

            selectRecommendationActorWhy.hitRecommendationActorWhy = false;
            selectRecommendationGenreWhy.hitRecommendationGenreWhy = false;
            selectRecommendationRatingWhy.hitRecommendationRatingWhy = false;

            List<string> names = new List<string>();
            List<string> posterUrls = new List<string>();
            foreach (int movieID in reasonUserLikedMovies)
            {
                names.Add(TMDBApiController.GetMovieName(movieID));
                posterUrls.Add(posterImageBase + TMDBApiController.GetMoviePosters(movieID).Posters[0].FilePath);
            }

            movieRecommendationPublicityDetailController.UpdateRecommendationFullList_part(names, posterUrls);
            movieRecommendationPublicityDetailController.ShowRecommendationFullList_part();
        }
        else
        {
            if (previousHitRecommendationRatingWhy == false &&
                previousHitPublicityWhy == false &&
                previousHitRecommendationActorWhy == false &&
                previousHitRecommendationGenreWhy == false)
            {
                ShiftJustificationInterface("Close");
            }
            movieRecommendationPublicityDetailController.ClosePublicityJustification();
            selectPublicityShowNextMoremovies.hitPublicityMoreMovies = false;
            previousHitPublicityMoreMovies = false;
        }
    }

    private void ShowRecommendationPublicityMoreMoviesNextMovies_together()
    {
        if (previousHitPublicityMoreMovies == true)
        {

            movieRecommendationPublicityDetailController.ShowRecommendationFullList_part();

            previousHitPublicityMoreMovies = false;
            selectPublicityShowNextMoremovies.hitPublicityMoreMovies = false;
        }
    }

    private void ShowRecommendationGenreDetailMoreMovies()
    {
        if (previousHitGenreMoreMovies == true)
        {

            movieRecommendationGenreMoreController.ShowRecommendationFullList_part();

            previousHitGenreMoreMovies = false;
            selectGenreShowNextMoremovies.hitGenreMoreMovies = false;
        }
    }

    private void ShowRecommendationActorDetailMoreMovies()
    {
        if (previousHitActorMoreMovies == true)
        {

            movieRecommendationActorMoreController.ShowRecommendationFullList_part();

            previousHitActorMoreMovies = false;
            selectActorShowNextMoremovies.hitActorMoreMovies = false;
        }
    }

    private void ShowRecommendationGenreDetail()
    {
        if (previousHitRecommendationGenreWhy == true)
        {
            ShiftJustificationInterface();
            movieRecommendationGenreDetailController.OpenGenreJustification();

            List<string> genres = new List<string>();
            List<int> genreNums = new List<int>();
            int sum = 0;
            foreach (string genre in reasonMovieGenres.Keys)
            {
                genres.Add(genre);
                genreNums.Add(reasonMovieGenres[genre]);
                sum += reasonMovieGenres[genre];
            }
            movieRecommendationGenreDetailController.ChangeGenreBar(TMDBApiController.GetMovieName(movieIDPairs[previousTargetMovieName]), sum,
                                                                    genres, genreNums, recommendationCalculator.GetUserLikedMovieNum());

            selectRecommendationActorWhy.hitRecommendationActorWhy = false;
            selectRecommendationPublicityWhy.hitPublicityWhy = false;
            selectRecommendationRatingWhy.hitRecommendationRatingWhy = false;
        }
        else
        {
            if (previousHitRecommendationRatingWhy == false &&
                previousHitPublicityWhy == false &&
                previousHitRecommendationActorWhy == false &&
                previousHitRecommendationGenreWhy == false)
            {
                ShiftJustificationInterface("Close");
            }
            movieRecommendationGenreDetailController.CloseGenreJustification();
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
            ShiftJustificationInterface();
            movieRecommendationActorDetailController.OpenActorJustification();

            if (reasonCasts.Count == 1)
            {
                movieRecommendationActorDetailController.UpdateName(reasonCasts[0].Name);
                movieRecommendationActorDetailController.UpdatePoster(posterImageBase + TMDBApiController.GetActorPosters(reasonCasts[0].Id).Profiles[0].FilePath);
                movieRecommendationActorDetailController.UpdateMovieNumber(TMDBApiController.GetMovieName(movieIDPairs[previousTargetMovieName]),
                                                                           recommendationCalculator.GetUserLikedActorMovies(reasonCasts[0].Name).Count,
                                                                           recommendationCalculator.GetUserLikedActorMovies(reasonCasts[0].Name).Count.ToString());
            }
            else if (reasonCasts.Count == 2)
            {
                movieRecommendationActorDetailController.UpdateName(reasonCasts[0].Name, reasonCasts[1].Name);
                movieRecommendationActorDetailController.UpdatePoster(posterImageBase + TMDBApiController.GetActorPosters(reasonCasts[0].Id).Profiles[0].FilePath,
                                                                      posterImageBase + TMDBApiController.GetActorPosters(reasonCasts[1].Id).Profiles[0].FilePath);
                movieRecommendationActorDetailController.UpdateMovieNumber(TMDBApiController.GetMovieName(movieIDPairs[previousTargetMovieName]),
                                                                           recommendationCalculator.GetUserLikedActorMovies(reasonCasts[0].Name).Count + recommendationCalculator.GetUserLikedActorMovies(reasonCasts[1].Name).Count,
                                                                           recommendationCalculator.GetUserLikedActorMovies(reasonCasts[0].Name).Count.ToString(),
                                                                           recommendationCalculator.GetUserLikedActorMovies(reasonCasts[1].Name).Count.ToString());
            }
            else if (reasonCasts.Count >= 3)
            {
                movieRecommendationActorDetailController.UpdateName(reasonCasts[0].Name, reasonCasts[1].Name, reasonCasts[2].Name);
                movieRecommendationActorDetailController.UpdatePoster(posterImageBase + TMDBApiController.GetActorPosters(reasonCasts[0].Id).Profiles[0].FilePath,
                                                                      posterImageBase + TMDBApiController.GetActorPosters(reasonCasts[1].Id).Profiles[0].FilePath,
                                                                      posterImageBase + TMDBApiController.GetActorPosters(reasonCasts[2].Id).Profiles[0].FilePath);
                movieRecommendationActorDetailController.UpdateMovieNumber(TMDBApiController.GetMovieName(movieIDPairs[previousTargetMovieName]),
                                                                           recommendationCalculator.GetUserLikedActorMovies(reasonCasts[0].Name).Count + recommendationCalculator.GetUserLikedActorMovies(reasonCasts[1].Name).Count + recommendationCalculator.GetUserLikedActorMovies(reasonCasts[2].Name).Count,
                                                                           recommendationCalculator.GetUserLikedActorMovies(reasonCasts[0].Name).Count.ToString(),
                                                                           recommendationCalculator.GetUserLikedActorMovies(reasonCasts[1].Name).Count.ToString(),
                                                                           recommendationCalculator.GetUserLikedActorMovies(reasonCasts[2].Name).Count.ToString());
            }
            else
            {
                movieRecommendationActorDetailController.UpdateName();
                movieRecommendationActorDetailController.UpdatePoster();
                movieRecommendationActorDetailController.UpdateMovieNumber(TMDBApiController.GetMovieName(movieIDPairs[previousTargetMovieName]), 0);
            }

            selectRecommendationGenreWhy.hitRecommendationGenreWhy = false;
            selectRecommendationPublicityWhy.hitPublicityWhy = false;
            selectRecommendationRatingWhy.hitRecommendationRatingWhy = false;
        }
        else
        {
            if (previousHitRecommendationRatingWhy == false &&
                previousHitPublicityWhy == false &&
                previousHitRecommendationActorWhy == false &&
                previousHitRecommendationGenreWhy == false)
            {
                ShiftJustificationInterface("Close");
            }
            movieRecommendationActorDetailController.CloseActorJustification();
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
                movieRecommendationActorMoreController.UpdateRecommendationFullList_part(recommendationCalculator.GetUserLikedActorMovies(reasonCasts[0].Name),
                                                                                         recommendationCalculator.GetUserLikedActorMovieUrls(reasonCasts[0].Name),
                                                                                         posterImageBase);
                movieRecommendationActorMoreController.ShowRecommendationFullList_part();

                movieRecommendationActorDetailController.gameObject.SetActive(false);
            }
            else
            {
                movieRecommendationActorMoreController.gameObject.SetActive(false);

                movieRecommendationActorDetailController.gameObject.SetActive(true);
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
                movieRecommendationActorMoreController.UpdateRecommendationFullList_part(recommendationCalculator.GetUserLikedActorMovies(reasonCasts[1].Name),
                                                                                         recommendationCalculator.GetUserLikedActorMovieUrls(reasonCasts[1].Name),
                                                                                         posterImageBase);
                movieRecommendationActorMoreController.ShowRecommendationFullList_part();

                movieRecommendationActorDetailController.gameObject.SetActive(false);
            }
            else
            {
                movieRecommendationActorMoreController.gameObject.SetActive(false);

                movieRecommendationActorDetailController.gameObject.SetActive(true);
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
                movieRecommendationActorMoreController.UpdateRecommendationFullList_part(recommendationCalculator.GetUserLikedActorMovies(reasonCasts[2].Name),
                                                                                         recommendationCalculator.GetUserLikedActorMovieUrls(reasonCasts[2].Name),
                                                                                         posterImageBase);
                movieRecommendationActorMoreController.ShowRecommendationFullList_part();

                movieRecommendationActorDetailController.gameObject.SetActive(false);
            }
            else
            {
                movieRecommendationActorMoreController.gameObject.SetActive(false);

                movieRecommendationActorDetailController.gameObject.SetActive(true);
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
                List<string> names = recommendationCalculator.GetUserLikedGenreMovieNames(genres[mode - 1]);
                List<string> posterUrls = recommendationCalculator.GetUserLikedGenreMovieUrls(genres[mode - 1]);

                movieRecommendationGenreMoreController.UpdateRecommendationGenreDetailTitle(reasonMovieGenres[genres[mode - 1]], genres[mode - 1]);
                movieRecommendationGenreMoreController.UpdateRecommendationFullList_part(names, posterUrls, posterImageBase);
                movieRecommendationGenreMoreController.ShowRecommendationFullList_part();

                movieRecommendationGenreDetailController.gameObject.SetActive(false);
            }
            else
            {
                movieRecommendationGenreMoreController.gameObject.SetActive(false);

                movieRecommendationGenreDetailController.gameObject.SetActive(true);
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
                List<string> names = recommendationCalculator.GetUserLikedGenreMovieNames(genres[mode - 1]);
                List<string> posterUrls = recommendationCalculator.GetUserLikedGenreMovieUrls(genres[mode - 1]);

                movieRecommendationGenreMoreController.UpdateRecommendationGenreDetailTitle(reasonMovieGenres[genres[mode - 1]], genres[mode - 1]);
                movieRecommendationGenreMoreController.UpdateRecommendationFullList_part(names, posterUrls, posterImageBase);
                movieRecommendationGenreMoreController.ShowRecommendationFullList_part();

                movieRecommendationGenreDetailController.gameObject.SetActive(false);
            }
            else
            {
                movieRecommendationGenreMoreController.gameObject.SetActive(false);

                movieRecommendationGenreDetailController.gameObject.SetActive(true);
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
                List<string> names = recommendationCalculator.GetUserLikedGenreMovieNames(genres[mode - 1]);
                List<string> posterUrls = recommendationCalculator.GetUserLikedGenreMovieUrls(genres[mode - 1]);

                movieRecommendationGenreMoreController.UpdateRecommendationGenreDetailTitle(reasonMovieGenres[genres[mode - 1]], genres[mode - 1]);
                movieRecommendationGenreMoreController.UpdateRecommendationFullList_part(names, posterUrls, posterImageBase);
                movieRecommendationGenreMoreController.ShowRecommendationFullList_part();

                movieRecommendationGenreDetailController.gameObject.SetActive(false);
            }
            else
            {
                movieRecommendationGenreMoreController.gameObject.SetActive(false);

                movieRecommendationGenreDetailController.gameObject.SetActive(true);
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
                List<string> names = recommendationCalculator.GetUserLikedGenreMovieNames(genres[mode - 1]);
                List<string> posterUrls = recommendationCalculator.GetUserLikedGenreMovieUrls(genres[mode - 1]);

                movieRecommendationGenreMoreController.UpdateRecommendationGenreDetailTitle(reasonMovieGenres[genres[mode - 1]], genres[mode - 1]);
                movieRecommendationGenreMoreController.UpdateRecommendationFullList_part(names, posterUrls, posterImageBase);
                movieRecommendationGenreMoreController.ShowRecommendationFullList_part();

                movieRecommendationGenreDetailController.gameObject.SetActive(false);
            }
            else
            {
                movieRecommendationGenreMoreController.gameObject.SetActive(false);

                movieRecommendationGenreDetailController.gameObject.SetActive(true);
            }
        }
    }

    public UserLikedMoviesController userLikedMoviesController;
    public FilterNumberHolder filterNumberHolder;
    public void UpdateRecommendationBase()
    {
        int new_actorScoreBase = (int)(userLikedMoviesController.GetUserLikedMovieAmount() * filterNumberHolder.actorBaseRatio);
        int new_genreScoreBase = (int)(userLikedMoviesController.GetUserLikedMovieAmount() * filterNumberHolder.genreBaseRatio);
        int new_publicityScoreBase = (int)(userLikedMoviesController.GetUserLikedMovieAmount() * filterNumberHolder.publicityBaseRatio);
        int new_ratingScoreBase = (int)(100 * filterNumberHolder.ratingBaseRatio);
        
        if (actorScoreBase != new_actorScoreBase ||
            genreScoreBase != new_genreScoreBase ||
            publicityScoreBase != new_publicityScoreBase ||
            ratingScoreBase != new_ratingScoreBase)
        {
            actorScoreBase = new_actorScoreBase;
            genreScoreBase = new_genreScoreBase;
            publicityScoreBase = new_publicityScoreBase;
            ratingScoreBase = new_ratingScoreBase;

            selectRecommendationWhy.hitRecommendationScoreWhy = false;
        }


    }

    public void UpdateRecommendationBase_DirectControlActor(float percentage)
    {
        int new_actorScoreBase = (int)(userLikedMoviesController.GetUserLikedMovieAmount() * percentage);

        if (actorScoreBase != new_actorScoreBase)
        {
            actorScoreBase = new_actorScoreBase;
            userPreferenceStore.SetActorPreference(actorScoreBase);

            UpdateRecommendation();
            //selectRecommendationWhy.hitRecommendationScoreWhy = false;

            movieRecommendationSubTitleController.DirectUpdateActorColor2(percentage, (float)actorScore / userLikedMoviesController.GetUserLikedMovieAmount());
        }
    }

    public void UpdateRecommendationBase_DirectControlGenre(float percentage)
    {
        int new_genreScoreBase = (int)(userLikedMoviesController.GetUserLikedMovieAmount() * percentage);

        if (genreScoreBase != new_genreScoreBase)
        {
            genreScoreBase = new_genreScoreBase;
            userPreferenceStore.SetGenrePreference(genreScoreBase);

            UpdateRecommendation();
            //selectRecommendationWhy.hitRecommendationScoreWhy = false;

            movieRecommendationSubTitleController.DirectUpdateGenreColor2(percentage, (float)genreScore / userLikedMoviesController.GetUserLikedMovieAmount());
        }
    }

    public void UpdateRecommendationBase_DirectControlPublicity(float percentage)
    {
        int new_puclicityScoreBase = (int)(userLikedMoviesController.GetUserLikedMovieAmount() * percentage);

        if (publicityScoreBase != new_puclicityScoreBase)
        {
            publicityScoreBase = new_puclicityScoreBase;
            userPreferenceStore.SetPublicityPreference(publicityScoreBase);

            UpdateRecommendation();
            //selectRecommendationWhy.hitRecommendationScoreWhy = false;

            movieRecommendationSubTitleController.DirectUpdatePublicityColor2(percentage, (float) publicityScore / userLikedMoviesController.GetUserLikedMovieAmount());
        }
    }

    public void UpdateRecommendationBase_DirectControlRating(float percentage)
    {
        int new_ratingScoreBase = (int)(100 * percentage);

        if (ratingScoreBase != new_ratingScoreBase)
        {
            ratingScoreBase = new_ratingScoreBase;
            userPreferenceStore.SetRatingPreference(ratingScoreBase);

            UpdateRecommendation();
            //selectRecommendationWhy.hitRecommendationScoreWhy = false;

            movieRecommendationSubTitleController.DirectUpdateRatingColor2(percentage, (float)ratingScore);
        }
    }

    public float GetActorScoreBaseRatio()
    {
        return (float)actorScoreBase / userLikedMoviesController.GetUserLikedMovieAmount();
    }

    public float GetGenreScoreBaseRatio()
    {
        return (float)genreScoreBase / userLikedMoviesController.GetUserLikedMovieAmount();
    }

    public float GetPublicityScoreBaseRatio()
    {
        return (float)publicityScoreBase / userLikedMoviesController.GetUserLikedMovieAmount();
    }

    public float GetRatingScoreBaseRatio()
    {
        return ratingScoreBase / 100.0f;
    }


    public MovieRecommendationReview movieRecommendationReview;
    public SelectMovieRecommendationReview selectMovieRecommendationReview;
    private void ShowRecommendationReview()
    {
        if (previousHitRecommendationReview == true)
        {
            movieRecommendationReview.gameObject.SetActive(true);
            movieRecommendationReview.ShowReview(TMDBApiController.GetMovieReview(movieIDPairs[previousTargetMovieName]));
        }
        else
        {
            movieRecommendationReview.gameObject.SetActive(false);
        }
    }

    public SelectMovieRecommendationReview selectMovieRecommendationReviewNext;
    private void ShowRecommendationReviewNext()
    {
        if (previousHitRecommendationReviewNext == true)
        {
            movieRecommendationReview.NextReview();
            previousHitRecommendationReviewNext = false;
            selectMovieRecommendationReviewNext.hitRecommendationReviewNext = false;
        }
    }

    public SelectMovieRecommendationReview selectMovieRecommendationReviewPrevious;
    private void ShowRecommendationReviewPrevious()
    {
        if (previousHitRecommendationReviewPrevious == true)
        {
            movieRecommendationReview.PreviousReview();
            previousHitRecommendationReviewPrevious = false;
            selectMovieRecommendationReviewPrevious.hitRecommendationReviewPrevious = false;
        }
    }
}
