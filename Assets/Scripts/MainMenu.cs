using Mono.Data.Sqlite;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ToGame()
    {
        CoinPick.EasyMode();
        SceneManager.LoadScene("Game");
    }

    public void ToGameMedium()
    {
        CoinPick.MediumMode();
        SceneManager.LoadScene("Game");
    }

    public void ToGameHard()
    {
        CoinPick.HardMode();
        SceneManager.LoadScene("Game");
    }
}
