using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using miejscestruktow;
using UnityEngine.SceneManagement;

public class Komentarz_Menadzer : MonoBehaviour {


    public GameObject panel;
    public GameObject viewport;

    public Color zielony;
    public Color czerwony;
    public List<ServerConnect.komentarz> lista_komentarzy = new List<ServerConnect.komentarz>();
    public Sprite kciuk_w_dol;

    public GameObject glowa;
    

    // Use this for initialization
    void Awake () {
        Debug.Log("Wczytujemy list ekomentarzy");
        //Kasuj_Liste_Komentarzy();
        ServerConnect.placowka glowa_info = GameObject.FindGameObjectWithTag("ServerConnection").GetComponent<ServerConnect>().Zwrot_Glowa();

        if (glowa_info.lajki > glowa_info.dislajki)
        {
            glowa.GetComponent<Image>().color = zielony;
        }
        else if (glowa_info.lajki < glowa_info.dislajki)
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


        lista_komentarzy = GameObject.FindGameObjectWithTag("ServerConnection").GetComponent<ServerConnect>().Zwroc_Liste_Komentarzy();
        //lista_komentarzy.Reverse();
        foreach (ServerConnect.komentarz p in lista_komentarzy)
        {
            GameObject tmp = Instantiate(panel, viewport.transform);
           // tmp.GetComponent<PanelId>().SetID(p.id_placowki);
            if (p.kciuk ==2)
            {
                tmp.GetComponent<Image>().color = zielony;
            }
            if (p.kciuk ==1)
            {
                tmp.GetComponent<Image>().color = czerwony;
            }
            foreach (Transform child in tmp.transform)
            {
                if (child.name == "Nick")
                    child.GetComponent<Text>().text = p.nick;
                if (child.name == "Data")
                    child.GetComponent<Text>().text = p.data;
                if (child.name == "Komentarz")
                    child.GetComponent<Text>().text = p.koment;
                if (child.name == "LiczbaLudkow")
                    child.GetComponent<Text>().text = p.liczba_ludzi.ToString();
                if (child.name == "Godzina")
                    child.GetComponent<Text>().text = p.godzina;
                if (child.name == "kciukwgore")
                {
                    if (p.kciuk == 1)
                        child.GetComponent<Image>().sprite = kciuk_w_dol;
                    else if(p.kciuk ==0)
                    {

                        child.GetComponent<Image>().color = Color.clear;
                    }
                }
                


            }
        }
       // GameObject.FindGameObjectWithTag("ServerConnection").GetComponent<ServerConnect>().wyczysc_liste_komentarzy();
    }

    public void Kasuj_Liste_Komentarzy()
    {
      //  lista_komentarzy.Clear();
    }
    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
            SceneManager.LoadScene(4);
    }

}
