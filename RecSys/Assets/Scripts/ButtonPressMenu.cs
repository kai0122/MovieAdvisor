using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPressMenu : MonoBehaviour
{
    public GameObject item1;
    public GameObject item2;
    public GameObject item3;
    public string foodName = "";
    public bool menuIsOpen = false;
    private float locationDistance = 0.05f;
    private float locationDestination = 1.05f;
    public bool movingOpen = false;
    public bool movingClose = false;

    public ButtonPressMenu anotherFood;

    private Touch touch;

    // Start is called before the first frame update
    void Start()
    {
        item1.SetActive(false);
        item2.SetActive(false);
        item3.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit, 100.0f))
                {
                    //explanationController.ShowNextMovieForSelection();
#pragma warning disable CS0618 // 類型或成員已經過時
                    if (hit.transform.gameObject.GetComponent<ButtonPressMenu>().foodName == gameObject.GetComponent<ButtonPressMenu>().foodName)
                    {
#pragma warning restore
                        if (hit.transform.gameObject.GetComponent<ButtonPressMenu>().menuIsOpen)
                        {
                            // close both menu
                            hit.transform.gameObject.GetComponent<ButtonPressMenu>().movingClose = true;
                            anotherFood.menuIsOpen = false;
                            anotherFood.item1.SetActive(false);
                            anotherFood.item2.SetActive(false);
                            anotherFood.item3.SetActive(false);
                            anotherFood.item1.transform.localPosition = new Vector3(0, 0, 0);
                            anotherFood.item2.transform.localPosition = new Vector3(0, 0, 0);
                            anotherFood.item3.transform.localPosition = new Vector3(0, 0, 0);
                        }
                        else if (anotherFood.menuIsOpen)
                        {
                            // change food expression
                            hit.transform.gameObject.GetComponent<ButtonPressMenu>().menuIsOpen = true;
                            hit.transform.gameObject.GetComponent<ButtonPressMenu>().item1.SetActive(true);
                            hit.transform.gameObject.GetComponent<ButtonPressMenu>().item2.SetActive(true);
                            hit.transform.gameObject.GetComponent<ButtonPressMenu>().item3.SetActive(true);
                            hit.transform.gameObject.GetComponent<ButtonPressMenu>().item1.transform.localPosition = new Vector3(-locationDestination, 0, 0);
                            hit.transform.gameObject.GetComponent<ButtonPressMenu>().item2.transform.localPosition = new Vector3(0, 0, locationDestination);
                            hit.transform.gameObject.GetComponent<ButtonPressMenu>().item3.transform.localPosition = new Vector3(locationDestination, 0, 0);
                            anotherFood.menuIsOpen = false;
                            anotherFood.item1.SetActive(false);
                            anotherFood.item2.SetActive(false);
                            anotherFood.item3.SetActive(false);
                        }
                        else
                        {
                            hit.transform.gameObject.GetComponent<ButtonPressMenu>().movingOpen = true;
                            hit.transform.gameObject.GetComponent<ButtonPressMenu>().item1.SetActive(true);
                            hit.transform.gameObject.GetComponent<ButtonPressMenu>().item2.SetActive(true);
                            hit.transform.gameObject.GetComponent<ButtonPressMenu>().item3.SetActive(true);
                        }
                    }
                }
            }

        }

        if (gameObject.GetComponent<ButtonPressMenu>().movingOpen)
        {
            gameObject.GetComponent<ButtonPressMenu>().openMenu();
        }
        else if (gameObject.GetComponent<ButtonPressMenu>().movingClose)
        {
            gameObject.GetComponent<ButtonPressMenu>().closeMenu();
        }
    }

    private void openMenu()
    {
        if (item1.transform.localPosition.x > -locationDestination)
        {
            item1.transform.localPosition += new Vector3(-locationDistance, 0, 0);
            item2.transform.localPosition += new Vector3(0, 0, locationDistance);
            item3.transform.localPosition += new Vector3(locationDistance, 0, 0);
        }
        else
        {
            gameObject.GetComponent<ButtonPressMenu>().movingOpen = false;
            gameObject.GetComponent<ButtonPressMenu>().menuIsOpen = true;
        }

    }

    private void closeMenu()
    {
        if (item1.transform.localPosition.x < 0)
        {
            item1.transform.localPosition += new Vector3(locationDistance, 0, 0);
            item2.transform.localPosition += new Vector3(0, 0, -locationDistance);
            item3.transform.localPosition += new Vector3(-locationDistance, 0, 0);
        }
        else
        {
            gameObject.GetComponent<ButtonPressMenu>().movingClose = false;
            gameObject.GetComponent<ButtonPressMenu>().menuIsOpen = false;
            gameObject.GetComponent<ButtonPressMenu>().item1.SetActive(false);
            gameObject.GetComponent<ButtonPressMenu>().item2.SetActive(false);
            gameObject.GetComponent<ButtonPressMenu>().item3.SetActive(false);
        }
    }
}
