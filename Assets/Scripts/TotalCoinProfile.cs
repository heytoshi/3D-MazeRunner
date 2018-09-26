using Mono.Data.Sqlite;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TotalCoinProfile : MonoBehaviour {
    public Text scoreTextProfile;

    // Use this for initialization
    void Start () {
        var coins = FindData("student", "Coin", "StuID", SubmitName.getStuID());
        int intCoin = Convert.ToInt32(coins);
        scoreTextProfile.text = ((int)intCoin).ToString();
    }
	
	// Update is called once per frame
	void Update () {
		
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
