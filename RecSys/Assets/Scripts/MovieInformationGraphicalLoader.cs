using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovieInformationGraphicalLoader : MonoBehaviour
{
    public GameObject newMoviePosterGameObject;
    private int gameObjectNumber = 0;
    private float startX = -0.2f;
    private float startY = 0.05f;
    private float distX = 0.2f;
    private float distY = -0.3f;
    private int rowNum = 3;
    public List<GameObject> myCreateGB = new List<GameObject>();

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
#pragma warning disable CS0618 // 類型或成員已經過時
        if (gameObject.active)
        {
#pragma warning restore CS0618 // 類型或成員已經過時
            GameObject newPoster = Instantiate(newMoviePosterGameObject, new Vector3(0, 0, 0), Quaternion.identity);
            newPoster.transform.parent = gameObject.transform;
            newPoster.transform.name = "MoviePoster" + gameObjectNumber.ToString();

            newPoster.transform.localPosition = new Vector3(startX + gameObjectNumber % rowNum * distX, startY + gameObjectNumber / rowNum * distY, -0.1f);
            newPoster.transform.localRotation = Quaternion.identity;

            ImageLoader imageLoader = newPoster.GetComponent<ImageLoader>();
            imageLoader.ChangeImage(_url);

            gameObjectNumber += 1;

            myCreateGB.Add(newPoster);
        }
    }

    public void SetGraphActive(bool _bool)
    {
        if (_bool == false)
        {
            foreach (GameObject gb in myCreateGB)
            {
                Destroy(gb);
            }
            gameObjectNumber = 0;
        }
        gameObject.SetActive(_bool);
    }
}
