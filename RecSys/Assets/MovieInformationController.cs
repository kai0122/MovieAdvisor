using System.Collections;
using System.Collections.Generic;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Movies;
using UnityEngine;

public class MovieInformationController : MonoBehaviour
{
    public List<ImageLoader> actorPoster;
    public ImageLoader targetMoviePoster;
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
    public List<GameObject> genreImage;

    // Start is called before the first frame update
    void Start()
    {
        startPosition_MovieInfoGameObject = MovieInfoGameObject.transform.localPosition;
        plane2.SetActive(false);
        plane3.SetActive(false);
        plane4.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject MovieInfoGameObject;
    public GameObject plane2;
    public GameObject plane3;
    public GameObject plane4;
    private Vector3 startPosition_MovieInfoGameObject;
    public void ExpendRecommendationWhy()
    {
        plane2.SetActive(true);
        plane3.SetActive(false);
        MovieInfoGameObject.transform.localPosition = startPosition_MovieInfoGameObject - new Vector3(0, 0, 0.54f);
    }

    public void ExpendRecommendationWhy_v2()
    {
        plane2.SetActive(true);
        plane3.SetActive(false);
        MovieInfoGameObject.transform.localPosition = startPosition_MovieInfoGameObject - new Vector3(0, 0, 0.735f);
    }

    public void ExpendRecommendationWhy_2()
    {
        plane2.SetActive(true);
        plane3.SetActive(false);
        MovieInfoGameObject.transform.localPosition = startPosition_MovieInfoGameObject - new Vector3(0, 0, 0.732f);
    }

    public void ExpendRecommendationWhy_2_v2()
    {
        plane2.SetActive(true);
        plane3.SetActive(false);
        MovieInfoGameObject.transform.localPosition = startPosition_MovieInfoGameObject - new Vector3(0, 0, 1.0f);
    }

    public void CloseRecommendationWhy()
    {
        plane2.SetActive(false);
        plane3.SetActive(false);
        MovieInfoGameObject.transform.localPosition = startPosition_MovieInfoGameObject;
    }

    public void ExpendRecommendationActor_1()
    {
        plane3.SetActive(true);
        plane4.SetActive(false);
        MovieInfoGameObject.transform.localPosition = startPosition_MovieInfoGameObject - new Vector3(0, 0, 1.1f);
    }

    public void ExpendRecommendationActor_2()
    {
        plane3.SetActive(true);
        plane4.SetActive(false);
        MovieInfoGameObject.transform.localPosition = startPosition_MovieInfoGameObject - new Vector3(0, 0, 1.4f);
    }

    public void ExpendRecommendationActorMore_1()
    {
        plane4.SetActive(true);
        MovieInfoGameObject.transform.localPosition = startPosition_MovieInfoGameObject - new Vector3(0, 0, 1.56f);
    }

    public void CloseRecommendationWhyActor_1()
    {
#pragma warning disable CS0618 // 類型或成員已經過時
        if (plane3.active == true)
        {
#pragma warning restore
            plane3.SetActive(false);
            plane4.SetActive(false);
            MovieInfoGameObject.transform.localPosition = startPosition_MovieInfoGameObject - new Vector3(0, 0, 0.54f);
        }
    }

    public void CloseRecommendationWhyActor_2()
    {
#pragma warning disable CS0618 // 類型或成員已經過時
        if (plane3.active == true)
        {
#pragma warning restore
            plane3.SetActive(false);
            plane4.SetActive(false);
            MovieInfoGameObject.transform.localPosition = startPosition_MovieInfoGameObject - new Vector3(0, 0, 0.732f);
        }
    }

    public void CloseRecommendationWhyActorMore_1()
    {
#pragma warning disable CS0618 // 類型或成員已經過時
        if (plane4.active == true)
        {
#pragma warning restore
            plane4.SetActive(false);
            MovieInfoGameObject.transform.localPosition = startPosition_MovieInfoGameObject - new Vector3(0, 0, 1.1f);
        }
    }

    public void ExpendRecommendationWhyPublicity_1()
    {
        plane3.SetActive(true);
        plane4.SetActive(false);
        MovieInfoGameObject.transform.localPosition = startPosition_MovieInfoGameObject - new Vector3(0, 0, 0.7f);
    }

    public void ExpendRecommendationWhyPublicity_2()
    {
        plane3.SetActive(true);
        plane4.SetActive(false);
        MovieInfoGameObject.transform.localPosition = startPosition_MovieInfoGameObject - new Vector3(0, 0, 1f);
    }

    public void CloseRecommendationWhyPublicity_1()
    {
#pragma warning disable CS0618 // 類型或成員已經過時
        if (plane3.active == true)
        {
#pragma warning restore
            plane3.SetActive(false);
            plane4.SetActive(false);
            MovieInfoGameObject.transform.localPosition = startPosition_MovieInfoGameObject - new Vector3(0, 0, 0.54f);
        }
    }

    public void CloseRecommendationWhyPublicity_2()
    {
#pragma warning disable CS0618 // 類型或成員已經過時
        if (plane3.active == true)
        {
#pragma warning restore
            plane3.SetActive(false);
            plane4.SetActive(false);
            MovieInfoGameObject.transform.localPosition = startPosition_MovieInfoGameObject - new Vector3(0, 0, 0.732f);
        }
    }

    public void ExpendRecommendationWhyPublicityMore_1()
    {
        MovieInfoGameObject.transform.localPosition = startPosition_MovieInfoGameObject - new Vector3(0, 0, 1.1f);
    }

    public void ExpendRecommendationWhyPublicityMore_2()
    {
        MovieInfoGameObject.transform.localPosition = startPosition_MovieInfoGameObject - new Vector3(0, 0, 1.4f);
    }

    public void CloseRecommendationWhyPublicityMore_1()
    {
#pragma warning disable CS0618 // 類型或成員已經過時
        if (plane3.active == true)
        {
#pragma warning restore
            MovieInfoGameObject.transform.localPosition = startPosition_MovieInfoGameObject - new Vector3(0, 0, 0.7f);
        }
    }

    public void CloseRecommendationWhyPublicityMore_2()
    {
#pragma warning disable CS0618 // 類型或成員已經過時
        if (plane3.active == true)
        {
#pragma warning restore
            MovieInfoGameObject.transform.localPosition = startPosition_MovieInfoGameObject - new Vector3(0, 0, 1f);
        }
    }

    public void ExpendRecommendationWhyRating_1()
    {
        plane3.SetActive(true);
        plane4.SetActive(false);
        MovieInfoGameObject.transform.localPosition = startPosition_MovieInfoGameObject - new Vector3(0, 0, 0.949f);
    }

    public void ExpendRecommendationWhyRating_2()
    {
        plane3.SetActive(true);
        plane4.SetActive(false);
        MovieInfoGameObject.transform.localPosition = startPosition_MovieInfoGameObject - new Vector3(0, 0, 1.158f);
    }

    public void CloseRecommendationWhyRating_1()
    {
#pragma warning disable CS0618 // 類型或成員已經過時
        if (plane3.active == true)
        {
#pragma warning restore
            plane3.SetActive(false);
            plane4.SetActive(false);
            MovieInfoGameObject.transform.localPosition = startPosition_MovieInfoGameObject - new Vector3(0, 0, 0.54f);
        }
    }

    public void CloseRecommendationWhyRating_2()
    {
#pragma warning disable CS0618 // 類型或成員已經過時
        if (plane3.active == true)
        {
#pragma warning restore
            plane3.SetActive(false);
            plane4.SetActive(false);
            MovieInfoGameObject.transform.localPosition = startPosition_MovieInfoGameObject - new Vector3(0, 0, 0.732f);
        }
    }

    public void ExpendRecommendationWhyRatingMore_1()
    {
        plane4.SetActive(true);
        MovieInfoGameObject.transform.localPosition = startPosition_MovieInfoGameObject - new Vector3(0, 0, 1.593f);
    }

    public void ExpendRecommendationWhyRatingMore_2()
    {
        plane4.SetActive(true);
        MovieInfoGameObject.transform.localPosition = startPosition_MovieInfoGameObject - new Vector3(0, 0, 1.765f);
    }

    public void CloseRecommendationWhyRatingMore_1()
    {
#pragma warning disable CS0618 // 類型或成員已經過時
        if (plane4.active == true)
        {
#pragma warning restore
            plane4.SetActive(false);
            MovieInfoGameObject.transform.localPosition = startPosition_MovieInfoGameObject - new Vector3(0, 0, 0.949f);
        }
    }

    public void CloseRecommendationWhyRatingMore_2()
    {
#pragma warning disable CS0618 // 類型或成員已經過時
        if (plane4.active == true)
        {
#pragma warning restore
            plane4.SetActive(false);
            MovieInfoGameObject.transform.localPosition = startPosition_MovieInfoGameObject - new Vector3(0, 0, 1.158f);
        }
    }

    public void ChangeMovieName(string _name)
    {
        TMPro.TextMeshPro textMeshPro = gameObject.transform.Find("Name").GetComponent<TMPro.TextMeshPro>();
        textMeshPro.text = _name;
    }

    public void ChangeMovieInfo3(double rating, int peopleNum, List<Genre> genres, List<Cast> stars, List<string> starPhotoUrls, string moviePhotoUrl)
    {
        //gameObject.transform.Find("MovieInfoRatingNum").GetComponent<TMPro.TextMeshPro>().text = "(Rated by " + peopleNum + " people)";

        TMPro.TextMeshPro textMeshPro = MovieInfoGameObject.transform.Find("MovieInfo").GetComponent<TMPro.TextMeshPro>();
        string info = "";
        // info += "Ratings: " + rating.ToString() + " / 10";
        //info += "\n\n";

        info += genres[0].Name;
        int genreCount = 4;
        if (genres.Count < 4) genreCount = genres.Count;
        for (int i = 1; i < 4; i++)
        {
            if (i < genres.Count && i%2 != 0)
                info += ", " + genres[i].Name;
            else if (i < genres.Count)
                info += ",\n" + genres[i].Name;
            else
                info += "\n";
        }

        info += "\nActor/Actress:";
        textMeshPro.text = info;

        int starCount = 4;
        if (stars.Count < 4) starCount = stars.Count;
        for (int i = 0; i < starCount; i++)
        {
            MovieInfoGameObject.transform.Find("MovieName" + (i + 1).ToString()).GetComponent<TMPro.TextMeshPro>().text = stars[i].Name;
            actorPoster[i].ChangeImage(starPhotoUrls[i]);
        }

        //targetMoviePoster.ChangeImage(moviePhotoUrl);
    }

    public void ChangeMovieInfo(double rating, int peopleNum, List<Genre> genres, List<Cast> stars, List<string> starPhotoUrls, string moviePhotoUrl)
    {
        //gameObject.transform.Find("MovieInfoRatingNum").GetComponent<TMPro.TextMeshPro>().text = "(Rated by " + peopleNum + " people)";

        TMPro.TextMeshPro textMeshPro = gameObject.transform.Find("MovieInfo").GetComponent<TMPro.TextMeshPro>();
        string info = "";
        // info += "Ratings: " + rating.ToString() + " / 10";
        //info += "\n\n";

        info += genres[0].Name;
        int genreCount = 4;
        if (genres.Count < 4) genreCount = genres.Count;
        for (int i = 1; i < 4; i++)
        {
            if (i < genres.Count)
                info += ",\n" + genres[i].Name;
            else
                info += "\n";
        }

        info += "\n\n\nActor/Actress:";
        textMeshPro.text = info;

        int starCount = 4;
        if (stars.Count < 4) starCount = stars.Count;
        for (int i = 0; i < starCount; i++)
        {
            gameObject.transform.Find("MovieName" + (i + 1).ToString()).GetComponent<TMPro.TextMeshPro>().text = stars[i].Name;
            actorPoster[i].ChangeImage(starPhotoUrls[i]);
        }

        targetMoviePoster.ChangeImage(moviePhotoUrl);
    }

    public void ChangeMovieInfo2(double rating, int peopleNum, List<Genre> genres, List<Cast> stars, List<string> starPhotoUrls, string moviePhotoUrl)
    {
        TMPro.TextMeshPro textMeshPro = gameObject.transform.Find("MovieInfo").GetComponent<TMPro.TextMeshPro>();
        string info = "";
        info += "\n\n\n\n\n\nActor/Actress:";
        textMeshPro.text = info;

        int starCount = 4;
        if (stars.Count < 4) starCount = stars.Count;
        for (int i = 0; i < starCount; i++)
        {
            gameObject.transform.Find("MovieName" + (i + 1).ToString()).GetComponent<TMPro.TextMeshPro>().text = stars[i].Name;
            actorPoster[i].ChangeImage(starPhotoUrls[i]);
        }

        targetMoviePoster.ChangeImage(moviePhotoUrl);
        for (int i = 0; i < 4; i++)
        {
            if (i < genres.Count)
            {
                gameObject.transform.Find("MovieGenreName" + (i + 1).ToString()).GetComponent<TMPro.TextMeshPro>().text = genres[i].Name;
                genreImage[i].GetComponent<Renderer>().material = new Material(Shader.Find("Unlit/Transparent"));
                if (genres[i].Name == "Action")
                {
                    genreImage[i].GetComponent<Renderer>().material.mainTexture = actionMovie;
                }
                else if (genres[i].Name == "Adventure")
                {
                    genreImage[i].GetComponent<Renderer>().material.mainTexture = adventureMovie;
                }
                else if (genres[i].Name == "Animation")
                {
                    genreImage[i].GetComponent<Renderer>().material.mainTexture = animationMovie;
                }
                else if (genres[i].Name == "Science Fiction")
                {
                    genreImage[i].GetComponent<Renderer>().material.mainTexture = scifiMovie;
                }
                else if (genres[i].Name == "Family")
                {
                    genreImage[i].GetComponent<Renderer>().material.mainTexture = familyMovie;
                }
                else if (genres[i].Name == "Comedy")
                {
                    genreImage[i].GetComponent<Renderer>().material.mainTexture = comedyMovie;
                }
                else if (genres[i].Name == "Romance")
                {
                    genreImage[i].GetComponent<Renderer>().material.mainTexture = romanceMovie;
                }
                else if (genres[i].Name == "Horror")
                {
                    genreImage[i].GetComponent<Renderer>().material.mainTexture = horrorMovie;
                }
                else if (genres[i].Name == "Thriller")
                {
                    genreImage[i].GetComponent<Renderer>().material.mainTexture = thrillerMovie;
                }
                else if (genres[i].Name == "Drama")
                {
                    genreImage[i].GetComponent<Renderer>().material.mainTexture = dramaMovie;
                }
                else if (genres[i].Name == "Fantasy")
                {
                    genreImage[i].GetComponent<Renderer>().material.mainTexture = fantasyMovie;
                }
                else if (genres[i].Name == "History")
                {
                    genreImage[i].GetComponent<Renderer>().material.mainTexture = historyMovie;
                }
                else if (genres[i].Name == "Musical")
                {
                    genreImage[i].GetComponent<Renderer>().material.mainTexture = musicalMovie;
                }
                else
                {
                    genreImage[i].SetActive(false);
                    gameObject.transform.Find("MovieGenreName" + (i + 1).ToString()).GetComponent<TMPro.TextMeshPro>().text = "";
                }
            }
            else
            {
                genreImage[i].SetActive(false);
                gameObject.transform.Find("MovieGenreName" + (i + 1).ToString()).GetComponent<TMPro.TextMeshPro>().text = "";
            }
        }

    }

    public void ChangeMovieStory(string story)
    {
        string subStory = "";
        foreach (string str in story.Split('.'))
        {
            if ((subStory + str).Length < 150)
            {
                subStory += str + ".";
            }
            else
            {
                subStory += "...";
                break;
            }
        }

        if (subStory.Length < 10)
        {
            // for demo video, video = Tom and Jerry 3
            subStory = "Tom the cat and Jerry the mouse get kicked out of their home and relocate to a fancy New York hotel, where a scrappy employee named Kayla will lose her job if she can’t evict Jerry before a high-class wedding at the hotel. Her solution? Hiring Tom to get rid of the pesky mouse.";
        }

        gameObject.transform.Find("MovieInfo").Find("MovieStory").GetComponent<TMPro.TextMeshPro>().text = subStory;

    }
}
