using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public AddMovieToUserLikedList addMovieToUserLikedList;
    public RemoveMovieFromUserLikedList removeMovieFromUserLikedList;

    public void AssignLikedMovie()
    {
        addMovieToUserLikedList.gameObject.SetActive(false);
        removeMovieFromUserLikedList.gameObject.SetActive(true);
    }

    public void AssignUnlikedMovie()
    {
        addMovieToUserLikedList.gameObject.SetActive(true);
        removeMovieFromUserLikedList.gameObject.SetActive(false);
    }
}
