using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserLikedMoviesController : MonoBehaviour
{
    public Dictionary<int, string> userLikedMoviesName = new Dictionary<int, string>()
        {
            {512200, "Jumanji: The Next Level"},
            {381288, "Split"},
            {339846, "Baywatch"},
            {475557, "Joker"},
            {479455, "Men in Black: International"},
            {545609, "Extraction"},
            {299534, "Avengers: Endgame"},
            {450465, "Glass"},
            {284052, "Doctor Strange"},
            {337401, "Mulan"},
            {560050, "Over the Moon"},
            {474350, "It Chapter Two"},
            {416194, "Skjelvet"},
            {1571, "Live Free or Die Hard"},
            {19959, "Surrogates"},
            {14869, "G.I. Joe: The Rise of Cobra"},
            {8587, "The Lion King"},
            {777350, "Dory's Reef Cam"},
            {290859, "Terminator: Dark Fate"},
            {537915, "After"},
            {350, "The Devil Wears Prada"},
            {82695, "Les Misérables"},
            {567970, "Lost Girls"},
            {433498, "Papillon"},
            {255709, "So-won"},
            {570508, "Jeungin"},
            {370567, "Sherlock Gnomes"},
            {321697, "Steve Jobs"},
            {287947, "Shazam!"},
            {269149, "Zootopia"},
        };

    // Start is called before the first frame update
    public JustificationController_Final_version justificationController_Final_Version_1;
    void Start()
    {
        totalTargetMovieNum = justificationController_Final_Version_1.movieIDPairs.Count;
        for(int i = 0;i < totalTargetMovieNum; i++)
        {
            ifUpdateList.Add(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetUserLikedMovieAmount()
    {
        return userLikedMoviesName.Count;
    }

    public RecommendationCalculator recommendationCalculator;
    public FilterNumberHolder filterNumberHolder;

    public void AddMovieToList(int movieId, string movieName)
    {
        if (!userLikedMoviesName.ContainsKey(movieId))
        {
            userLikedMoviesName.Add(movieId, movieName);

            for(int i = 0; i < totalTargetMovieNum; i++)
            {
                ifUpdateList[i] = true;
            }

            filterNumberHolder.UpdateRatio_with_new_userLikedList();
        }
    }

    public void RemoveMovieFromList(int movieId)
    {
        if (userLikedMoviesName.ContainsKey(movieId))
        {
            // avoid duplicate remove
            userLikedMoviesName.Remove(movieId);

            for (int i = 0; i < totalTargetMovieNum; i++)
            {
                ifUpdateList[i] = true;
            }

            filterNumberHolder.UpdateRatio_with_new_userLikedList();
        }
    }

    private int totalTargetMovieNum;
    private List<bool> ifUpdateList = new List<bool>();

    public bool GetIfUpdateList(string name)
    {
        for (int i = 0; i < totalTargetMovieNum; i++)
        {
            if (name.Contains(i.ToString()))
            {
                if (ifUpdateList[i])
                {
                    Debug.Log("NOW, " + name + "has updated list ...");
                    ifUpdateList[i] = false;
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        return false;
    }
}
