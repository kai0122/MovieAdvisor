using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovieRecommendationRatingDetailPercent2 : MonoBehaviour
{
    public List<GameObject> RatingBars;

    private float barLength = 0.07f;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeRecommendationRatingDetail(int score, int votes, double percent, string movieName)
    {
        float length = (float)(percent * barLength);
        float shiftX = (0.1f - ((0.07f - length) / 2) * 10.0f);
        int index = RatingBars.Count - score;

        RatingBars[index].transform.localScale = new Vector3(length, RatingBars[index].transform.localScale.y, RatingBars[index].transform.localScale.z);
        RatingBars[index].transform.localPosition = new Vector3(shiftX, RatingBars[index].transform.localPosition.y, RatingBars[index].transform.localPosition.z);
        gameObject.transform.Find("Percent" + score.ToString()).GetComponent<TMPro.TextMeshPro>().text = string.Format("{0:N2}%", percent * 100);
        gameObject.transform.Find("Vote" + score.ToString()).GetComponent<TMPro.TextMeshPro>().text = votes.ToString();
        //gameObject.transform.Find("MovieName").GetComponent<TMPro.TextMeshPro>().text = movieName;
    }

    public void ChangeTitle(string mode, int voteCount, float vote)
    {
        if (mode == "TMDb")
        {
            gameObject.transform.Find("MovieName").GetComponent<TMPro.TextMeshPro>().text = "TMDb Rating Distribution from " + voteCount.ToString() + " users";
        }
        if (mode == "IMDb")
        {
            gameObject.transform.Find("MovieName").GetComponent<TMPro.TextMeshPro>().text = "IMDb Rating Distribution from " + voteCount.ToString() + " users";
        }
        if (mode == "Rotten")
        {
            gameObject.transform.Find("MovieName").GetComponent<TMPro.TextMeshPro>().text = "Rotten Tomatoe Rating Distribution from " + voteCount.ToString() + " users";
        }
    }

}
