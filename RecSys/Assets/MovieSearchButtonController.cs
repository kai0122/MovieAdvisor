using System.Collections;
using System.Collections.Generic;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Search;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class MovieSearchButtonController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SearchListActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool SearchListActive;
    public GameObject SearchList;
    public GameObject SearchListScrollbar;

    public UserLikedMoviesController userLikedMoviesController;
    public RecommendationCalculator recommendationCalculator;
    public TMDBApiController tMDBApiController;
    public MovieSearchController movieSearchController;
    public MovieListButtonController movieListButtonController;

    private string previousSearchedMovieName = "Default";

    public void OnButtonPress()
    {
        if (SearchListActive)
        {
            SearchListActive = false;
        }
        else
        {
            SearchListActive = true;
            movieSearchController.DefaultSearchString();
            ClearSearchedGameObjects();


            movieListButtonController.MovieListActive = true;
            movieListButtonController.OnButtonPress();
        }
        SearchList.SetActive(SearchListActive);
        SearchListScrollbar.SetActive(SearchListActive);
    }

    public GameObject MovieItem;
    public GameObject ParentObject;
    public Texture NoImg;

    private Dictionary<int, Texture> movieIdPosterCache = new Dictionary<int, Texture>();
    private Dictionary<int, GameObject> searchedMovieGameObjects = new Dictionary<int, GameObject>();


    public void OnSearchedMovieNameChange()
    {
        if (SearchListActive)
        {
            if (previousSearchedMovieName != movieSearchController.GetCurrentSearchedMovieName())
            {
                foreach(GameObject gb in searchedMovieGameObjects.Values)
                {
                    Destroy(gb);
                }

                previousSearchedMovieName = movieSearchController.GetCurrentSearchedMovieName();
                if (previousSearchedMovieName != "")
                {
                    List<SearchMovie> searchedMovies = tMDBApiController.SearchForMoviesToAdd(previousSearchedMovieName);

                    searchedMovieGameObjects.Clear();
                    foreach (SearchMovie movie in searchedMovies)
                    {
                        GameObject newMovieItem = GameObject.Instantiate(MovieItem);
                        newMovieItem.transform.SetParent(ParentObject.transform);
                        newMovieItem.name = movie.Id.ToString();
                        newMovieItem.transform.Find("MovieName").GetComponent<TMPro.TextMeshProUGUI>().text = movie.Title;

                        searchedMovieGameObjects.Add(movie.Id, newMovieItem);

                        if (movieIdPosterCache.ContainsKey(movie.Id))
                        {
                            RawImage rend = newMovieItem.transform.Find("MoviePoster").GetComponent<RawImage>();
                            rend.material = new Material(Shader.Find("Unlit/Transparent"));
                            rend.material.mainTexture = movieIdPosterCache[movie.Id];
                        }
                        else
                        {
                            List<ImageData> posterList = tMDBApiController.GetMoviePosters(movie.Id).Posters;
                            if (posterList.Count == 0)
                            {
                                RawImage rend = newMovieItem.transform.Find("MoviePoster").GetComponent<RawImage>();
                                rend.material = new Material(Shader.Find("Unlit/Transparent"));
                                rend.material.mainTexture = NoImg;

                                if (movieIdPosterCache.Count < 20)
                                {
                                    movieIdPosterCache.Add(movie.Id, NoImg);
                                }
                                else
                                {
                                    int counter = 0;
                                    foreach (int id in movieIdPosterCache.Keys)
                                    {
                                        if (counter == 19)
                                        {
                                            movieIdPosterCache.Remove(id);
                                            break;
                                        }
                                        counter += 1;
                                    }
                                    movieIdPosterCache.Add(movie.Id, NoImg);
                                }
                            }
                            else
                            {
                                StartCoroutine(DownloadImage(posterImageBase + posterList[0].FilePath, movie.Id));
                            }
                        }

                        newMovieItem.transform.Find("AddIcon").GetComponent<AddMovieToUserLikedList>().movieListButtonController = movieListButtonController;
                        newMovieItem.transform.Find("RemoveIcon").GetComponent<RemoveMovieFromUserLikedList>().movieListButtonController = movieListButtonController;
                    }

                    UpdateButtons();
                }
            }
        }
    }

    private void UpdateButtons()
    {
        foreach(int searchedId in searchedMovieGameObjects.Keys)
        {
            if (recommendationCalculator.GetUserLikedMoviePairs().ContainsKey(searchedId))
            {
                // user liked this movie already
                searchedMovieGameObjects[searchedId].GetComponent<ButtonController>().AssignLikedMovie();
            }
            else
            {
                searchedMovieGameObjects[searchedId].GetComponent<ButtonController>().AssignUnlikedMovie();
            }
        }
    }

    private void ClearSearchedGameObjects()
    {
        foreach (int searchedId in searchedMovieGameObjects.Keys)
        {
            Destroy(searchedMovieGameObjects[searchedId]);
        }
    }

    private string posterImageBase = "https://image.tmdb.org/t/p/original//";
    IEnumerator DownloadImage(string MediaUrl, int movieId)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(MediaUrl);
        yield return request.SendWebRequest();
        if (request.isNetworkError || request.isHttpError)
            Debug.Log(request.error);
        else
        {
            RawImage rend = searchedMovieGameObjects[movieId].transform.Find("MoviePoster").GetComponent<RawImage>();
            rend.material = new Material(Shader.Find("Unlit/Transparent"));
            rend.material.mainTexture = ((DownloadHandlerTexture)request.downloadHandler).texture;

            if (movieIdPosterCache.Count < 20)
            {
                movieIdPosterCache.Add(movieId, rend.material.mainTexture);
            }
            else
            {
                int counter = 0;
                foreach (int id in movieIdPosterCache.Keys)
                {
                    if (counter == 19)
                    {
                        movieIdPosterCache.Remove(id);
                        break;
                    }
                    counter += 1;
                }
                movieIdPosterCache.Add(movieId, rend.material.mainTexture);
            }
        }
    }
}
