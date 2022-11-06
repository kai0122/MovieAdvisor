using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovieRecommendationSubTitleController2 : MonoBehaviour
{
    public GameObject actorScoreBar;
    public GameObject actorScoreBarBase;
    public GameObject genreScoreBar;
    public GameObject genreScoreBarBase;
    public GameObject publicityScoreBar;
    public GameObject publicityScoreBarBase;
    public GameObject ratingScoreBar;
    public GameObject ratingScoreBarBase;

    public Material greenMaterial;
    public Material redMaterial;

    private float barLength = 0.05f;

    private int MIN_NUM = 30;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeRecommendationSubTitleActor(int score, int baseScore, int totalScore)
    {
        Debug.Log("Actor Score..." + score.ToString());
        Debug.Log("Actor Base Score..." + baseScore.ToString());
        if (score >= baseScore)
        {
            actorScoreBar.GetComponent<Renderer>().material = greenMaterial;
        }
        else
        {
            actorScoreBar.GetComponent<Renderer>().material = redMaterial;
        }

        score = score > MIN_NUM ? MIN_NUM : score;
        float length = ((float)score / MIN_NUM) * barLength;
        float shiftX = (-(barLength - length) / 2) * 10;

        actorScoreBar.transform.localScale = new Vector3(length, actorScoreBar.transform.localScale.y, actorScoreBar.transform.localScale.z);
        actorScoreBar.transform.localPosition = new Vector3(shiftX, actorScoreBar.transform.localPosition.y, actorScoreBar.transform.localPosition.z);
        TMPro.TextMeshPro textMeshPro = gameObject.transform.Find("ActorScore").GetComponent<TMPro.TextMeshPro>();
        textMeshPro.text = score.ToString();

        length = ((float)baseScore / MIN_NUM) * barLength;
        shiftX = (-(barLength - length) / 2) * 10;

        actorScoreBarBase.transform.localScale = new Vector3(length, actorScoreBarBase.transform.localScale.y, actorScoreBarBase.transform.localScale.z);
        actorScoreBarBase.transform.localPosition = new Vector3(shiftX, actorScoreBarBase.transform.localPosition.y, actorScoreBarBase.transform.localPosition.z);
        textMeshPro = gameObject.transform.Find("ActorScoreBase").GetComponent<TMPro.TextMeshPro>();
        textMeshPro.text = (baseScore).ToString("0");
    }

    public void ChangeRecommendationSubTitleGenre(int score, int baseScore, int totalScore)
    {
        if (score >= baseScore)
        {
            genreScoreBar.GetComponent<Renderer>().material = greenMaterial;
        }
        else
        {
            genreScoreBar.GetComponent<Renderer>().material = redMaterial;
        }

        score = score > MIN_NUM ? MIN_NUM : score;
        float length = ((float)score / MIN_NUM) * barLength;
        float shiftX = (-(barLength - length) / 2) * 10;

        genreScoreBar.transform.localScale = new Vector3(length, genreScoreBar.transform.localScale.y, genreScoreBar.transform.localScale.z);
        genreScoreBar.transform.localPosition = new Vector3(shiftX, genreScoreBar.transform.localPosition.y, genreScoreBar.transform.localPosition.z);
        TMPro.TextMeshPro textMeshPro = gameObject.transform.Find("GenreScore").GetComponent<TMPro.TextMeshPro>();
        textMeshPro.text = score.ToString();

        length = ((float)baseScore / MIN_NUM) * barLength;
        shiftX = (-(barLength - length) / 2) * 10;

        genreScoreBarBase.transform.localScale = new Vector3(length, genreScoreBarBase.transform.localScale.y, genreScoreBarBase.transform.localScale.z);
        genreScoreBarBase.transform.localPosition = new Vector3(shiftX, genreScoreBarBase.transform.localPosition.y, genreScoreBarBase.transform.localPosition.z);
        textMeshPro = gameObject.transform.Find("GenreScoreBase").GetComponent<TMPro.TextMeshPro>();
        textMeshPro.text = (baseScore).ToString("0");
    }

    public void ChangeRecommendationSubTitlePublicity(int score, int baseScore, int totalScore)
    {
        if (score >= baseScore)
        {
            publicityScoreBar.GetComponent<Renderer>().material = greenMaterial;
        }
        else
        {
            publicityScoreBar.GetComponent<Renderer>().material = redMaterial;
        }

        score = score > MIN_NUM ? MIN_NUM : score;
        float length = ((float)score / MIN_NUM) * barLength;
        float shiftX = (-(barLength - length) / 2) * 10;

        publicityScoreBar.transform.localScale = new Vector3(length, publicityScoreBar.transform.localScale.y, publicityScoreBar.transform.localScale.z);
        publicityScoreBar.transform.localPosition = new Vector3(shiftX, publicityScoreBar.transform.localPosition.y, publicityScoreBar.transform.localPosition.z);
        TMPro.TextMeshPro textMeshPro = gameObject.transform.Find("PublicityScore").GetComponent<TMPro.TextMeshPro>();
        textMeshPro.text = score.ToString();

        length = ((float)baseScore / MIN_NUM) * barLength;
        shiftX = (-(barLength - length) / 2) * 10;

        publicityScoreBarBase.transform.localScale = new Vector3(length, publicityScoreBarBase.transform.localScale.y, publicityScoreBarBase.transform.localScale.z);
        publicityScoreBarBase.transform.localPosition = new Vector3(shiftX, publicityScoreBarBase.transform.localPosition.y, publicityScoreBarBase.transform.localPosition.z);
        textMeshPro = gameObject.transform.Find("PublicityScoreBase").GetComponent<TMPro.TextMeshPro>();
        textMeshPro.text = (baseScore).ToString("0");
    }

    public void ChangeRecommendationSubTitleRating(double score, int baseScore, int totalScore)
    {
        if (score >= baseScore)
        {
            ratingScoreBar.GetComponent<Renderer>().material = greenMaterial;
        }
        else
        {
            ratingScoreBar.GetComponent<Renderer>().material = redMaterial;
        }

        Debug.Log(score.ToString() + " / " + baseScore.ToString());

        float length = ((float)score / 100) * barLength;
        float shiftX = (-(barLength - length) / 2) * 10;

        ratingScoreBar.transform.localScale = new Vector3(length, ratingScoreBar.transform.localScale.y, ratingScoreBar.transform.localScale.z);
        ratingScoreBar.transform.localPosition = new Vector3(shiftX, ratingScoreBar.transform.localPosition.y, ratingScoreBar.transform.localPosition.z);
        TMPro.TextMeshPro textMeshPro = gameObject.transform.Find("RatingScore").GetComponent<TMPro.TextMeshPro>();
        textMeshPro.text = ((double)score / 10).ToString("0.0");

        length = ((float)baseScore / 100) * barLength;
        shiftX = (-(barLength - length) / 2) * 10;

        ratingScoreBarBase.transform.localScale = new Vector3(length, ratingScoreBarBase.transform.localScale.y, ratingScoreBarBase.transform.localScale.z);
        ratingScoreBarBase.transform.localPosition = new Vector3(shiftX, ratingScoreBarBase.transform.localPosition.y, ratingScoreBarBase.transform.localPosition.z);
        textMeshPro = gameObject.transform.Find("RatingScoreBase").GetComponent<TMPro.TextMeshPro>();
        textMeshPro.text = (baseScore / 10.0f).ToString("0.0");
    }

}
