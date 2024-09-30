using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon;

public class SpawnPlayers : MonoBehaviour
{

    public GameObject playerPrefab;
    Vector2[] spawns = {new Vector2(-6.6f, 0f), new Vector2(6.4f, 0f)};

    // Start is called before the first frame update
    void Start()
    {
        int players = PhotonNetwork.PlayerList.Length - 1;
        object[] customData = {players};
        PhotonNetwork.Instantiate(playerPrefab.name, spawns[PhotonNetwork.PlayerList.Length - 1], Quaternion.identity, 0, customData);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
