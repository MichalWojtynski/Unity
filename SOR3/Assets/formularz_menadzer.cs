using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using miejscestruktow;
using UnityEngine.UI;

using UnityEngine.SceneManagement;

public class formularz_menadzer : MonoBehaviour {

    public GameObject glowa;
    public GameObject nick_holder;
    public GameObject nr_holder;
    public GameObject koment_holder;
    public Color zielony;
    public Color czerwony;

    public int wybor = 0;
    public string data;
    public string godzina;
	// Use this for initialization
	void Awake () {
        ServerConnect.placowka glowa_info = GameObject.FindGameObjectWithTag("ServerConnection").GetComponent<ServerConnect>().Zwrot_Glowa();

        if (glowa_info.lajki > glowa_info.dislajki)
        {
            glowa.GetComponent<Image>().color = zielony;
        }
        if (glowa_info.lajki < glowa_info.dislajki)
        {
            glowa.GetComponent<Image>().color = czerwony;

        }
        foreach (Transform child in glowa.transform)
        {
            if (child.name == "nazwa")
                child.GetComponent<Text>().text = glowa_info.nazwa;
            if (child.name == "Adres")
                child.GetComponent<Text>().text = glowa_info.ulica + glowa_info.nazwa_miasta;
            if (child.name == "Adres (1)")
                child.GetComponent<Text>().text = glowa_info.liczba_ludzi.ToString();
            if (child.name == "Adres (2)")
                child.GetComponent<Text>().text = glowa_info.lajki.ToString();
            if (child.name == "Adres (3)")
                child.GetComponent<Text>().text = glowa_info.dislajki.ToString();

        }
    }
	public void zmien_wartosc_lapki(int tmp)
    {
        wybor = tmp;
    }

	public void Wyslij_Komentarz()
    {
        string tmp = System.DateTime.Now.Minute.ToString();
        if (System.DateTime.Now.Minute < 10)
            tmp = "0" + System.DateTime.Now.Minute;

        data = System.DateTime.Now.Day + "/" + System.DateTime.Now.Month + "/" + System.DateTime.Now.Year;
        godzina = System.DateTime.Now.Hour + ":" + tmp;

        GameObject.FindGameObjectWithTag("ServerConnection").GetComponent<ServerConnect>().dodaj_komentarz
            (
             nick_holder.GetComponent<InputField>().text, data, godzina,
             int.Parse(nr_holder.GetComponent<InputField>().text),
             koment_holder.GetComponent<InputField>().text,
             wybor
             
             );
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
            SceneManager.LoadScene(5);
    }
}
