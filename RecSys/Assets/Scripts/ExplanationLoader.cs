using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplanationLoader : MonoBehaviour
{
    public TextLoader textLoader;
    
    // Start is called before the first frame update
    void Start()
    {
        textLoader = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (textLoader == null)
        {
            try
            {
                textLoader = gameObject.transform.Find("MovieInformation").GetComponent<TextLoader>();
            }
            catch (NullReferenceException e)
            {
                Debug.Log(e);
            }
        }
    }

    public void SetTextActive(bool _bool)
    {
        if (textLoader == null)
        {
            textLoader = gameObject.transform.Find("MovieInformation").GetComponent<TextLoader>();
            textLoader.gameObject.SetActive(_bool);
        }
        else
        {
            textLoader.gameObject.SetActive(_bool);
        }
    }

    public TextLoader GetTextLoader()
    {
        if (textLoader == null)
        {
            textLoader = gameObject.transform.Find("MovieInformation").GetComponent<TextLoader>();
        }
        return textLoader;
    }
}
