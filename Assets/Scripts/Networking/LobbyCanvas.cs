﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyCanvas : MonoBehaviour {
    //Finished
    [SerializeField]
    private RoomLayoutGroup _roomLayoutGroup;
    public RoomLayoutGroup RoomLayoutGroup
    {
        get { return _roomLayoutGroup; }
    }

    public void OnClickJoinRoom(string roomName)
    {
        if (PhotonNetwork.JoinRoom(roomName))
        {

        } else {
            print("Join room failed.");
        }
    }
}
