using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//new version of the 2d character controller script
[RequireComponent(typeof(CapsuleCollider2D))]
public class NewCharacterContoller2D : MonoBehaviour
{
    [SerializeField] float moveSpeed = 6;
    [SerializeField] float jumpHeight = 2;
    [SerializeField] float gravity = 20;
    public bool facingRight;

    [Range(0, 10), SerializeField] float airControl = 5;

    Vector2 moveDirection = Vector2.zero;

    CapsuleCollider2D capsuleCollider2D;

    bool grounded;

    private void Start()
    {
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
    }

    private void FixedUpdate()
    {
        var input = new Vector2(Input.GetAxis("Horizontal"), 0f);

        input *= moveSpeed;

        input = transform.TransformDirection(input);

        CheckForBottomCollision();

        if (grounded)
        {
            moveDirection = input;

            if (Input.GetButton("Jump"))
            {
                moveDirection.y = Mathf.Sqrt(2 * gravity * jumpHeight);
            }
            else
            {
                moveDirection.y = 0;
            }
        }
        else
        {
            input.y = moveDirection.y;
            moveDirection = Vector2.Lerp(moveDirection, input, airControl * Time.deltaTime);
        }

        moveDirection.y -= gravity * Time.deltaTime;

        transform.Translate(moveDirection * Time.deltaTime);

        //checking for look direction
        if (moveDirection.x > 0 && !facingRight)
        {
            facingRight = true;
            Debug.Log("Right");
        }
        else if (moveDirection.x < 0 && facingRight)
        {
            facingRight = false;
            Debug.Log("Left");
        }
    }

    private void CheckForBottomCollision()
    {
        Bounds colliderBounds = capsuleCollider2D.bounds;
        float colliderRadius = capsuleCollider2D.size.x * 0.4f * Mathf.Abs(transform.localScale.x);
        Vector3 groundPosCheck = colliderBounds.min + new Vector3(colliderBounds.size.x * 0.5f, colliderRadius * 0.9f, 0);

        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundPosCheck, colliderRadius);
        grounded = false;
        if (colliders.Length > 0)
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i] != capsuleCollider2D)
                {
                    grounded = true;
                    break;
                }
            }
        }
    }
}
