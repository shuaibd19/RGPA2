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
    [SerializeField] float interactingRange = 1;

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
        Vector2 startPositionR = (Vector2)transform.position + new Vector2(0.5f, 0.2f);
        Vector2 startPositionL = (Vector2)transform.position + new Vector2(-0.5f, 0.2f);


        //create a layer mask that represents every layer except the players
        var everythingExceptPlayers = ~(1 << LayerMask.NameToLayer("Player"));

        //filtering the layers
        var layerMask = Physics.DefaultRaycastLayers & everythingExceptPlayers;


        RaycastHit2D hit;

        if (direction.facingRight)
        {
            hit = Physics2D.Raycast(startPositionR, Vector2.right, interactingRange, layerMask, 0);
        }
        else
        {
            hit = Physics2D.Raycast(startPositionL, Vector2.left, interactingRange, layerMask, 0);
        }

        //If the collider of the object hit is not NUll
        if (hit.collider != null)
        {
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
