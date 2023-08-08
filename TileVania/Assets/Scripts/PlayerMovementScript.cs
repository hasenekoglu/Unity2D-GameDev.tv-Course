using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerMovementScript : MonoBehaviour
{
  [SerializeField] float runSpeed = 10f;
  [SerializeField] float jumpSpeed = 5f;
  [SerializeField] float climbSpeed = 5;
  [SerializeField] GameObject bullet;
  [SerializeField] Transform gun;
  [SerializeField] float gameSoundVolume = .4f;

  [SerializeField] AudioClip bulletSFX;
  [SerializeField] AudioClip dieSFX;
  [SerializeField] AudioSource gameSound;


  Vector2 moveInput;
  Rigidbody2D myRigidbody;
  Animator myAnimator;
  CapsuleCollider2D myBodyCollider;
  BoxCollider2D myFeetCollider;

  float gravityScaleAtStart;
  bool playerHasHorizontalSpeed;
  bool isAlive = true;


  void Start()
  {
    gameSound = GetComponent<AudioSource>();

    gameSound.Play();
    gameSound.volume = gameSoundVolume;
    myRigidbody = GetComponent<Rigidbody2D>();
    myAnimator = GetComponent<Animator>();
    myBodyCollider = GetComponent<CapsuleCollider2D>();
    myFeetCollider = GetComponent<BoxCollider2D>();
    gravityScaleAtStart = myRigidbody.gravityScale;


  }


  void Update()
  {


    if (!isAlive) { return; }
    Run();
    FlipSprite();
    ClimbLadder();
    Die();
  }

  void OnFire(InputValue value)
  {
    if (!isAlive) { return; }
    GetComponent<AudioSource>().PlayOneShot(bulletSFX);


    Instantiate(bullet, gun.position, transform.rotation);
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
    if (!isAlive) { return; }
    moveInput = value.Get<Vector2>();



  }
  void OnJump(InputValue value)
  {
    if (!isAlive) { return; }
    if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) { return; }
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
    if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
    {
      myRigidbody.gravityScale = gravityScaleAtStart;
      myAnimator.SetBool("isClimbing", false);
      return;
    }

    Vector2 climbVelocity = new Vector2(myRigidbody.velocity.x, moveInput.y * climbSpeed);
    myRigidbody.velocity = climbVelocity;
    myRigidbody.gravityScale = 0;

    bool playerHasClimbingSpeed = Mathf.Abs(myRigidbody.velocity.y) > Mathf.Epsilon;
    myAnimator.SetBool("isClimbing", playerHasClimbingSpeed);


  }
  void Die()
  {
    if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemies", "Water")))
    {
      GetComponent<AudioSource>().PlayOneShot(dieSFX);
      isAlive = false;
      myAnimator.SetTrigger("Dying");
      myRigidbody.velocity += new Vector2(0f, jumpSpeed);
      FindObjectOfType<GameSession>().ProcessPlayerDeath();

    }
  }





}
