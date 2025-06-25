using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleLevelUp : MonoBehaviour
{
    public CanvasGroup levelUpCanvas;
    private bool levelUpOpened = false;
    private void Update()
    {
        if (Input.GetButtonDown("ToggleLevelUp"))
        {
            if (levelUpOpened)
            {
                Time.timeScale = 1;
                levelUpCanvas.alpha = 0;
                levelUpCanvas.blocksRaycasts = false;
                levelUpOpened = false;
            }else
            {
                Time.timeScale = 0;
                levelUpCanvas.alpha = 1;
                levelUpCanvas.blocksRaycasts = true;
                levelUpOpened = true;
            }
        }
    }
}
