using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserPreferenceStore : MonoBehaviour
{
    private int actorPreference = 6;
    private int genrePreference = 6;
    private int publicityPreference = 6;
    private int ratingPreference = 20;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetActorPreference(int _score)
    {
        actorPreference = _score;
    }
    public void SetGenrePreference(int _score)
    {
        genrePreference = _score;
    }
    public void SetPublicityPreference(int _score)
    {
        publicityPreference = _score;
    }
    public void SetRatingPreference(int _score)
    {
        ratingPreference = _score;
    }
    public int GetActorPreference()
    {
        return actorPreference;
    }
    public int GetGenrePreference()
    {
        return genrePreference;
    }
    public int GetPublicityPreference()
    {
        return publicityPreference;
    }
    public int GetRatingPreference()
    {
        return ratingPreference;
    }
}
