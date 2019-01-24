using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

  private Rigidbody2D rigidbody;
  private Animator anim;
  private float dirX;
  private bool isHurting, isDead;
  private bool isFacingRight = true;
  private Vector3 localScale;

  [SerializeField]
  private float walkingSpeed = 5f;
  [SerializeField]
  private float runningSpeed = 10f;
  private float moveSpeed;
  //[SerializeField]
  //private int healthPoints = 3;

  void Start() {
    rigidbody = GetComponent<Rigidbody2D>();
    anim = GetComponent<Animator>();
    moveSpeed = walkingSpeed;
  }

  void Update() {

    updateJumpState();
    updateRunningState();

    SetAnimationState();
    SetAudioState();

    if (!isDead) {
      dirX = Input.GetAxisRaw("Horizontal") * moveSpeed;
    }
  }

  void FixedUpdate() {
    //if (!isHurting)
    rigidbody.velocity = new Vector2(dirX, rigidbody.velocity.y);
  }

  void LateUpdate() {
    CheckWhereToFace();
  }

  void updateJumpState() {
    if (Input.GetButtonDown("Jump") && !isDead && isGrouded()) {
      rigidbody.AddForce(Vector2.up * 600f);
    }
  }

  void updateRunningState() {
    if (Input.GetKey(KeyCode.LeftShift)) {
      moveSpeed = runningSpeed;
    } else {
      moveSpeed = walkingSpeed;
    }
  }

  void SetAnimationState() {
    if (isIdle()) {
      anim.SetBool("isWalking", false);
      anim.SetBool("isRunning", false);
    }

    if (isGrouded()) {
      anim.SetBool("isJumping", false);
      anim.SetBool("isFalling", false);
    }

    if (isWalking()) {
      anim.SetBool("isWalking", true);
    }

    if (isRunning()) {
      anim.SetBool("isRunning", true);
    } else {
      anim.SetBool("isRunning", false);
    }

    if (isJumping()) {
      anim.SetBool("isJumping", true);
    }

    //if (Input.GetKey(KeyCode.DownArrow) && isRunningSpeed()) {
    //  anim.SetBool("isSliding", true);
    //} else {
    //  anim.SetBool("isSliding", false);
    //}

    //if (isFalling()) {
    //  anim.SetBool("isJumping", false);
    //  anim.SetBool("isFalling", true);
    //}
  }

  void SetAudioState() {

  }

  bool isIdle() {
    return dirX == 0;
  }

  bool isGrouded() {
    return rigidbody.velocity.y == 0;
  }

  bool isJumping() {
    return rigidbody.velocity.y > 0;
  }
  //bool isFalling() {
  //  return rigidbody.velocity.y < 0;
  //}

  bool isRunningSpeed() {
    return Mathf.Abs(dirX) == 10;
  }

  bool isWalkingSpeed() {
    return Mathf.Abs(dirX) == 5;
  }

  bool isRunning() {
    return isRunningSpeed() && isGrouded();
  }

  bool isWalking() {
    return isWalkingSpeed() && isGrouded();
  }

  void CheckWhereToFace() {
    if (dirX > 0) {
      isFacingRight = true;
    } else if (dirX < 0) {
      isFacingRight = false;
    }

    Vector3 localScale = transform.localScale;
    if (((isFacingRight) && (localScale.x < 0)) || ((!isFacingRight) && (localScale.x > 0))) {
      localScale.x *= -1;
    }

    transform.localScale = localScale;

  }

  //void OnTriggerEnter2D(Collider2D col) {
  //  if (col.gameObject.name.Equals("Fire")) {
  //    healthPoints -= 1;
  //  }

  //  if (col.gameObject.name.Equals("Fire") && healthPoints > 0) {
  //    anim.SetTrigger("isHurting");
  //    StartCoroutine("Hurt");
  //  } else {
  //    dirX = 0;
  //    isDead = true;
  //    anim.SetTrigger("isDead");
  //  }
  //}

  //IEnumerator Hurt() {
  //  isHurting = true;
  //  rigidbody.velocity = Vector2.zero;

  //  if (facingRight) {
  //    rigidbody.AddForce(new Vector2(-200f, 200f));
  //  } else {
  //    rigidbody.AddForce(new Vector2(200f, 200f));
  //  }
  //  yield return new WaitForSeconds(0.5f);

  //  isHurting = false;
  //}

}
