using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script used to control character movement in 2d side scroller perspective
/// </summary>
/// 

//this script requires a box collider 2d to work
[RequireComponent(typeof(BoxCollider2D))]
public class CharacterController2D : MonoBehaviour
{
    [SerializeField, Tooltip("Max speed, in units per second the character moves in")]
    float characterSpeed = 9f;

    [SerializeField, Tooltip("Acceleration while grounded")]
    float walkAcceleration = 75f;

    [SerializeField, Tooltip("Acceleration while in the air")]
    float airAcceleration = 30f;

    [SerializeField, Tooltip("Deceleration applied when character is grounded and not attempting to jump")]
    float groundDeceleration = 70f;

    [SerializeField, Tooltip("Max height character will jump regardless of gravity")]
    float jumpHeight = 4f;

    BoxCollider2D boxCollider;

    Vector2 velocity;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        //get the character input from a and d
        var moveInput = Input.GetAxisRaw("Horizontal");

        //checking for player input
        if (moveInput != 0)
        {
            velocity.x = Mathf.MoveTowards(velocity.x, characterSpeed * moveInput, walkAcceleration * Time.deltaTime);
        }
        else
        {
            velocity.x = Mathf.MoveTowards(velocity.x, 0, groundDeceleration * Time.deltaTime);
        }

        //assign the x value of the velocity 
        transform.Translate(velocity * Time.deltaTime);
    }
}
