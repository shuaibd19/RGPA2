using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// implements the ability to be interacted with
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class Interactable : MonoBehaviour
{
    public void Interact(GameObject fromObject)
    {
        Debug.LogFormat("I've been interacted with by {0}", fromObject);

        if (gameObject.tag == "npc")
        {
            var npc = GetComponent<NPCInteract>();
            if (npc != null)
            {
                //if the menu is currently false
                if (!SceneLoading.instance.studentScene)
                {
                    //load the menu scene
                    SceneLoading.instance.LoadSceneAsync("StudentScene");
                    //set the menu to being true
                    SceneLoading.instance.setScene();
                }
            }
        }
    }
}
