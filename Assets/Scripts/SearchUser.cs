using Mono.Data.Sqlite;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SearchUser : MonoBehaviour {

    public InputField textUser;
    public Text HS;
    public Text UserName;
    public Text UserCoin;
    public static string otherUserName;
    private string founD;
    private string temp;
    public GameObject bronze;
    public GameObject gold;
    public GameObject silver;

    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        otherUserName = textUser.text;
	}

    public void findUser()
    {
        founD = textUser.text;
        var userName = FindData("student", "StuUserName", "StuUserName", founD);
        temp = userName.ToString();
        if (temp == founD) {
            //find other users score
            var score = FindData("student", "Score", "StuUserName", founD);
            int intScore = Convert.ToInt32(score);
            HS.text = ((int)intScore).ToString();

            //find other users username
            UserName.text = temp;

            //find other users coin
            var coins = FindData("student", "Coin", "StuUserName", founD);
            int intCoin = Convert.ToInt32(coins);
            UserCoin.text = ((int)intCoin).ToString();

            if (intScore >= 10 && intScore < 20)
            {
                bronze.SetActive(true);
            }
            else
            {
                bronze.SetActive(false);
            }

            if (intScore >= 20 && intScore < 30)
            {
                silver.SetActive(true);
            }
            else
            {
                silver.SetActive(false);
            }

            if (intScore >= 30)
            {
                gold.SetActive(true);
            }
            else
            {
                gold.SetActive(false);
            }

            print(temp);
        }
    }



    public object FindData(string tableName, string selectHeader, string whereHeader, object whereValue)
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
                            return dbReader.GetValue(0);
                    }
                }
            }
        }
        return null;
    }
}
