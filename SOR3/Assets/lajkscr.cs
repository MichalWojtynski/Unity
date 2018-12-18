using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class lajkscr : MonoBehaviour {

    bool klikniety = false;
    public Color zielony;

    public void klik()
    {
        if (!klikniety)
        {
            GetComponent<Image>().color = zielony;
            klikniety = true;
        }
        else
            unklik();
    }

    public void unklik()
    {
        klikniety = false;
        GetComponent<Image>().color = Color.white;
    }
}
