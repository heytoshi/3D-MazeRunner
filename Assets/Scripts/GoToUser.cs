using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToUser : MonoBehaviour
{

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	public void ToUserEdit()
	{
		SceneManager.LoadScene("AddUser");
	}

	public void ToWordEdit()
	{
		SceneManager.LoadScene("AddWord");
	}

	public void ToRemoveUser()
	{
		SceneManager.LoadScene("RemoveUser");
	}

	public void ToTeacherMenu()
	{
		SceneManager.LoadScene("TeacherMenu");
	}

    public void ToLeaderboards()
    {
        SceneManager.LoadScene("ScoreBoard");
    }

    public void ToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

}
