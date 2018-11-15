using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetManager : Photon.MonoBehaviour
{

    public const string verNum = "1.0";
    public Transform[] spawnPoints;
    public string roomName = "room01";
    public string playerName = "player 420";
    public bool isConnected = false;
    public bool isInRoom = false;
    public GameObject avatarprefab;
    public int value2 = 40;



    void Start()
    {


        roomName = "Room " + Random.Range(0, 999);
        playerName = "Player " + Random.Range(0, 999);
        PhotonNetwork.ConnectUsingSettings(verNum);
        Debug.Log("Starting Connection!");




    }

    public void OnJoinedLobby()
    {
        isConnected = true;
        Debug.Log("Starting Server!");

    }

    public void OnJoinedRoom()
    {
        PhotonNetwork.playerName = playerName;

        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject pl in players)
        {
            if (pl.name == PhotonNetwork.playerName)
            {
                PhotonNetwork.playerName = playerName;
            }
        }


        isConnected = false;
        isInRoom = true;
        spawnPlayer(avatarprefab.name);
    }
    public void spawnPlayer(string prefName)
    {
        isInRoom = false;
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        Debug.Log("Spawning Player" + avatarprefab.name);
        foreach (PhotonPlayer pl2 in PhotonNetwork.playerList)
        {
            if (pl2.name == PhotonNetwork.playerName)
            {
                PhotonNetwork.playerName = playerName;
            }
        }
        GameObject o = PhotonNetwork.Instantiate(prefName, spawnPoints[PhotonNetwork.otherPlayers.Length].position, Quaternion.identity, 0);
        Debug.Log("Spawned " + playerName);

        o.GetComponent<CopyScript>().enabled = true;
        o.GetComponent<CopyScript>().camera.SetActive(true);


    }
    void OnGUI()
    {

        if (isConnected)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            GUI.skin.label.fontSize = GUI.skin.box.fontSize = GUI.skin.button.fontSize = value2;
            GUILayout.BeginArea(new Rect(Screen.width / 2 - 250, Screen.height / 2 - 250, 500, 500));
            playerName = GUILayout.TextField(playerName);
            roomName = GUILayout.TextField(roomName);

            if (GUILayout.Button("Create"))
            {
                PhotonNetwork.JoinOrCreateRoom(roomName, null, null);
                Debug.Log("roomName " + roomName);
            }

            foreach (RoomInfo game in PhotonNetwork.GetRoomList())
            {
                if (GUILayout.Button(game.name + " " + game.playerCount + "/" + game.maxPlayers))
                {
                    PhotonNetwork.JoinOrCreateRoom(game.name, null, null);
                }
            }
            GUILayout.EndArea();
        }

        if (isInRoom)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            GUILayout.BeginArea(new Rect(Screen.width / 2 - 250, Screen.height / 2 - 250, 500, 500));



     
            if (GUILayout.Button("Disconnect"))
            {
                PhotonNetwork.Disconnect();
                Application.LoadLevel(0);
            }

            GUILayout.EndArea();
        }
    }

}
