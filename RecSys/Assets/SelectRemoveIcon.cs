using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectRemoveIcon : MonoBehaviour
{
    public MovieListButtonController movieListButtonController;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ClickRemoveMovie()
    {
        movieListButtonController.RemoveMovie(Int32.Parse(gameObject.transform.parent.name));
        Debug.Log(Int32.Parse(gameObject.transform.parent.name));
    }
}
