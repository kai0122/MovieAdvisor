using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMDbLib.Client;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Search;
using UnityEngine;


public class ExplanationController : MonoBehaviour
{
    public MovieDatabase LargeMovieDatabase;

    public TargetLister targetLister;
    public ChildObjectsActivator childObjectsActivator;
    //private List<movieDBItem> MovieDataBase = new List<movieDBItem>();
    //private Dictionary<int, movieDBItem> MovieRecDB = new Dictionary<int, movieDBItem>();

    public Loader movieDisplayer;
    public ExplanationLoader movieExplanationDisplayer;
    public ExplanationLoader movieExplanationDisplayer2;
    public ExplanationLoader movieExplanationDisplayer_Name;
    public ExplanationLoader movieExplanationDisplayer_Info;
    public ExplanationTrailerLoader movieExplanationDisplayer4;
    private MovieInformation movieInformation = new MovieInformation();
    //public List<movieRecommendItem> recommendMovies = new List<movieRecommendItem>();
    //private List<movieDBItem> userLikedMovies = new List<movieDBItem>();

    //private string FILEPATH = "movieSearched.csv";
    //private string FILEPATH_REC = "movieRec.csv";
    //private List<int> movieLikedIds = new List<int> { 2062, 299534, 857, 424694, 207703, 259316, 333339 };

    private string posterImageBase = "https://image.tmdb.org/t/p/original//";
    private string youtubeVideoBase = "https://www.youtube.com/watch?v=";

    private int currentMovieIndexForRecommend;

    private string _movideDbApiKey = "06be00eafe8419222c854f8db00b8197";
    TMDbClient client = null;


    //private List<string> userLikedGenres = new List<string>();
    //private Dictionary<string, int> userLikedGenresCount = new Dictionary<string, int>();
    //private Dictionary<string, float> userLikedGenresRatio = new Dictionary<string, float>();

    public MovieInformationGraphicalLoader movieInformationGraphicalLoader;
    public SimpleBar movieInformationBar;


    // Start is called before the first frame update
    void Start()
    {
        //client = new TMDbClient(_movideDbApiKey);

        //LoadFromFile(FILEPATH, false);
        //LoadFromFile(FILEPATH_REC, true);

        //ReadFromFile_Android(FILEPATH, false);
        //ReadFromFile_Android(FILEPATH_REC, true);

        //ShowDebugText(movieDisplayer, "Load " + LargeMovieDatabase.MovieDataBase.Count + " Movie in DataBase...\n" +
        //                              "Load " + LargeMovieDatabase.MovieRecDB.Count + " Movie in RecSys DataBase...");

        //GetUserLikedMovies();
        //LoadRecommendMovies(1);
        //ShowDebugText(movieDisplayer, "Recommend " + LargeMovieDatabase.recommendMovies.Count + " Movies ...");
        //WriteMovieRecDB();
        /*
        foreach(movieRecommendItem movie in recommendMovies)
        {
            Debug.Log(movie.movie.title);
            Debug.Log(posterImageBase + movie.movie.posterKey);
        }
        */
        //currentMovieIndexForRecommend = 0;
        //ShowNextMovie();
        //ShowNextMovieForSelection();
    }

    string prevTargetName = "FirstName";
    // Update is called once per frame
    void Update()
    {
        if (childObjectsActivator.currentTrackedName != prevTargetName)
        {
            // target change
            prevTargetName = childObjectsActivator.currentTrackedName;

            if (LargeMovieDatabase.mode == 0)
            {
                movieExplanationDisplayer.SetTextActive(true);

                movieInformationBar.gameObject.SetActive(false);
                movieInformationGraphicalLoader.gameObject.SetActive(false);
            }
            else if (LargeMovieDatabase.mode == 1)
            {
                movieExplanationDisplayer.SetTextActive(false);

                movieInformationBar.gameObject.SetActive(true);
                movieInformationGraphicalLoader.gameObject.SetActive(true);
            }

            ShowExplanation();
            //ShowExplanationText(movieExplanationDisplayer, targetLister.trackedTargetName);

        }

        CheckChangeExplanationMode();
    }

    private int prevMode = 0;
    public GameObject explanation;
    public void CheckChangeExplanationMode()
    {
#pragma warning disable CS0618 // 類型或成員已經過時
        if (explanation.active)
#pragma warning restore CS0618 // 類型或成員已經過時
        {
            if (prevMode != LargeMovieDatabase.mode)
            {
                prevMode = LargeMovieDatabase.mode;
                if (prevMode == 0)
                {
                    movieExplanationDisplayer.SetTextActive(true);

                    movieInformationBar.SetGraphActive(false);
                    movieInformationGraphicalLoader.SetGraphActive(false);
                }
                else if (prevMode == 1)
                {
                    movieExplanationDisplayer.SetTextActive(false);

                    movieInformationBar.SetGraphActive(true);
                    movieInformationGraphicalLoader.SetGraphActive(true);
                }
                if (childObjectsActivator.currentTrackedName == prevTargetName)
                {
                    ShowExplanation();
                }
                else
                {
                    prevTargetName = childObjectsActivator.currentTrackedName;
                }
            }
        }
    }

    public void PressMovieDisplayBoard()
    {
        Debug.Log("hit!!");
        /*
        if (currentMovieIndexForRecommend == 0)
        {
            foreach (string key in recommendMovies[recommendMovies.Count - 1].movie.videoKey.Split(' '))
            {
                UnityEngine.Debug.Log(key);
                if (ShowVideo(key))
                {
                    break;
                }
            }
            
        }
        else
        {
            foreach (string key in recommendMovies[currentMovieIndexForRecommend - 1].movie.videoKey.Split(' '))
            {
                UnityEngine.Debug.Log(key);
                if (ShowVideo(key))
                {
                    break;
                }
            }
        }*/
        ShowNextMovie();
        //ShowNextMovieForSelection();
    }

    private void GetUserLikedMovies()
    {
        foreach (movieDBItem movie in LargeMovieDatabase.MovieDataBase)
        {
            if (LargeMovieDatabase.movieLikedIds.Contains(movie.id))
            {
                LargeMovieDatabase.userLikedMovies.Add(movie);
            }
        }
    }

    public void ShowNextMovieForSelection()
    {
        movieDBItem result = LargeMovieDatabase.MovieDataBase[currentMovieIndexForRecommend];
        ShowImage(movieDisplayer, result.posterKey);
        ShowDebugText(movieDisplayer, result.title + "/" + result.id.ToString());

        if (currentMovieIndexForRecommend < LargeMovieDatabase.MovieDataBase.Count - 1)
        {
            currentMovieIndexForRecommend += 1;
        }
        else
        {
            currentMovieIndexForRecommend = 0;
        }

    }

    public void ShowNextMovie()
    {
        movieRecommendItem result = LargeMovieDatabase.recommendMovies[currentMovieIndexForRecommend];
        ShowImage(movieDisplayer, result.movie.posterKey);
        ShowInformationText(movieDisplayer,
                            result.movie.title,
                            result.movie.releaseDate.Substring(0, 4),
                            result.movie.popularity,
                            result.movie.overview,
                            result.movie.voteAverage,
                            result.movie.voteCount,
                            result.movie.genreIds,
                            result.movie.genres);

        if (currentMovieIndexForRecommend < LargeMovieDatabase.recommendMovies.Count - 1)
        {
            currentMovieIndexForRecommend += 1;
        }
        else
        {
            currentMovieIndexForRecommend = 0;
        }
        
    }

    public void ShowExplanation()
    {
        movieRecommendItem result = null;
        foreach (movieRecommendItem movie in LargeMovieDatabase.recommendMovies)
        {
            Debug.Log(movie.movie.title.Replace(" ", "").Replace(":", "-").Replace("!", "").Replace("'", "").Replace("&", ""));
            if (movie.movie.title.Replace(" ", "").Replace(":", "-").Replace("!", "").Replace("'", "").Replace("&", "") == prevTargetName)
            {
                result = movie;
                break;
            }
        }

        if (result != null)
        {
            string reason = "Recommend because: \n";
            foreach (movieDBItem movie in result.parentMovie)
            {
                reason += ("   |  " + movie.title + "\n");
                ShowExplanationImage(movie.posterKey);
            }
            //ShowExplanationText(movieExplanationDisplayer, reason);

            string reason2 = "Movies In Your Profile: \n";
            int counting = 0;
            for(int index = 0; index < LargeMovieDatabase.userLikedGenres.Count; index++)
            {
                if (result.movie.genres.Contains(LargeMovieDatabase.userLikedGenres[index])){
                    //counting -= userLikedGenresRatio[userLikedGenres[index]] * 100;
                    //reason2 += userLikedGenres[index] + ": " + userLikedGenresRatio[userLikedGenres[index]] * 100 + "%\n";
                    if (LargeMovieDatabase.mode == 0) reason2 += "  |  " + LargeMovieDatabase.userLikedGenres[index] + ": " + LargeMovieDatabase.userLikedGenresCount[LargeMovieDatabase.userLikedGenres[index]] + "\n";
                    ShowExplanationBar(LargeMovieDatabase.userLikedGenres[index] + ": " + LargeMovieDatabase.userLikedGenresCount[LargeMovieDatabase.userLikedGenres[index]].ToString() + "/" + LargeMovieDatabase.userLikedMovies.Count.ToString(), counting++, (float)LargeMovieDatabase.userLikedGenresCount[LargeMovieDatabase.userLikedGenres[index]]/ LargeMovieDatabase.userLikedMovies.Count, result.parentMovie.Count);
                }
            }
            //ShowExplanationText(movieExplanationDisplayer2, reason2);
            ShowExplanationText(movieExplanationDisplayer, reason + "\n\n\n" + reason2);

            string reason3 = result.movie.genres;
            ShowExplanationText(movieExplanationDisplayer_Info, reason3);

            string reason4 = result.movie.title + ":";
            ShowExplanationText(movieExplanationDisplayer_Name, reason4);

            /*
            foreach (string key in result.movie.videoKey.Split(' '))
            {
                if (ShowExplanationVideo(key))
                {
                    break;
                }
            }
            */
        }
        else
        {
            ShowExplanationText(movieExplanationDisplayer, "");
            ShowExplanationText(movieExplanationDisplayer2, "");
            ShowExplanationText(movieExplanationDisplayer_Info, "Not found movie in database...");
        }
    }

    public void PlayPauseTrailer()
    {
        VideoLoader videoLoader = movieExplanationDisplayer4.GetVideoLoader().GetComponent<VideoLoader>();
        videoLoader.ChangeVideoPlayMode();
    }

    private int ShowNextMovieFromList(int _mode)
    {
        if (currentMovieIndexForRecommend >= LargeMovieDatabase.recommendMovies.Count - 1)
        {
            currentMovieIndexForRecommend = 0;
        }
        else
        {
            currentMovieIndexForRecommend += 1;
        }

        if (LargeMovieDatabase.recommendMovies.Count == 0)
        {
            if (_mode == 1)
            {
                foreach (movieDBItem movie in LargeMovieDatabase.userLikedMovies)
                {
                    foreach (int movieID in movie.similarMovieIds)
                    {
                        int index = ContainsMovieInRecList(movieID);
                        if (index == -1) //  not exist
                        {
                            movieRecommendItem newMovie = new movieRecommendItem();
                            newMovie.movie = LargeMovieDatabase.MovieRecDB[movieID];
                            newMovie.parentMovie = new List<movieDBItem>();
                            newMovie.parentMovie.Add(movie);
                            newMovie.counting = 1;

                            LargeMovieDatabase.recommendMovies.Add(newMovie);
                        }
                        else
                        {
                            int i = 0;
                            foreach (movieDBItem parentMovie in LargeMovieDatabase.recommendMovies[index].parentMovie)
                            {
                                if (parentMovie.id == movie.id)
                                {
                                    break;
                                }
                                ++i;
                            }

                            if (i == LargeMovieDatabase.recommendMovies[index].parentMovie.Count)
                            {
                                LargeMovieDatabase.recommendMovies[index].parentMovie.Add(movie);
                                LargeMovieDatabase.recommendMovies[index].counting += 1;
                            }
                        }
                    }
                }
            }
            else
            {
                foreach (movieDBItem movie in LargeMovieDatabase.userLikedMovies)
                {
                    foreach (int movieID in movie.recommendationMovieIds)
                    {
                        int index = ContainsMovieInRecList(movieID);
                        if (index == -1) //  not exist
                        {
                            movieRecommendItem newMovie = new movieRecommendItem();
                            newMovie.movie = LargeMovieDatabase.MovieRecDB[movieID];
                            newMovie.parentMovie = new List<movieDBItem>();
                            newMovie.parentMovie.Add(movie);
                            newMovie.counting = 1;

                            LargeMovieDatabase.recommendMovies.Add(newMovie);
                        }
                        else
                        {
                            int i = 0;
                            foreach (movieDBItem parentMovie in LargeMovieDatabase.recommendMovies[index].parentMovie)
                            {
                                if (parentMovie.id == movie.id)
                                {
                                    break;
                                }
                                ++i;
                            }

                            if (i == LargeMovieDatabase.recommendMovies[index].parentMovie.Count)
                            {
                                LargeMovieDatabase.recommendMovies[index].parentMovie.Add(movie);
                                LargeMovieDatabase.recommendMovies[index].counting += 1;
                            }
                        }
                    }
                }
            }
            UnityEngine.Debug.Log("Recommend " + LargeMovieDatabase.recommendMovies.Count.ToString() + " movies for you...");

            string s = "";
            foreach (movieRecommendItem item in LargeMovieDatabase.recommendMovies)
            {
                s += item.counting.ToString();
            }
            UnityEngine.Debug.Log(s);
            LargeMovieDatabase.recommendMovies = Sort(LargeMovieDatabase.recommendMovies);
            s = "";
            foreach (movieRecommendItem item in LargeMovieDatabase.recommendMovies)
            {
                s += item.counting.ToString();
            }
            UnityEngine.Debug.Log(s);
        }

        //UnityEngine.Debug.Log(result.Title + " (" + result.ReleaseDate + ")");
        //UnityEngine.Debug.Log(result.VoteAverage);
        //UnityEngine.Debug.Log("Movie ID: " + result.Id.ToString());

        movieRecommendItem result = LargeMovieDatabase.recommendMovies[currentMovieIndexForRecommend];
        ShowImage(movieDisplayer, result.movie.posterKey);
        ShowInformationText(movieDisplayer,
                            result.movie.title,
                            result.movie.releaseDate.Substring(0, 4),
                            result.movie.popularity,
                            result.movie.overview,
                            result.movie.voteAverage,
                            result.movie.voteCount,
                            result.movie.genreIds,
                            result.movie.genres);


        return result.movie.id;
    }

    private movieDBItem SearchMovie2MovieDBItem(SearchMovie result)
    {
        movieDBItem _movieDBItem = new movieDBItem();
        _movieDBItem.id = result.Id;
        _movieDBItem.title = result.Title;
        _movieDBItem.releaseDate = result.ReleaseDate.ToString();
        _movieDBItem.popularity = result.Popularity;
        _movieDBItem.overview = result.Overview;
        _movieDBItem.voteAverage = result.VoteAverage;
        _movieDBItem.voteCount = result.VoteCount;
        _movieDBItem.genreIds = new List<int>(Array.ConvertAll(string.Join(" ", result.GenreIds).Split(' '), int.Parse));
        _movieDBItem.genres = GetMovieGenreStringWithGenreIds(result.GenreIds, FindMovieGenres());
        _movieDBItem.videoKey = string.Join(" ", GetVideoKeyWithMovieID(result.Id));
        _movieDBItem.posterKey = result.PosterPath;
        if (string.Join(" ", FindSimilarIdsWithGenreId(result.GenreIds)) != "")
            _movieDBItem.similarMovieIds = new List<int>(Array.ConvertAll(string.Join(" ", FindSimilarIdsWithGenreId(result.GenreIds)).Split(' '), int.Parse));
        if (string.Join(" ", FindRecommendationIdsWithMovieID(result.Id)) != "")
            _movieDBItem.recommendationMovieIds = new List<int>(Array.ConvertAll(string.Join(" ", FindRecommendationIdsWithMovieID(result.Id)).Split(' '), int.Parse));

        return _movieDBItem;
    }

    private void ReadFromFile_Android(string _fileName, bool _isRecMovie)
    {
        string path = "jar:file://" + Application.dataPath + "!/assets/" + _fileName;

        WWW wwwfile = new WWW(path);
        while (!wwwfile.isDone) { }

        var filepath = string.Format("{0}/{1}", Application.persistentDataPath, _fileName);
        File.WriteAllBytes(filepath, wwwfile.bytes);

        StreamReader read = new StreamReader(filepath);
        string file = read.ReadToEnd();

        //These are the variables I set
        int id = 0;
        string title = "";
        string date = "";
        float popularity = 0;
        string overview = "";
        float voteAverage = 0;
        int voteCount = 0;
        string genreIds = "";    // need to be List in future
        string genreNames = "";
        string videoKey = "";
        string posterKey = "";
        string similarIds = "";
        string recommendationIds = "";

        //This is to get all the lines using Method StreamReader
        string[] lines = file.Split("\n"[0]);

        for (var i = 0; i < lines.Length - 1; i++)
        {
            //This is to get every thing that is comma separated
            string[] parts = lines[i].Split("^"[0]);

            int.TryParse(parts[0], out id);
            title = parts[1];
            date = parts[2];
            float.TryParse(parts[3], out popularity);
            overview = parts[4];
            float.TryParse(parts[5], out voteAverage);
            int.TryParse(parts[6], out voteCount);
            genreIds = parts[7];
            genreNames = parts[8];
            videoKey = parts[9];
            posterKey = parts[10];
            similarIds = parts[11];
            recommendationIds = parts[12];

            movieDBItem _movieDBItem = new movieDBItem();
            _movieDBItem.id = id;
            _movieDBItem.title = title;
            _movieDBItem.releaseDate = date;
            _movieDBItem.popularity = popularity;
            _movieDBItem.overview = overview;
            _movieDBItem.voteAverage = voteAverage;
            _movieDBItem.voteCount = voteCount;
            _movieDBItem.genreIds = new List<int>(Array.ConvertAll(genreIds.Split(' '), int.Parse));
            _movieDBItem.genres = genreNames;
            _movieDBItem.videoKey = videoKey;
            _movieDBItem.posterKey = posterKey;
            if (similarIds != "")
            {
                _movieDBItem.similarMovieIds = new List<int>(Array.ConvertAll(similarIds.Split(' '), int.Parse));
            }
            else
            {
                _movieDBItem.similarMovieIds = new List<int>();
            }
            if (recommendationIds != "")
            {
                _movieDBItem.recommendationMovieIds = new List<int>(Array.ConvertAll(recommendationIds.Split(' '), int.Parse));
            }
            else
            {
                _movieDBItem.recommendationMovieIds = new List<int>();
            }

            if (_isRecMovie)
            {
                if (!LargeMovieDatabase.MovieRecDB.ContainsKey(_movieDBItem.id))
                {
                    LargeMovieDatabase.MovieRecDB.Add(_movieDBItem.id, _movieDBItem);
                }
            }
            else
            {
                LargeMovieDatabase.MovieDataBase.Add(_movieDBItem);
            }
        }

        if (_isRecMovie)
        {
            UnityEngine.Debug.Log("Load " + LargeMovieDatabase.MovieRecDB.Count + " Movie in RecSys DataBase...");
        }
        else
        {
            LargeMovieDatabase.MovieDataBase = Shuffle(LargeMovieDatabase.MovieDataBase);
            UnityEngine.Debug.Log("Add " + LargeMovieDatabase.MovieDataBase.Count + " Movie in DataBase...");
        }
    }

    private List<SearchMovie> FindMovieRecommendationWithMovieID(int _movieId)
    {
        SearchContainer<SearchMovie> results = client.GetMovieRecommendationsAsync(_movieId, page: 2).Result;

        UnityEngine.Debug.Log($"Got {results.Results.Count:N0} of {results.TotalResults:N0} results");
        return results.Results;
    }

    private List<int> FindSimilarIdsWithGenreId(List<int> _genreId)
    {
        List<int> idList = new List<int>();

        foreach (int genreId in _genreId)
        {
#pragma warning disable CS0618 // 類型或成員已經過時
            SearchContainer<SearchMovie> results = client.GetGenreMoviesAsync(genreId, page: 1).Result;
#pragma warning restore CS0618 // 類型或成員已經過時

            foreach (SearchMovie result in results.Results)
            {
                idList.Add(result.Id);
            }
        }

        return idList;
    }

    private List<SearchMovie> FindSimilarMovieWithGenreId(List<int> _genreId)
    {
        List<SearchMovie> movieList = new List<SearchMovie>();

        foreach (int genreId in _genreId)
        {
#pragma warning disable CS0618 // 類型或成員已經過時
            SearchContainer<SearchMovie> results = client.GetGenreMoviesAsync(genreId, page: 1).Result;
#pragma warning restore CS0618 // 類型或成員已經過時

            movieList.AddRange(results.Results);
        }

        return movieList;
    }

    private List<int> FindRecommendationIdsWithMovieID(int _movieId)
    {
        SearchContainer<SearchMovie> results = client.GetMovieRecommendationsAsync(_movieId, page: 2).Result;

        UnityEngine.Debug.Log($"Got {results.Results.Count:N0} of {results.TotalResults:N0} results");

        List<int> idList = new List<int>();
        foreach (SearchMovie result in results.Results)
        {
            idList.Add(result.Id);
        }
        return idList;
    }

    private List<Genre> FindMovieGenres()
    {
        List<Genre> results = client.GetMovieGenresAsync().Result;

        foreach (Genre result in results)
        {
            UnityEngine.Debug.Log(result.Name);
        }
        return results;
    }

    private List<string> GetVideoKeyWithMovieID(int _movieId)
    {
        ResultContainer<TMDbLib.Objects.General.Video> results = client.GetMovieVideosAsync(_movieId).Result;

        List<string> keyList = new List<string>();
        foreach (TMDbLib.Objects.General.Video result in results.Results)
        {
            keyList.Add(result.Key);
        }
        return keyList;
    }

    private string GetMovieGenreStringWithGenreIds(List<int> _ids, List<Genre> _genres)
    {
        string genreStr = "";

        foreach (Genre genre in _genres)
        {
            if (_ids.Contains(genre.Id))
            {
                genreStr += genre.Name + " | ";
            }
        }

        return genreStr;
    }

    private SearchContainer<SearchMovie> FindTrendingMovies()
    {
        SearchContainer<SearchMovie> results = client.GetTrendingMoviesAsync(TMDbLib.Objects.Trending.TimeWindow.Week).Result;

        UnityEngine.Debug.Log($"Got {results.Results.Count:N0} of {results.TotalResults:N0} results");
        return results;
    }

    private SearchContainer<SearchMovie> FindPopularMovies(int _page)
    {
        SearchContainer<SearchMovie> results = client.GetMoviePopularListAsync(page: _page, language: "en").Result;

        UnityEngine.Debug.Log($"Got {results.Results.Count:N0} of {results.TotalResults:N0} results");
        return results;
    }

    private SearchContainer<SearchMovie> FindTopRatedMovies(int _page)
    {
        SearchContainer<SearchMovie> results = client.GetMovieTopRatedListAsync(page: _page, language: "en").Result;

        UnityEngine.Debug.Log($"Got {results.Results.Count:N0} of {results.TotalResults:N0} results");
        return results;
    }

    private int ContainsMovieInRecList(int _newMovieID)
    {
        int index = 0;
        foreach (movieRecommendItem movie in LargeMovieDatabase.recommendMovies)
        {
            if (_newMovieID == movie.movie.id)
            {
                return index;
            }
            ++index;
        }
        return -1;
    }

    private string getPath(string _fileName)
    {
#if     UNITY_EDITOR
        return Application.dataPath + "/StreamingAssets/" + _fileName;
#elif   UNITY_ANDROID
        return "jar:file://" + Application.dataPath + "!/assets/" + _fileName;
#elif   UNITY_IPHONE
        return Application.persistentDataPath+"/"+_fileName;
#else
        return Application.streamingAssetsPath + "/" + _fileName;
#endif
    }

    private List<T> Shuffle<T>(List<T> _list)
    {
        List<T> list = _list;

        System.Random rnd = new System.Random();
        for (var i = 0; i < list.Count; i++)
            list = Swap(list, i, rnd.Next(i, list.Count));

        return list;
    }

    private List<T> Swap<T>(List<T> _list, int i, int j)
    {
        var temp = _list[i];
        _list[i] = _list[j];
        _list[j] = temp;

        return _list;
    }

    private List<movieRecommendItem> Sort(List<movieRecommendItem> _movieRecommends)
    {
        int n = _movieRecommends.Count, i, j, flag;
        movieRecommendItem val;
        for (i = 1; i < n; i++)
        {
            val = _movieRecommends[i];
            flag = 0;
            for (j = i - 1; j >= 0 && flag != 1;)
            {
                if (val.counting > _movieRecommends[j].counting)
                {
                    _movieRecommends[j + 1] = _movieRecommends[j];
                    j--;
                    _movieRecommends[j + 1] = val;
                }
                else flag = 1;
            }
        }

        return _movieRecommends;
    }

    private List<string> SortUserGenres(List<string> _userLikedGenres, Dictionary<string, float> _ratioDict)
    {
        int n = _userLikedGenres.Count, i, j, flag;
        string val;
        for (i = 1; i < n; i++)
        {
            val = _userLikedGenres[i];
            flag = 0;
            for (j = i - 1; j >= 0 && flag != 1;)
            {
                if (_ratioDict[val] > _ratioDict[_userLikedGenres[j]])
                {
                    _userLikedGenres[j + 1] = _userLikedGenres[j];
                    j--;
                    _userLikedGenres[j + 1] = val;
                }
                else flag = 1;
            }
        }

        return _userLikedGenres;
    }

    private void ShowImage(Loader _loader, string _posterPath)
    {
        ImageLoader imageLoader = _loader.GetImageLoader().GetComponent<ImageLoader>();
        //UnityEngine.Debug.Log(posterImageBase + _posterPath);
        imageLoader.ChangeImage(posterImageBase + _posterPath);
    }

    private void ShowExplanationImage(string _posterPath)
    {
        movieInformationGraphicalLoader.AddNewMoviePoster(posterImageBase + _posterPath);
    }

    private void ShowExplanationBar(string _genreName, int _number, float _ratio, int _parentMovieCount)
    {
        movieInformationBar.AddNewSingleBar(_number, _genreName, _ratio, _parentMovieCount);
    }

    private bool ShowVideo(string _key)
    {
        UnityEngine.Debug.Log(_key);
        VideoLoader videoLoader = movieDisplayer.GetVideoLoader().GetComponent<VideoLoader>();
        return videoLoader.ChangeVideo(youtubeVideoBase + _key);
    }

    private bool ShowExplanationVideo(string _key)
    {
        UnityEngine.Debug.Log(_key);
        VideoLoader videoLoader = movieExplanationDisplayer4.GetVideoLoader().GetComponent<VideoLoader>();
        return videoLoader.ChangeVideo(youtubeVideoBase + _key);
    }

    private void ShowInformationText(Loader _loader, string _title, string _releaseDate, double _popularity, string _overview, double _voteAverage, int _voteCount, List<int> _genreIds, string _genres)
    {
        TextLoader textLoader = _loader.GetTextLoader().GetComponent<TextLoader>();
        textLoader.ChangeText(movieInformation.GenerateMovieInformation(_title, _releaseDate, _popularity, _overview, _voteAverage, _voteCount, _genreIds, _genres));
    }

    private void ShowDebugText(Loader _loader, string errMsg)
    {
        TextLoader textLoader = _loader.GetTextLoader().GetComponent<TextLoader>();
        textLoader.ChangeText(errMsg);
    }

    private void ShowExplanationText(ExplanationLoader _loader, string msg)
    {
        TextLoader textLoader = _loader.GetTextLoader().GetComponent<TextLoader>();
        textLoader.ChangeText(msg);
    }

    private List<SearchMovie> Distinct(List<SearchMovie> _list)
    {
        List<SearchMovie> list = _list;

        int i = 0;
        while (i < list.Count - 1)
        {
            for (var j = i + 1; j < list.Count; j++)
            {
                if (list[i].Id == list[j].Id)
                {
                    list.RemoveAt(j);
                }
            }

            ++i;
        }

        return list;
    }

    private void writeToFile(List<movieDBItem> _results, string _fileName)
    {
        StreamWriter writer = new StreamWriter(getPath(_fileName));

        //writer.WriteLine("ID,Title,ReleaseDate,Popularity,Overview,VoteAverage,VoteCount,GenreIds,Genres");
        foreach (movieDBItem result in _results)
        {
            writer.WriteLine(result.id.ToString() + "^" +
                            result.title + "^" +
                            result.releaseDate.ToString() + "^" +
                            result.popularity + "^" +
                            result.overview + "^" +
                            result.voteAverage + "^" +
                            result.voteCount + "^" +
                            string.Join(" ", result.genreIds) + "^" +
                            result.genres + "^" +
                            result.videoKey + "^" +
                            result.posterKey + "^" +
                            "^" +
                            "^");
        }
        writer.Flush();
        writer.Close();
    }


}
