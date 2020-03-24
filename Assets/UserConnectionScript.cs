﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

#pragma warning disable CS0618

public class UserConnectionScript : NetworkBehaviour
{
    public string Username = "no name yet";
    public GameObject TestBoxPrefab;

    // Start is called before the first frame update
    void Start()
    {
        if (isLocalPlayer == false)
        {
            return;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (isLocalPlayer == false)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            CmdSpawnABox();
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            CmdUpdateUsername("Joe");
        }
    }

    //executes on server
    [Command]
    void CmdSpawnABox()
    {
        GameObject Box = Instantiate(TestBoxPrefab);
        NetworkServer.SpawnWithClientAuthority(Box, connectionToClient);
    }

    [Command]
    void CmdUpdateUsername(string n)
    {
        Username = n;
        RpcUpdateUsername(n);
    }

    //executes on everyone's computer
    [ClientRpc]
    void RpcUpdateUsername(string n)
    {
        Username = n;
    }
}
