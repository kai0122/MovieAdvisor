using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeTextColor(string _reason)
    {
        List<string> reasons = new List<string>(){"Movie", "Actor", "Genre"};
        foreach(string reason in reasons)
        {
            if (reason == _reason)
            {
                TMPro.TextMeshPro textMeshPro = gameObject.transform.Find(reason).GetComponent<TMPro.TextMeshPro>();
                textMeshPro.color = Color.blue;
            }
            else
            {
                TMPro.TextMeshPro textMeshPro = gameObject.transform.Find(reason).GetComponent<TMPro.TextMeshPro>();
                textMeshPro.color = Color.black;
            }
        }
        
    }

    public void ChangeIconPosition(string reason)
    {
        GameObject checkIcon = gameObject.transform.Find("CheckIcon").gameObject;

        if (reason == "Actor")
        {
            checkIcon.transform.localPosition = new Vector3(-1.9f, 0.01f, 0.28f);
        }
        else if(reason == "Genre")
        {
            checkIcon.transform.localPosition = new Vector3(-1.9f, 0.01f, -0.25f);
        }
        else
        {
            checkIcon.transform.localPosition = new Vector3(-1.9f, 0.01f, -0.81f);
        }
    }
}
