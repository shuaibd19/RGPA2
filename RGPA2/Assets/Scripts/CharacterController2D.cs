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

    private bool grounded;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        //get the character input from a and d
        var moveInput = Input.GetAxisRaw("Horizontal");

        if (grounded)
        {
            velocity.y = 0;
            //checking for input for jumping
            if (Input.GetButtonDown("Jump"))
            {
                //y component of velocity when we jump
                velocity.y = Mathf.Sqrt(2 * jumpHeight * Mathf.Abs(Physics2D.gravity.y));
            }

        }

        //momentum forces
        //if the player is on the ground apply walk acceleration if he is airborne then the air acceleration
        float acceleration = grounded ? walkAcceleration : airAcceleration;
        //if the player is airborne decelerate him if not then dont decelerate
        float deceleration = grounded ? groundDeceleration : 0;

        //checking for player input
        if (moveInput != 0)
        {
            velocity.x = Mathf.MoveTowards(velocity.x, characterSpeed * moveInput, acceleration * Time.deltaTime);
        }
        else
        {
            velocity.x = Mathf.MoveTowards(velocity.x, 0, deceleration * Time.deltaTime);
        }

        //artificial gravity
        velocity.y += Physics2D.gravity.y * Time.deltaTime;

        //assign the x value of the velocity 
        transform.Translate(velocity * Time.deltaTime);

        grounded = false;

        //checking for collisions

        //assigning any overlapping objects to our objects position
        Collider2D[] hits = Physics2D.OverlapBoxAll(transform.position, boxCollider.size, 0);


        //iterating through the hits for box colliders
        foreach (var hit in hits)
        {
            //ignore our own box collider
            if (hit == boxCollider)
            {
                continue;
            }

            //difference in distance between collider overlaps
            ColliderDistance2D colliderDistance = hit.Distance(boxCollider);

            //if there is an overlap
            if (colliderDistance.isOverlapped)
            {
                //pushing our gameobject out of the collision
                transform.Translate(colliderDistance.pointA - colliderDistance.pointB);

                //check if we are on the ground
                if (Vector2.Angle(colliderDistance.normal, Vector2.up) < 90 && velocity.y < 0)
                {
                    grounded = true;
                }
            }
        }

       


    }
}
