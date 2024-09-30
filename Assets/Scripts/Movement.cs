using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Movement : MonoBehaviour, IPunInstantiateMagicCallback
{
[SerializeField]
float speed = 10f;
[SerializeField]
float jumpVelocity = 7f;
[SerializeField]
GameObject leftLeg;
[SerializeField]
GameObject rightLeg;
[SerializeField]
LayerMask stageLayer;
Rigidbody2D rb;
float input;
float movement;
Animator anim;
CompositeCollider2D coll;
bool canDoubleJump;
Collider2D leftLegColl;
Collider2D rightLegColl;
PhotonView view;
bool isHit;
Vector2 hitDir;
WeaponSetup weaponStats;
float weaponId;
bool attacking = false;

public void OnPhotonInstantiate(PhotonMessageInfo info) {
  object[] instantiationData = info.photonView.InstantiationData;
  Debug.Log((int) instantiationData[0]);
  if ((int) instantiationData[0] == 1) {
    transform.localScale = new Vector3(-1*Mathf.Abs(transform.localScale.x),transform.localScale.y,transform.localScale.z);
  }
}

void OnBecameInvisible() {
  if (view.IsMine) {
        PhotonNetwork.Destroy(view);
        Debug.Log("destroyed");
  }
}

void Awake() {
  view = GetComponent<PhotonView>();
  weaponId = (int) view.Owner.CustomProperties["weaponId"];
  var weapons = GetComponentsInChildren<WeaponSetup>(true);
  foreach (WeaponSetup w in weapons) {
    if (w.getId() == weaponId) {
      w.gameObject.SetActive(true);
      weaponStats = w;
      break;
    }
  }
}

// Start is called before the first frame update
void Start()
{
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<CompositeCollider2D>();
        leftLegColl = leftLeg.GetComponent<Collider2D>();
        rightLegColl = rightLeg.GetComponent<Collider2D>();
        speed *= weaponStats.speed;
        jumpVelocity *= weaponStats.jump;
}

void OnTriggerEnter2D(Collider2D other) {
  if (other.tag == "Weapon") {
    isHit = true;
    hitDir = transform.root.position - other.transform.root.position;
    hitDir = new Vector2(Mathf.Sign(hitDir.x) * 100, 100);
  }
}

// Update is called once per frame
void Update()
{
 if (view.IsMine) {
   // jump
   if (attacking) {
     attacking = false;
     anim.SetBool("Attacking", false);
   }

   if (IsGrounded()) {
           anim.SetBool("Jumping", false);
   } else {
           anim.SetBool("Jumping", true);
   }

   if (Input.GetKeyDown(KeyCode.Space)) {
           if (IsGrounded()) {
                   rb.velocity = new Vector2(rb.velocity.x, jumpVelocity);
                   anim.SetBool("Jumping", true);
                   canDoubleJump = true;
           } else if (canDoubleJump) {
                   canDoubleJump = false;
                   rb.velocity = new Vector2(rb.velocity.x, jumpVelocity);
           }
   }
   if (Input.GetMouseButtonDown(0) && !attacking) {
           attacking = true;
           anim.SetBool("Attacking", true);
   }
 }
}

void FixedUpdate()
{
  if (view.IsMine) {

          if (isHit) {
            rb.velocity = hitDir;
          } else {
            rb.velocity = new Vector2(movement, rb.velocity.y);
          }

          // if falling increase gravity
          if (rb.velocity.y < 0) {
                  rb.gravityScale = 6;
          } else {
                  rb.gravityScale = 2;
          }

          // x movement
          input = Input.GetAxis("Horizontal");
          anim.SetBool("Moving", rb.velocity.x != 0 && !isHit);
          if (input < 0) {
                  transform.localScale = new Vector3(-1*Mathf.Abs(transform.localScale.x),transform.localScale.y,transform.localScale.z);
          } else if (input > 0 ) {
                  transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x),transform.localScale.y,transform.localScale.z);
          }
          movement = input * speed;
  }
}


// check grounded with raycasts at both legs
private bool IsGrounded()
{
        RaycastHit2D raycastHitLeftLeg =  Physics2D.Raycast(leftLegColl.bounds.center, Vector2.down, leftLegColl.bounds.extents.y + 0.05f, stageLayer);
        RaycastHit2D raycastHitRightLeg =  Physics2D.Raycast(rightLegColl.bounds.center, Vector2.down, rightLegColl.bounds.extents.y + 0.05f, stageLayer);
        // Color leftRayColor;
        // Color rightRayColor;
        // if (raycastHitLeftLeg.collider != null) {
        //         leftRayColor = Color.green;
        // } else {
        //         leftRayColor = Color.red;
        // }
        // if (raycastHitRightLeg.collider != null) {
        //         rightRayColor = Color.green;
        // } else {
        //         rightRayColor = Color.red;
        // }
        // Debug.DrawRay(leftLegColl.bounds.center, Vector2.down * leftLegColl.bounds.extents.y, leftRayColor);
        // Debug.DrawRay(rightLegColl.bounds.center, Vector2.down * rightLegColl.bounds.extents.y, rightRayColor);
        return raycastHitLeftLeg.collider != null || raycastHitRightLeg.collider != null;
}


}
