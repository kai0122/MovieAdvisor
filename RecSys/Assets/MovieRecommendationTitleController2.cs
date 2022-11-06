using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovieRecommendationTitleController2 : MonoBehaviour
{
    public GameObject scoreBackground;
    public Texture pass;
    public Texture fail;
    public Material red;
    public Material green;

    public Texture thumbsUp;
    public Texture thumbsDown;

    public Texture starOn;
    public Texture starOff;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeRecommendationTitle(double score, double passingScore)
    {
        
        TMPro.TextMeshPro textMeshPro = gameObject.transform.Find("Text").GetComponent<TMPro.TextMeshPro>();

        if (score >= passingScore)
        {
            textMeshPro.text = "Recommend this movie!";
            scoreBackground.GetComponent<Renderer>().material.mainTexture = pass;
        }
        else
        {
            textMeshPro.text = "Do not recommend this movie!";
            scoreBackground.GetComponent<Renderer>().material.mainTexture = fail;
        }

        gameObject.transform.Find("Score").GetComponent<TMPro.TextMeshPro>().text = ((int)(score * 100)).ToString();
        
    }

    public void UpdateRecommendationStar(bool showStar)
    {
        if (showStar)
        {
            gameObject.transform.Find("RecommendationStarIcon1").transform.gameObject.SetActive(true);
            gameObject.transform.Find("RecommendationStarIcon2").transform.gameObject.SetActive(true);
            gameObject.transform.Find("RecommendationStarIcon3").transform.gameObject.SetActive(true);
            gameObject.transform.Find("RecommendationStarIcon4").transform.gameObject.SetActive(true);
        }
        else
        {
            gameObject.transform.Find("RecommendationStarIcon1").transform.gameObject.SetActive(false);
            gameObject.transform.Find("RecommendationStarIcon2").transform.gameObject.SetActive(false);
            gameObject.transform.Find("RecommendationStarIcon3").transform.gameObject.SetActive(false);
            gameObject.transform.Find("RecommendationStarIcon4").transform.gameObject.SetActive(false);
        }
        
    }

    public void ChangeRecommendationTitle(double actorScore, int actorBase, double genreScore, int genreBase, double publicityScore, int publicityBase, double ratingScore, int ratingBase)
    {
        /*
        TMPro.TextMeshPro textMeshPro = gameObject.transform.Find("Text").GetComponent<TMPro.TextMeshPro>();

        if (actorScore >= actorBase || genreScore >= genreBase || publicityScore >= publicityBase || ratingScore >= ratingBase)
        {
            textMeshPro.text = "Recommend this movie!";
            gameObject.transform.Find("BackGround").GetComponent<Renderer>().material = green;
        }
        else
        {
            textMeshPro.text = "Do not recommend this movie!";
            gameObject.transform.Find("BackGround").GetComponent<Renderer>().material = red;
        }
        */

        /*
        if (actorScore >= actorBase || genreScore >= genreBase || publicityScore >= publicityBase || ratingScore >= ratingBase)
        {
            gameObject.transform.Find("RecommendationIcon").GetComponent<Renderer>().material = new Material(Shader.Find("Unlit/Transparent"));
            gameObject.transform.Find("RecommendationIcon").GetComponent<Renderer>().material.mainTexture = thumbsUp;
        }
        else
        {
            gameObject.transform.Find("RecommendationIcon").GetComponent<Renderer>().material = new Material(Shader.Find("Unlit/Transparent"));
            gameObject.transform.Find("RecommendationIcon").GetComponent<Renderer>().material.mainTexture = thumbsDown;
        }
        */

        if (actorScore >= actorBase && genreScore >= genreBase && publicityScore >= publicityBase && ratingScore >= ratingBase)
        {
            gameObject.transform.Find("Text").GetComponent<TMPro.TextMeshPro>().text = "Recommendation Score: 4 / 4";
            gameObject.transform.Find("RecommendationStarIcon1").GetComponent<Renderer>().material = new Material(Shader.Find("Unlit/Transparent"));
            gameObject.transform.Find("RecommendationStarIcon1").GetComponent<Renderer>().material.mainTexture = starOn;

            gameObject.transform.Find("RecommendationStarIcon2").GetComponent<Renderer>().material = new Material(Shader.Find("Unlit/Transparent"));
            gameObject.transform.Find("RecommendationStarIcon2").GetComponent<Renderer>().material.mainTexture = starOn;

            gameObject.transform.Find("RecommendationStarIcon3").GetComponent<Renderer>().material = new Material(Shader.Find("Unlit/Transparent"));
            gameObject.transform.Find("RecommendationStarIcon3").GetComponent<Renderer>().material.mainTexture = starOn;

            gameObject.transform.Find("RecommendationStarIcon4").GetComponent<Renderer>().material = new Material(Shader.Find("Unlit/Transparent"));
            gameObject.transform.Find("RecommendationStarIcon4").GetComponent<Renderer>().material.mainTexture = starOn;
        }
        else if ((actorScore >= actorBase && genreScore >= genreBase && publicityScore >= publicityBase && ratingScore < ratingBase) ||
                 (actorScore >= actorBase && genreScore >= genreBase && publicityScore <  publicityBase && ratingScore >= ratingBase) ||
                 (actorScore >= actorBase && genreScore <  genreBase && publicityScore >= publicityBase && ratingScore >= ratingBase) ||
                 (actorScore <  actorBase && genreScore >= genreBase && publicityScore >= publicityBase && ratingScore >= ratingBase))
        {
            gameObject.transform.Find("Text").GetComponent<TMPro.TextMeshPro>().text = "Recommendation Score: 3 / 4";
            gameObject.transform.Find("RecommendationStarIcon1").GetComponent<Renderer>().material = new Material(Shader.Find("Unlit/Transparent"));
            gameObject.transform.Find("RecommendationStarIcon1").GetComponent<Renderer>().material.mainTexture = starOn;

            gameObject.transform.Find("RecommendationStarIcon2").GetComponent<Renderer>().material = new Material(Shader.Find("Unlit/Transparent"));
            gameObject.transform.Find("RecommendationStarIcon2").GetComponent<Renderer>().material.mainTexture = starOn;

            gameObject.transform.Find("RecommendationStarIcon3").GetComponent<Renderer>().material = new Material(Shader.Find("Unlit/Transparent"));
            gameObject.transform.Find("RecommendationStarIcon3").GetComponent<Renderer>().material.mainTexture = starOn;

            gameObject.transform.Find("RecommendationStarIcon4").GetComponent<Renderer>().material = new Material(Shader.Find("Unlit/Transparent"));
            gameObject.transform.Find("RecommendationStarIcon4").GetComponent<Renderer>().material.mainTexture = starOff;
        }
        else if ((actorScore >= actorBase && genreScore >= genreBase && publicityScore < publicityBase && ratingScore < ratingBase) ||
                 (actorScore >= actorBase && genreScore <  genreBase && publicityScore < publicityBase && ratingScore >= ratingBase) ||
                 (actorScore <  actorBase && genreScore <  genreBase && publicityScore >= publicityBase && ratingScore >= ratingBase) ||
                 (actorScore >= actorBase && genreScore <  genreBase && publicityScore >= publicityBase && ratingScore < ratingBase) ||
                 (actorScore <  actorBase && genreScore >= genreBase && publicityScore <  publicityBase && ratingScore >= ratingBase) ||
                 (actorScore < actorBase && genreScore >= genreBase && publicityScore >= publicityBase && ratingScore < ratingBase))
        {
            gameObject.transform.Find("Text").GetComponent<TMPro.TextMeshPro>().text = "Recommendation Score: 2 / 4";
            gameObject.transform.Find("RecommendationStarIcon1").GetComponent<Renderer>().material = new Material(Shader.Find("Unlit/Transparent"));
            gameObject.transform.Find("RecommendationStarIcon1").GetComponent<Renderer>().material.mainTexture = starOn;

            gameObject.transform.Find("RecommendationStarIcon2").GetComponent<Renderer>().material = new Material(Shader.Find("Unlit/Transparent"));
            gameObject.transform.Find("RecommendationStarIcon2").GetComponent<Renderer>().material.mainTexture = starOn;

            gameObject.transform.Find("RecommendationStarIcon3").GetComponent<Renderer>().material = new Material(Shader.Find("Unlit/Transparent"));
            gameObject.transform.Find("RecommendationStarIcon3").GetComponent<Renderer>().material.mainTexture = starOff;

            gameObject.transform.Find("RecommendationStarIcon4").GetComponent<Renderer>().material = new Material(Shader.Find("Unlit/Transparent"));
            gameObject.transform.Find("RecommendationStarIcon4").GetComponent<Renderer>().material.mainTexture = starOff;
        }
        else if ((actorScore >= actorBase && genreScore < genreBase && publicityScore < publicityBase && ratingScore < ratingBase) ||
                 (actorScore < actorBase && genreScore >= genreBase && publicityScore < publicityBase && ratingScore < ratingBase) ||
                 (actorScore < actorBase && genreScore < genreBase && publicityScore >= publicityBase && ratingScore < ratingBase) ||
                 (actorScore < actorBase && genreScore < genreBase && publicityScore < publicityBase && ratingScore >= ratingBase))
        {
            gameObject.transform.Find("Text").GetComponent<TMPro.TextMeshPro>().text = "Recommendation Score: 1 / 4";
            gameObject.transform.Find("RecommendationStarIcon1").GetComponent<Renderer>().material = new Material(Shader.Find("Unlit/Transparent"));
            gameObject.transform.Find("RecommendationStarIcon1").GetComponent<Renderer>().material.mainTexture = starOn;

            gameObject.transform.Find("RecommendationStarIcon2").GetComponent<Renderer>().material = new Material(Shader.Find("Unlit/Transparent"));
            gameObject.transform.Find("RecommendationStarIcon2").GetComponent<Renderer>().material.mainTexture = starOff;

            gameObject.transform.Find("RecommendationStarIcon3").GetComponent<Renderer>().material = new Material(Shader.Find("Unlit/Transparent"));
            gameObject.transform.Find("RecommendationStarIcon3").GetComponent<Renderer>().material.mainTexture = starOff;

            gameObject.transform.Find("RecommendationStarIcon4").GetComponent<Renderer>().material = new Material(Shader.Find("Unlit/Transparent"));
            gameObject.transform.Find("RecommendationStarIcon4").GetComponent<Renderer>().material.mainTexture = starOff;
        }
        else
        {
            gameObject.transform.Find("Text").GetComponent<TMPro.TextMeshPro>().text = "Recommendation Score: 0 / 4";
            gameObject.transform.Find("RecommendationStarIcon1").GetComponent<Renderer>().material = new Material(Shader.Find("Unlit/Transparent"));
            gameObject.transform.Find("RecommendationStarIcon1").GetComponent<Renderer>().material.mainTexture = starOff;

            gameObject.transform.Find("RecommendationStarIcon2").GetComponent<Renderer>().material = new Material(Shader.Find("Unlit/Transparent"));
            gameObject.transform.Find("RecommendationStarIcon2").GetComponent<Renderer>().material.mainTexture = starOff;

            gameObject.transform.Find("RecommendationStarIcon3").GetComponent<Renderer>().material = new Material(Shader.Find("Unlit/Transparent"));
            gameObject.transform.Find("RecommendationStarIcon3").GetComponent<Renderer>().material.mainTexture = starOff;

            gameObject.transform.Find("RecommendationStarIcon4").GetComponent<Renderer>().material = new Material(Shader.Find("Unlit/Transparent"));
            gameObject.transform.Find("RecommendationStarIcon4").GetComponent<Renderer>().material.mainTexture = starOff;
        }
    }
}
