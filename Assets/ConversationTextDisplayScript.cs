﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConversationTextDisplayScript: MonoBehaviour
{
    public Text ConversationTextBox;
    public static bool final = false;

    // Start is called before the first frame update
    void Start()
    {
        final = false;
        ConversationTextBox.text = null;
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

    private IEnumerator countdown()  //this is a co-routine, can run in parallel with other scripts/functions
    {
        yield return new WaitForSeconds(5);
        if (final)
        {
            ConversationTextBox.text = "Final Turn! Make all your moves, then click the die to end the game!";
        }
        else
        {
            ConversationTextBox.text = null;
        }
        yield break;
    }
}



