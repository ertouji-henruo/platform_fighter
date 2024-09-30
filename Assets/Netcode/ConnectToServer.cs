using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class ConnectToServer : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
      PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster() {
      PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby() {
      ExitGames.Client.Photon.Hashtable weapon = new ExitGames.Client.Photon.Hashtable();
      weapon.Add("weaponId", PlayerPrefs.GetInt("weaponId"));
      weapon.Add("size", PlayerPrefs.GetFloat("size"));
      weapon.Add("attackSpeed", PlayerPrefs.GetFloat("attackSpeed"));
      weapon.Add("blockChance", PlayerPrefs.GetFloat("blockChance"));
      weapon.Add("speed", PlayerPrefs.GetFloat("speed"));
      weapon.Add("jump", PlayerPrefs.GetFloat("jump"));
      weapon.Add("abilityCDR", PlayerPrefs.GetFloat("abilityCDR"));
      PhotonNetwork.LocalPlayer.SetCustomProperties(weapon);
      SceneManager.LoadScene("Lobby");
    }
}
