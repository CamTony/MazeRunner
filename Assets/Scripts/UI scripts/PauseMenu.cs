using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused = false;
    public GameObject pauseMenu;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("Pushed");
            if(isPaused)
            {
                pauseMenu.SetActive(false);
                Time.timeScale = 1;
                isPaused = false;
                
            }
            else
            {
                pauseMenu.SetActive(true);
                Time.timeScale = 0;
                isPaused = true;
            }
        }
    }
}
