using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Mono.Data.SqliteClient;
using UnityEngine.SceneManagement;

public class Score : MonoBehaviour
{

    public static float score = 0.0f;
    private int difficultyLevel = 1;
    private int maxDifficultyLevel = 2;
    private int scoreToNextLevel = 100;
    private bool isDeath = false;
    public Text scoreText;
	public Text tokenText;

    public DeathMenu deathMenu;
	public static int id = 0;



    public string FindData(string tableName, string selectHeader, string whereHeader, object whereValue)
    {
        using (var dbConnection = new SqliteConnection("URI=file:" + Application.dataPath + "/StreamingAssets/MumboJumbos.db"))
        {
            dbConnection.Open();

            using (var dbCommand = dbConnection.CreateCommand())
            {
                dbCommand.CommandText = string.Format("SELECT {0} " +
                                                       "FROM {1} " +
                                                       "WHERE {2} = @whereValue", selectHeader, tableName, whereHeader);

                SqliteParameter whereParam = new SqliteParameter("@whereValue", whereValue);
                dbCommand.Parameters.Add(whereParam);

                using (var dbReader = dbCommand.ExecuteReader())
                {
                    while (dbReader.Read())
                    {
                        if (dbReader.GetValue(0) != null)
							return dbReader.GetValue(0).ToString();
                    }
                }
            }
        }
        return null;
    }

    public static void UpdateData(string tableName, string whereHeader, object whereValue, string setHeader, object setValue)
    {
        using (var dbConnection = new SqliteConnection("URI=file:" + Application.dataPath + "/StreamingAssets/MumboJumbos.db"))
        {
            dbConnection.Open();

            using (var dbCommand = dbConnection.CreateCommand())
            {

                dbCommand.CommandText = string.Format("UPDATE {0} " + "SET {1} = @setValue " + "WHERE {2} = @whereValue",
                    tableName, setHeader, whereHeader);

                SqliteParameter setParam = new SqliteParameter("@setValue", setValue);
                SqliteParameter whereParam = new SqliteParameter("@whereValue", whereValue);

                dbCommand.Parameters.Add(setParam);
                dbCommand.Parameters.Add(whereParam);

                Debug.Log(dbCommand.CommandText);

                dbCommand.ExecuteNonQuery();
            }
        }
    }

    // Use this for initialization
    void Start()
    {
        
    }

	public static void setID(int newID){
		
		id = newID;
	}
    // Update is called once per frame
    void Update() {
        if(isDeath){
            return;
        }
        if(score >= scoreToNextLevel) {
           // LevelUp();
        }
        score += Time.deltaTime;
        scoreText.text = ((int)score).ToString();
    }

    void LevelUp() {
        if(difficultyLevel == maxDifficultyLevel) {
            return;
        }
        scoreToNextLevel *= 2;
        difficultyLevel++;
        GetComponent<CharacterControl>().SetSpeed(difficultyLevel);

        Debug.Log(difficultyLevel);
    }

    public void OnDeath() {
		//direct db connection to where the db is stored in app
		//and open connection
		const string connectionString = "URI=file:Assets\\StreamingAssets\\MumboJumbos.db";
		IDbConnection dbcon = new SqliteConnection(connectionString);
		dbcon.Open();
		IDbCommand dbcmd = dbcon.CreateCommand();

		//create query for adding score
		String command =
			"INSERT INTO score " +
			"(userID, totalScore, balance, grade) " +
			"VALUES (@two, @three, @four, @five)";

		//dbcmd.Parameters.Add(new SqliteParameter("@one", one)); 
		dbcmd.Parameters.Add(new SqliteParameter("@two", id));
		dbcmd.Parameters.Add(new SqliteParameter("@three", Int32.Parse(scoreText.text)));
		dbcmd.Parameters.Add(new SqliteParameter("@four", Int32.Parse(scoreText.text)));
		dbcmd.Parameters.Add(new SqliteParameter("@five", SubmitName.getStuGrade()));

		string sql = command;
		dbcmd.CommandText = sql;
		IDataReader reader = dbcmd.ExecuteReader();

		Debug.Log(sql);
		Debug.Log(SubmitName.getTeachID());
		Debug.Log(scoreText.text);
		Debug.Log (tokenText.text);
		Debug.Log(SubmitName.getStuGrade());

        isDeath = true;
        PlayerPrefs.SetFloat("Highscore", score);
        deathMenu.ToggleEndMenu (score);
        var hscore = FindData("student", "Score", "StuID", SubmitName.getStuID());
		int hscores = Convert.ToInt32(hscore);
        if (hscores < score) {
            UpdateData("student", "StuID", SubmitName.getStuID(), "Score", score);
        }
        Score.score = 0;
    }
}
