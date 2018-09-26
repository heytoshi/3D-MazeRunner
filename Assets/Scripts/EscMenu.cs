using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscMenu : MonoBehaviour {

    public static bool GameIsPause = false;


    public GameObject pauseGameMenu;
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPause)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
	}
    public void Resume ()
    {
        pauseGameMenu.SetActive(false);
        Time.timeScale = 1f;
        GameIsPause = false;

    }

    void Pause ()
    {
        pauseGameMenu.SetActive(true);
        Time.timeScale = 0f;
        GameIsPause = true;

    }
}
