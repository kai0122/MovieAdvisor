using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddMovieToUserLikedList : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public MovieListButtonController movieListButtonController;

    public void OnButtonClick()
    {
        movieListButtonController.AddMovie(Int32.Parse(gameObject.transform.parent.name), gameObject.transform.parent.Find("MovieName").GetComponent<TMPro.TextMeshProUGUI>().text);

        gameObject.transform.parent.GetComponent<ButtonController>().AssignLikedMovie();
    }
}
