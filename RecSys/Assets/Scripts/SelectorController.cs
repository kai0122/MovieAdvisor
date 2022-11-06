using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMDbLib.Client;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Search;
using UnityEngine;


public class SelectorController : MonoBehaviour
{
    private List<movieDBItem> MovieDataBase = new List<movieDBItem>();
    private Dictionary<int, movieDBItem> MovieRecDB = new Dictionary<int, movieDBItem>();

    public Loader movieDisplayer;
    private MovieInformation movieInformation = new MovieInformation();
    public List<movieRecommendItem> recommendMovies = new List<movieRecommendItem>();
    private List<movieDBItem> userLikedMovies = new List<movieDBItem>();

    private string FILEPATH = "movieSearched.csv";
    private string FILEPATH_REC = "movieRec.csv";

    private string posterImageBase = "https://image.tmdb.org/t/p/original//";
    private string youtubeVideoBase = "https://www.youtube.com/watch?v=";

    private int currentMovieIndexForRecommend;

    private string _movideDbApiKey = "06be00eafe8419222c854f8db00b8197";
    TMDbClient client = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
    }

    private void GetUserLikedMovies()
    {
        for (int i = 0;i < 10; i++)
        {
            userLikedMovies.Add(MovieDataBase[i * 10]);
        }
    }

    private void ShowNextMovie()
    {
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

        if (currentMovieIndexForRecommend < recommendMovies.Count - 1)
        {
            currentMovieIndexForRecommend += 1;
        }
        else
        {
            currentMovieIndexForRecommend = 0;
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
            foreach (movieRecommendItem item in recommendMovies)
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

    private void LoadRecommendMovies(int _mode)
    {
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
            foreach (movieRecommendItem item in recommendMovies)
            {
                s += item.counting.ToString();
            }

            recommendMovies = Sort(recommendMovies);
            s = "";
            foreach (movieRecommendItem item in recommendMovies)
            {
                s += item.counting.ToString();
            }

            currentMovieIndexForRecommend = 0;
        }
    }

    private int ContainsMovieInRecList(int _newMovieID)
    {
        int index = 0;
        foreach (movieRecommendItem movie in recommendMovies)
        {
            if (_newMovieID == movie.movie.id)
            {
                return index;
            }
            ++index;
        }
        return -1;
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

    private void ShowImage(Loader _loader, string _posterPath)
    {
        ImageLoader imageLoader = _loader.GetImageLoader().GetComponent<ImageLoader>();
        //UnityEngine.Debug.Log(posterImageBase + _posterPath);
        imageLoader.ChangeImage(posterImageBase + _posterPath);
    }

    private bool ShowVideo(string _key)
    {
        UnityEngine.Debug.Log(_key);
        VideoLoader videoLoader = movieDisplayer.GetVideoLoader().GetComponent<VideoLoader>();
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

}
