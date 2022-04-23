using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIControl : MonoBehaviour
{
    public bool SelectStart = true;
    public bool SelectQuit = false;

    public bool isBegin = false;

    //public GameObject title;
    //public GameObject start;
    //public GameObject quit;
    // Start is called before the first frame update
    void Start()
    {
        //if (this.name == "title") {
        //    title = this.gameObject;
        //}
        //if (this.name == "title")
        //{
        //    title = this.gameObject;
        //}
        //if (this.name == "title")
        //{
        //    title = this.gameObject;
        //}
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow)) {
            if (SelectStart && !SelectQuit) {
                SelectStart = false;
                SelectQuit = true;
            }
            else if (!SelectStart && SelectQuit) {
                SelectStart = true;
                SelectQuit = false;
            }
        }

        if (this.name == "Start" && SelectStart && !SelectQuit) {
            this.GetComponent<Text>().fontStyle = FontStyle.BoldAndItalic;
            GameObject.Find("Quit").GetComponent<Text>().fontStyle = FontStyle.Normal;
        }

        if (this.name == "Quit" && !SelectStart && SelectQuit)
        {
            this.GetComponent<Text>().fontStyle = FontStyle.BoldAndItalic;
            GameObject.Find("Start").GetComponent<Text>().fontStyle = FontStyle.Normal;

        }

        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space)) {
            if (!SelectQuit && SelectStart) {
                GameObject.Find("hero").GetComponent<CharacterController>().isBegin = true;
                AudioHub.Instance.StopSound();
                AudioHub.Instance.PlaySound("01-main-theme-overworld");
            }
            else if (SelectQuit && !SelectStart) {
                Application.Quit();
            }
        }

    }
}
