﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Photon.Pun;

public class UIDrop2World : MonoBehaviour, IDropHandler
{
    //THIS SCRIPT IS ATTACHED TO THE JEWEL (JOULE) UI OBJECT (used for unbonding)

    //  Variable Definitions
    private Vector3 prefabWorldPosition; // Position that the prefab spawns in.
    public GameObject PrefabToSpawn;

    public void OnDrop(PointerEventData eventData)
    {

        RectTransform panel = transform as RectTransform;

        //the point where the particle should be spawned
        prefabWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y));
        prefabWorldPosition.z = 0;

        PhotonNetwork.Instantiate(PrefabToSpawn.name, prefabWorldPosition, Quaternion.identity);
        UnbondingScript2.WaitABit = 8;  //this makes the unbonding Joule remain on screen for 8 Updates (about 0.16 sec)
    }

}