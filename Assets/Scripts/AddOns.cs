using Mono.Data.Sqlite;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Mono.Data.Sqlite;
using System.Data;
using System;

public class AddOns : MonoBehaviour {
    private GameObject butt;
    // Use this for initialization
    void Start () {
        butt = GameObject.FindGameObjectWithTag("butterfly");
    }

    public void OnClick()
    {
        var coins = FindData("student", "Coin", "StuID", SubmitName.getStuID());
        int intCoin = Convert.ToInt32(coins);
        if (intCoin > 20) {
            print(intCoin);
            DeductData("student", "StuID", SubmitName.getStuID(), "Coin", 20);
            butt.transform.localScale += new Vector3(0.2f, 0.2f, 0.2f);
        }
    }

    public void OnClick2()
    {
        butt.SetActive(false);
    }


    // Update is called once per frame
    void Update () {
		
	}

    public void DeductData(string tableName, string whereHeader, object whereValue, string setHeader, object setValue)
    {
        using (var dbConnection = new SqliteConnection("URI=file:" + Application.dataPath + "/StreamingAssets/MumboJumbos.db"))
        {
            dbConnection.Open();

            using (var dbCommand = dbConnection.CreateCommand())
            {

                dbCommand.CommandText = string.Format("UPDATE {0} " + "SET {1} = {1} - @setValue " + "WHERE {2} = @whereValue",
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
