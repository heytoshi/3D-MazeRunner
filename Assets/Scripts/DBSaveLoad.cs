using Mono.Data.Sqlite;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DBSaveLoad : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
   
    public static void UpdateData(string tableName, string whereHeader, object whereValue, string setHeader, object setValue)
    {
        using (var dbConnection = new SqliteConnection("URI=file:" + Application.dataPath + "/StreamingAssets/MumboJumbos.db"))
        {
            dbConnection.Open();

            using (var dbCommand = dbConnection.CreateCommand())
            {

                dbCommand.CommandText = string.Format("UPDATE {0} " + "SET {1} = {1} + @setValue " + "WHERE {2} = @whereValue",
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
		
}
