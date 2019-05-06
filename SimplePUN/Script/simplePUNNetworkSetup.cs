using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class simplePUNNetworkSetup : MonoBehaviourPunCallbacks
{
    public string playerPrefab;
    GameObject menuGUI;
   
    TMP_InputField friendRoom;
  
    TMP_InputField playerName;
    Photon.Realtime.RoomOptions ro;
    
    simplePUNSpawnPoint[] spawnList;
    public enum gameMode:uint
    {
        quickPlay = 0,
        playWithFriend = 1
    };
    gameMode gMode;
    // Start is called before the first frame update

    void Start()
    {
        menuGUI = transform.Find("Panel").gameObject;
        
        playerName = menuGUI.transform.Find("Nickname").GetComponent<TMP_InputField>();
        friendRoom = menuGUI.transform.Find("FriendParnel/FriendRoom").GetComponent<TMP_InputField>();

        ro = new Photon.Realtime.RoomOptions();
        ro.MaxPlayers = 2;
        
        spawnList = FindObjectsOfType<simplePUNSpawnPoint>();
    }

    public void startQuickGame()
    {
        gMode = gameMode.quickPlay;
        PhotonNetwork.ConnectUsingSettings();
        
    }

    public void startPlayWithFriend()
    {
        gMode = gameMode.playWithFriend;
        PhotonNetwork.ConnectUsingSettings();
    }

    
    #region callbackEvent
    // ========== Photon network callback method ==========
    public override void OnConnected()
    {
        Debug.Log("Connected");
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to master");

        if(gMode == gameMode.quickPlay)
        {
            Debug.Log("Start join random game");
            PhotonNetwork.JoinRandomRoom();
        }else if(gMode == gameMode.playWithFriend)
        {
            // create game with friend code
            PhotonNetwork.JoinOrCreateRoom("fri_" + friendRoom.text,ro,null,null);
        }
    }

    public override void OnDisconnected(Photon.Realtime.DisconnectCause cause)
    {
        Debug.Log("Disconnected");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to join random room");
        Debug.Log("Create new room");
        PhotonNetwork.CreateRoom("ran_" + Time.time.ToString(), ro,null,null );
    }
    public override void OnCreatedRoom()
    {
        Debug.Log("Created room");
    }
    public override void OnJoinedRoom()
    {
        Debug.Log("Joined room");
        Debug.Log("Start Game");
        menuGUI.SetActive(false);

        //spawn the player
        simplePUNSpawnPoint spspp = spawnList[Random.Range(0,spawnList.Length)];
        while(spspp.HaveOwner)
        {
            spspp = spawnList[Random.Range(0,spawnList.Length)];
        }
        Transform spp = spspp.getPoint();

        //instantiate character
        GameObject playerObj= PhotonNetwork.Instantiate(playerPrefab,spp.position,Quaternion.identity);
        playerObj.GetComponent<simplePUNCharacterNetwork>().charName = playerName.text;

    }
    #endregion
}
