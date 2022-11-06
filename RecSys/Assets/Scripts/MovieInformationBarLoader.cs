using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovieInformationBarLoader : MonoBehaviour
{
    public GameObject newMoviePosterGameObject;
    private int gameObjectNumber = 0;
    private float startX = -0.3f;
    private float startY = 0.2f;
    private float distX = 0.3f;
    private float distY = -0.4f;
    private int rowNum = 3;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddNewMoviePoster(string _url)
    {
        GameObject newPoster = Instantiate(newMoviePosterGameObject, new Vector3(0, 0, 0), Quaternion.identity);
        newPoster.transform.parent = gameObject.transform;
        newPoster.transform.name = "MoviePoster" + gameObjectNumber.ToString();

        newPoster.transform.localPosition = new Vector3(startX + gameObjectNumber % rowNum * distX, startY + gameObjectNumber / rowNum * distY, -0.02f);
        newPoster.transform.localRotation = Quaternion.identity;

        ImageLoader imageLoader = newPoster.GetComponent<ImageLoader>();
        imageLoader.ChangeImage(_url);

        gameObjectNumber += 1;
    }
}
