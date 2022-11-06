using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMDbLib.Client;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Search;
using UnityEngine;


public class MovieDatabase : MonoBehaviour
{
    public List<movieDBItem> MovieDataBase = new List<movieDBItem>();
    public Dictionary<int, movieDBItem> MovieRecDB = new Dictionary<int, movieDBItem>();

    public List<movieRecommendItem> recommendMovies = new List<movieRecommendItem>();
    public List<movieDBItem> userLikedMovies = new List<movieDBItem>();

    private string FILEPATH = "movieSearched.csv";
    private string FILEPATH_REC = "movieRec.csv";
    public List<int> movieLikedIds = new List<int> { 2062, 299534, 857, 424694, 207703, 259316, 333339 };

    private string posterImageBase = "https://image.tmdb.org/t/p/original//";
    private string youtubeVideoBase = "https://www.youtube.com/watch?v=";

    private int currentMovieIndexForRecommend;

    private string _movideDbApiKey = "06be00eafe8419222c854f8db00b8197";
    TMDbClient client = null;


    public List<string> userLikedGenres = new List<string>();
    public Dictionary<string, int> userLikedGenresCount = new Dictionary<string, int>();
    public Dictionary<string, float> userLikedGenresRatio = new Dictionary<string, float>();

    public int mode;

    // Start is called before the first frame update
    void Start()
    {
        mode = 0;
        client = new TMDbClient(_movideDbApiKey);

        //LoadFromFile(FILEPATH, false);
        //LoadFromFile(FILEPATH_REC, true);

        ReadFromFile_Android(FILEPATH, false);
        ReadFromFile_Android(FILEPATH_REC, true);

        GetUserLikedMovies();
        LoadRecommendMovies(1);
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
        
    }

    public void ChangeExplanationMode(int _mode)
    {
        mode = _mode;
        Debug.Log("Now Mode: " + _mode);
    }

    private void GetUserLikedMovies()
    {
        foreach (movieDBItem movie in MovieDataBase)
        {
            if (movieLikedIds.Contains(movie.id))
            {
                userLikedMovies.Add(movie);
            }
        }
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
            // get recommend genres
            int totalCount = 0;
            foreach (movieDBItem movie in userLikedMovies)
            {
                foreach (string genre in movie.genres.Replace(" ", "").Split('|'))
                {
                    if (genre != "")
                    {
                        if (!userLikedGenres.Contains(genre))
                        {
                            userLikedGenres.Add(genre);
                            userLikedGenresCount.Add(genre, 1);
                        }
                        else
                        {
                            userLikedGenresCount[genre] += 1;
                        }
                    }
                }
            }
            foreach (string genre in userLikedGenres)
            {
                totalCount += userLikedGenresCount[genre];
            }
            foreach (string genre in userLikedGenres)
            {
                userLikedGenresRatio.Add(genre, (float)userLikedGenresCount[genre] / (float)totalCount);
                Debug.Log(genre + ": " + (float)userLikedGenresCount[genre] / (float)totalCount);
            }
            userLikedGenres = SortUserGenres(userLikedGenres, userLikedGenresRatio);
            foreach (string genre in userLikedGenres)
            {
                Debug.Log(genre);
            }


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

    private void WriteMovieRecDB()
    {
        List<movieDBItem> allRecMovies = new List<movieDBItem>();
        foreach (movieRecommendItem movie in recommendMovies)
        {
            allRecMovies.Add(movie.movie);
        }
        allRecMovies.AddRange(userLikedMovies);
        writeToFile(allRecMovies, "New_Rec_List.csv");
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
