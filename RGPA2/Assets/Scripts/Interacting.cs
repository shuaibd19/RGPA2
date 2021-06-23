using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//adds the ability to interact with interactable gameworld objects
[RequireComponent(typeof(NewCharacterContoller2D))]
public class Interacting : MonoBehaviour
{
    //the key to press to interact with an object
    [SerializeField] KeyCode interactionKey = KeyCode.F;

    //the range we can interact with objects
    [SerializeField] float interactingRange = 2;

    NewCharacterContoller2D direction;

    private void Start()
    {
        direction = GetComponent<NewCharacterContoller2D>();
    }

    private void Update()
    {
        //did the user press the interaction key?
        if (Input.GetKeyDown(interactionKey))
        {
            //attempt to interact
            AttemptInteraction();
        }
    }

    private void AttemptInteraction()
    {
        Vector2 startPosition = (Vector2)transform.position + new Vector2(0.5f, 0.2f);
        int layerMask = LayerMask.GetMask("Default");
        //Get the first object hit by the ray
        RaycastHit2D hit = Physics2D.Raycast(startPosition, Vector2.right, interactingRange, layerMask, 0);

        //If the collider of the object hit is not NUll
        if (hit.collider != null)
        {
            //Hit something, print the tag of the object
            Debug.Log("Hitting: " + hit.collider.tag);

            //try to get the interactable component on the object we hit
            var interactable = hit.collider.GetComponent<Interactable>();

            //null checking
            if (interactable != null)
            {
                //signal that it was interacted with
                interactable.Interact(this.gameObject);
            }

        }
    }
}
