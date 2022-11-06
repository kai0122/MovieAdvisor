using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionReceiver : MonoBehaviour
{
    public TMDbController tMDbController;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("n"))
        {
            tMDbController.MovieSelectionMenuNewMovies();
            tMDbController.NextMovieInformation();
        }

        if (Input.GetKeyDown("g"))
        {
            // content-based recommendation
            tMDbController.ShowMovieWithGenres();
        }

        if (Input.GetKeyDown("r"))
        {
            // collaborative-based recommendation
            tMDbController.ShowMovieWithRecommendation();
        }

        if (Input.GetKeyDown("t"))
        {
            tMDbController.PlayTrailer();
        }

        if (Input.GetKeyDown("p"))
        {
            tMDbController.PlayPauseTrailer();
        }

        if (Input.GetKeyDown("o"))
        {
            tMDbController.StopTrailer();
        }
    }
}
