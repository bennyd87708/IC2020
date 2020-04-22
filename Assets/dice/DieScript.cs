﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieScript : MonoBehaviour {

	Rigidbody rb;
	public Vector3 dieVelocity;
	private Vector3 startPos;
	private Quaternion startRot;
	public static int rolling = 0;
    public static int totalRolls = 0;

	void Start()
	{
		rb = GetComponent<Rigidbody>();
		startPos = transform.position;
		startRot = transform.rotation;
        totalRolls = 0;
	}
	
	void Update()
	{
		dieVelocity = rb.velocity;
    }

    public void RollDiceAnimation()
    {
        if (rolling == 0)
        {
            if (totalRolls < 13)
            {
                rolling++;
                totalRolls++;
                rb.velocity = Vector3.zero;
                float dirX = Random.Range(-500, 500);
                float dirY = Random.Range(-500, 500);
                float dirZ = Random.Range(-500, 500);
                transform.position = startPos;
                transform.rotation = Random.rotation;
                rb.AddForce(new Vector3(Random.Range(-100f, 100f), 1000, 0));
                rb.AddTorque(dirX, dirY, dirZ);
                StartCoroutine(countdown());
            }
            else
            {
                //NEED TO CLEARLY MARK THE LAST TURN--PLAYER NEEDS TO GET AS MANY PTS AS POSSIBLE PRIOR TO "GAME OVER"
                GetComponent<resetScene>().gameOver();
            }
        }
    }




    public void Reset()
	{
		transform.position = startPos;
		transform.rotation = startRot;
	}

    private IEnumerator countdown()  //this is a co-routine, can run in parallel with other scripts/functions
    {
        yield return new WaitForSeconds(5);
        if (rolling == 1)
        {
            rb.velocity = Vector3.zero;
            float dirX = Random.Range(-500, 500);
            float dirY = Random.Range(-500, 500);
            float dirZ = Random.Range(-500, 500);
            transform.localPosition = startPos;
            transform.rotation = Random.rotation;
            rb.AddForce(new Vector3(Random.Range(-100f, 100f), 1000, 0));
            rb.AddTorque(dirX, dirY, dirZ);
            StartCoroutine(countdown());
        }
        yield break;
    }


}
