﻿using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;

public class OrcController : MonoBehaviour {

    public Camera Cam;
    public bool canTakeShot = false;
    public GameObject OrcHands;
    public Canvas canvas;
    public LayerMask GoblinLayer;
    public bool waitingForShot = false;
    private string currentScreenName = "first";
    private List<string> takenShots = new List<string>();
    private bool unactiveUI = false;
    public Text MessageOrc;
	
	void Update ()
    {
        if (unactiveUI)
        {
            canvas.gameObject.SetActive(true);
            unactiveUI = false;
        }
        if (waitingForShot)
        {
            RaycastHit hit;
            Debug.DrawRay(Cam.transform.position, Cam.transform.forward * 100, Color.white, 0.1f);
            if (Physics.Raycast(Cam.transform.position, Cam.transform.forward, out hit, 100f, GoblinLayer.value))
            {
                if (hit.transform != null && !canTakeShot)
                {
                    MessageOrc.text = "Ein - Press A to shot a picture.";
                    canTakeShot = true;
                }
            }
            else
            {
                MessageOrc.text = "";
                canTakeShot = false;
            }
            if (canTakeShot)
            {
                if (Input.GetButtonDown("ScreenShot") && !takenShots.Contains(currentScreenName))
                {
                    unactiveUI = true;
                    Debug.Log(Application.persistentDataPath + "/" + currentScreenName + ".png");
                    Application.CaptureScreenshot(Application.persistentDataPath + "/" + currentScreenName + ".png");
                    canvas.gameObject.SetActive(false);
                    canTakeShot = false;
                    waitingForShot = false;
                }
            }
        }
	}

    public void WaitForShot(string screenName)
    {
        if (!takenShots.Contains(screenName))
        {
            waitingForShot = true;
            currentScreenName = screenName;
        }
    }

    public void NotWaitForShot()
    {
        waitingForShot = false;
    }
}
