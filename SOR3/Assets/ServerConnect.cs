using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Data;
using System.Data.SqlClient;
using UnityEngine.UI;
using MySql.Data.MySqlClient;
using UnityEngine.SceneManagement;

namespace miejscestruktow
{
    public class ServerConnect : MonoBehaviour
    {

        private MySqlConnection con = null;
        private MySqlCommand cmd = null;
        private MySqlDataReader rdr = null;
        public int id_wyszukiwania = 0;
        int wyszukaj_id_miasta = 0;
        
        private MD5 md5Hash;
        string connectionString =
                     "Server=s16.zenbox.pl;" +
                     "Database=adrocket_b;" +
                     "User Id=adrocket_secsky;" +
                     "Password=misiek95;";

        public struct placowka
        {
            public int id_placowki;
            public int id_miasta;
            public string nazwa;
            public string ulica;
            public string telefon;
            public int liczba_ludzi;
            public int lajki;
            public int dislajki;
            public string nazwa_miasta;
        };

        public List<placowka> lista_placowek = new List<placowka>();
        public List<placowka> zwrot_placowek = new List<placowka>();
       
        public struct komentarz
        {
            public string nick;
            public string koment;
            public int id_placowki;
            public int liczba_ludzi;
            public string data;
            public string godzina;
            public int kciuk;
        };

        public List<komentarz> lista_komentarzy = new List<komentarz>();

        void Start()
        {
            DontDestroyOnLoad(gameObject);
            
                SciagnijListePlacowek();
          //  logi3.GetComponent<Text>().text = e.ToString();
            
        }

        public placowka Zwrot_Glowa()
        {
            placowka tmp = new placowka();
            foreach(placowka p in lista_placowek)
            {
                if (p.id_placowki == id_wyszukiwania)
                {
                    tmp = p;
                    break;
                }
            }
            return tmp;
        }

        public void Wyszukaj(int id)
        {
            wyszukaj_id_miasta = id;
            foreach (placowka p in lista_placowek)
            {
                if (p.id_miasta == id)
                {
                    zwrot_placowek.Add(p);
                    Debug.Log(p.id_placowki + " " + p.nazwa + " " + p.nazwa_miasta);

                }

            }
        }

        public void lupka(string tekst)
        {
            //List<placowka> tmp = new List<placowka>();
            zwrot_placowek.Clear();
            foreach (placowka p in lista_placowek)
            {
                //Debug.Log(p.nazwa_miasta.Contains(tekst.ToUpper()));
                if (p.nazwa_miasta.Contains(tekst.ToUpper()))
                {
                    zwrot_placowek.Add(p);
                    Debug.Log(tekst+ " "  +p.nazwa);
                } else if (p.nazwa.Contains(tekst.ToUpper()))
                {
                    zwrot_placowek.Add(p);
                } else if (p.ulica.Contains(tekst.ToUpper()))
                {
                    zwrot_placowek.Add(p);
                }
            }

            Application.LoadLevel(4);

               
        }

        

        public List<placowka> Zwroc_liste()
        {
            return zwrot_placowek;
        }

        private void OnApplicationQuit()
        {
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

        public void dodaj_komentarz(string nick, string data, string godzina, int liczba_ludzi, string komentarz, int lapka)
        {
            try
            {
                con = new MySqlConnection(connectionString);
                con.Open();

                string sqluserstrig =
                    "Insert into komentarze(nick, data, godzina, liczba_osob ,komentarz, id_placowki, kciuk)" +
                    "values ('" + nick + "','" + data + "','" + godzina + "'," +liczba_ludzi + ",'" + komentarz + "'," + id_wyszukiwania + "," + lapka + ");";

                if (lapka == 2)
                    sqluserstrig += "Update placowki set lajki = lajki+1 where id_placowki =" + id_wyszukiwania + ";";
                else if (lapka == 1)
                    sqluserstrig += "Update placowki set dislajki = dislajki+1 where id_placowki =" + id_wyszukiwania + ";";

                sqluserstrig += "Update placowki set liczba_ludzi = " + liczba_ludzi + " Where id_placowki = " + id_wyszukiwania + ";";
                    
                MySqlCommand cmd = new MySqlCommand(sqluserstrig, con);
                cmd.CommandTimeout = 200;
                cmd.ExecuteNonQuery();

               

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
            catch (Exception e)
            {
                Debug.Log(e);
                //   logi3.GetComponent<Text>().text += "\nRzucony wyjatek: \n" + e.ToString();
            }
            //SciagnijListePlacowek();
            for (int i = 0; i<lista_placowek.Count;i++)
            {
                if (lista_placowek[i].id_placowki == id_wyszukiwania)
                {
                    placowka tmp = lista_placowek[i];
                    tmp.liczba_ludzi = liczba_ludzi;

                    if (lapka == 2)
                        tmp.lajki++;
                    else if (lapka == 1)
                        tmp.dislajki++;

                    lista_placowek[i] = tmp;
                    break;
                }
            }
            for (int i = 0; i < zwrot_placowek.Count; i++)
            {
                if (zwrot_placowek[i].id_placowki == id_wyszukiwania)
                {
                    placowka tmp = zwrot_placowek[i];
                    tmp.liczba_ludzi = liczba_ludzi;

                    if (lapka == 2)
                        tmp.lajki++;
                    else if (lapka == 1)
                        tmp.dislajki++;

                    zwrot_placowek[i] = tmp;
                    break;
                }
            }


            sciagnij_komentarze(id_wyszukiwania);
            SceneManager.LoadScene(5);
           // Destroy(gameObject);

        }

        
        private void SciagnijListePlacowek()
        {
            try
            {
                con = new MySqlConnection(connectionString);
                con.Open();
                //   logi.GetComponent<Text>().text = "";
                using (IDbCommand dbcmd = con.CreateCommand())
                {
                    string sql =
                          "SELECT *" +
                           "FROM placowki p JOIN miasta m ON m.id_miasta = p.id_miasta";
                    dbcmd.CommandText = sql;
                    using (IDataReader reader = dbcmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            placowka tmp = new placowka();
                            tmp.id_placowki = (int)reader["id_placowki"];
                            tmp.id_miasta = (int)reader["id_miasta"];
                            tmp.nazwa = (string)reader["nazwa"];
                            tmp.ulica = (string)reader["ulica"];
                            tmp.telefon = (string)reader["telefon"];
                            tmp.liczba_ludzi = (int)reader["liczba_ludzi"];
                            tmp.lajki = (int)reader["lajki"];
                            tmp.dislajki = (int)reader["dislajki"];
                            // int smiec = (int)reader["id_miasta"];
                            tmp.nazwa_miasta = (string)reader["nazwa_miasta"];
                            lista_placowek.Add(tmp);

                        }
                        //foreach (placowka p in lista_placowek)
                       // {
                        //    Debug.Log(p.id_placowki + " " + p.nazwa + " " + p.nazwa_miasta);
                        //}
                    }
                }
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
            catch (Exception e)
            {
                Debug.Log(e);
            }
        }

        public void sciagnij_komentarze(int id_placowki)
        {
            lista_komentarzy.Clear();
            try
            {
                con = new MySqlConnection(connectionString);
                con.Open();
                id_wyszukiwania = id_placowki;
                //   logi.GetComponent<Text>().text = "";
                using (IDbCommand dbcmd = con.CreateCommand())
                {
                    string sql =
                          "SELECT *" +
                           "FROM komentarze WHERE id_placowki = " + id_placowki + " ORDER BY id_komentarza DESC LIMIT 30;";
                    dbcmd.CommandText = sql;
                    using (IDataReader reader = dbcmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            komentarz tmp = new komentarz();
                            tmp.id_placowki = (int)reader["id_placowki"];
                            tmp.liczba_ludzi = (int)reader["liczba_osob"];
                            tmp.nick = (string)reader["nick"];
                            tmp.koment = (string)reader["komentarz"];
                            tmp.data = (string)reader["data"];
                            tmp.kciuk = (int)reader["kciuk"];
                            tmp.godzina = (string)reader["godzina"];
                            lista_komentarzy.Add(tmp);

                        }
                        foreach (komentarz p in lista_komentarzy)
                        {
                            Debug.Log(p.nick + " " + p.koment);
                        }
                    }
                }
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
            catch (Exception e)
            {
                Debug.Log(e);
            }
        }

        public int zwroc_id_wyszukiwania()
        {
            return id_wyszukiwania;
        }
        public List<komentarz> Zwroc_Liste_Komentarzy()
        {
            //lista_komentarzy.Reverse();
            return lista_komentarzy;
        }

        public void wyczysc_liste_komentarzy()
        {
            lista_komentarzy.Clear();
        }
    }
}