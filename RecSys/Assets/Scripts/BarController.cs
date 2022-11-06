using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarController : MonoBehaviour
{
    private string text = "";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void SetText(string _text)
    {
        text = _text;

        TMPro.TextMeshPro textMeshPro = gameObject.transform.Find("Text").GetComponent<TMPro.TextMeshPro>();
        textMeshPro.text = text;
        textMeshPro.color = Color.white;
    }

    public void SetBarLength(float _ratio)
    {
        GameObject fullBar = gameObject.transform.Find("FullBar").gameObject;
        GameObject emptyBar = gameObject.transform.Find("EmptyBar").gameObject;

        fullBar.transform.localScale = new Vector3(0.6f * _ratio, 0.05f, 0.001f);
        fullBar.transform.localPosition = new Vector3(0.6f * _ratio/2 - 0.4f, fullBar.transform.localPosition.y, fullBar.transform.localPosition.z);
        emptyBar.transform.localScale = new Vector3(0.6f * (1 - _ratio), 0.05f, 0.001f);
        emptyBar.transform.localPosition = fullBar.transform.localPosition + new Vector3(0.6f * _ratio / 2, 0, 0) + new Vector3(0.6f * (1 - _ratio) / 2, 0, 0);
    }
}
