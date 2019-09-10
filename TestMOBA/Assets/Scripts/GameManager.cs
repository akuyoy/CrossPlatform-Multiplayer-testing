using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class GameManager : MonoBehaviourPunCallbacks {

    public GameObject playerPrefab;

    void Start()
    {
        if (playerPrefab == null)
        {
            Debug.LogError("missing player prefab");
        }
        else
        {
            if (PlayerControl.LocalPlayerInstance == null)
            {
                PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(0f, 5f, 0f), Quaternion.identity, 0);
            }
            else
            {
                Debug.LogFormat("Already exist");
            }
        }
    }

    public override void OnLeftRoom()
    {
        SceneManager.LoadScene("Main");
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    void LoadLevel()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            Debug.LogError("not the master client");
        }
        Debug.LogFormat("Loading Level : {0}", PhotonNetwork.CurrentRoom.PlayerCount);
        PhotonNetwork.LoadLevel("GameScene");
    }

    #region Callbacks

    public override void OnPlayerEnteredRoom(Player other)
    {
        Debug.LogFormat(other.NickName + " enter room"); // not seen if you're the player connecting

        if (PhotonNetwork.IsMasterClient)
        {
            Debug.LogFormat("IsMasterClient : " + PhotonNetwork.IsMasterClient); // called before OnPlayerLeftRoom
            LoadLevel();
        }
    }


    public override void OnPlayerLeftRoom(Player other)
    {
        Debug.LogFormat(other.NickName + " leave room", other.NickName); // seen when other disconnects

        if (PhotonNetwork.IsMasterClient)
        {
            Debug.LogFormat("IsMasterClient : " + PhotonNetwork.IsMasterClient); // called before OnPlayerLeftRoom
            LoadLevel();
        }
    }

    #endregion
}
