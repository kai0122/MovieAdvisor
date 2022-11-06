using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovieSearchController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public InputField inputField;
    public GameObject defaultDisplay;
    public GameObject textDisplay;
    public void OnSearchMovieNameChange()
    {
        string newName = textDisplay.GetComponent<Text>().text;
        Debug.Log(newName);
    }

    public string GetCurrentSearchedMovieName()
    {
        return textDisplay.GetComponent<Text>().text;
    }

    public void DefaultSearchString()
    {
        inputField.text = "";
        defaultDisplay.GetComponent<Text>().text = "Search for a movie";
        textDisplay.GetComponent<Text>().text = "";
    }
}
