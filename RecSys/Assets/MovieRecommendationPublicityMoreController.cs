using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovieRecommendationPublicityMoreController : MonoBehaviour
{
    public List<ImageLoader> posters;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateRecommendationPublicityDetailTitle(int totalNum)
    {
        gameObject.transform.Find("Title").GetComponent<TMPro.TextMeshPro>().text = "All " + totalNum.ToString() + " movies:";
    }

    public void UpdateRecommendationFullList(List<string> movieNames)
    {
        if (movieNames.Count < 15)
        {
            string text = "";
            foreach (string name in movieNames)
            {
                text += name + "\n";
            }
            TMPro.TextMeshPro textMeshPro = gameObject.transform.Find("MovieNames1").GetComponent<TMPro.TextMeshPro>();
            textMeshPro.text = text;

            textMeshPro = gameObject.transform.Find("MovieNames2").GetComponent<TMPro.TextMeshPro>();
            textMeshPro.text = "";
        }
        else
        {
            string text = "";
            foreach (string name in movieNames.GetRange(0, 15))
            {
                text += name + "\n";
            }
            TMPro.TextMeshPro textMeshPro = gameObject.transform.Find("MovieNames1").GetComponent<TMPro.TextMeshPro>();
            textMeshPro.text = text;

            text = "";
            foreach (string name in movieNames.GetRange(15, movieNames.Count-15))
            {
                text += name + "\n";
            }
            textMeshPro = gameObject.transform.Find("MovieNames2").GetComponent<TMPro.TextMeshPro>();
            textMeshPro.text = text;
        }
    }

    private int currentPage;
    List<string> names;
    List<string> posterUrls;
    public void UpdateRecommendationFullList_part(List<string> movieNames, List<string> _posterUrls)
    {
        currentPage = -3;
        names = movieNames;
        posterUrls = _posterUrls;
    }

    public void ShowRecommendationFullList_part()
    {
        currentPage += 3;

        for(int i = 0;i < 3; i++)
        {
            if (currentPage + i < names.Count)
            {
                posters[i].gameObject.SetActive(true);
                posters[i].ChangeImage(posterUrls[currentPage + i]);
                gameObject.transform.Find("MovieName" + (i + 1).ToString()).GetComponent<TMPro.TextMeshPro>().text = names[currentPage + i];
            }
            else if (i == 0)
            {
                currentPage = 0;
                posters[i].gameObject.SetActive(true);
                posters[i].ChangeImage(posterUrls[currentPage + i]);
                gameObject.transform.Find("MovieName" + (i + 1).ToString()).GetComponent<TMPro.TextMeshPro>().text = names[currentPage + i];
            }
            else
            {
                posters[i].gameObject.SetActive(false);
                gameObject.transform.Find("MovieName" + (i + 1).ToString()).GetComponent<TMPro.TextMeshPro>().text = "";
                if (i == 2)
                {
                    currentPage = -3;
                }
            }
        }
    }
}
