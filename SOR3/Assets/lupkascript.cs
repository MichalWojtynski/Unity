using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using miejscestruktow;
using UnityEngine.UI;

public class lupkascript : MonoBehaviour {

    public GameObject tekst;

    public void Wywolaj_szukanie()
    {
        GameObject.FindGameObjectWithTag("ServerConnection").GetComponent<ServerConnect>().lupka(tekst.GetComponent<InputField>().text);
    }
}
