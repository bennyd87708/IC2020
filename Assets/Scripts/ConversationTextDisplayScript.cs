﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class ConversationTextDisplayScript : MonoBehaviour, IPunObservable
{
    private Text ConversationTextBox;
    private bool final;
    private bool unbondingMessageAlreadyGiven;

    public static ConversationTextDisplayScript Instance { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        ConversationTextBox = GetComponent<Text>();
        final = false;
        unbondingMessageAlreadyGiven = false;

        ChangeUnbondingText.Instance.OnMouseClickUnbond += FirstTimeUnbonding;

        // Singleton design pattern
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void Denied()
    {
        ConversationTextBox.text = "You don't have enough Joules to break this bond!";
        StartCoroutine(countdown());
    }

    public void OutOfInventory()
    {
        ConversationTextBox.text = "These atoms are exhausted.  Roll again!";
        StartCoroutine(countdown());
    }

    // Called when no more of an element can be drawn (the user has the option to pick another)
    public void OutOfInventory2()
    {
        ConversationTextBox.text = "You can't have any more of that atom, choose another!";
        StartCoroutine(countdown());
    }

    public void OutOfInventory3()
    {
        ConversationTextBox.text = "You can't swap that atom, you alrady are at the Limit!";
        StartCoroutine(countdown());
    }

    public void finalTurn()
    {
        final = true;
        ConversationTextBox.text = "Final Turn! Make all your moves, then click the die to end the game!";
        StartCoroutine(countdown());
    }

    [PunRPC]
    public void finalTurnWrapper()
    {
        finalTurn();
    }

    public void noStack()
    {
        ConversationTextBox.text = "Don't stack atoms on top of each other!";
        StartCoroutine(countdown());
    }

    public void NoBondToBreak()
    {
        ConversationTextBox.text = "This Bond is Already Broken";
        StartCoroutine(countdown());
    }

    public void FirstTimeUnbonding()
    {
        if (!unbondingMessageAlreadyGiven)
        {
            ConversationTextBox.text = "Click a bond to unbond it!";
            StartCoroutine(countdown());
            unbondingMessageAlreadyGiven = true;
        }
    }

    public void GameStartTutorial()
    {
        ConversationTextBox.text = "Press the die to roll, and press End Turn once you are done";
        StartCoroutine(countdown());
    }

    public void NoRoll()
    {
        ConversationTextBox.text = "You need to roll the die before ending your turn!";
        StartCoroutine(countdown());
    }

    private IEnumerator countdown()  //this is a co-routine, can run in parallel with other scripts/functions
    {
        yield return new WaitForSeconds(5);
        if (final)
        {
            ConversationTextBox.text = "Final Turn! Make all your moves, then click the die to end the game!";
        }
        else
        {
            ConversationTextBox.text = "";
        }
        yield break;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(ConversationTextBox.text);
        }
        else if (stream.IsReading)
        {
            ConversationTextBox.text = (string)stream.ReceiveNext();
        }
    }
}
