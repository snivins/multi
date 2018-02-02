﻿using UnityEngine;
using Prototype.NetworkLobby;
using System.Collections;
using UnityEngine.Networking;

public class NetworkLobbyHook : LobbyHook 
{
	public GameObject minion;
    public override void OnLobbyServerSceneLoadedForPlayer(NetworkManager manager, GameObject lobbyPlayer, GameObject player)
    {
        LobbyPlayer lobby = lobbyPlayer.GetComponent<LobbyPlayer>();
		PlayerControllerbeta pl = player.GetComponent<PlayerControllerbeta>();
		pl.colorino = lobby.playerColor;

		/*
        NetworkSpaceship spaceship = gamePlayer.GetComponent<NetworkSpaceship>();

        spaceship.name = lobby.name;
        spaceship.color = lobby.playerColor;
        spaceship.score = 0;
        spaceship.lifeCount = 3;*/
    }
}
