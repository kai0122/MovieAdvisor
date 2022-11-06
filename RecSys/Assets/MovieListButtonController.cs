using System.Collections;
using System.Collections.Generic;
using TMDbLib.Objects.General;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class MovieListButtonController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        MovieListActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject MovieList;
    public GameObject MovieListScrollbar;
    public bool MovieListActive;

    public RecommendationCalculator recommendationCalculator;
    public UserLikedMoviesController userLikedMoviesController;
    public TMDBApiController tMDBApiController;
    public GameObject MovieItem;
    public GameObject ParentObject;

    public List<Texture> userLikedMoviePoster;
    public Dictionary<int, int> userLikedMoviePosterIndex = new Dictionary<int, int>()
        {
            {512200, 0},
            {381288, 1},
            {339846, 2},
            {475557, 3},
            {479455, 4},
            {545609, 5},
            {299534, 6},
            {450465, 7},
            {284052, 8},
            {337401, 9},
            {560050, 10},
            {474350, 11},
            {416194, 12},
            {1571, 13},
            {19959, 14},
            {14869, 15},
            {8587, 16},
            {777350, 17},
            {290859, 18},
            {537915, 19},
            {350, 20},
            {82695, 21},
            {567970, 22},
            {433498, 23},
            {255709, 24},
            {570508, 25},
            {370567, 26},
            {321697, 27},
            {287947, 28},
            {269149, 29},
        };


    private bool firstTimeShow = true;
    public Dictionary<int, GameObject> movieIdGameobjectPairs = new Dictionary<int, GameObject>();
    public MovieSearchButtonController movieSearchButtonController;

    public void OnButtonPress()
    {
        if (MovieListActive)
        {
            MovieListActive = false;
        }
        else
        {
            MovieListActive = true;
            movieSearchButtonController.SearchListActive = true;
            movieSearchButtonController.OnButtonPress();
        }
        MovieList.SetActive(MovieListActive);
        MovieListScrollbar.SetActive(MovieListActive);

        if (MovieListActive)
        {
            //firstTimeShow = false;
            Dictionary<int, string> userLikedMoviePairs = recommendationCalculator.GetUserLikedMoviePairs();

            foreach(int movieID in userLikedMoviePairs.Keys)
            {
                if (!movieIdGameobjectPairs.ContainsKey(movieID))
                {
                    if (userLikedMoviePosterIndex.ContainsKey(movieID))
                    {
                        // default movies
                        GameObject newMovieItem = GameObject.Instantiate(MovieItem);
                        newMovieItem.transform.SetParent(ParentObject.transform);
                        newMovieItem.name = movieID.ToString();
                        newMovieItem.transform.Find("MovieName").GetComponent<TMPro.TextMeshProUGUI>().text = userLikedMoviePairs[movieID];

                        movieIdGameobjectPairs.Add(movieID, newMovieItem);

                        List<ImageData> posterList = tMDBApiController.GetMoviePosters(movieID).Posters;
                        if (posterList.Count == 0)
                        {
                            RawImage rend = newMovieItem.transform.Find("MoviePoster").GetComponent<RawImage>();
                            rend.material = new Material(Shader.Find("Unlit/Transparent"));
                            rend.material.mainTexture = NoImg;
                        }
                        else
                        {
                            RawImage rend = newMovieItem.transform.Find("MoviePoster").GetComponent<RawImage>();
                            rend.material = new Material(Shader.Find("Unlit/Transparent"));
                            rend.material.mainTexture = userLikedMoviePoster[userLikedMoviePosterIndex[movieID]];
                        }

                        newMovieItem.transform.Find("RemoveIcon").GetComponent<SelectRemoveIcon>().movieListButtonController = gameObject.GetComponent<MovieListButtonController>();
                    }
                    else
                    {
                        AddMovie(movieID, userLikedMoviePairs[movieID]);
                    }
                }
            }
        }
    }

    public Texture NoImg;
    public Texture RemoveIcon;
    private string posterImageBase = "https://image.tmdb.org/t/p/original//";

    public void AddMovie(int movieId, string movieName)
    {
        userLikedMoviesController.AddMovieToList(movieId, movieName);

        GameObject newMovieItem = GameObject.Instantiate(MovieItem);
        newMovieItem.transform.SetParent(ParentObject.transform);
        newMovieItem.name = movieId.ToString();
        newMovieItem.transform.Find("MovieName").GetComponent<TMPro.TextMeshProUGUI>().text = movieName;

        movieIdGameobjectPairs.Add(movieId, newMovieItem);

        List<ImageData> posterList = tMDBApiController.GetMoviePosters(movieId).Posters;
        Debug.Log(posterList.Count);
        if (posterList.Count == 0)
        {
            RawImage rend = newMovieItem.transform.Find("MoviePoster").GetComponent<RawImage>();
            rend.material = new Material(Shader.Find("Unlit/Transparent"));
            rend.material.mainTexture = NoImg;
        }
        else
        {
            Debug.Log(posterImageBase + posterList[0].FilePath);
            StartCoroutine(DownloadImage(posterImageBase + posterList[0].FilePath, movieId));
        }

        newMovieItem.transform.Find("RemoveIcon").GetComponent<SelectRemoveIcon>().movieListButtonController = gameObject.GetComponent<MovieListButtonController>();
    }

    public void RemoveMovie(int movieId)
    {
        if (movieIdGameobjectPairs.ContainsKey(movieId))
        {
            Destroy(movieIdGameobjectPairs[movieId]);
            movieIdGameobjectPairs.Remove(movieId);
        }

        userLikedMoviesController.RemoveMovieFromList(movieId);
        previousRemoveMovieId = movieId;
    }

    public int previousRemoveMovieId = 0;

    IEnumerator DownloadImage(string MediaUrl, int movieId)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(MediaUrl);
        yield return request.SendWebRequest();
        if (request.isNetworkError || request.isHttpError)
            Debug.Log(request.error);
        else
        {
            RawImage rend = movieIdGameobjectPairs[movieId].transform.Find("MoviePoster").GetComponent<RawImage>();
            rend.material = new Material(Shader.Find("Unlit/Transparent"));
            rend.material.mainTexture = ((DownloadHandlerTexture)request.downloadHandler).texture;
        }
    }
}
