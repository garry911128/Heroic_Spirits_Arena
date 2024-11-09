using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WelcomeSceneController : MonoBehaviour
{
    void Start()
    {
        GameManager.instance.ClearData();
    }

    void Update()
    {
        if (  !Input.GetMouseButton(0) && (Input.anyKeyDown && !Input.GetKeyDown(KeyCode.Tab)) )
        {
            Debug.Log("mouse click on welcome scene");
            GameManager.instance.StartNewMatch();
        }
    }
}

