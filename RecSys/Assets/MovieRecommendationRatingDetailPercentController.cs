using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovieRecommendationRatingDetailPercentController : MonoBehaviour
{
    public List<GameObject> RatingBars;

    private float barLength = 0.07f;
    private float barLengthHisto = 0.05f;


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
        if (gameObject.name.Contains("Histogram"))
        {
            float length = (float)(percent * barLengthHisto);
            float shiftZ = (0.01f - ((barLengthHisto - length) / 2) * 9.6f);
            int index = RatingBars.Count - score;

            RatingBars[index].transform.localScale = new Vector3(RatingBars[index].transform.localScale.x, RatingBars[index].transform.localScale.y, length);
            RatingBars[index].transform.localPosition = new Vector3(RatingBars[index].transform.localPosition.x, RatingBars[index].transform.localPosition.y, shiftZ);
            gameObject.transform.Find("Percent" + score.ToString()).GetComponent<TMPro.TextMeshPro>().text = string.Format("{0}%", percent * 100);
            gameObject.transform.Find("Vote" + score.ToString()).GetComponent<TMPro.TextMeshPro>().text = votes.ToString();
            gameObject.transform.Find("Vote" + score.ToString()).transform.localPosition = new Vector3(gameObject.transform.Find("Vote" + score.ToString()).transform.localPosition.x,
                                                                                                       gameObject.transform.Find("Vote" + score.ToString()).transform.localPosition.y,
                                                                                                       RatingBars[index].transform.localPosition.z + (RatingBars[index].transform.localScale.z * 5.0f) + 0.03f);
            //gameObject.transform.Find("MovieName").GetComponent<TMPro.TextMeshPro>().text = movieName;
        }
        else
        {
            float length = (float)(percent * barLength);
            float shiftX = (0.1f - ((0.07f - length) / 2) * 10.0f);
            int index = RatingBars.Count - score;

            RatingBars[index].transform.localScale = new Vector3(length, RatingBars[index].transform.localScale.y, RatingBars[index].transform.localScale.z);
            RatingBars[index].transform.localPosition = new Vector3(shiftX, RatingBars[index].transform.localPosition.y, RatingBars[index].transform.localPosition.z);
            gameObject.transform.Find("Percent" + score.ToString()).GetComponent<TMPro.TextMeshPro>().text = string.Format("{0:N2}%", percent * 100);
            gameObject.transform.Find("Vote" + score.ToString()).GetComponent<TMPro.TextMeshPro>().text = votes.ToString();
            gameObject.transform.Find("Percent" + score.ToString()).transform.localPosition = new Vector3(RatingBars[index].transform.localPosition.x + (RatingBars[index].transform.localScale.x * 5.0f) + 0.27f,
                                                                                                          gameObject.transform.Find("Vote" + score.ToString()).transform.localPosition.y,
                                                                                                          gameObject.transform.Find("Vote" + score.ToString()).transform.localPosition.z);
            //gameObject.transform.Find("MovieName").GetComponent<TMPro.TextMeshPro>().text = movieName;
        }
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
