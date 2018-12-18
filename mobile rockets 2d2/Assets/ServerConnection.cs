using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Data;
using System.Data.SqlClient;
using UnityEngine.UI;
using MySql.Data.MySqlClient;


public class ServerConnection : MonoBehaviour {

    // Use this for initialization
    public GameObject logi;
    public GameObject logi2;
    public GameObject logi3;

    private MySqlConnection con = null;
    private MySqlCommand cmd = null;
    private MySqlDataReader rdr = null;

    private MD5 md5Hash;
    string connectionString =
                 "Server=s16.zenbox.pl;" +
                 "Database=adrocket_clientdisc;" +
                 "User Id=adrocket_secsky;" +
                 "Password=misiek95;";
    void Start()
    {

     /*   try
        {
           
           // DownloadClientList();
          //  Debug.Log("State: " + con.State);
         //   logi3.GetComponent<Text>().text = "Lacze: " + con.State;
           // DownloadClientList();
        }
        catch (Exception e)
        {
            Debug.Log(e);
            logi3.GetComponent<Text>().text = e.ToString();
        }*/



    }

    private void OnApplicationQuit()
    {
        if(con!= null)
        {
            if(con.State.ToString()!= "Closed")
            {
                con.Close();
                Debug.Log("DB con closed");
            }
            con.Dispose();
        }
    }

    public void AddClient(string imieklienta, string nazwiskoklienta, string emailklienta)
    {
        try
        {
           con = new MySqlConnection(connectionString);
            con.Open();

            string sqluserstrig =
                "Insert into klienci(imie,nazwisko,email)" +
                "values ('" + imieklienta + "','" + nazwiskoklienta + "','" + emailklienta + "');";

            MySqlCommand cmd = new MySqlCommand(sqluserstrig,con);
            cmd.CommandTimeout = 200;
            cmd.ExecuteNonQuery();

           Debug.Log( "\nDodano: " + imieklienta + nazwiskoklienta + emailklienta);
            //    DownloadClientList();
            if (con != null)
            {
                if (con.State.ToString() != "Closed")
                {
                    con.Close();
                    Debug.Log("DB con closed");
                }
                con.Dispose();
            }
        }
        catch(Exception e)
        {
            Debug.Log(e);
         //   logi3.GetComponent<Text>().text += "\nRzucony wyjatek: \n" + e.ToString();
        }      
    
    }
   /* private void DownloadClientList()
    {
      
            logi.GetComponent<Text>().text = "";
            using (IDbCommand dbcmd = con.CreateCommand())
            {
                string sql =
                    "SELECT imie, nazwisko " +
                    "FROM klienci";
                dbcmd.CommandText = sql;
                using (IDataReader reader = dbcmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string FirstName = (string)reader["imie"];
                        string LastName = (string)reader["nazwisko"];
                        logi.GetComponent<Text>().text += "\nName: " + FirstName + " " + LastName;
                       // Debug.Log("Name: " +
                     //        FirstName + " " + LastName);
                    }
                }
            }
       

        
    }*/
    private void Update()
    {
         
    }
}