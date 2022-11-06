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

public class MovieDbdataset
{
    public string Posterpath;
    public string Title;
    public string Genres;
    public string Overview;
    //public string ProductionCompanies;
    public string Homepage;
    public string Releasedate;
    public string Runtime;
    public string Tagline;
}

public enum MenuState
{
    Starting,
    SelectMovies,
    ShowMovies,
    ShowMovie,
    PlayMovie
}

public struct movieDBItem
{
    public int id;
    public string title;
    public string releaseDate;
    public double popularity;
    public string overview;
    public double voteAverage;
    public int voteCount;
    public List<int> genreIds;
    public string genres;
    public string videoKey;
    public string posterKey;

    public List<int> similarMovieIds;
    public List<int> recommendationMovieIds;
}

public class movieRecommendItem
{
    public movieDBItem movie;
    public List<movieDBItem> parentMovie;
    public int counting = 0;
}

public class MovieInformation
{
    private string title;
    private string releaseDate;
    private double popularity;
    private string overview;
    private double voteAverage;
    private int voteCount;
    private List<int> genreIds;
    private string genres;

    private string GetYear(string _date)
    {
        _date = _date.Substring(_date.IndexOf('/')+1, _date.Length- _date.IndexOf('/')-1);
        //_date = _date.Substring(_date.IndexOf('/')+1, 4);
        return _date;
    }

    public string GenerateMovieInformation(string _title, string _releaseDate)
    {
        this.title = _title;
        this.releaseDate = _releaseDate;
        this.popularity = 0f;
        this.overview = "";
        this.voteAverage = 0f;
        this.voteCount = 0;
        this.genreIds = null;
        this.genres = null;

        return title + " (" + GetYear(releaseDate) + ")";
    }

    public string GenerateMovieInformation(string _title, string _releaseDate, double _popularity, string _overview, double _voteAverage, int _voteCount, List<int> _genreIds, string _genres)
    {
        this.title = _title;
        this.releaseDate = _releaseDate;
        this.popularity = _popularity;
        this.overview = _overview;
        this.voteAverage = _voteAverage;
        this.voteCount = _voteCount;
        this.genreIds = _genreIds;
        this.genres = _genres;

        return title + " (" + GetYear(releaseDate) + ")\n" +
               //"Genres: " + genres + "\n" +
               "Popularity: " + popularity + "\n" +
               "Vote Average: " + voteAverage + "\n" +
               "Vote Count: " + voteCount + "\n" +
               "Overview:\n" +
               overview + "\n";
    }

    public List<int> GetGenreIds()
    {
        return genreIds;
    }
}

public class TMDbController : MonoBehaviour
{
    //public GameObject moviePosterPlane;
    //public GameObject movieTrailerPlane;
    //public GameObject movieInformationPlane;
    public ExplanationController explanationController;

    public Loader movieDisplayer;
    public MovieSelection movieSelector;
    public List<Loader> movieSelectorLoader;

    private List<movieDBItem> MovieDataBase = new List<movieDBItem>();
    private MovieInformation movieInformation = new MovieInformation();
    private Dictionary<int, movieDBItem> MovieRecDB = new Dictionary<int, movieDBItem>();

    public MenuState menuState;
    //private List<movieDBItem> moviesSearched;
    private int currentMovieToShow;
    public int currentMovieIndexForRecommend;
    private int currentMovieId;

    private List<movieDBItem> userLikedMovies = new List<movieDBItem>();
    private List<movieDBItem> userLikedMoviesCache = new List<movieDBItem>();
    public List<movieRecommendItem> recommendMovies = new List<movieRecommendItem>();
    public string recommendMovieMode = "";

    private string _movideDbApiKey = "06be00eafe8419222c854f8db00b8197";
    private string posterImageBase = "https://image.tmdb.org/t/p/original//";
    private string youtubeVideoBase = "https://www.youtube.com/watch?v=";
    private int ROTATETIME = 20;
    private string FILEPATH = "movieSearched.csv";
    private string FILEPATH_REC = "movieRec.csv";

    TMDbClient client = null;

    // Start is called before the first frame update
    void Start()
    {
        client = new TMDbClient(_movideDbApiKey);
        currentMovieId = 0;
        menuState = MenuState.Starting;
        //FindVideoWithMovieID(client, 740996);
        //movieSelector.transform.gameObject.SetActive(false);

        ShowMovieSelectionMenu();
        movieDisplayer.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(userLikedMovies.Count);
    }

    public void ShowMovieSelectionMenu()
    {
        if (menuState == MenuState.Starting)
        {
            LoadMoviesForSelection();
            ShowNextMoviesForSelection();
            menuState = MenuState.SelectMovies;
        }
    }

    public void MovieSelectionMenuNewMovies()
    {
        if (menuState == MenuState.SelectMovies)
        {
            userLikedMovies.AddRange(userLikedMoviesCache);
            userLikedMoviesCache.Clear();

            if (currentMovieToShow >= MovieDataBase.Count - 5)
            {
                currentMovieToShow = 0;
            }
            else
            {
                currentMovieToShow += 5;
            }

            ShowNextMoviesForSelection();
            movieSelector.SetSelectionDisappear();
        }
    }

    public void UpdateSelectMovies()
    {
        userLikedMoviesCache = FetchSelectedMovies();
        foreach(movieDBItem movie in userLikedMoviesCache)
        {
            UnityEngine.Debug.Log("Add selection: " + movie.title);
        }
    }

    public void NextMovieInformation()
    {
        if (menuState == MenuState.ShowMovies)
        {
            currentMovieId = ShowNextMovieFromList(0);
            if (recommendMovieMode == "similar")
            {
                //explanationController.ShowSimilarMovieExplanation();
            }
            else
            {
                //explanationController.ShowRecommendMovieExplanation();
            }
        }
    }

    public void ShowMovieWithGenres()
    {
        userLikedMovies.AddRange(userLikedMoviesCache);
        userLikedMoviesCache.Clear();

        userLikedMovies = Shuffle(userLikedMovies);
        currentMovieToShow = 0;
        currentMovieIndexForRecommend = -1;
        movieDisplayer.SetActive(true);
        movieSelector.transform.gameObject.SetActive(false);

        if (menuState == MenuState.SelectMovies)
        {
            // Show similar movies
            currentMovieId = ShowNextMovieFromList(1);
            recommendMovieMode = "similar";
            //explanationController.ShowSimilarMovieExplanation();
            ShowPieChartInfo();

            menuState = MenuState.ShowMovies;
        }        
    }
    
    public void ShowMovieWithRecommendation()
    {
        userLikedMovies.AddRange(userLikedMoviesCache);
        userLikedMoviesCache.Clear();

        userLikedMovies = Shuffle(userLikedMovies);
        currentMovieToShow = 0;
        currentMovieIndexForRecommend = -1;
        movieDisplayer.SetActive(true);
        movieSelector.transform.gameObject.SetActive(false);

        if (menuState == MenuState.SelectMovies)
        {
            // Show recommendation movies
            currentMovieId = ShowNextMovieFromList(2);
            recommendMovieMode = "recommendation";
            //explanationController.ShowRecommendMovieExplanation();
            ShowPieChartInfo();

            menuState = MenuState.ShowMovies;
        }
    }

    private int ContainsMovieInRecList(int _newMovieID)
    {
        int index = 0;
        foreach(movieRecommendItem movie in recommendMovies)
        {
            if (_newMovieID == movie.movie.id)
            {
                return index;
            }
            ++index;
        }
        return -1;
    }

    public void PlayTrailer()
    {
        if (menuState == MenuState.ShowMovies)
        {
            RotateInformationText(movieDisplayer, ROTATETIME);
            RotateImage(movieDisplayer, ROTATETIME);
            ShowVideoWithMovieID(currentMovieId);
            menuState = MenuState.PlayMovie;
        }
    }

    public void PlayPauseTrailer()
    {
        if (menuState == MenuState.PlayMovie)
        {
            VideoLoader videoLoader = movieDisplayer.GetVideoLoader().GetComponent<VideoLoader>();
            videoLoader.ChangeVideoPlayMode();
        }
        
    }

    public void StopTrailer()
    {
        if (menuState == MenuState.PlayMovie)
        {
            VideoLoader videoLoader = movieDisplayer.GetVideoLoader().GetComponent<VideoLoader>();
            videoLoader.StopVideoPlaying();

            RotateInformationText(movieDisplayer, -ROTATETIME);
            RotateImage(movieDisplayer, -ROTATETIME);
            menuState = MenuState.ShowMovies;
        }
    }

    private List<movieDBItem> FetchSelectedMovies()
    {
        List<movieDBItem> movies = new List<movieDBItem>();
        List<bool> items = movieSelector.FetchSelectedMovieIndex();

        for (int i = 0; i < items.Count; i++)
        {
            if (items[i])
            {
                movies.Add(MovieDataBase[currentMovieToShow + i]);
            }
        }

        return movies;
    }

    private void ShowVideoWithMovieID(int _movieId)
    {
        /*
        ResultContainer<TMDbLib.Objects.General.Video> results = client.GetMovieVideosAsync(_movieId).Result;

        foreach (TMDbLib.Objects.General.Video result in results.Results)
        {
            UnityEngine.Debug.Log(result.Key);
            if (ShowVideo(result.Key))
            {
                break;
            }
        }
        */
        UnityEngine.Debug.Log(MovieRecDB[_movieId].videoKey);
        foreach (string key in MovieRecDB[_movieId].videoKey.Split(' '))
        {
            UnityEngine.Debug.Log(key);
            if (ShowVideo(key))
            {
                break;
            }
        }
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

    private List<Genre> FindMovieGenres()
    {
        List<Genre> results = client.GetMovieGenresAsync().Result;

        foreach (Genre result in results)
        {
            UnityEngine.Debug.Log(result.Name);
        }
        return results;
    }

    private string GetMovieGenreStringWithGenreIds(List<int> _ids, List<Genre> _genres)
    {
        string genreStr = "";
        
        foreach (Genre genre in _genres)
        {
            if (_ids.Contains(genre.Id)){
                genreStr += genre.Name + " | ";
            }
        }

        return genreStr;
    }

    private void ShowNextMoviesForSelection()
    {
        List<movieDBItem> results = MovieDataBase.GetRange(currentMovieToShow, 5);

        int counter = 0;
        foreach (movieDBItem result in results)
        {
            //UnityEngine.Debug.Log(result.Title + " (" + result.ReleaseDate + ")");
            //UnityEngine.Debug.Log(result.VoteAverage);
            //UnityEngine.Debug.Log("Movie ID: " + result.Id.ToString());

            ShowImage(movieSelectorLoader[counter], result.posterKey);
            ShowInformationText(movieSelectorLoader[counter++],
                                result.title,
                                result.releaseDate.Substring(0,4));
        }
    }

    private int ShowNextMovieFromList(int _mode)
    {
        if (currentMovieIndexForRecommend >= recommendMovies.Count - 1)
        {
            currentMovieIndexForRecommend = 0;
        }
        else
        {
            currentMovieIndexForRecommend += 1;
        }

        if (recommendMovies.Count == 0)
        {
            if (_mode == 1)
            {
                foreach (movieDBItem movie in userLikedMovies)
                {
                    foreach (int movieID in movie.similarMovieIds)
                    {
                        int index = ContainsMovieInRecList(movieID);
                        if (index == -1) //  not exist
                        {
                            movieRecommendItem newMovie = new movieRecommendItem();
                            newMovie.movie = MovieRecDB[movieID];
                            newMovie.parentMovie = new List<movieDBItem>();
                            newMovie.parentMovie.Add(movie);
                            newMovie.counting = 1;

                            recommendMovies.Add(newMovie);
                        }
                        else
                        {
                            int i = 0;
                            foreach (movieDBItem parentMovie in recommendMovies[index].parentMovie)
                            {
                                if (parentMovie.id == movie.id)
                                {
                                    break;
                                }
                                ++i;
                            }

                            if (i == recommendMovies[index].parentMovie.Count)
                            {
                                recommendMovies[index].parentMovie.Add(movie);
                                recommendMovies[index].counting += 1;
                            }
                        }
                    }
                }
            }
            else
            {
                foreach (movieDBItem movie in userLikedMovies)
                {
                    foreach (int movieID in movie.recommendationMovieIds)
                    {
                        int index = ContainsMovieInRecList(movieID);
                        if (index == -1) //  not exist
                        {
                            movieRecommendItem newMovie = new movieRecommendItem();
                            newMovie.movie = MovieRecDB[movieID];
                            newMovie.parentMovie = new List<movieDBItem>();
                            newMovie.parentMovie.Add(movie);
                            newMovie.counting = 1;

                            recommendMovies.Add(newMovie);
                        }
                        else
                        {
                            int i = 0;
                            foreach (movieDBItem parentMovie in recommendMovies[index].parentMovie)
                            {
                                if (parentMovie.id == movie.id)
                                {
                                    break;
                                }
                                ++i;
                            }

                            if (i == recommendMovies[index].parentMovie.Count)
                            {
                                recommendMovies[index].parentMovie.Add(movie);
                                recommendMovies[index].counting += 1;
                            }
                        }
                    }
                }
            }
            UnityEngine.Debug.Log("Recommend " + recommendMovies.Count.ToString() + " movies for you...");

            string s = "";
            foreach(movieRecommendItem item in recommendMovies)
            {
                s += item.counting.ToString();
            }
            UnityEngine.Debug.Log(s);
            recommendMovies = Sort(recommendMovies);
            s = "";
            foreach (movieRecommendItem item in recommendMovies)
            {
                s += item.counting.ToString();
            }
            UnityEngine.Debug.Log(s);
        }

        //UnityEngine.Debug.Log(result.Title + " (" + result.ReleaseDate + ")");
        //UnityEngine.Debug.Log(result.VoteAverage);
        //UnityEngine.Debug.Log("Movie ID: " + result.Id.ToString());

        movieRecommendItem result = recommendMovies[currentMovieIndexForRecommend];
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

    private void LoadMoviesForSelection()
    {
        //  If file is not empty: contain movie information => Load from file
        if (new FileInfo(getPath(FILEPATH)).Length != 0)
        {
            LoadFromFile(FILEPATH, false);
            LoadFromFile(FILEPATH_REC, true);
        }

        //  If DB is empty => Download
        if (MovieDataBase.Count == 0)
        {
            List<SearchMovie> results = new List<SearchMovie>();

            results.AddRange(FindTrendingMovies().Results);
            for(int page = 0; page < 50; page++)
            {
                results.AddRange(FindPopularMovies(page).Results);
                results.AddRange(FindTopRatedMovies(page).Results);
            }

            UnityEngine.Debug.Log("Original Movie Number Fetched: " + results.Count.ToString());

            List<SearchMovie> tempResults = new List<SearchMovie>();
            foreach (SearchMovie result in results)
            {   //&& result.VoteAverage > 7.0f
                if (result.VoteCount > 10000)
                {
                    tempResults.Add(result);
                }
            }
            results.Clear();
            results.AddRange(tempResults);

            UnityEngine.Debug.Log("Original Movie Number For Selection: " + results.Count.ToString());
            results = Distinct(results);
            UnityEngine.Debug.Log("After Remove Duplicate, Movie Number For Selection: " + results.Count.ToString());
            results = Shuffle(results);

            writeToFile(results, FILEPATH);
            LoadFromFile(FILEPATH, false);

            WriteMovieRecDB();
            LoadFromFile(FILEPATH_REC, true);
        }

        currentMovieToShow = 0;
    }

    private void WriteMovieRecDB()
    {
        List<SearchMovie> allRecMovies = new List<SearchMovie>();

        foreach(movieDBItem movie in MovieDataBase)
        {
            List<SearchMovie> similarMovieResults = FindSimilarMovieWithGenreId(movie.genreIds);
            allRecMovies.AddRange(similarMovieResults);


            List<SearchMovie> recommendMovieResults = FindMovieRecommendationWithMovieID(movie.id);
            allRecMovies.AddRange(recommendMovieResults);
        }

        UnityEngine.Debug.Log("Original Movie Number For RecSys: " + allRecMovies.Count.ToString());
        allRecMovies = Distinct(allRecMovies);
        UnityEngine.Debug.Log("After Remove Duplicate, Movie Number For RecSys: " + allRecMovies.Count.ToString());
        allRecMovies = Shuffle(allRecMovies);

        writeToFile(allRecMovies, FILEPATH_REC);
    }

    private void LoadFromFile(string _fileName, bool _isRecMovie)
    {
        FileStream fileStream = new FileStream(getPath(_fileName), FileMode.Open, FileAccess.ReadWrite);
        StreamReader read = new StreamReader(fileStream);
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

        for (var i = 0; i < lines.Length-1; i++)
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
                if (!MovieRecDB.ContainsKey(_movieDBItem.id))
                {
                    MovieRecDB.Add(_movieDBItem.id, _movieDBItem);
                }
            }
            else
            {
                MovieDataBase.Add(_movieDBItem);
            }
        }

        if (_isRecMovie)
        {
            UnityEngine.Debug.Log("Load " + MovieRecDB.Count + " Movie in RecSys DataBase...");
        }
        else
        {
            MovieDataBase = Shuffle(MovieDataBase);
            UnityEngine.Debug.Log("Add " + MovieDataBase.Count + " Movie in DataBase...");
        }
    }

    private void writeToFile(List<SearchMovie> _results, string _fileName)
    {
        StreamWriter writer = new StreamWriter(getPath(_fileName));

        //writer.WriteLine("ID,Title,ReleaseDate,Popularity,Overview,VoteAverage,VoteCount,GenreIds,Genres");
        foreach(SearchMovie result in _results)
        {
            if (_fileName == FILEPATH)
            {
                writer.WriteLine(result.Id.ToString() + "^" +
                            result.Title + "^" +
                            result.ReleaseDate.ToString() + "^" +
                            result.Popularity + "^" +
                            result.Overview + "^" +
                            result.VoteAverage + "^" +
                            result.VoteCount + "^" +
                            string.Join(" ", result.GenreIds) + "^" +
                            GetMovieGenreStringWithGenreIds(result.GenreIds, FindMovieGenres()) + "^" +
                            string.Join(" ", GetVideoKeyWithMovieID(result.Id)) + "^" +
                            result.PosterPath + "^" +
                            string.Join(" ", FindSimilarIdsWithGenreId(result.GenreIds)) + "^" +
                            string.Join(" ", FindRecommendationIdsWithMovieID(result.Id)) + "^");
            }
            else
            {
                writer.WriteLine(result.Id.ToString() + "^" +
                            result.Title + "^" +
                            result.ReleaseDate.ToString() + "^" +
                            result.Popularity + "^" +
                            result.Overview + "^" +
                            result.VoteAverage + "^" +
                            result.VoteCount + "^" +
                            string.Join(" ", result.GenreIds) + "^" +
                            GetMovieGenreStringWithGenreIds(result.GenreIds, FindMovieGenres()) + "^" +
                            string.Join(" ", GetVideoKeyWithMovieID(result.Id)) + "^" +
                            result.PosterPath + "^" +
                            "^" +
                            "^");
            }
            
        }
        writer.Flush();
        writer.Close();
    }

    private string getPath(string _fileName)
    {
#if     UNITY_EDITOR
        return Application.dataPath + "/StreamingAssets/" + _fileName;
#elif   UNITY_ANDROID
        return Application.persistentDataPath+_fileName;
#elif   UNITY_IPHONE
        return Application.persistentDataPath+"/"+_fileName;
#else
        return Application.streamingAssetsPath + "/" + _fileName;
#endif
    }

    private SearchContainer<SearchMovie> FindTrendingMovies()
    {
        SearchContainer<SearchMovie> results = client.GetTrendingMoviesAsync(TMDbLib.Objects.Trending.TimeWindow.Week).Result;

        UnityEngine.Debug.Log($"Got {results.Results.Count:N0} of {results.TotalResults:N0} results");
        currentMovieToShow = 0;
        return results;
    }

    private SearchContainer<SearchMovie> FindPopularMovies(int _page)
    {
        SearchContainer<SearchMovie> results = client.GetMoviePopularListAsync(page: _page, language: "en").Result;

        UnityEngine.Debug.Log($"Got {results.Results.Count:N0} of {results.TotalResults:N0} results");
        currentMovieToShow = 0;
        return results;
    }

    private SearchContainer<SearchMovie> FindTopRatedMovies(int _page)
    {
        SearchContainer<SearchMovie> results = client.GetMovieTopRatedListAsync(page: _page, language: "en").Result;

        UnityEngine.Debug.Log($"Got {results.Results.Count:N0} of {results.TotalResults:N0} results");
        currentMovieToShow = 0;
        return results;
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

    private List<SearchMovie> FindMovieRecommendationWithMovieID(int _movieId)
    {
        SearchContainer<SearchMovie> results = client.GetMovieRecommendationsAsync(_movieId, page: 2).Result;

        UnityEngine.Debug.Log($"Got {results.Results.Count:N0} of {results.TotalResults:N0} results");
        currentMovieToShow = 0;
        return results.Results;
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

    private void RotateImage(Loader _loader, int _rotateTime)
    {
        ImageLoader imageLoader = _loader.GetImageLoader().GetComponent<ImageLoader>();
        imageLoader.RotateImage(_rotateTime);
    }

    private void RotateInformationText(Loader _loader, int _rotateTime)
    {
        TextLoader textLoader = _loader.GetTextLoader().GetComponent<TextLoader>();
        textLoader.RotateText(_rotateTime);
    }

    private void ShowImage(Loader _loader, string _posterPath)
    {
        ImageLoader imageLoader = _loader.GetImageLoader().GetComponent<ImageLoader>();
        //UnityEngine.Debug.Log(posterImageBase + _posterPath);
        imageLoader.ChangeImage(posterImageBase + _posterPath);
    }

    private void ShowInformationText(Loader _loader, string _title, string _releaseDate)
    {
        TextLoader textLoader = _loader.GetTextLoader().GetComponent<TextLoader>();
        textLoader.ChangeText(movieInformation.GenerateMovieInformation(_title, _releaseDate));
    }

    private void ShowInformationText(Loader _loader, string _title, string _releaseDate, double _popularity, string _overview, double _voteAverage, int _voteCount, List<int> _genreIds, string _genres)
    {
        TextLoader textLoader = _loader.GetTextLoader().GetComponent<TextLoader>();
        textLoader.ChangeText(movieInformation.GenerateMovieInformation(_title, _releaseDate, _popularity, _overview, _voteAverage, _voteCount, _genreIds, _genres));
    }

    private bool ShowVideo(string _key)
    {
        UnityEngine.Debug.Log(_key);
        VideoLoader videoLoader = movieDisplayer.GetVideoLoader().GetComponent<VideoLoader>();
        return videoLoader.ChangeVideo(youtubeVideoBase + _key);
    }

    private List<SearchMovie> Distinct(List<SearchMovie> _list)
    {
        List<SearchMovie> list = _list;

        int i = 0;
        while(i < list.Count - 1)
        {
            for(var j = i+1; j < list.Count; j++)
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

    private void ShowPieChartInfo()
    {
        int totalGenreNumber = 0;
        Dictionary<string, int> genreMovieNumber = new Dictionary<string, int>();

        foreach(movieRecommendItem movie in recommendMovies)
        {
            string[] genreList = movie.movie.genres.Replace(" ", "").Split('|');

            for (int i = 0; i < genreList.Length - 1; i++)
            {
                if (genreMovieNumber.ContainsKey(genreList[i]))
                {
                    genreMovieNumber[genreList[i]] += 1;
                }
                else
                {
                    totalGenreNumber += 1;
                    genreMovieNumber.Add(genreList[i], 1);
                }
            }
        }

        //explanationController.ShowMoviePieChart(genreMovieNumber);
    }


}
