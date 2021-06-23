using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LoadSceneInput : MonoBehaviour
{
    private void Update()
    {
        //if the user presses the escape button on the keyboard or the m button
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneLoading.instance.LoadSceneAsync("MainScene");
        }
    }
}
