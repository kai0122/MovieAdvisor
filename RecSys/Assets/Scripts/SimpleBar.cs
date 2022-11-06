using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleBar : MonoBehaviour
{
    public GameObject simpleBarGameObject;
    private float startPosY = 0.1f;
    private float distPosY = -0.15f;
    private float originY = 0;
    private Vector3 originPos;
    public List<GameObject> myCreateGB = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddNewSingleBar(int _barNum, string _name, float _ratio, int _parentMovieCount)
    {
#pragma warning disable CS0618 // 類型或成員已經過時
        if (gameObject.active)
#pragma warning restore CS0618 // 類型或成員已經過時
        {
            GameObject newBar = Instantiate(simpleBarGameObject, new Vector3(0, 0, 0), Quaternion.identity);
            newBar.transform.parent = gameObject.transform;
            newBar.transform.name = _name;

            if (_parentMovieCount <= 3)
            {
                startPosY = 0.5f;
            }
            else if (_parentMovieCount <= 6)
            {
                startPosY = 0.2f;
            }
            newBar.transform.localPosition = new Vector3(0f, startPosY + _barNum * distPosY, -0.01f);
            newBar.transform.localRotation = Quaternion.identity;
            newBar.transform.localScale = new Vector3(1, 1, 1);

            newBar.GetComponent<BarController>().SetText(_name);
            newBar.GetComponent<BarController>().SetBarLength(_ratio);

            if (_barNum >= 4)
            {
                Debug.Log(_barNum);
                GameObject backgound = gameObject.transform.Find("Background").gameObject;
                backgound.transform.localScale = new Vector3(backgound.transform.localScale.x, originY * (_barNum + 2) / 5, backgound.transform.localScale.z);
                backgound.transform.localPosition = originPos - new Vector3(0, ((float)(_barNum + 2) / 5 - 1) / 2, 0);
            }
            else
            {
                GameObject backgound = gameObject.transform.Find("Background").gameObject;
                originY = backgound.transform.localScale.y;
                originPos = backgound.transform.localPosition;
            }

            myCreateGB.Add(newBar);
        }
    }

    public void SetGraphActive(bool _bool)
    {
        if (_bool == false)
        {
            foreach(GameObject gb in myCreateGB)
            {
                Destroy(gb);
            }
        }
        gameObject.SetActive(_bool);
    }
}
