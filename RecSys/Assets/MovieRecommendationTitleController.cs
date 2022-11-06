using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovieRecommendationTitleController : MonoBehaviour
{
    public GameObject scoreBackground;
    public Texture pass;
    public Texture fail;
    public Material red;
    public Material green;

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

    public void ChangeRecommendationTitle(double actorScore, int actorBase, double genreScore, int genreBase, double publicityScore, int publicityBase, double ratingScore, int ratingBase)
    {
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

        
    }
}
