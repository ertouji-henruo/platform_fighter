using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class WeaponSetup : MonoBehaviour
{

    [SerializeField]
    WeaponStats weaponStats;
    [HideInInspector]
    public float size;
    [HideInInspector]
    public float attackSpeed;
    [HideInInspector]
    public float blockChance;
    [HideInInspector]
    public float abilityCDR;
    [HideInInspector]
    public float speed;
    [HideInInspector]
    public float jump;
    Animator anim;
    [SerializeField]
    float id;
    PhotonView view;

    void Awake()
    {
      anim = GetComponentInParent<Animator>();
      view = GetComponentInParent<PhotonView>();
        size = (float) view.Owner.CustomProperties["size"];
        attackSpeed = (float) view.Owner.CustomProperties["attackSpeed"];
        blockChance = (float) view.Owner.CustomProperties["blockChance"];
        speed = (float) view.Owner.CustomProperties["speed"];
        jump = (float) view.Owner.CustomProperties["jump"];
        abilityCDR = (float) view.Owner.CustomProperties["abilityCDR"];
      transform.localScale = new Vector3(size*transform.localScale.x, size*transform.localScale.y, 1);
      anim.SetFloat("AttackSpeed", attackSpeed);
    }

    public float getId()
    {
      return id;
    }

}
