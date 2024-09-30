using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    GameObject mainCamera;
    [SerializeField]
    GameObject weaponCamera;
    [SerializeField]
    GameObject loginCamera;
    [SerializeField]
    GameObject registerCamera;
    [SerializeField]
    GameObject shopCamera;
    [SerializeField]
    ListItem listItem;
    GameObject activeCamera;

    public void Awake() {
      activeCamera = loginCamera;
    }

    public void SetActiveMenu(string name) {
      switch(name) {
        case "main":
          activeCamera.SetActive(false);
          activeCamera = mainCamera;
          activeCamera.SetActive(true);
          break;
        case "weapon":
          activeCamera.SetActive(false);
          activeCamera = weaponCamera;
          activeCamera.SetActive(true);
          break;
        case "login":
          activeCamera.SetActive(false);
          activeCamera = loginCamera;
          activeCamera.SetActive(true);
          break;
        case "register":
          activeCamera.SetActive(false);
          activeCamera = registerCamera;
          activeCamera.SetActive(true);
          break;
        case "shop":
          activeCamera.SetActive(false);
          activeCamera = shopCamera;
          activeCamera.SetActive(true);
          break;
      }
    }
}
