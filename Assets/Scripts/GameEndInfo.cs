﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;

public class GameEndInfo : MonoBehaviour
{
    public static int Player1Score;
    public static int Player2Score;
    public static Player LocalPlayer;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        Debug.Log(LocalPlayer);
    }
}
