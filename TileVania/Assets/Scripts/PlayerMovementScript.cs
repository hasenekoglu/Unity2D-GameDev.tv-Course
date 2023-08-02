using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementScript : MonoBehaviour
{
  [SerializeField] float runSpeed = 10f;
  [SerializeField] float jumpSpeed = 5f;
  [SerializeField] float climbSpeed = 5;

  Vector2 moveInput;
  Rigidbody2D myRigidbody;
  Animator myAnimator;
  bool playerHasHorizontalSpeed;

  CapsuleCollider2D myCapsuleCollider;


  void Start()
  {
    myRigidbody = GetComponent<Rigidbody2D>();
    myAnimator = GetComponent<Animator>();
    myCapsuleCollider = GetComponent<CapsuleCollider2D>();
  }


  void Update()
  {
    Run();
    FlipSprite();
    ClimbLadder();
  }

  private void FlipSprite()
  {
    playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;

    if (playerHasHorizontalSpeed)
    {
      transform.localScale = new Vector2(Mathf.Sign(myRigidbody.velocity.x), 1f);
    }


  }

  void OnMove(InputValue value)
  {
    moveInput = value.Get<Vector2>();


  }
  void OnJump(InputValue value)
  {
    if (!myCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) { return; }
    if (value.isPressed)
    {

      myRigidbody.velocity += new Vector2(0f, jumpSpeed);
    }

  }
  void Run()
  {
    Vector2 playerVelocity = new Vector2(moveInput.x * runSpeed, myRigidbody.velocity.y);

    myRigidbody.velocity = playerVelocity;

    playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;

    myAnimator.SetBool("isRunning", playerHasHorizontalSpeed);

  }
  void ClimbLadder()
  {
    if (!myCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Climbing"))) { return; }
    Vector2 climbVelocity = new Vector2(myRigidbody.velocity.x, moveInput.y * climbSpeed);
    myRigidbody.velocity = climbVelocity;
    bool playerHasClimbingSpeed = Mathf.Abs(myRigidbody.velocity.y) > Mathf.Epsilon;
    myAnimator.SetBool("isClimbing", playerHasClimbingSpeed);

  }
}
