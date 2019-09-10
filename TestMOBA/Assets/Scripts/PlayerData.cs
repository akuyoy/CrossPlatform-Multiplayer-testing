using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Photon.Pun;

public class PlayerData : MonoBehaviour {

    public InputField nameField;
    string playerName = "player";

	void Start () {
		// load saved name here later
	}
	
    public void SetPlayerName()
    {
        if(string.IsNullOrEmpty(nameField.text))
        {
            Debug.LogError("Are you siccckkkk?");
            return;
        }
        PhotonNetwork.NickName = nameField.text;
    }
}
