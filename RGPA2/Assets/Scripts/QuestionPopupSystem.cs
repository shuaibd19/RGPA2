using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionPopupSystem : MonoBehaviour
{
    public Canvas questionStruct;//don't know what it actually is

    // Start is called before the first frame update
    void Start()
    {
        questionStruct.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreateQuestion(string question, Vector3 answers, int correctAnswer)
    {
        //fill in the prefab with this info
        //make it visible on screen
    }
}
