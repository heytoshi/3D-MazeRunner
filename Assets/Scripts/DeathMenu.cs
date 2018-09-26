using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Mono.Data.SqliteClient;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour {

    public Text scoreText;
	public Text tokenText;

    public Image backgroundImage;
    private bool isShowed = false;
    private float transition = 0.0f;

	// Use this for initialization
	void Start () {
        gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		if(!isShowed)  {
            return;
        }
        transition += Time.deltaTime;
        backgroundImage.color = Color.Lerp(new Color(0,0,0,0),Color.black,transition);
	}

    public void ToggleEndMenu(float score) {
        gameObject.SetActive(true);
        scoreText.text = ((int)score).ToString();
        isShowed = true;
    }

    public void Restart() {
		SceneManager.LoadScene("Game");
	}

	public void ToMenu() {
		Time.timeScale = 1f;
		SceneManager.LoadScene("Menu");

	} 
		
}
