using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using miejscestruktow;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class lista2menager : MonoBehaviour {

    public GameObject panel;
    public GameObject viewport;

    public Color zielony;
    public Color czerwony;
    public List<ServerConnect.placowka> lista_placowek_zwroconych = new List<ServerConnect.placowka>();
    // Use this for initialization
    private void Awake()
    {
        Debug.Log("Wczytujemy Liste");
        dawaj();
    }
    public void dawaj () {
        Debug.Log("Wczytuje wyszukana liste");
       lista_placowek_zwroconych =  GameObject.FindGameObjectWithTag("ServerConnection").GetComponent<ServerConnect>().Zwroc_liste();
        foreach(ServerConnect.placowka p in lista_placowek_zwroconych)
        {
            GameObject tmp = Instantiate(panel, viewport.transform);
            tmp.GetComponent<PanelId>().SetID(p.id_placowki);
            if (p.lajki > p.dislajki)
            {
                tmp.GetComponent<Image>().color = zielony;
            }
            if(p.lajki <p.dislajki)
            {
                tmp.GetComponent<Image>().color = czerwony;
            }
            foreach(Transform child in tmp.transform)
            {
                if(child.name == "nazwa")
                    child.GetComponent<Text>().text = p.nazwa;
                if (child.name == "Adres")
                    child.GetComponent<Text>().text = p.ulica  + p.nazwa_miasta;
                if (child.name == "Adres (1)")
                    child.GetComponent<Text>().text = p.liczba_ludzi.ToString();
                if (child.name == "Adres (2)")
                    child.GetComponent<Text>().text = p.lajki.ToString();
                if (child.name == "Adres (3)")
                    child.GetComponent<Text>().text = p.dislajki.ToString();


            }
        }
        
    }

    public void Kasuj_Liste()
    {
        lista_placowek_zwroconych.Clear();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(3);
            Kasuj_Liste();
        }
    }
}
