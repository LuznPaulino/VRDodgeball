using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

[System.Serializable]
public class GameRoomDescription
{
    public string RoomName;
    public int maxNumPlayers;
    public int sceneIndex;
}

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public List<GameRoomDescription> roomDescList;
    private GameObject playerPrefab;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        PhotonNetwork.ConnectUsingSettings();
        Debug.Log("Connecting to the PUN server now...");
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        Debug.Log("Already connected to the server...");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        Debug.Log("Joined the lobby");
    }


    public void SetupRoom(int roomIndex)
    {
        PhotonNetwork.AutomaticallySyncScene = false;
        Debug.Log("Joining room #" + roomIndex);

        PhotonNetwork.LoadLevel(roomDescList[roomIndex].sceneIndex);
 
        RoomOptions options = new RoomOptions();
        options.MaxPlayers = (byte) roomDescList[roomIndex].maxNumPlayers;
        options.IsVisible = true;
        options.IsOpen = true;

        PhotonNetwork.JoinOrCreateRoom(roomDescList[roomIndex].RoomName, options, TypedLobby.Default);
    }


    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Debug.Log("Joined a room");
        playerPrefab = PhotonNetwork.Instantiate("Network Player", new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), transform.rotation);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        Debug.Log("Another player just joined the room");
    }

    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        PhotonNetwork.Destroy(playerPrefab);
    }
}
