using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{

    Collider2D hitbox;
    [SerializeField]
    bool hitboxActive = false;
    // Start is called before the first frame update
    void Start()
    {
      var hitboxes = GetComponentsInChildren<Collider2D>();
      foreach (Collider2D coll in hitboxes) {
        if (coll.gameObject.activeSelf) {
          hitbox = coll;
        }
      }
    }

    // Update is called once per frame
    void Update()
    {
      if (hitboxActive) {
        hitbox.enabled = true;
      } else {
        hitbox.enabled = false;
      }
    }
}
