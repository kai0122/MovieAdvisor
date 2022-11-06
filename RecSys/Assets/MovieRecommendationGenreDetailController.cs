using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovieRecommendationGenreDetailController : MonoBehaviour
{
    public List<GameObject> genreFullLists;
    public List<GameObject> genreMoreLists;

    public float startX;
    public float startY;
    public float startZ;
    public float endX;
    public float endY;
    public float endZ;
    private float shiftX = 0.5f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (OpenGenreJustification_ing)
        {
            if (Vector3.Distance(gameObject.transform.localPosition, new Vector3(endX, endY, endZ)) > shiftX)
            {
                gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x + shiftX,
                                                                 gameObject.transform.localPosition.y,
                                                                 gameObject.transform.localPosition.z);
            }
            else
            {
                gameObject.transform.localPosition = new Vector3(endX, endY, endZ);
                OpenGenreJustification_ing = false;
            }
        }

        if (CloseGenreJustification_ing)
        {
            if (Vector3.Distance(gameObject.transform.localPosition, new Vector3(startX, startY, startZ)) > shiftX)
            {
                gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x - shiftX,
                                                                 gameObject.transform.localPosition.y,
                                                                 gameObject.transform.localPosition.z);
            }
            else
            {
                gameObject.transform.localPosition = new Vector3(startX, startY, startZ);
                CloseGenreJustification_ing = false;
                gameObject.SetActive(false);
            }
        }
    }

    public bool OpenGenreJustification_ing = false;
    public bool CloseGenreJustification_ing = false;

    public void OpenGenreJustification()
    {
        gameObject.SetActive(true);
        //OpenGenreJustification_ing = true;
    }

    public void CloseGenreJustification()
    {
        //gameObject.SetActive(true);
        //CloseGenreJustification_ing = true;
        gameObject.SetActive(false);
    }

    public void ChangeGenreText(List<string> genres, int totalMovies)
    {
        string title = "Because you watched:\n\n";
        for (int i = 0; i< genreFullLists.Count; i++)
        {
            if (i < genres.Count)
            {
                genreFullLists[i].SetActive(true);
                if (genreMoreLists.Count > 0)
                {
                    genreMoreLists[i].SetActive(true);
                }
                genreFullLists[i].GetComponent<TMPro.TextMeshPro>().text = genres[i];
                title += "\n\n";
            }
            else
            {
                genreFullLists[i].SetActive(false);
                if (genreMoreLists.Count > 0)
                {
                    genreMoreLists[i].SetActive(false);
                }
            }
        }

        //totalMovies = 60;
        //title += "out of " + totalMovies.ToString() + " genre movies";
        gameObject.transform.Find("Text").GetComponent<TMPro.TextMeshPro>().text = title;

    }

    public List<GameObject> GenreBars;
    public List<GameObject> GenreNums;
    public List<GameObject> GenreIcons;

    public Texture actionMovie;
    public Texture adventureMovie;
    public Texture scifiMovie;
    public Texture animationMovie;
    public Texture familyMovie;
    public Texture comedyMovie;
    public Texture romanceMovie;
    public Texture horrorMovie;
    public Texture thrillerMovie;
    public Texture dramaMovie;
    public Texture fantasyMovie;
    public Texture historyMovie;
    public Texture musicalMovie;

    public void ChangeGenreBar(string _movieName, int _sum, List<string> genres, List<int> genreNums, int totalMovies)
    {
        int sum = 0;
        foreach(int genreNum in genreNums)
        {
            sum += genreNum;
        }

        string title = "Because you watched:\n\n";
        float accumulatedDistanceZ = 0;
        for (int i = 0; i < genreFullLists.Count; i++)
        {
            if (i < genres.Count)
            {
                genreFullLists[i].SetActive(true);
                GenreNums[i].SetActive(true);
                //GenreBars[i].SetActive(true);
                GenreIcons[i].SetActive(true);
                if (genreMoreLists.Count > 0)
                {
                    genreMoreLists[i].SetActive(true);
                }
                genreFullLists[i].GetComponent<TMPro.TextMeshPro>().text = genres[i] + " movies";
                title += "\n\n";
                GenreNums[i].GetComponent<TMPro.TextMeshPro>().text = genreNums[i].ToString();
                //GenreBars[i].transform.localScale = new Vector3(GenreBars[i].transform.localScale.x,
                //                                                GenreBars[i].transform.localScale.y,
                //                                                0.2f * ((float) genreNums[i] / sum));
                //GenreBars[i].transform.localPosition = new Vector3(GenreBars[i].transform.localPosition.x,
                //                                                   GenreBars[i].transform.localPosition.y,
                //                                                   (-0.15f + (0.2f - 0.2f * ((float)genreNums[i] / sum)) / 2.0f * 10.0f) - accumulatedDistanceZ);

                GenreNums[i].transform.localPosition = new Vector3(GenreNums[i].transform.localPosition.x,
                                                                   GenreNums[i].transform.localPosition.y,
                                                                   GenreNums[i].transform.localPosition.z);

                genreFullLists[i].transform.localPosition = new Vector3(genreFullLists[i].transform.localPosition.x,
                                                                        genreFullLists[i].transform.localPosition.y,
                                                                        GenreNums[i].transform.localPosition.z);
                if (genreMoreLists.Count > 0)
                {
                    genreMoreLists[i].transform.localPosition = new Vector3(genreMoreLists[i].transform.localPosition.x,
                                                                            genreMoreLists[i].transform.localPosition.y,
                                                                            GenreNums[i].transform.localPosition.z);
                }

                accumulatedDistanceZ += GenreBars[i].transform.localScale.z * 10.0f;

                GenreIcons[i].transform.localPosition = new Vector3(GenreIcons[i].transform.localPosition.x,
                                                                    GenreIcons[i].transform.localPosition.y,
                                                                    GenreNums[i].transform.localPosition.z);

                GenreIcons[i].GetComponent<Renderer>().material = new Material(Shader.Find("Unlit/Transparent"));
                if (genres[i] == "Action")
                {
                    GenreIcons[i].GetComponent<Renderer>().material.mainTexture = actionMovie;
                }
                else if (genres[i] == "Adventure")
                {
                    GenreIcons[i].GetComponent<Renderer>().material.mainTexture = adventureMovie;
                }
                else if (genres[i] == "Animation")
                {
                    GenreIcons[i].GetComponent<Renderer>().material.mainTexture = animationMovie;
                }
                else if (genres[i] == "Science Fiction")
                {
                    GenreIcons[i].GetComponent<Renderer>().material.mainTexture = scifiMovie;
                }
                else if (genres[i] == "Family")
                {
                    GenreIcons[i].GetComponent<Renderer>().material.mainTexture = familyMovie;
                }
                else if (genres[i] == "Comedy")
                {
                    GenreIcons[i].GetComponent<Renderer>().material.mainTexture = comedyMovie;
                }
                else if (genres[i] == "Romance")
                {
                    GenreIcons[i].GetComponent<Renderer>().material.mainTexture = romanceMovie;
                }
                else if (genres[i] == "Horror")
                {
                    GenreIcons[i].GetComponent<Renderer>().material.mainTexture = horrorMovie;
                }
                else if (genres[i] == "Thriller")
                {
                    GenreIcons[i].GetComponent<Renderer>().material.mainTexture = thrillerMovie;
                }
                else if (genres[i] == "Drama")
                {
                    GenreIcons[i].GetComponent<Renderer>().material.mainTexture = dramaMovie;
                }
                else if (genres[i] == "Fantasy")
                {
                    GenreIcons[i].GetComponent<Renderer>().material.mainTexture = fantasyMovie;
                }
                else if (genres[i] == "History")
                {
                    GenreIcons[i].GetComponent<Renderer>().material.mainTexture = historyMovie;
                }
                else if (genres[i] == "Musical")
                {
                    GenreIcons[i].GetComponent<Renderer>().material.mainTexture = musicalMovie;
                }
                else
                {
                    GenreIcons[i].SetActive(false);
                }
            }
            else
            {
                genreFullLists[i].SetActive(false);
                GenreNums[i].SetActive(false);
                //GenreBars[i].SetActive(false);
                GenreIcons[i].SetActive(false);
                if (genreMoreLists.Count > 0)
                {
                    genreMoreLists[i].SetActive(false);
                }
            }
        }

        //totalMovies = 60;
        //title += "out of " + totalMovies.ToString() + " genre movies";
        gameObject.transform.Find("Text").GetComponent<TMPro.TextMeshPro>().text = title;

        gameObject.transform.Find("GenreHint").GetComponent<TMPro.TextMeshPro>().text = "The Amount of movies that you liked correspond to " + _movieName.ToUpper() + "'s genres: " + _sum.ToString();
    }

    public void ChangeGenreBar(List<string> genres, List<int> genreNums, int totalMovies)
    {
        int sum = 0;
        foreach (int genreNum in genreNums)
        {
            sum += genreNum;
        }

        string title = "Because you watched:\n\n";
        float accumulatedDistanceZ = 0;
        for (int i = 0; i < genreFullLists.Count; i++)
        {
            if (i < genres.Count)
            {
                genreFullLists[i].SetActive(true);
                GenreNums[i].SetActive(true);
                //GenreBars[i].SetActive(true);
                GenreIcons[i].SetActive(true);
                if (genreMoreLists.Count > 0)
                {
                    genreMoreLists[i].SetActive(true);
                }
                genreFullLists[i].GetComponent<TMPro.TextMeshPro>().text = genres[i] + " movies";
                title += "\n\n";
                GenreNums[i].GetComponent<TMPro.TextMeshPro>().text = genreNums[i].ToString();
                //GenreBars[i].transform.localScale = new Vector3(GenreBars[i].transform.localScale.x,
                //                                                GenreBars[i].transform.localScale.y,
                //                                                0.2f * ((float) genreNums[i] / sum));
                //GenreBars[i].transform.localPosition = new Vector3(GenreBars[i].transform.localPosition.x,
                //                                                   GenreBars[i].transform.localPosition.y,
                //                                                   (-0.15f + (0.2f - 0.2f * ((float)genreNums[i] / sum)) / 2.0f * 10.0f) - accumulatedDistanceZ);

                GenreNums[i].transform.localPosition = new Vector3(GenreNums[i].transform.localPosition.x,
                                                                   GenreNums[i].transform.localPosition.y,
                                                                   GenreNums[i].transform.localPosition.z);

                genreFullLists[i].transform.localPosition = new Vector3(genreFullLists[i].transform.localPosition.x,
                                                                        genreFullLists[i].transform.localPosition.y,
                                                                        GenreNums[i].transform.localPosition.z);
                if (genreMoreLists.Count > 0)
                {
                    genreMoreLists[i].transform.localPosition = new Vector3(genreMoreLists[i].transform.localPosition.x,
                                                                            genreMoreLists[i].transform.localPosition.y,
                                                                            GenreNums[i].transform.localPosition.z);
                }

                accumulatedDistanceZ += GenreBars[i].transform.localScale.z * 10.0f;

                GenreIcons[i].transform.localPosition = new Vector3(GenreIcons[i].transform.localPosition.x,
                                                                    GenreIcons[i].transform.localPosition.y,
                                                                    GenreNums[i].transform.localPosition.z);

                GenreIcons[i].GetComponent<Renderer>().material = new Material(Shader.Find("Unlit/Transparent"));
                if (genres[i] == "Action")
                {
                    GenreIcons[i].GetComponent<Renderer>().material.mainTexture = actionMovie;
                }
                else if (genres[i] == "Adventure")
                {
                    GenreIcons[i].GetComponent<Renderer>().material.mainTexture = adventureMovie;
                }
                else if (genres[i] == "Animation")
                {
                    GenreIcons[i].GetComponent<Renderer>().material.mainTexture = animationMovie;
                }
                else if (genres[i] == "Science Fiction")
                {
                    GenreIcons[i].GetComponent<Renderer>().material.mainTexture = scifiMovie;
                }
                else if (genres[i] == "Family")
                {
                    GenreIcons[i].GetComponent<Renderer>().material.mainTexture = familyMovie;
                }
                else if (genres[i] == "Comedy")
                {
                    GenreIcons[i].GetComponent<Renderer>().material.mainTexture = comedyMovie;
                }
                else if (genres[i] == "Romance")
                {
                    GenreIcons[i].GetComponent<Renderer>().material.mainTexture = romanceMovie;
                }
                else if (genres[i] == "Horror")
                {
                    GenreIcons[i].GetComponent<Renderer>().material.mainTexture = horrorMovie;
                }
                else if (genres[i] == "Thriller")
                {
                    GenreIcons[i].GetComponent<Renderer>().material.mainTexture = thrillerMovie;
                }
                else if (genres[i] == "Drama")
                {
                    GenreIcons[i].GetComponent<Renderer>().material.mainTexture = dramaMovie;
                }
                else if (genres[i] == "Fantasy")
                {
                    GenreIcons[i].GetComponent<Renderer>().material.mainTexture = fantasyMovie;
                }
                else if (genres[i] == "History")
                {
                    GenreIcons[i].GetComponent<Renderer>().material.mainTexture = historyMovie;
                }
                else if (genres[i] == "Musical")
                {
                    GenreIcons[i].GetComponent<Renderer>().material.mainTexture = musicalMovie;
                }
                else
                {
                    GenreIcons[i].SetActive(false);
                }
            }
            else
            {
                genreFullLists[i].SetActive(false);
                GenreNums[i].SetActive(false);
                //GenreBars[i].SetActive(false);
                GenreIcons[i].SetActive(false);
                if (genreMoreLists.Count > 0)
                {
                    genreMoreLists[i].SetActive(false);
                }
            }
        }

        //totalMovies = 60;
        //title += "out of " + totalMovies.ToString() + " genre movies";
        gameObject.transform.Find("Text").GetComponent<TMPro.TextMeshPro>().text = title;

    }
}
