﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class drag : MonoBehaviour
{
    private Vector3 mOffset;
    private float mZCoord;
    private GameObject rightMenu;
    private Vector3 menuOffest;
    private Ray ray;
    private RaycastHit hit;
    private GameObject rightCanvas;

    void OnMouseDown()
    {
        mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
		
        // Store offset = gameobject world pos - mouse world pos
        // offset allows you to grab the object from the side of the circle, not just the center
        mOffset = gameObject.transform.position - GetMouseAsWorldPoint();
    }
	
    private Vector3 GetMouseAsWorldPoint()
    {
        // Pixel coordinates of mouse (x,y)
        Vector3 mousePoint = Input.mousePosition;

        // z coordinate of game object on screen
        mousePoint.z = mZCoord;

        // Convert it to world points
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    void OnMouseDrag()
    {
        transform.position = GetMouseAsWorldPoint() + mOffset;
        //Debug.Log(this.transform.position);
    }

    void OnMouseUp()
    {
        
    }

    void Start()
    {
        rightCanvas = GameObject.Find("Right-Click Canvas");
        rightMenu = rightCanvas.GetComponent<RightClickHelper>().rightMenu;
        rightMenu.SetActive(false);
        mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        menuOffest = new Vector3(85f, -117.5f, mZCoord);
    }
    //updates everyframe
    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //like OnMouseDown, but for right-click
        if (Input.GetMouseButtonDown(1))
        {
            if (Physics.Raycast(ray, out hit))
            {
                rightMenu.transform.position = Input.mousePosition + menuOffest;
                rightMenu.SetActive(true);
                UpdateRightMenuStats();
                Debug.Log("yep");

            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            //rightMenu.SetActive(false);
        }
        
    }

    private float num;
    private void UpdateRightMenuStats()
    {
            StringToFloat(rightCanvas.GetComponent<RightClickHelper>().Mass.GetComponent<InputField>().text, num );
            num = gameObject.GetComponent<Rigidbody>().mass;
            Debug.Log("adjifg");
    }

    public void StringToFloat(string inputString, float floatToRetrun)
    {
        if (inputString != null)
        {
             floatToRetrun = float.Parse(inputString);
        }
    }
}